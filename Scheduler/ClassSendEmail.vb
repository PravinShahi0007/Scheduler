Imports System.IO
Imports System.Net.Mail
Imports System.Threading

Public Class ClassSendEmail
    Public id_report As String = "-1"
    Public report_mark_type As String = "-1"
    'prod pib/duty
    Public total_notif As Integer = 0
    Public past_due_date As Integer = 0
    Public soon_due As Integer = 0
    Public pr_due As Integer = 0
    Public par1 As String = "-1"
    Public par2 As String = "-1"
    Public par3 As String = ""
    Public par4 As String = ""
    Public subj As String = ""
    Public titl As String = ""
    Public head As String = ""
    Public dt As DataTable

    Public is_daily As String = "-1"


    '
    Sub send_email_html()
        Dim is_ssl = get_setup_field("system_email_is_ssl").ToString
        Dim client As SmtpClient = New SmtpClient()
        If is_ssl = "1" Then
            client.Port = get_setup_field("system_email_ssl_port").ToString
            client.DeliveryMethod = SmtpDeliveryMethod.Network
            client.UseDefaultCredentials = False
            client.Host = get_setup_field("system_email_ssl_server").ToString
            client.EnableSsl = True
            client.Credentials = New System.Net.NetworkCredential(get_setup_field("system_email_ssl").ToString, get_setup_field("system_email_ssl_pass").ToString)
        Else
            client.Port = get_setup_field("system_email_port").ToString
            client.DeliveryMethod = SmtpDeliveryMethod.Network
            client.UseDefaultCredentials = False
            client.Host = get_setup_field("system_email_server").ToString
            client.Credentials = New System.Net.NetworkCredential(get_setup_field("system_email").ToString, get_setup_field("system_email_pass").ToString)
        End If

        Dim query_opt As String = "SELECT opt.management_mail,send_weekly_attn,send_stock_leave,send_weekly_attn_headdept,send_stock_leave_headdept,emp.employee_name,emp.email_external
                                    FROM tb_opt_scheduler opt 
                                    LEFT JOIN tb_m_employee emp ON emp.id_employee=opt.id_emp_headdept_toreport 
                                    LIMIT 1"
        Dim data_opt As DataTable = execute_query(query_opt, -1, True, "", "", "", "")

        '
        If report_mark_type = "weekly_attn" Then
            If data_opt.Rows(0)("send_weekly_attn").ToString = "1" Then
                'Create a new report. 
                'query dept
                Dim query_dept As String = "SELECT dept.id_departement,dept.departement,emp.id_employee,emp.email_external,emp.employee_name FROM tb_m_departement dept
                                            INNER JOIN tb_m_user usr ON dept.id_user_head=usr.id_user
                                            INNER JOIN tb_m_employee emp ON emp.id_employee=usr.id_employee
                                    WHERE is_office_dept='1'"
                Dim data_dept As DataTable = execute_query(query_dept, -1, True, "", "", "", "")
                For i As Integer = 0 To data_dept.Rows.Count - 1
                    Dim ix As Integer = i
                    send_mail_weekly_attn(data_dept.Rows(ix)("id_departement").ToString, data_dept.Rows(ix)("departement").ToString, data_dept.Rows(ix)("employee_name").ToString, data_dept.Rows(ix)("email_external").ToString)
                Next
            End If
        ElseIf report_mark_type = "weekly_attn_head" Then
            If data_opt.Rows(0)("send_weekly_attn_headdept").ToString = "1" Then
                send_mail_weekly_attn_head(data_opt.Rows(0)("employee_name").ToString, data_opt.Rows(0)("email_external").ToString)

                'Dim sender_thread = New Thread(Sub() send_mail_weekly_attn_head(data_opt.Rows(0)("employee_name").ToString, data_opt.Rows(0)("email_external").ToString))
                'sender_thread.Start()
            End If
        ElseIf report_mark_type = "monthly_leave_remaining" Then
            If data_opt.Rows(0)("send_stock_leave").ToString = "1" Then
                ' Create a new report. 
                'query dept
                Dim query_dept As String = "SELECT dept.id_departement,dept.departement,emp.id_employee,emp.email_external,emp.employee_name FROM tb_m_departement dept
                                            INNER JOIN tb_m_user usr ON dept.id_user_head=usr.id_user
                                            INNER JOIN tb_m_employee emp ON emp.id_employee=usr.id_employee
                                    WHERE is_office_dept='1'"
                Dim data_dept As DataTable = execute_query(query_dept, -1, True, "", "", "", "")
                For i As Integer = 0 To data_dept.Rows.Count - 1
                    ReportEmpLeaveStock.id_dept = data_dept.Rows(i)("id_departement").ToString
                    ReportEmpLeaveStock.is_head_dept = "-1"
                    Dim Report As New ReportEmpLeaveStock

                    ' Create a new memory stream and export the report into it as PDF.
                    Dim Mem As New MemoryStream()
                    Report.ExportToPdf(Mem)

                    ' Create a new attachment and put the PDF report into it.
                    Mem.Seek(0, System.IO.SeekOrigin.Begin)
                    '
                    Dim Att = New Attachment(Mem, "Monthly Remaining Leave Report - " & data_dept.Rows(i)("departement").ToString & ".pdf", "application/pdf")
                    '
                    Dim mail As MailMessage = New MailMessage("system@volcom.co.id", data_dept.Rows(i)("email_external").ToString)
                    'Dim mail As MailMessage = New MailMessage("system@volcom.co.id", "septian@volcom.co.id")
                    Dim cc_mail_management As MailAddress = New MailAddress(data_opt.Rows(0)("management_mail").ToString, "Management")
                    Dim cc_mail_hr As MailAddress = New MailAddress(data_opt.Rows(0)("email_external").ToString, "HR")
                    mail.CC.Add(cc_mail_management)
                    mail.CC.Add(cc_mail_hr)
                    '
                    mail.Attachments.Add(Att)

                    mail.Subject = "Monthly Remaining Leave Report (" & data_dept.Rows(i)("departement").ToString & ")"
                    mail.IsBodyHtml = True
                    mail.Body = email_temp_monthly(data_dept.Rows(i)("employee_name").ToString, False)
                    client.Send(mail)

                    Report.Dispose()
                    Mem.Dispose()
                    Att.Dispose()
                    mail.Dispose()

                    'log
                    Dim query_log As String = "INSERT INTo tb_scheduler_attn_log(id_log_type,`datetime`,log) VALUES('3',NOW(),'Sending Monthly Remaining Leave Report (" & data_dept.Rows(i)("departement").ToString & ") to " & data_dept.Rows(i)("email_external").ToString & "')"
                    execute_non_query(query_log, True, "", "", "", "")
                Next
            End If
        ElseIf report_mark_type = "monthly_leave_remaining_head" Then
            If data_opt.Rows(0)("send_stock_leave_headdept").ToString = "1" Then
                ' Create a new report. 
                ReportEmpLeaveStock.id_dept = "dept_head"
                ReportEmpLeaveStock.is_head_dept = "1"
                Dim Report As New ReportEmpLeaveStock

                ' Create a new memory stream and export the report into it as PDF.
                Dim Mem As New MemoryStream()
                Report.ExportToPdf(Mem)

                ' Create a new attachment and put the PDF report into it.
                Mem.Seek(0, System.IO.SeekOrigin.Begin)
                '
                Dim Att = New Attachment(Mem, "Monthly Remaining Leave Report -  Department Head.pdf", "application/pdf")
                '
                Dim mail As MailMessage = New MailMessage("system@volcom.co.id", data_opt.Rows(0)("email_external").ToString)
                Dim cc_mail As MailAddress = New MailAddress(data_opt.Rows(0)("management_mail").ToString, "Management")
                mail.CC.Add(cc_mail)
                'Dim mail As MailMessage = New MailMessage("system@volcom.co.id", "septian@volcom.co.id")
                mail.Attachments.Add(Att)

                mail.Subject = "Monthly Remaining Leave Report (Department Head)"
                mail.IsBodyHtml = True
                mail.Body = email_temp_monthly(data_opt.Rows(0)("employee_name").ToString, True)
                client.Send(mail)

                Report.Dispose()
                Mem.Dispose()
                Att.Dispose()
                mail.Dispose()

                'log
                Dim query_log As String = "INSERT INTo tb_scheduler_attn_log(id_log_type,`datetime`,log) VALUES('3',NOW(),'Sending Monthly Remaining Leave Report (Department Head) to " & data_opt.Rows(0)("employee_name").ToString & "')"
                execute_non_query(query_log, True, "", "", "", "")
            End If
        ElseIf report_mark_type = "226" Then
            'EMAIL pemberitahuan/peringatan
            Dim mail_address_from As String = execute_query("SELECT m.mail_address FROM tb_mail_manage_member m WHERE m.id_mail_manage=" + id_report + " AND m.id_mail_member_type=1 ORDER BY m.id_mail_manage_member ASC LIMIT 1", 0, True, "", "", "", "")

            Dim from_mail As MailAddress = New MailAddress(mail_address_from, head)
            Dim mail As MailMessage = New MailMessage()
            mail.From = from_mail


            'Send to => design_code : email; design : contact person;
            Dim query_send_to As String = "SELECT  m.id_mail_member_type,m.mail_address, IF(ISNULL(m.id_comp_contact), e.employee_name, cc.contact_person) AS `display_name`
            FROM tb_mail_manage_member m 
            LEFT JOIN tb_m_comp_contact cc ON cc.id_comp_contact = m.id_comp_contact
            LEFT JOIN tb_m_user u ON u.id_user = m.id_user
            LEFT JOIN tb_m_employee e ON e.id_employee = u.id_employee
            WHERE m.id_mail_manage=" + id_report + " AND m.id_mail_member_type>1 
            ORDER BY m.id_mail_member_type ASC,m.id_mail_manage_member ASC "
            Dim data_send_to As DataTable = execute_query(query_send_to, -1, True, "", "", "", "")
            For i As Integer = 0 To data_send_to.Rows.Count - 1
                Dim to_mail As MailAddress = New MailAddress(data_send_to.Rows(i)("mail_address").ToString, data_send_to.Rows(i)("display_name").ToString)
                If data_send_to.Rows(i)("id_mail_member_type").ToString = "2" Then
                    mail.To.Add(to_mail)
                ElseIf data_send_to.Rows(i)("id_mail_member_type").ToString = "3" Then
                    mail.CC.Add(to_mail)
                End If
            Next
            'include email management
            Dim management_mail As String = getMailManagement(report_mark_type)
            If management_mail <> "" Then
                mail.CC.Add(management_mail)
            End If

            '-- start attachment 
            '-- sementara nonaktif
            'Create a New report. 
            'Dim id_sales_pos As String = ""
            'For i As Integer = 0 To (dt.Rows.Count - 1)
            '    If i > 0 Then
            '        id_sales_pos += ","
            '    End If
            '    id_sales_pos += dt.Rows(i)("id_report").ToString
            'Next
            'ReportSummaryInvoice.id = id_sales_pos
            'Dim rpt As New ReportSummaryInvoice()

            '' Create a new memory stream and export the report into it as PDF.
            'Dim Mem As New MemoryStream()
            ''Dim unik_file As String = execute_query("SELECT UNIX_TIMESTAMP(NOW())", 0, True, "", "", "", "")
            'rpt.ExportToXlsx(Mem)
            '' Create a new attachment and put the PDF report into it.
            'Mem.Seek(0, System.IO.SeekOrigin.Begin)
            'Dim Att = New Attachment(Mem, "list_inv" & report_mark_type & "_" & id_report & ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            'mail.Attachments.Add(Att)
            '-- end attachment

            Dim body_temp As String = email_body_invoice_jatuh_tempo(dt, titl.ToUpper, par1, par2, par3, par4)
            mail.Subject = subj
            mail.IsBodyHtml = True
            mail.Body = body_temp
            client.Send(mail)
        ElseIf report_mark_type = "228" Then
            '--- on hold delivery
            Dim mm As New ClassMailManage()
            Dim id_mail As String = "-1"
            Try
                'cek rows
                Dim query_cek_eval As String = "SELECT COUNT(e.id_ar_eval) AS jum_eval FROM tb_ar_eval e WHERE e.eval_date='" + par1 + "' "
                Dim data_cek_eval As DataTable = execute_query(query_cek_eval, -1, True, "", "", "", "")
                If data_cek_eval.Rows(0)("jum_eval") > 0 Then
                    'create mail manage
                    Dim mail_subject As String = get_setup_field("mail_subject_on_hold") + " - " + par2.ToString
                    Dim mail_title As String = get_setup_field("mail_title_on_hold")
                    Dim mail_content As String = get_setup_field("mail_content_on_hold")
                    Dim query_mail_content_to As String = "SELECT CONCAT(e.employee_name, ' (',e.employee_position,')') AS `to_content_mail`
                    FROM tb_opt o
                    INNER JOIN tb_m_employee e ON e.id_employee = o.id_emp_wh_manager "
                    Dim mail_content_to As String = execute_query(query_mail_content_to, 0, True, "", "", "", "")

                    'send paramenter class
                    mm.rmt = report_mark_type
                    mm.mail_subject = mail_subject
                    mm.mail_title = mail_title
                    mm.par1 = par1
                    mm.createEmail("-1", 0, "NULL", "NULL", "")
                    id_mail = mm.id_mail_manage

                    'send email
                    Dim from_mail As MailAddress = New MailAddress("system@volcom.co.id", mail_title)
                    Dim mail As MailMessage = New MailMessage()
                    mail.From = from_mail
                    Dim query_send_to As String = "SELECT  m.id_mail_member_type,m.mail_address, IF(ISNULL(m.id_comp_contact), e.employee_name, cc.contact_person) AS `display_name`
                    FROM tb_mail_manage_member m 
                    LEFT JOIN tb_m_comp_contact cc ON cc.id_comp_contact = m.id_comp_contact
                    LEFT JOIN tb_m_user u ON u.id_user = m.id_user
                    LEFT JOIN tb_m_employee e ON e.id_employee = u.id_employee
                    WHERE m.id_mail_manage=" + id_mail + " AND m.id_mail_member_type>1 
                    ORDER BY m.id_mail_member_type ASC,m.id_mail_manage_member ASC "
                    Dim data_send_to As DataTable = execute_query(query_send_to, -1, True, "", "", "", "")
                    For i As Integer = 0 To data_send_to.Rows.Count - 1
                        Dim to_mail As MailAddress = New MailAddress(data_send_to.Rows(i)("mail_address").ToString, data_send_to.Rows(i)("display_name").ToString)
                        If data_send_to.Rows(i)("id_mail_member_type").ToString = "2" Then
                            mail.To.Add(to_mail)
                        ElseIf data_send_to.Rows(i)("id_mail_member_type").ToString = "3" Then
                            mail.CC.Add(to_mail)
                        End If
                    Next
                    'include email management
                    Dim management_mail As String = getMailManagement(report_mark_type)
                    If management_mail <> "" Then
                        mail.CC.Add(management_mail)
                    End If

                    mail.Subject = mail_subject
                    mail.IsBodyHtml = True
                    mail.Body = emailOnHold(mail_content_to, mail_content, mm.getDetailData())
                    client.Send(mail)

                    'log
                    Dim query_log As String = "INSERT INTO tb_ar_eval_log(eval_date, log_time, log) 
                    VALUES('" + par1 + "', NOW(), 'Email Sent successfully'); " + mm.queryInsertLog("0", "2", "Sent successfully")
                    execute_non_query(query_log, True, "", "", "", "")

                    'dispose memory
                    mail.Dispose()
                    data_cek_eval.Dispose()
                End If
            Catch ex As Exception
                'Log
                Dim query_log As String = "INSERT INTO tb_ar_eval_log(eval_date, log_time, log) 
                VALUES('" + par1 + "', NOW(), 'Failed send email : " + addSlashes(ex.ToString) + "'); " + mm.queryInsertLog("0", "3", addSlashes(ex.ToString))
                execute_non_query(query_log, True, "", "", "", "")
            End Try
            mm.id_mail_manage = "-1"
            mm.mail_subject = ""
            mm.mail_title = ""
            mm.rmt = "-1"
            mm.par1 = ""
        End If
        client.Dispose()
    End Sub
    Sub send_mail_weekly_attn(ByVal id_dept As String, ByVal dept As String, ByVal dept_head As String, ByVal dept_head_email As String)
        Dim is_ssl = get_setup_field("system_email_is_ssl").ToString
        Dim client As SmtpClient = New SmtpClient()
        If is_ssl = "1" Then
            client.Port = get_setup_field("system_email_ssl_port").ToString
            client.DeliveryMethod = SmtpDeliveryMethod.Network
            client.UseDefaultCredentials = False
            client.Host = get_setup_field("system_email_ssl_server").ToString
            client.EnableSsl = True
            client.Credentials = New System.Net.NetworkCredential(get_setup_field("system_email_ssl").ToString, get_setup_field("system_email_ssl_pass").ToString)
        Else
            client.Port = get_setup_field("system_email_port").ToString
            client.DeliveryMethod = SmtpDeliveryMethod.Network
            client.UseDefaultCredentials = False
            client.Host = get_setup_field("system_email_server").ToString
            client.Credentials = New System.Net.NetworkCredential(get_setup_field("system_email").ToString, get_setup_field("system_email_pass").ToString)
        End If

        ReportEmpAttn.id_dept = id_dept
        ReportEmpAttn.is_head_dept = "-1"
        ReportEmpAttn.is_daily = "-1"

        If is_daily = "1" Then
            ReportEmpAttn.is_daily = "1"
        End If

        Dim Report As New ReportEmpAttn()

        ' Create a new memory stream and export the report into it as PDF.
        Dim Mem As New MemoryStream()
        Report.ExportToPdf(Mem)

        ' Create a new attachment and put the PDF report into it.
        Mem.Seek(0, SeekOrigin.Begin)
        '
        Dim weekly_title As String = "Weekly"
        If is_daily = "1" Then
            weekly_title = "Daily"
        End If
        Dim Att = New Attachment(Mem, weekly_title + " Attendance Report - " & dept & ".pdf", "application/pdf")
        '
        Dim mail_from As MailAddress = New MailAddress("system@volcom.co.id", get_setup_field("app_name").ToString)
        Dim mail_to As MailAddress = New MailAddress(dept_head_email, dept_head)
        Dim mail As MailMessage = New MailMessage(mail_from, mail_to)
        'Dim mail As MailMessage = New MailMessage("system@volcom.co.id", "septian@volcom.co.id")
        mail.Attachments.Add(Att)

        mail.Subject = weekly_title + " Attendance Report (" & dept & ")"
        mail.IsBodyHtml = True
        mail.Body = email_temp(dept_head, False)
        'cc
        Dim query_cc As String = "SELECT emp.`email_external` as email,emp.employee_name FROM tb_m_departement_cc cc
                                  INNER JOIN tb_m_employee emp ON cc.`id_employee`=emp.`id_employee` WHERE cc.id_departement='" & id_dept & "'"

        If get_opt_scheduler_field("weekly_attn_include_management").ToString = "1" Then
            query_cc += " UNION SELECT management_mail AS email, 'Management' AS employee_name FROM tb_opt_scheduler"
        End If

        If is_daily = "1" Then
            query_cc = "
                SELECT emp.`email_external` AS email,emp.employee_name FROM `tb_attn_daily_cc` cc
                INNER JOIN tb_m_employee emp ON cc.`id_employee`=emp.`id_employee`
            "

            If get_opt_scheduler_field("daily_attn_include_management").ToString = "1" Then
                query_cc += " UNION SELECT management_mail AS email, 'Management' AS employee_name FROM tb_opt_scheduler"
            End If
        End If

        Dim data_cc As DataTable = execute_query(query_cc, -1, True, "", "", "", "")

        For i As Integer = 0 To data_cc.Rows.Count - 1
            Dim copy As MailAddress = New MailAddress(data_cc.Rows(i)("email").ToString, data_cc.Rows(i)("employee_name").ToString)
            'Dim copy As MailAddress = New MailAddress("septian@volcom.co.id", data_cc.Rows(i)("email").ToString & " - " & data_cc.Rows(i)("employee_name").ToString)
            mail.CC.Add(copy)
        Next
        '
        client.Send(mail)
        '
        Report.Dispose()
        Mem.Dispose()
        Att.Dispose()
        mail.Dispose()
        client.Dispose()

        'log
        Dim query_log As String = "INSERT INTO tb_scheduler_attn_log(id_log_type,`datetime`,log) VALUES('3',NOW(),'Sending Weekly Attendance Report (" & dept & ") to " & dept_head_email & "')"
        execute_non_query(query_log, True, "", "", "", "")
    End Sub
    Function get_setup_field(ByVal field As String)
        'opt as var choose field
        Dim ret_var, query As String
        ret_var = ""

        Try
            query = "SELECT " & field & " FROM tb_opt LIMIT 1"
            ret_var = execute_query(query, 0, True, "", "", "", "")
        Catch ex As Exception
            ret_var = ""
        End Try

        Return ret_var
    End Function
    Sub send_mail_weekly_attn_head(ByVal emp_name As String, ByVal emp_email As String)
        Dim is_ssl = get_setup_field("system_email_is_ssl").ToString
        Dim client As SmtpClient = New SmtpClient()
        If is_ssl = "1" Then
            client.Port = get_setup_field("system_email_ssl_port").ToString
            client.DeliveryMethod = SmtpDeliveryMethod.Network
            client.UseDefaultCredentials = False
            client.Host = get_setup_field("system_email_ssl_server").ToString
            client.EnableSsl = True
            client.Credentials = New System.Net.NetworkCredential(get_setup_field("system_email_ssl").ToString, get_setup_field("system_email_ssl_pass").ToString)
        Else
            client.Port = get_setup_field("system_email_port").ToString
            client.DeliveryMethod = SmtpDeliveryMethod.Network
            client.UseDefaultCredentials = False
            client.Host = get_setup_field("system_email_server").ToString
            client.Credentials = New System.Net.NetworkCredential(get_setup_field("system_email").ToString, get_setup_field("system_email_pass").ToString)
        End If

        ReportEmpAttn.id_dept = "dept_head"
        ReportEmpAttn.is_head_dept = "1"
        Dim Report As New ReportEmpAttn()

        ' Create a new memory stream and export the report into it as PDF.
        Dim Mem As New MemoryStream()
        Report.ExportToPdf(Mem)

        ' Create a new attachment and put the PDF report into it.
        Mem.Seek(0, System.IO.SeekOrigin.Begin)
        '
        Dim Att = New Attachment(Mem, "Weekly Attendance Report - Department Head.pdf", "application/pdf")
        '
        Dim mail_from As MailAddress = New MailAddress("system@volcom.co.id", get_setup_field("app_name").ToString)
        Dim mail_to As MailAddress = New MailAddress(emp_email, emp_name)
        Dim mail As MailMessage = New MailMessage(mail_from, mail_to)
        'Dim mail As MailMessage = New MailMessage("system@volcom.co.id", "septian@volcom.co.id")
        mail.Attachments.Add(Att)

        mail.Subject = "Weekly Attendance Report (Department Head)"
        mail.IsBodyHtml = True
        mail.Body = email_temp(emp_name, True)
        'cc
        If get_opt_scheduler_field("weekly_head_attn_include_management").ToString = "1" Then
            Dim query_cc = "SELECT management_mail AS email, 'Management' AS employee_name FROM tb_opt_scheduler"
            Dim data_cc As DataTable = execute_query(query_cc, -1, True, "", "", "", "")

            For i As Integer = 0 To data_cc.Rows.Count - 1
                Dim copy As MailAddress = New MailAddress(data_cc.Rows(i)("email").ToString, data_cc.Rows(i)("employee_name").ToString)
                'Dim copy As MailAddress = New MailAddress("septian@volcom.co.id", data_cc.Rows(i)("email").ToString & " - " & data_cc.Rows(i)("employee_name").ToString)
                mail.CC.Add(copy)
            Next
        End If
        client.Send(mail)
        '
        Report.Dispose()
        Mem.Dispose()
        Att.Dispose()
        mail.Dispose()
        client.Dispose()
        '
        'log
        Dim query_log As String = "INSERT INTo tb_scheduler_attn_log(id_log_type,`datetime`,log) VALUES('3',NOW(),'Sending Weekly Attendance Report (Department Head) to " & emp_email & "')"
        execute_non_query(query_log, True, "", "", "", "")
    End Sub
    Function email_temp(ByVal employee_name As String, ByVal is_dept_head As Boolean)
        Dim dep As String = ""
        If is_dept_head = False Then
            dep = "your department"
        Else
            dep = "department head"
        End If
        Dim weekly_title As String = "weekly"
        If is_daily = "1" Then
            weekly_title = "daily"
        End If
        Dim body_temp As String = ""
        body_temp = "<table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100.0%;background:#eeeeee'>
                     <tbody><tr>
                      <td style='padding:30.0pt 30.0pt 30.0pt 30.0pt'>
                      <div align='center'>

                      <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='600' style='width:6.25in;background:white'>
                       <tbody><tr>
                        <td style='padding:0in 0in 0in 0in'></td>
                       </tr>
                       <tr>
                        <td style='padding:0in 0in 0in 0in'>
                        <p class='MsoNormal' align='center' style='text-align:center'><a href='http://www.volcom.co.id/' title='Volcom' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.volcom.co.id/&amp;source=gmail&amp;ust=1480121870771000&amp;usg=AFQjCNEjXvEZWgDdR-Wlke7nn0fmc1ZUuA'><span style='text-decoration:none'><img border='0' width='180' id='m_1811720018273078822_x0000_i1025' src='https://ci3.googleusercontent.com/proxy/x-zXDZUS-2knkEkbTh3HzgyAAusw1Wz7dqV-lbnl39W_4F6T97fJ2_b9doP3nYi0B6KHstdb-tK8VAF_kOaLt2OH=s0-d-e1-ft#http://www.volcom.co.id/enews/img/volcom.jpg' alt='Volcom' class='CToWUd'></span></a><u></u><u></u></p>
                        </td>
                       </tr>
                       <tr>
                        <td style='padding:0in 0in 0in 0in'></td>
                       </tr>
                       <tr>
                        <td style='padding:0in 0in 0in 0in'>
                        <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='600' style='width:6.25in;background:white'>
                         <tbody><tr>
                          <td style='padding:0in 0in 0in 0in'>
      
                          </td>
                         </tr>
                        </tbody></table>
                        <p class='MsoNormal' style='background-color:#eff0f1'><span style='display:block;background-color:#eff0f1;height: 5px;'><u></u>&nbsp;<u></u></span></p>
                        <p class='MsoNormal'><span style='display:none'><u></u>&nbsp;<u></u></span></p>
                        <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' style='background:white'>
                         <tbody><tr>
                          <td style='padding:15.0pt 15.0pt 15.0pt 15.0pt'>
                          <div>
                          <p class='MsoNormal' style='line-height:14.25pt'><b><span style='font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060'>Dear " & employee_name & ",</span></b><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'><u></u><u></u></span></p>
                          <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Here's your " + weekly_title + " attendance report for " & dep & ". Please see attachment.
                    <u></u><u></u></span></p>
                          <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Thank you<br /><b>Volcom ERP</b><u></u><u></u></span></p>

                          </div>
                          </div>
                          </td>
                         </tr>
                        </tbody></table>
                        <p class='MsoNormal' style='background-color:#eff0f1'><span style='display:block;height: 10px;'><u></u>&nbsp;<u></u></span></p>
                        <p class='MsoNormal'><span style='display:none'><u></u>&nbsp;<u></u></span></p>
                        <div align='center'>
                        <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' style='background:white'>
                         <tbody><tr>
                          <td style='padding:6.0pt 6.0pt 6.0pt 6.0pt;text-align:center;'>
                            <span style='text-align:center;font-size:7.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#a0a0a0;letter-spacing:.4pt;'>This email send directly from system. Do not reply.</b><u></u><u></u></span>
                          <p class='MsoNormal' align='center' style='margin-bottom:12.0pt;text-align:center;padding-top:0px;'><img border='0' width='300' id='m_1811720018273078822_x0000_i1028' src='https://ci6.googleusercontent.com/proxy/xq6o45mp_D9Z7DHCK5WT7GKuQ2QDaLg1hyMxoHX5ofUIv_m7GwasoczpbAOn6l6Ze-UfLuIUAndSokPvO633nnO9=s0-d-e1-ft#http://www.volcom.co.id/enews/img/footer.jpg' class='CToWUd'><u></u><u></u></p>
                          </td>
                         </tr>
                        </tbody></table>
                        </div>
                        </td>
                       </tr>
                      </tbody></table>
                      </div>
                      </td>
                     </tr>
                    </tbody></table>"
        Return body_temp
    End Function
    Function email_temp_monthly(ByVal employee_name As String, ByVal is_dept_head As Boolean)
        Dim body_temp As String = ""
        Dim dep As String = ""
        If is_dept_head = False Then
            dep = "your department"
        Else
            dep = "department head"
        End If
        body_temp = "<table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100.0%;background:#eeeeee'>
                     <tbody><tr>
                      <td style='padding:30.0pt 30.0pt 30.0pt 30.0pt'>
                      <div align='center'>

                      <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='600' style='width:6.25in;background:white'>
                       <tbody><tr>
                        <td style='padding:0in 0in 0in 0in'></td>
                       </tr>
                       <tr>
                        <td style='padding:0in 0in 0in 0in'>
                        <p class='MsoNormal' align='center' style='text-align:center'><a href='http://www.volcom.co.id/' title='Volcom' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.volcom.co.id/&amp;source=gmail&amp;ust=1480121870771000&amp;usg=AFQjCNEjXvEZWgDdR-Wlke7nn0fmc1ZUuA'><span style='text-decoration:none'><img border='0' width='180' id='m_1811720018273078822_x0000_i1025' src='https://ci3.googleusercontent.com/proxy/x-zXDZUS-2knkEkbTh3HzgyAAusw1Wz7dqV-lbnl39W_4F6T97fJ2_b9doP3nYi0B6KHstdb-tK8VAF_kOaLt2OH=s0-d-e1-ft#http://www.volcom.co.id/enews/img/volcom.jpg' alt='Volcom' class='CToWUd'></span></a><u></u><u></u></p>
                        </td>
                       </tr>
                       <tr>
                        <td style='padding:0in 0in 0in 0in'></td>
                       </tr>
                       <tr>
                        <td style='padding:0in 0in 0in 0in'>
                        <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='600' style='width:6.25in;background:white'>
                         <tbody><tr>
                          <td style='padding:0in 0in 0in 0in'>
      
                          </td>
                         </tr>
                        </tbody></table>
                        <p class='MsoNormal' style='background-color:#eff0f1'><span style='display:block;background-color:#eff0f1;height: 5px;'><u></u>&nbsp;<u></u></span></p>
                        <p class='MsoNormal'><span style='display:none'><u></u>&nbsp;<u></u></span></p>
                        <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' style='background:white'>
                         <tbody><tr>
                          <td style='padding:15.0pt 15.0pt 15.0pt 15.0pt'>
                          <div>
                          <p class='MsoNormal' style='line-height:14.25pt'><b><span style='font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060'>Dear " & employee_name & ",</span></b><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'><u></u><u></u></span></p>
                          <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Here's your monthly leave remaining report for " & dep & ". Please see attachment.
                    <u></u><u></u></span></p>
                          <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Thank you<br /><b>Volcom ERP</b><u></u><u></u></span></p>

                          </div>
                          </div>
                          </td>
                         </tr>
                        </tbody></table>
                        <p class='MsoNormal' style='background-color:#eff0f1'><span style='display:block;height: 10px;'><u></u>&nbsp;<u></u></span></p>
                        <p class='MsoNormal'><span style='display:none'><u></u>&nbsp;<u></u></span></p>
                        <div align='center'>
                        <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' style='background:white'>
                         <tbody><tr>
                          <td style='padding:6.0pt 6.0pt 6.0pt 6.0pt;text-align:center;'>
                            <span style='text-align:center;font-size:7.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#a0a0a0;letter-spacing:.4pt;'>This email send directly from system. Do not reply.</b><u></u><u></u></span>
                          <p class='MsoNormal' align='center' style='margin-bottom:12.0pt;text-align:center;padding-top:0px;'><img border='0' width='300' id='m_1811720018273078822_x0000_i1028' src='https://ci6.googleusercontent.com/proxy/xq6o45mp_D9Z7DHCK5WT7GKuQ2QDaLg1hyMxoHX5ofUIv_m7GwasoczpbAOn6l6Ze-UfLuIUAndSokPvO633nnO9=s0-d-e1-ft#http://www.volcom.co.id/enews/img/footer.jpg' class='CToWUd'><u></u><u></u></p>
                          </td>
                         </tr>
                        </tbody></table>

                        </div>
                        </td>
                       </tr>
                      </tbody></table>
                      </div>
                      </td>
                     </tr>
                    </tbody></table>"
        Return body_temp
    End Function
    Sub send_mail_duty()
        Dim is_ssl = get_setup_field("system_email_is_ssl").ToString
        Dim client As SmtpClient = New SmtpClient()
        If is_ssl = "1" Then
            client.Port = get_setup_field("system_email_ssl_port").ToString
            client.DeliveryMethod = SmtpDeliveryMethod.Network
            client.UseDefaultCredentials = False
            client.Host = get_setup_field("system_email_ssl_server").ToString
            client.EnableSsl = True
            client.Credentials = New System.Net.NetworkCredential(get_setup_field("system_email_ssl").ToString, get_setup_field("system_email_ssl_pass").ToString)
        Else
            client.Port = get_setup_field("system_email_port").ToString
            client.DeliveryMethod = SmtpDeliveryMethod.Network
            client.UseDefaultCredentials = False
            client.Host = get_setup_field("system_email_server").ToString
            client.Credentials = New System.Net.NetworkCredential(get_setup_field("system_email").ToString, get_setup_field("system_email_pass").ToString)
        End If

        Dim Report As New ReportDuty()

        ' Create a new memory stream and export the report into it as XLS.
        Dim Mem As New MemoryStream()
        Report.ExportToXls(Mem)

        ' Create a new attachment and put the XLS report into it.
        Mem.Seek(0, SeekOrigin.Begin)
        '
        Dim Att = New Attachment(Mem, "Duty List (" & Now.ToString("dd MMMM yyyy") & ").xls", "application/ms-excel")
        '
        Dim query_email As String = "SELECT opt.id_emp_duty_toreport,emp.employee_name,emp.email_external FROM tb_opt_scheduler opt INNER JOIN tb_m_employee emp ON emp.id_employee=opt.id_emp_duty_toreport"
        Dim data_mail As DataTable = execute_query(query_email, -1, True, "", "", "", "")
        '
        Dim mail As MailMessage = New MailMessage("system@volcom.co.id", data_mail.Rows(0)("email_external").ToString)
        mail.Attachments.Add(Att)
        mail.Subject = "Duty Reminder"
        mail.IsBodyHtml = True
        mail.Body = email_temp_duty(data_mail.Rows(0)("employee_name").ToString)
        client.Send(mail)
        '
        Report.Dispose()
        Mem.Dispose()
        Att.Dispose()
        mail.Dispose()
        client.Dispose()

        'log
        Dim query_log As String = "INSERT INTO tb_scheduler_prod_log(id_log_type,`datetime`,log) VALUES('1',NOW(),'Sending PIB/Duty Reminder')"
        execute_non_query(query_log, True, "", "", "", "")
    End Sub
    Function email_temp_duty(ByVal employee_name As String)
        Dim body_temp As String = ""
        '
        body_temp = "<table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100.0%;background:#eeeeee'>
                     <tbody><tr>
                      <td style='padding:30.0pt 30.0pt 30.0pt 30.0pt'>
                      <div align='center'>

                      <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='600' style='width:6.25in;background:white'>
                       <tbody><tr>
                        <td style='padding:0in 0in 0in 0in'></td>
                       </tr>
                       <tr>
                        <td style='padding:0in 0in 0in 0in'>
                        <p class='MsoNormal' align='center' style='text-align:center'><a href='http://www.volcom.co.id/' title='Volcom' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.volcom.co.id/&amp;source=gmail&amp;ust=1480121870771000&amp;usg=AFQjCNEjXvEZWgDdR-Wlke7nn0fmc1ZUuA'><span style='text-decoration:none'><img border='0' width='180' id='m_1811720018273078822_x0000_i1025' src='https://ci3.googleusercontent.com/proxy/x-zXDZUS-2knkEkbTh3HzgyAAusw1Wz7dqV-lbnl39W_4F6T97fJ2_b9doP3nYi0B6KHstdb-tK8VAF_kOaLt2OH=s0-d-e1-ft#http://www.volcom.co.id/enews/img/volcom.jpg' alt='Volcom' class='CToWUd'></span></a><u></u><u></u></p>
                        </td>
                       </tr>
                       <tr>
                        <td style='padding:0in 0in 0in 0in'></td>
                       </tr>
                       <tr>
                        <td style='padding:0in 0in 0in 0in'>
                        <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='600' style='width:6.25in;background:white'>
                         <tbody><tr>
                          <td style='padding:0in 0in 0in 0in'>

                          </td>
                         </tr>
                        </tbody></table>
                        <p class='MsoNormal' style='background-color:#eff0f1'><span style='display:block;background-color:#eff0f1;height: 5px;'><u></u>&nbsp;<u></u></span></p>
                        <p class='MsoNormal'><span style='display:none'><u></u>&nbsp;<u></u></span></p>
                        <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' style='background:white'>
                         <tbody><tr>
                          <td style='padding:15.0pt 15.0pt 15.0pt 15.0pt'>
                          <div>
                          <p class='MsoNormal' style='line-height:14.25pt'><b><span style='font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060'>Dear " & employee_name & ",</span></b><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'><u></u><u></u></span></p>
                          <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>This is daily reminder for PIB payment.
                          "
        If Not pr_due.ToString = "0" Then
            body_temp += "<br/> - " & pr_due & " PIB need to request payment to accounting."
        End If
        If Not soon_due.ToString = "0" Then
            body_temp += "<br/> - " & soon_due & " PIB will due soon."
        End If
        If Not past_due_date.ToString = "0" Then
            body_temp += "<br/> - " & past_due_date & " PIB past the due date."
        End If
        body_temp += "<br/>Make sure to process it immediately. Please see attachment for detail.
                    <u></u><u></u></span></p>
                          <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Thank you<br /><b>Volcom ERP</b><u></u><u></u></span></p>

                          </div>
                          </div>
                          </td>
                         </tr>
                        </tbody></table>
                        <p class='MsoNormal' style='background-color:#eff0f1'><span style='display:block;height: 10px;'><u></u>&nbsp;<u></u></span></p>
                        <p class='MsoNormal'><span style='display:none'><u></u>&nbsp;<u></u></span></p>
                        <div align='center'>
                        <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' style='background:white'>
                         <tbody><tr>
                          <td style='padding:6.0pt 6.0pt 6.0pt 6.0pt;text-align:center;'>
                            <span style='text-align:center;font-size:7.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#a0a0a0;letter-spacing:.4pt;'>This email send directly from system. Do not reply.</b><u></u><u></u></span>
                          <p class='MsoNormal' align='center' style='margin-bottom:12.0pt;text-align:center;padding-top:0px;'><img border='0' width='300' id='m_1811720018273078822_x0000_i1028' src='https://ci6.googleusercontent.com/proxy/xq6o45mp_D9Z7DHCK5WT7GKuQ2QDaLg1hyMxoHX5ofUIv_m7GwasoczpbAOn6l6Ze-UfLuIUAndSokPvO633nnO9=s0-d-e1-ft#http://www.volcom.co.id/enews/img/footer.jpg' class='CToWUd'><u></u><u></u></p>
                          </td>
                         </tr>
                        </tbody></table>
                        </div>
                        </td>
                       </tr>
                      </tbody></table>
                      </div>
                      </td>
                     </tr>
                    </tbody></table>"
        Return body_temp
    End Function

    Function emailOnHold(ByVal to_par As String, ByVal content_par As String, ByVal dt_par As DataTable)
        Dim body_temp As String = "<table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100.0%;background:#eeeeee'>
            <tbody><tr>
              <td style='padding:30.0pt 30.0pt 30.0pt 30.0pt'>
              <div align='center'>

              <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='600' style='width:6.25in;background:white'>
               <tbody><tr>
                <td style='padding:0in 0in 0in 0in'></td>
               </tr>
               <tr>
                <td style='padding:0in 0in 0in 0in'>
                <p class='MsoNormal' align='center' style='text-align:center'><a href='http://www.volcom.co.id/' title='Volcom' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.volcom.co.id/&amp;source=gmail&amp;ust=1480121870771000&amp;usg=AFQjCNEjXvEZWgDdR-Wlke7nn0fmc1ZUuA'><span style='text-decoration:none'><img border='0' width='180' id='m_1811720018273078822_x0000_i1025' src='https://ci3.googleusercontent.com/proxy/x-zXDZUS-2knkEkbTh3HzgyAAusw1Wz7dqV-lbnl39W_4F6T97fJ2_b9doP3nYi0B6KHstdb-tK8VAF_kOaLt2OH=s0-d-e1-ft#http://www.volcom.co.id/enews/img/volcom.jpg' alt='Volcom' class='CToWUd'></span></a><u></u><u></u></p>
                </td>
               </tr>
               <tr>
                <td style='padding:0in 0in 0in 0in'></td>
               </tr>
               <tr>
                <td style='padding:0in 0in 0in 0in'>
                <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='600' style='width:6.25in;background:white'>
                 <tbody><tr>
                  <td style='padding:0in 0in 0in 0in'>

                  </td>
                 </tr>
                </tbody></table>


                <p class='MsoNormal' style='background-color:#eff0f1'><span style='display:block;background-color:#eff0f1;height: 5px;'><u></u>&nbsp;<u></u></span></p>
                <p class='MsoNormal'><span style='display:none'><u></u>&nbsp;<u></u></span></p>
                

                <!-- start body -->
                <table width='100%' class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' style='background:white'>
                 <tbody>
                <tr>
                  <td style='padding:15.0pt 15.0pt 0.0pt 15.0pt' colspan='3'>
                  <div>
                    <b><span class='MsoNormal' style='line-height:14.25pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060'>Hold Delivery - Nama Toko Sesuai List</span></b>
                  </div>
                  </td>
                 </tr>

                 <tr>
                  <td style='padding:15.0pt 15.0pt 15.0pt 15.0pt' colspan='3'>
                  <div style='margin-bottom: 5pt;'>
                    <span class='MsoNormal' style='line-height:15.25pt; font-size: 10pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060'>Kepada : " + to_par + " </span>
                  </div>

                  <div>
                    <span class='MsoNormal' style='line-height:15.25pt; font-size: 10pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060'>" + content_par + "</span>
                  </div>
                  </td>
                 </tr>
               
         
                 <tr>
                  <td style='padding:0.0pt 0.0pt 0.0pt 5.0pt' colspan='3'>
                  	<ol > "

        For i As Integer = 0 To dt_par.Rows.Count - 1
            'data
            body_temp += "<li class='MsoNormal' style='line-height:15.25pt; font-size: 10pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060'>" + dt_par.Rows(i)("group_store").ToString + "</li>"
        Next
        body_temp += "</ol>
                    </td>
                 </tr> 

         
          <tr>
                  <td style='padding:15.0pt 15.0pt 15.0pt 15.0pt' colspan='3'>
                  <div>
                  <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Thank you<br /><b>Volcom ERP</b><u></u><u></u></span></p>

                  </div>
                  </td>
                 </tr>
                </tbody>
              </table>
              <!-- end body -->


                <p class='MsoNormal' style='background-color:#eff0f1'><span style='display:block;height: 10px;'><u></u>&nbsp;<u></u></span></p>
                <p class='MsoNormal'><span style='display:none'><u></u>&nbsp;<u></u></span></p>
                <div align='center'>
                <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' style='background:white'>
                 <tbody><tr>
                  <td style='padding:6.0pt 6.0pt 6.0pt 6.0pt;text-align:center;'>
                    <span style='text-align:center;font-size:7.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#a0a0a0;letter-spacing:.4pt;'>This email send directly from system. Do not reply.</b><u></u><u></u></span>
                  <p class='MsoNormal' align='center' style='margin-bottom:12.0pt;text-align:center;padding-top:0px;'><img border='0' width='300' id='m_1811720018273078822_x0000_i1028' src='https://ci6.googleusercontent.com/proxy/xq6o45mp_D9Z7DHCK5WT7GKuQ2QDaLg1hyMxoHX5ofUIv_m7GwasoczpbAOn6l6Ze-UfLuIUAndSokPvO633nnO9=s0-d-e1-ft#http://www.volcom.co.id/enews/img/footer.jpg' class='CToWUd'><u></u><u></u></p>
                  </td>
                 </tr>
                </tbody></table>
                </div>
                </td>
               </tr>
              </tbody></table>	
              </div>
              </td>
             </tr>
            </tbody>
        </table> "
        Return body_temp
    End Function

    Function email_body_invoice_jatuh_tempo(ByVal dtp As DataTable, ByVal titlep As String, ByVal content_head As String, ByVal content As String, ByVal content_end As String, ByVal tot_amountp As String)
        Dim body_temp As String = "<table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100.0%;background:#eeeeee'>
            <tbody><tr>
              <td style='padding:30.0pt 30.0pt 30.0pt 30.0pt'>
              <div align='center'>

              <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='600' style='width:6.25in;background:white'>
               <tbody><tr>
                <td style='padding:0in 0in 0in 0in'></td>
               </tr>
               <tr>
                <td style='padding:0in 0in 0in 0in'>
                <p class='MsoNormal' align='center' style='text-align:center'><a href='http://www.volcom.co.id/' title='Volcom' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.volcom.co.id/&amp;source=gmail&amp;ust=1480121870771000&amp;usg=AFQjCNEjXvEZWgDdR-Wlke7nn0fmc1ZUuA'><span style='text-decoration:none'><img border='0' width='180' id='m_1811720018273078822_x0000_i1025' src='https://ci3.googleusercontent.com/proxy/x-zXDZUS-2knkEkbTh3HzgyAAusw1Wz7dqV-lbnl39W_4F6T97fJ2_b9doP3nYi0B6KHstdb-tK8VAF_kOaLt2OH=s0-d-e1-ft#http://www.volcom.co.id/enews/img/volcom.jpg' alt='Volcom' class='CToWUd'></span></a><u></u><u></u></p>
                </td>
               </tr>
               <tr>
                <td style='padding:0in 0in 0in 0in'></td>
               </tr>
               <tr>
                <td style='padding:0in 0in 0in 0in'>
                <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='600' style='width:6.25in;background:white'>
                 <tbody><tr>
                  <td style='padding:0in 0in 0in 0in'>

                  </td>
                 </tr>
                </tbody></table>


                <p class='MsoNormal' style='background-color:#eff0f1'><span style='display:block;background-color:#eff0f1;height: 5px;'><u></u>&nbsp;<u></u></span></p>
                <p class='MsoNormal'><span style='display:none'><u></u>&nbsp;<u></u></span></p>
                

                <!-- start body -->
                <table width='100%' class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' style='background:white'>
                 <tbody>
                 <tr>
                  <td style='padding:15.0pt 15.0pt 0pt 15.0pt' colspan='3'>
                  <div>
                    <b><span class='MsoNormal' style='line-height:14.25pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060'>" + titlep + "</span></b>
                  </div>
                  </td>
                 </tr>

                 <tr>
                 	<td style='padding:15.0pt 15.0pt 5.0pt 15.0pt' colspan='3'>
                 		<p style='font-size:10.0pt; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060; border-spacing:0 7px;'>" + content_head + "</p>
                 		<p style='margin-bottom:5pt; line-height:20.25pt; font-size:10.0pt; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060; border-spacing:0 7px;'>" + content + "</p>
	                  	
	                 </td>
                 </tr>

               
         
                 <tr>
                  <td style='padding:1.0pt 15.0pt 15.0pt 15.0pt' colspan='3'>
                    <table width='100%' class='m_1811720018273078822MsoNormalTable' border='1' cellspacing='0' cellpadding='5' style='background:white; font-size: 12px; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060'>
                    <tr>
                      <th>No</th>
                      <th>Store</th>
                      <th>No Invoice</th>
                      <th>Periode Penjualan</th>
                      <th>Jatuh Tempo</th>
                      <th>Nominal</th>
                    </tr> "

        For i As Integer = 0 To dtp.Rows.Count - 1
            body_temp += "<tr>
                <td>" + (i + 1).ToString + "</td>
                <td>" + dtp.Rows(i)("store").ToString + "</td>
                <td>" + dtp.Rows(i)("report_number").ToString + "</td>
                <td>" + dtp.Rows(i)("period").ToString + "</td>
                <td>" + dtp.Rows(i)("sales_pos_due_date").ToString + "</td>
                <td align='center'>" + Decimal.Parse(dtp.Rows(i)("amount").ToString).ToString("N2") + "</td>
            </tr>"
        Next

        body_temp += "<tr>
            <th colspan='5'>TOTAL</th>
            <th>" + tot_amountp + "</th>
        </tr> "

        body_temp += "</table>
                  </td>

                 </tr>

                 <tr>
                 	<td style='padding:5.0pt 15.0pt 5.0pt 15.0pt' colspan='3'>
                 		<p style='line-height:20.25pt;font-size:10.0pt; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060; border-spacing:0 7px;'>" + content_end + "</p>
	                 </td>
                 </tr>

         
          <tr>
                  <td style='padding:15.0pt 15.0pt 15.0pt 15.0pt' colspan='3'>
                  <div>
                  <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'><b>Volcom ERP</b><u></u><u></u></span></p>

                  </div>
                  </td>
                 </tr>
                </tbody>
              </table>
              <!-- end body -->


                <p class='MsoNormal' style='background-color:#eff0f1'><span style='display:block;height: 10px;'><u></u>&nbsp;<u></u></span></p>
                <p class='MsoNormal'><span style='display:none'><u></u>&nbsp;<u></u></span></p>
                <div align='center'>
                <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' style='background:white'>
                 <tbody><tr>
                  <td style='padding:6.0pt 6.0pt 6.0pt 6.0pt;text-align:center;'>
                    <span style='text-align:center;font-size:7.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#a0a0a0;letter-spacing:.4pt;'></b><u></u><u></u></span>
                  <p class='MsoNormal' align='center' style='margin-bottom:12.0pt;text-align:center;padding-top:0px;'><img border='0' width='300' id='m_1811720018273078822_x0000_i1028' src='https://ci6.googleusercontent.com/proxy/xq6o45mp_D9Z7DHCK5WT7GKuQ2QDaLg1hyMxoHX5ofUIv_m7GwasoczpbAOn6l6Ze-UfLuIUAndSokPvO633nnO9=s0-d-e1-ft#http://www.volcom.co.id/enews/img/footer.jpg' class='CToWUd'><u></u><u></u></p>
                  </td>
                 </tr>
                </tbody></table>
                </div>
                </td>
               </tr>
              </tbody></table>	
              </div>
              </td>
             </tr>
            </tbody>
        </table>"
        Return body_temp
    End Function

    Function get_opt_scheduler_field(ByVal field As String)
        'opt as var choose field
        Dim ret_var, query As String
        ret_var = ""

        Try
            query = "SELECT " & field & " FROM tb_opt_scheduler LIMIT 1"
            ret_var = execute_query(query, 0, True, "", "", "", "")
        Catch ex As Exception
            ret_var = ""
        End Try

        Return ret_var
    End Function
End Class
