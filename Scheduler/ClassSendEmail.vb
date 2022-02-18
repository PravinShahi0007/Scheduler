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
    Public design_code As String = ""
    Public design As String = ""
    Public comment_by As String = ""
    Public comment As String = ""
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
        ElseIf report_mark_type = "226" Or report_mark_type = "227" Then
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
            Dim mail_address_from As String = execute_query("SELECT m.mail_address FROM tb_mail_manage_member m WHERE m.id_mail_manage=" + id_report + " AND m.id_mail_member_type=1 ORDER BY m.id_mail_manage_member ASC LIMIT 1", 0, True, "", "", "", "")
            Dim from_mail As MailAddress = New MailAddress(mail_address_from, design_code)
            Dim mail As MailMessage = New MailMessage()
            mail.From = from_mail
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

            mail.Subject = design
            mail.IsBodyHtml = True
            mail.Body = emailOnHold(comment_by, comment, dt)
            client.Send(mail)
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

        Dim Report As New ReportEmpAttn()

        Report.id_dept = id_dept
        Report.is_head_dept = "-1"
        Report.is_daily = "-1"

        If is_daily = "1" Then
            Report.is_daily = "1"
        End If

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

        Dim Report As New ReportEmpAttn()

        Report.id_dept = "dept_head"
        Report.is_head_dept = "1"

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

    Sub send_mail_qc()
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

        Dim Report As New ReportReturnOut()
        Report.LReminderDate.Text = Date.Parse(Now().ToString).ToString("dd MMMM yyyy")

        ' Create a new memory stream and export the report into it as XLS.
        Dim Mem As New MemoryStream()
        Report.ExportToXls(Mem)

        ' Create a new attachment and put the XLS report into it.
        Mem.Seek(0, SeekOrigin.Begin)
        '
        Dim Att = New Attachment(Mem, "Return Out Reminder (" & Now.ToString("dd MMMM yyyy") & ").xls", "application/ms-excel")
        '
        Dim from_mail As MailAddress = New MailAddress("system@volcom.co.id", "Return Out Reminder - Volcom ERP")
        Dim mail As MailMessage = New MailMessage()
        mail.From = from_mail
        mail.Attachments.Add(Att)
        'Send to
        Dim query_send_to As String = "SELECT emp.`email_external`,emp.`employee_name` 
            FROM tb_mail_to md
            INNER JOIN tb_m_user usr ON usr.`id_user`=md.id_user
            INNER JOIN tb_m_employee emp ON emp.`id_employee`=usr.`id_employee`
            WHERE is_to='1' AND md.report_mark_type=296 "
        Dim data_send_to As DataTable = execute_query(query_send_to, -1, True, "", "", "", "")
        For i As Integer = 0 To data_send_to.Rows.Count - 1
            If Not data_send_to.Rows(i)("email_external").ToString = "" Then
                Dim to_mail As MailAddress = New MailAddress(data_send_to.Rows(i)("email_external").ToString, data_send_to.Rows(i)("employee_name").ToString)
                mail.To.Add(to_mail)
            End If
        Next
        'CC
        Dim query_send_cc As String = "SELECT emp.`email_external`,emp.`employee_name` 
            FROM tb_mail_to md
            INNER JOIN tb_m_user usr ON usr.`id_user`=md.id_user
            INNER JOIN tb_m_employee emp ON emp.`id_employee`=usr.`id_employee`
            WHERE is_to='2' AND md.report_mark_type=296 "
        Dim data_send_cc As DataTable = execute_query(query_send_cc, -1, True, "", "", "", "")
        For i As Integer = 0 To data_send_cc.Rows.Count - 1
            If Not data_send_cc.Rows(i)("email_external").ToString = "" Then
                Dim cc_mail As MailAddress = New MailAddress(data_send_cc.Rows(i)("email_external").ToString, data_send_cc.Rows(i)("employee_name").ToString)
                mail.CC.Add(cc_mail)
            End If
        Next

        Dim qmail As String = "SELECT SUM(CASE WHEN tem.overdue > 0 THEN 1 ELSE 0 END) AS jml_overdue,SUM(CASE WHEN tem.overdue <= 0 THEN 1 ELSE 0 END) AS jml_overdue_soon
FROM
(
SELECT h.id_report_status, h.report_status, a.id_prod_order_ret_out, a.prod_order_ret_out_date, a.prod_order_ret_out_due_date, a.prod_order_ret_out_note,  
a.prod_order_ret_out_number , b.prod_order_number, c.id_season, c.season, CONCAT(e.comp_number,' - ',e.comp_name) AS comp_from, CONCAT(g.comp_number,' - ',g.comp_name) AS comp_to, dsg.design_code AS `code`, dsg.design_display_name AS `name`, SUM(ad.prod_order_ret_out_det_qty) AS `qty` 
,IFNULL(retin.qty_ret_in,0) AS qty_retin
,SUM(ad.prod_order_ret_out_det_qty)-IFNULL(retin.qty_ret_in,0) AS diff_qty
,DATEDIFF(DATE(NOW()),a.`prod_order_ret_out_due_date`) AS overdue
FROM tb_prod_order_ret_out a 
INNER JOIN tb_prod_order_ret_out_det ad ON ad.id_prod_order_ret_out = a.id_prod_order_ret_out AND a.is_closed=2 
INNER JOIN tb_prod_order b ON a.id_prod_order = b.id_prod_order 
INNER JOIN tb_prod_demand_design b1 ON b.id_prod_demand_design = b1.id_prod_demand_design 
INNER JOIN tb_m_design dsg ON dsg.id_design = b1.id_design 
INNER JOIN tb_prod_demand b2 ON b2.id_prod_demand = b1.id_prod_demand 
INNER JOIN tb_season c ON b2.id_season = c.id_season 
INNER JOIN tb_m_comp_contact d ON d.id_comp_contact = a.id_comp_contact_from 
INNER JOIN tb_m_comp e ON d.id_comp = e.id_comp 
INNER JOIN tb_m_comp_contact f ON f.id_comp_contact = a.id_comp_contact_to 
INNER JOIN tb_m_comp g ON f.id_comp = g.id_comp 
INNER JOIN tb_lookup_report_status h ON a.id_report_status = h.id_report_status 
LEFT JOIN 
(
	SELECT id_prod_order_ret_out,SUM(prod_order_ret_in_det_qty) AS qty_ret_in
	FROM `tb_prod_order_ret_in_det` retid
	INNER JOIN `tb_prod_order_ret_in` reti ON reti.`id_prod_order_ret_in`=retid.`id_prod_order_ret_in`
	WHERE reti.`id_report_status`!=5
	GROUP BY reti.`id_prod_order_ret_out`
)retin ON retin.id_prod_order_ret_out=a.`id_prod_order_ret_out`
WHERE a.`prod_order_ret_out_due_date` >= '2020-07-01' AND a.`prod_order_ret_out_due_date` <= DATE_ADD(DATE(NOW()),INTERVAL 3 DAY) AND a.`id_report_status`=6
GROUP BY a.id_prod_order_ret_out 
HAVING qty > qty_retin
ORDER BY a.id_prod_order_ret_out DESC
) tem"
        Dim dtmail As DataTable = execute_query(qmail, -1, True, "", "", "", "")

        mail.Subject = "Return Out Reminder (" & Now.ToString("dd MMMM yyyy") & ")"
        mail.IsBodyHtml = True
        mail.Body = email_qc(dtmail.Rows(0)("jml_overdue").ToString, dtmail.Rows(0)("jml_overdue_soon").ToString)
        client.Send(mail)
        '
        Report.Dispose()
        Mem.Dispose()
        Att.Dispose()
        mail.Dispose()
        client.Dispose()

        'log
        Dim query_log As String = "INSERT INTO tb_scheduler_prod_log(id_log_type,`datetime`,log) VALUES('1',NOW(),'Sending Return Out Reminder')"
        execute_non_query(query_log, True, "", "", "", "")
    End Sub

    Function email_qc(ByVal jml_return_out_due As String, ByVal jml_return_out_due_soon As String)
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
                          <p class='MsoNormal' style='line-height:14.25pt'><b><span style='font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060'>Dear Team,</span></b><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'><u></u><u></u></span></p>
                          <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>This is daily reminder for outstanding Return Out.
                          "
        If Not jml_return_out_due.ToString = "0" Then
            body_temp += "<br/> - " & jml_return_out_due & " overdue return out."
        End If
        If Not jml_return_out_due_soon.ToString = "0" Then
            body_temp += "<br/> - " & jml_return_out_due_soon & " return out will due soon."
        End If
        body_temp += "<br/>Make sure to follow up immediately. Please see attachment for detail.
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

    Sub send_mail_polis()
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

        Dim Report As New ReportPolis()
        Report.LReminderDate.Text = Date.Parse(Now().ToString).ToString("dd MMMM yyyy")

        ' Create a new memory stream and export the report into it as XLS.
        Dim Mem As New MemoryStream()
        Report.ExportToXls(Mem)

        ' Create a new attachment and put the XLS report into it.
        Mem.Seek(0, SeekOrigin.Begin)
        '
        Dim Att = New Attachment(Mem, "Polis Reminder (" & Now.ToString("dd MMMM yyyy") & ").xls", "application/ms-excel")
        '
        Dim from_mail As MailAddress = New MailAddress("system@volcom.co.id", "Polis Reminder - Volcom ERP")
        Dim mail As MailMessage = New MailMessage()
        mail.From = from_mail
        mail.Attachments.Add(Att)
        'Send to
        Dim query_send_to As String = "SELECT emp.`email_external`,emp.`employee_name` 
            FROM tb_mail_to md
            INNER JOIN tb_m_user usr ON usr.`id_user`=md.id_user
            INNER JOIN tb_m_employee emp ON emp.`id_employee`=usr.`id_employee`
            WHERE is_to='1' AND md.report_mark_type=297 "
        Dim data_send_to As DataTable = execute_query(query_send_to, -1, True, "", "", "", "")
        For i As Integer = 0 To data_send_to.Rows.Count - 1
            If Not data_send_to.Rows(i)("email_external").ToString = "" Then
                Dim to_mail As MailAddress = New MailAddress(data_send_to.Rows(i)("email_external").ToString, data_send_to.Rows(i)("employee_name").ToString)
                mail.To.Add(to_mail)
            End If
        Next
        'CC
        Dim query_send_cc As String = "SELECT emp.`email_external`,emp.`employee_name` 
            FROM tb_mail_to md
            INNER JOIN tb_m_user usr ON usr.`id_user`=md.id_user
            INNER JOIN tb_m_employee emp ON emp.`id_employee`=usr.`id_employee`
            WHERE is_to='2' AND md.report_mark_type=297 "
        Dim data_send_cc As DataTable = execute_query(query_send_cc, -1, True, "", "", "", "")
        For i As Integer = 0 To data_send_cc.Rows.Count - 1
            If Not data_send_cc.Rows(i)("email_external").ToString = "" Then
                Dim cc_mail As MailAddress = New MailAddress(data_send_cc.Rows(i)("email_external").ToString, data_send_cc.Rows(i)("employee_name").ToString)
                mail.CC.Add(cc_mail)
            End If
        Next

        Dim qmail As String = "SELECT p.`number` AS polis_number,COUNT(DISTINCT(p.`id_reff`)) AS jml_toko,IF(COUNT(DISTINCT(p.`id_reff`))>1,'Kolektif','Mandiri') AS jenis_polis,DATEDIFF(p.end_date,DATE(NOW())) AS expired_in,pol_by.comp_name AS comp_name_polis,d.`description` AS polis_untuk,p.`premi`,p.`start_date`,p.`end_date` 
FROM tb_polis p 
INNER JOIN tb_m_comp c ON c.`id_comp`=p.`id_reff` AND p.`id_polis_cat`=1
INNER JOIN tb_m_comp pol_by ON pol_by.id_comp=p.id_polis_by
INNER JOIN `tb_lookup_desc_premi` d ON d.`id_desc_premi`=p.`id_desc_premi`
WHERE p.`is_active`=1 AND DATEDIFF(p.end_date,DATE(NOW()))<60
GROUP BY p.number,p.`id_polis_by`,p.`end_date`"
        Dim dtmail As DataTable = execute_query(qmail, -1, True, "", "", "", "")

        mail.Subject = "Polis Reminder (" & Now.ToString("dd MMMM yyyy") & ")"
        mail.IsBodyHtml = True
        mail.Body = email_polis(dtmail)
        client.Send(mail)
        '
        Report.Dispose()
        Mem.Dispose()
        Att.Dispose()
        mail.Dispose()
        client.Dispose()

        'log
        Dim query_log As String = "INSERT INTO tb_scheduler_prod_log(id_log_type,`datetime`,log) VALUES('1',NOW(),'Sending Polis Reminder')"
        execute_non_query(query_log, True, "", "", "", "")
    End Sub

    Function email_polis(ByVal dtbody As DataTable)
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
        <p class='MsoNormal' align='center' style='text-align:center'><a href='http://www.volcom.co.id/' title='Volcom' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.volcom.co.id/&amp;source=gmail&amp;ust=1480121870771000&amp;usg=AFQjCNEjXvEZWgDdR-Wlke7nn0fmc1ZUuA'><span style='text-decoration:none'><img border='0' width='180' id='m_1811720018273078822_x0000_i1025' src='https://d3k81ch9hvuctc.cloudfront.net/company/VFgA3P/images/de2b6f13-9275-426d-ae31-640f3dcfc744.jpeg' alt='Volcom' class='CToWUd'></span></a><u></u><u></u></p>
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
                  <td style='padding:15.0pt 15.0pt 5.0pt 15.0pt' colspan='3'>
                  <div>
                  <p class='MsoNormal' style='line-height:14.25pt'><span style='font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060'>Dear Team, </span><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'><u></u><u></u></span></p>
                  </div>
                  </td>

                 </tr>
                 <tr>
                  <td style='padding:15.0pt 15.0pt 5.0pt 15.0pt' colspan='3'>
                  <div>
                  <p class='MsoNormal' style='line-height:14.25pt'><span style='font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060'>Terdapat polis asuransi yang akan/sudah expired dengan detail sebagai berikut: </span><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'><u></u><u></u></span></p>
                  </div>
                  </td>
                 </tr>
                 <tr>
                  <td style='padding:1.0pt 15.0pt 15.0pt 15.0pt' colspan='3'>
                  <table width='100%' class='m_1811720018273078822MsoNormalTable' border='1' cellspacing='0' cellpadding='5' style='background:white; font-size: 12px; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#000000'>
                  <tr style='background-color:black; font-size: 12px; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#ffffff'>
                    <th>Vendor</th>
                    <th>Tipe Polis</th>
                    <th>Nomor Polis</th>
                    <th>Keterangan</th>
                    <th>Jumlah Toko</th>
                    <th>Expired dalam (hari)</th>
                  </tr> 

                <!-- row data --> "
        For d As Integer = 0 To dtbody.Rows.Count - 1
            body_temp += "
      <td>" + dtbody.Rows(d)("comp_name_polis").ToString() + "</td>
      <td>" + dtbody.Rows(d)("polis_untuk").ToString() + "</td>
      <td>" + dtbody.Rows(d)("polis_number").ToString() + "</td>
      <td>" + dtbody.Rows(d)("jenis_polis").ToString() + "</td>
      <td>" + Decimal.Parse(dtbody.Rows(d)("jml_toko").ToString()).ToString("N0") + "</td>
      <td>" + Decimal.Parse(dtbody.Rows(d)("expired_in").ToString()).ToString("N0") + "</td>
      </tr>"
        Next
        body_temp += "</table>
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
            <span style='text-align:center;font-size:7.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#a0a0a0;letter-spacing:.4pt;'>This email send directly from Volcom ERP. Do not reply.</b><u></u><u></u></span>
          <p class='MsoNormal' align='center' style='margin-bottom:12.0pt;text-align:center;padding-top:0px;'><br></p>
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

    Sub send_mail_po_og()
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

        Dim Report As New ReportPurcOG()
        Report.LReminderDate.Text = Date.Parse(Now().ToString).ToString("dd MMMM yyyy")

        ' Create a new memory stream and export the report into it as XLS.
        Dim Mem As New MemoryStream()
        Report.ExportToXls(Mem)

        ' Create a new attachment and put the XLS report into it.
        Mem.Seek(0, SeekOrigin.Begin)
        '
        Dim Att = New Attachment(Mem, "PO OG Reminder (" & Now.ToString("dd MMMM yyyy") & ").xls", "application/ms-excel")
        '
        Dim from_mail As MailAddress = New MailAddress("system@volcom.co.id", "PO OG Reminder - Volcom ERP")
        Dim mail As MailMessage = New MailMessage()
        mail.From = from_mail
        mail.Attachments.Add(Att)
        'Send to
        Dim query_send_to As String = "SELECT emp.`email_external`,emp.`employee_name` 
            FROM tb_mail_to md
            INNER JOIN tb_m_user usr ON usr.`id_user`=md.id_user
            INNER JOIN tb_m_employee emp ON emp.`id_employee`=usr.`id_employee`
            WHERE is_to='1' AND md.report_mark_type=308 "
        Dim data_send_to As DataTable = execute_query(query_send_to, -1, True, "", "", "", "")
        For i As Integer = 0 To data_send_to.Rows.Count - 1
            If Not data_send_to.Rows(i)("email_external").ToString = "" Then
                Dim to_mail As MailAddress = New MailAddress(data_send_to.Rows(i)("email_external").ToString, data_send_to.Rows(i)("employee_name").ToString)
                mail.To.Add(to_mail)
            End If
        Next
        'CC
        Dim query_send_cc As String = "SELECT emp.`email_external`,emp.`employee_name` 
            FROM tb_mail_to md
            INNER JOIN tb_m_user usr ON usr.`id_user`=md.id_user
            INNER JOIN tb_m_employee emp ON emp.`id_employee`=usr.`id_employee`
            WHERE is_to='2' AND md.report_mark_type=308 "
        Dim data_send_cc As DataTable = execute_query(query_send_cc, -1, True, "", "", "", "")
        For i As Integer = 0 To data_send_cc.Rows.Count - 1
            If Not data_send_cc.Rows(i)("email_external").ToString = "" Then
                Dim cc_mail As MailAddress = New MailAddress(data_send_cc.Rows(i)("email_external").ToString, data_send_cc.Rows(i)("employee_name").ToString)
                mail.CC.Add(cc_mail)
            End If
        Next

        Dim qmail As String = "SELECT SUM(CASE WHEN tem.expired_in < 0 THEN 1 ELSE 0 END) AS jml_expired,SUM(CASE WHEN tem.expired_in >= 0 THEN 1 ELSE 0 END) AS jml_expired_soon
FROM 
(
SELECT po.`id_purc_order`,po.`purc_order_number`,po.`date_created`,po.`est_date_receive`,reqd.`id_item`,it.`item_desc`,reqd.`item_detail`,req.id_user_created,emp.`employee_name` AS req_by,req.`requirement_date`
,IFNULL(rec.qty,0) AS rec_qty,reqd.`qty` AS req_qty,pod.`qty` AS po_qty,c.`comp_name`,DATEDIFF(po.`est_date_receive`,DATE(NOW())) AS expired_in
FROM tb_purc_order_det pod
INNER JOIN tb_purc_req_det reqd ON reqd.`id_purc_req_det`=pod.`id_purc_req_det`
INNER JOIN tb_purc_req req ON req.`id_purc_req`=reqd.`id_purc_req` AND req.`id_report_status`='6'
INNER JOIN tb_purc_order po ON po.id_purc_order=pod.id_purc_order AND po.`id_report_status`='6' AND po.`is_close_rec`=2
INNER JOIN tb_item it ON it.`id_item`=reqd.`id_item`
INNER JOIN tb_m_uom uom ON uom.id_uom=it.id_uom
INNER JOIN tb_m_comp_contact cc ON po.`id_comp_contact`=cc.`id_comp_contact`
INNER JOIN tb_m_comp c ON c.`id_comp`=cc.`id_comp`
INNER JOIN tb_m_user usr_req ON usr_req.`id_user`=req.id_user_created
INNER JOIN tb_m_employee emp ON emp.`id_employee`=usr_req.`id_employee`
LEFT JOIN
(
	SELECT pod.`id_purc_order_det`,pod.`id_purc_req_det`,SUM(recd.`qty`) AS qty 
	FROM tb_purc_rec_det recd
	INNER JOIN tb_purc_order_det pod ON recd.id_purc_order_det=pod.id_purc_order_det
	INNER JOIN tb_purc_rec rec ON recd.`id_purc_rec`=rec.id_purc_rec
	WHERE rec.`id_report_status`!='5'
	GROUP BY pod.`id_purc_req_det`
)rec  ON rec.id_purc_req_det=reqd.`id_purc_req_det`
WHERE DATE(po.est_date_receive)<DATE_ADD(NOW(),INTERVAL 4 DAY) AND reqd.`qty`>IFNULL(rec.qty,0)
GROUP BY po.`id_purc_order`
) tem "
        Dim dtmail As DataTable = execute_query(qmail, -1, True, "", "", "", "")

        mail.Subject = "PO OG Reminder (" & Now.ToString("dd MMMM yyyy") & ")"
        mail.IsBodyHtml = True
        mail.Body = email_po_og(dtmail.Rows(0)("jml_expired").ToString, dtmail.Rows(0)("jml_expired_soon").ToString)
        client.Send(mail)

        Report.Dispose()
        Mem.Dispose()
        Att.Dispose()
        mail.Dispose()
        client.Dispose()

        'log
        Dim query_log As String = "INSERT INTO tb_scheduler_prod_log(id_log_type,`datetime`,log) VALUES('1',NOW(),'Sending PO OG Reminder')"
        execute_non_query(query_log, True, "", "", "", "")
    End Sub

    Function email_po_og(ByVal jml_expired As String, ByVal jml_expired_soon As String)
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
                          <p class='MsoNormal' style='line-height:14.25pt'><b><span style='font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060'>Dear Team,</span></b><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'><u></u><u></u></span></p>
                          <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>This is daily reminder for Purchase Order Operational Goods.
                          "
        If Not jml_expired.ToString = "0" Then
            body_temp += "<br/> - " & jml_expired & " overdue for receiving."
        End If
        If Not jml_expired_soon.ToString = "0" Then
            body_temp += "<br/> - " & jml_expired_soon & " PO receiving will due soon."
        End If
        body_temp += "<br/>Make sure to follow up immediately. Please see attachment for detail.
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

    Sub send_mail_pr_og()
        Dim qloop As String = "SELECT pr.`id_purc_req`,dep.`id_departement`,dep.`departement`,prd.`id_purc_req_det`,prd.`item_detail`,pr.`is_cancel`,pr.`purc_req_number`,pr.`requirement_date`,po.po_number,po.report_status,po.est_date_receive,prd.`qty` AS qty_pr,IFNULL(po.qty,0) AS qty_po
,IFNULL(rec.qty,0) AS qty_rec,empc.`employee_name`,empc.`email_external`
FROM tb_purc_req_det prd
INNER JOIN tb_purc_req pr ON pr.`id_purc_req`=prd.`id_purc_req` AND pr.`id_report_status`=6 AND pr.`is_cancel`=2 AND prd.`is_close`=2 AND prd.`is_unable_fulfill`=2
INNER JOIN tb_m_departement dep ON dep.`id_departement`=pr.`id_departement`
INNER JOIN tb_m_user usrc ON usrc.`id_user`=pr.`id_user_created`
INNER JOIN tb_m_employee empc ON empc.`id_employee`=usrc.`id_employee`
LEFT JOIN 
(
	SELECT pod.id_purc_req_det,SUM(pod.`qty`) AS qty,GROUP_CONCAT(DISTINCT po.purc_order_number ORDER BY po.purc_order_number) AS po_number
	,GROUP_CONCAT(DISTINCT IF(po.is_submit=1,sts.report_status,'Created, Not submitted') ORDER BY po.purc_order_number) AS report_status,MAX(po.est_date_receive) AS est_date_receive
	FROM tb_purc_order_det pod 
	INNER JOIN tb_purc_order po ON po.id_purc_order=pod.id_purc_order AND po.id_report_status!=5 AND pod.`is_drop`=2
	INNER JOIN tb_lookup_report_status sts ON sts.`id_report_status`=po.`id_report_status`
	GROUP BY pod.id_purc_req_det
) po ON po.id_purc_req_det =prd.`id_purc_req_det`
LEFT JOIN 
(
	SELECT pod.`id_purc_req_det`,SUM(recd.`qty`) AS qty FROM tb_purc_rec_det recd
	INNER JOIN tb_purc_order_det pod ON recd.id_purc_order_det=pod.id_purc_order_det
	INNER JOIN tb_purc_rec rec ON recd.`id_purc_rec`=rec.id_purc_rec
	WHERE rec.`id_report_status`!='5'
	GROUP BY pod.`id_purc_req_det`
)rec ON rec.id_purc_req_det=prd.`id_purc_req_det`
WHERE (prd.`qty`>IFNULL(po.qty,0) OR prd.`qty`>IFNULL(rec.qty,0)) 
GROUP BY pr.`id_departement`"
        Dim dtloop As DataTable = execute_query(qloop, -1, True, "", "", "", "")

        'loop
        For iloop As Integer = 0 To dtloop.Rows.Count - 1
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

            'get id_departement
            Dim id_dep As String = dtloop.Rows(iloop)("id_departement").ToString

            Dim Report As New ReportPROG()
            Report.id_departement = id_dep
            Report.LReminderDate.Text = Date.Parse(Now().ToString).ToString("dd MMMM yyyy")

            ' Create a new memory stream and export the report into it as XLS.
            Dim Mem As New MemoryStream()
            Report.ExportToXls(Mem)

            ' Create a new attachment and put the XLS report into it.
            Mem.Seek(0, SeekOrigin.Begin)
            '
            Dim Att = New Attachment(Mem, "PR OG Reminder (" & dtloop.Rows(iloop)("departement").ToString & " - " & Now.ToString("dd MMMM yyyy") & ").xls", "application/ms-excel")
            '
            Dim from_mail As MailAddress = New MailAddress("system@volcom.co.id", "PR OG Reminder (" & dtloop.Rows(iloop)("departement").ToString & " - " & Now.ToString("dd MMMM yyyy") & ") - Volcom ERP")
            Dim mail As MailMessage = New MailMessage()
            mail.From = from_mail
            mail.Attachments.Add(Att)

            'Send to
            Dim query_send_to As String = "SELECT emp.`email_external`,emp.`employee_name` 
            FROM tb_mail_to md
            INNER JOIN tb_m_user usr ON usr.`id_user`=md.id_user
            INNER JOIN tb_m_employee emp ON emp.`id_employee`=usr.`id_employee`
            WHERE is_to='1' AND md.report_mark_type=311 "
            Dim data_send_to As DataTable = execute_query(query_send_to, -1, True, "", "", "", "")
            For i As Integer = 0 To data_send_to.Rows.Count - 1
                If Not data_send_to.Rows(i)("email_external").ToString = "" Then
                    Dim to_mail As MailAddress = New MailAddress(data_send_to.Rows(i)("email_external").ToString, data_send_to.Rows(i)("employee_name").ToString)
                    mail.To.Add(to_mail)
                End If
            Next

            'CC
            Dim query_send_cc As String = "SELECT empc.`employee_name`,empc.`email_external`
FROM tb_purc_req_det prd
INNER JOIN tb_purc_req pr ON pr.`id_purc_req`=prd.`id_purc_req` AND pr.`id_report_status`=6 AND pr.`is_cancel`=2 AND prd.`is_close`=2 AND prd.`is_unable_fulfill`=2
INNER JOIN tb_m_departement dep ON dep.`id_departement`=pr.`id_departement`
INNER JOIN tb_m_user usrc ON usrc.`id_user`=pr.`id_user_created`
INNER JOIN tb_m_employee empc ON empc.`id_employee`=usrc.`id_employee` AND empc.`id_employee_active`=1
LEFT JOIN 
(
	SELECT pod.id_purc_req_det,SUM(pod.`qty`) AS qty,GROUP_CONCAT(DISTINCT po.purc_order_number ORDER BY po.purc_order_number) AS po_number
	,GROUP_CONCAT(DISTINCT IF(po.is_submit=1,sts.report_status,'Created, Not submitted') ORDER BY po.purc_order_number) AS report_status,MAX(po.est_date_receive) AS est_date_receive
	FROM tb_purc_order_det pod 
	INNER JOIN tb_purc_order po ON po.id_purc_order=pod.id_purc_order AND po.id_report_status!=5 AND pod.`is_drop`=2
	INNER JOIN tb_lookup_report_status sts ON sts.`id_report_status`=po.`id_report_status`
	GROUP BY pod.id_purc_req_det
) po ON po.id_purc_req_det =prd.`id_purc_req_det`
LEFT JOIN 
(
	SELECT pod.`id_purc_req_det`,SUM(recd.`qty`) AS qty FROM tb_purc_rec_det recd
	INNER JOIN tb_purc_order_det pod ON recd.id_purc_order_det=pod.id_purc_order_det
	INNER JOIN tb_purc_rec rec ON recd.`id_purc_rec`=rec.id_purc_rec
	WHERE rec.`id_report_status`!='5'
	GROUP BY pod.`id_purc_req_det`
)rec ON rec.id_purc_req_det=prd.`id_purc_req_det`
WHERE (prd.`qty`>IFNULL(po.qty,0) OR prd.`qty`>IFNULL(rec.qty,0)) AND DATE(pr.requirement_date)<DATE_ADD(NOW(),INTERVAL 4 DAY)  AND pr.`id_departement`='" & id_dep & "'
AND dep.`id_user_head` != usrc.`id_user`
GROUP BY pr.`id_user_created`
UNION ALL
SELECT empc.`employee_name`,empc.`email_external`
FROM
tb_m_departement dep 
INNER JOIN tb_m_user usrc ON usrc.`id_user`=dep.`id_user_head`
INNER JOIN tb_m_employee empc ON empc.`id_employee`=usrc.`id_employee` AND empc.`id_employee_active`=1
WHERE dep.`id_departement`='" & id_dep & "'"
            Dim data_send_cc As DataTable = execute_query(query_send_cc, -1, True, "", "", "", "")
            For i As Integer = 0 To data_send_cc.Rows.Count - 1
                If Not data_send_cc.Rows(i)("email_external").ToString = "" Then
                    Dim cc_mail As MailAddress = New MailAddress(data_send_cc.Rows(i)("email_external").ToString, data_send_cc.Rows(i)("employee_name").ToString)
                    'Dim cc_mail As MailAddress = New MailAddress("septian@volcom.co.id", data_send_cc.Rows(i)("employee_name").ToString)
                    mail.CC.Add(cc_mail)
                End If
            Next

            Dim qmail As String = "SELECT SUM(CASE WHEN tem.expired_in < 0 THEN 1 ELSE 0 END) AS jml_expired,SUM(CASE WHEN tem.expired_in >= 0 THEN 1 ELSE 0 END) AS jml_expired_soon
FROM 
(
SELECT IF(DATE(pr.requirement_date)<DATE(NOW()),'Overdue','Due Soon') AS group_report,DATEDIFF(pr.`requirement_date`,DATE(NOW())) AS expired_in,
pr.`id_purc_req`,dep.`id_departement`,dep.`departement`,prd.`id_purc_req_det`,prd.`item_detail`,pr.`is_cancel`,pr.`purc_req_number`,pr.`requirement_date`,po.po_number,po.report_status,po.est_date_receive,prd.`qty` AS qty_pr,IFNULL(po.qty,0) AS qty_po
,IFNULL(rec.qty,0) AS qty_rec,empc.`employee_name`,empc.`email_external`
FROM tb_purc_req_det prd
INNER JOIN tb_purc_req pr ON pr.`id_purc_req`=prd.`id_purc_req` AND pr.`id_report_status`=6 AND pr.`is_cancel`=2 AND prd.`is_close`=2 AND prd.`is_unable_fulfill`=2
INNER JOIN tb_m_departement dep ON dep.`id_departement`=pr.`id_departement`
INNER JOIN tb_m_user usrc ON usrc.`id_user`=pr.`id_user_created`
INNER JOIN tb_m_employee empc ON empc.`id_employee`=usrc.`id_employee`
LEFT JOIN 
(
	SELECT pod.id_purc_req_det,SUM(pod.`qty`) AS qty,GROUP_CONCAT(DISTINCT po.purc_order_number ORDER BY po.purc_order_number) AS po_number
	,GROUP_CONCAT(DISTINCT IF(po.is_submit=1,sts.report_status,'Created, Not submitted') ORDER BY po.purc_order_number) AS report_status,MAX(po.est_date_receive) AS est_date_receive
	FROM tb_purc_order_det pod 
	INNER JOIN tb_purc_order po ON po.id_purc_order=pod.id_purc_order AND po.id_report_status!=5 AND pod.`is_drop`=2
	INNER JOIN tb_lookup_report_status sts ON sts.`id_report_status`=po.`id_report_status`
	GROUP BY pod.id_purc_req_det
) po ON po.id_purc_req_det =prd.`id_purc_req_det`
LEFT JOIN 
(
	SELECT pod.`id_purc_req_det`,SUM(recd.`qty`) AS qty FROM tb_purc_rec_det recd
	INNER JOIN tb_purc_order_det pod ON recd.id_purc_order_det=pod.id_purc_order_det
	INNER JOIN tb_purc_rec rec ON recd.`id_purc_rec`=rec.id_purc_rec
	WHERE rec.`id_report_status`!='5'
	GROUP BY pod.`id_purc_req_det`
)rec ON rec.id_purc_req_det=prd.`id_purc_req_det`
WHERE (prd.`qty`>IFNULL(po.qty,0) OR prd.`qty`>IFNULL(rec.qty,0)) AND DATE(pr.requirement_date)<DATE_ADD(NOW(),INTERVAL 4 DAY) AND pr.`id_departement`='" & id_dep & "'
GROUP BY pr.id_purc_req
ORDER BY pr.requirement_date
) tem "
            Dim dtmail As DataTable = execute_query(qmail, -1, True, "", "", "", "")

            mail.Subject = "PR OG Reminder (" & Now.ToString("dd MMMM yyyy") & ")"
            mail.IsBodyHtml = True
            mail.Body = email_pr_og(dtmail.Rows(0)("jml_expired").ToString, dtmail.Rows(0)("jml_expired_soon").ToString)
            client.Send(mail)

            Report.Dispose()
            Mem.Dispose()
            Att.Dispose()
            mail.Dispose()
            client.Dispose()
        Next

        'log
        Dim query_log As String = "INSERT INTO tb_scheduler_prod_log(id_log_type,`datetime`,log) VALUES('1',NOW(),'Sending PR OG Reminder')"
        execute_non_query(query_log, True, "", "", "", "")
    End Sub

    Function email_pr_og(ByVal jml_expired As String, ByVal jml_expired_soon As String)
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
                          <p class='MsoNormal' style='line-height:14.25pt'><b><span style='font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060'>Dear Team,</span></b><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'><u></u><u></u></span></p>
                          <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>This is daily reminder for Purchase Request Operational Goods.
                          "
        If Not jml_expired.ToString = "0" Then
            body_temp += "<br/> - " & jml_expired & " PR past its requirement date."
        End If
        If Not jml_expired_soon.ToString = "0" Then
            body_temp += "<br/> - " & jml_expired_soon & " PR will due its requirement date soon."
        End If
        body_temp += "<br/>Make sure to follow up immediately. Please see attachment for detail.
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

    Sub send_mail_pib_notif()
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

        Dim Report As New ReportPIBReviewNotif()
        Report.LReminderDate.Text = Date.Parse(Now().ToString).ToString("dd MMMM yyyy")

        ' Create a new memory stream and export the report into it as XLS.
        Dim Mem As New MemoryStream()
        Report.ExportToXls(Mem)

        ' Create a new attachment and put the XLS report into it.
        Mem.Seek(0, SeekOrigin.Begin)
        '
        Dim Att = New Attachment(Mem, "PIB Review Notification (" & Now.ToString("dd MMMM yyyy") & ").xls", "application/ms-excel")
        '
        Dim from_mail As MailAddress = New MailAddress("system@volcom.co.id", "PIB Review Notifications (" & Now.ToString("dd MMMM yyyy") & ") - Volcom ERP")
        Dim mail As MailMessage = New MailMessage()
        mail.From = from_mail
        mail.Attachments.Add(Att)
        'Send to
        Dim query_send_to As String = "SELECT emp.`email_external`,emp.`employee_name` 
            FROM tb_mail_to md
            INNER JOIN tb_m_user usr ON usr.`id_user`=md.id_user
            INNER JOIN tb_m_employee emp ON emp.`id_employee`=usr.`id_employee`
            WHERE is_to='1' AND md.report_mark_type=360 "
        Dim data_send_to As DataTable = execute_query(query_send_to, -1, True, "", "", "", "")
        For i As Integer = 0 To data_send_to.Rows.Count - 1
            If Not data_send_to.Rows(i)("email_external").ToString = "" Then
                Dim to_mail As MailAddress = New MailAddress(data_send_to.Rows(i)("email_external").ToString, data_send_to.Rows(i)("employee_name").ToString)
                mail.To.Add(to_mail)
            End If
        Next
        'CC
        Dim query_send_cc As String = "SELECT emp.`email_external`,emp.`employee_name` 
            FROM tb_mail_to md
            INNER JOIN tb_m_user usr ON usr.`id_user`=md.id_user
            INNER JOIN tb_m_employee emp ON emp.`id_employee`=usr.`id_employee`
            WHERE is_to='2' AND md.report_mark_type=360 "
        Dim data_send_cc As DataTable = execute_query(query_send_cc, -1, True, "", "", "", "")
        For i As Integer = 0 To data_send_cc.Rows.Count - 1
            If Not data_send_cc.Rows(i)("email_external").ToString = "" Then
                Dim cc_mail As MailAddress = New MailAddress(data_send_cc.Rows(i)("email_external").ToString, data_send_cc.Rows(i)("employee_name").ToString)
                mail.CC.Add(cc_mail)
            End If
        Next

        Dim qmail As String = "SELECT prec.number,pir.pib_no,DATE_FORMAT(pir.pib_date,'%d %M %Y') AS pib_date,DATE_FORMAT(pir.vp_due_date,'%d %M %Y') AS second_due_date
,IF(pir.id_notif_type=2 OR pir.id_notif_type=4,'Second voluntary payment will due soon (45 days)',CONCAT('Article on PIB sell more than ',pir.`notif_qty_sales_percent`,'%')) AS notif_notes
FROM tb_pib_review pir
INNER JOIN tb_pre_cal_fgpo prec ON prec.`id_pre_cal_fgpo`=pir.`id_pre_cal_fgpo`
INNER JOIN tb_prod_order po ON pir.`id_prod_order`=po.`id_prod_order`
INNER JOIN tb_m_design d ON d.`id_design`=pir.`id_design`
WHERE pir.is_active=1 AND pir.is_notified=1 AND pir.notif_triggered_date=DATE_SUB(DATE(NOW()),INTERVAL 1 DAY)
GROUP BY pir.`id_pre_cal_fgpo`"
        Dim dtmail As DataTable = execute_query(qmail, -1, True, "", "", "", "")

        mail.Subject = "PIB Review Notification (" & Now.ToString("dd MMMM yyyy") & ")"
        mail.IsBodyHtml = True
        mail.Body = email_pib_notif(dtmail)
        client.Send(mail)
        '
        Report.Dispose()
        Mem.Dispose()
        Att.Dispose()
        mail.Dispose()
        client.Dispose()

        'log
        Dim query_log As String = "INSERT INTO tb_scheduler_prod_log(id_log_type,`datetime`,log) VALUES('1',NOW(),'Sending PIB Review Notification')"
        execute_non_query(query_log, True, "", "", "", "")
    End Sub

    Function email_pib_notif(ByVal dtbody As DataTable)
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
        <p class='MsoNormal' align='center' style='text-align:center'><a href='http://www.volcom.co.id/' title='Volcom' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.volcom.co.id/&amp;source=gmail&amp;ust=1480121870771000&amp;usg=AFQjCNEjXvEZWgDdR-Wlke7nn0fmc1ZUuA'><span style='text-decoration:none'><img border='0' width='180' id='m_1811720018273078822_x0000_i1025' src='https://d3k81ch9hvuctc.cloudfront.net/company/VFgA3P/images/de2b6f13-9275-426d-ae31-640f3dcfc744.jpeg' alt='Volcom' class='CToWUd'></span></a><u></u><u></u></p>
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
                  <td style='padding:15.0pt 15.0pt 5.0pt 15.0pt' colspan='3'>
                  <div>
                  <p class='MsoNormal' style='line-height:14.25pt'><span style='font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060'>Dear Team, </span><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'><u></u><u></u></span></p>
                  </div>
                  </td>

                 </tr>
                 <tr>
                  <td style='padding:15.0pt 15.0pt 5.0pt 15.0pt' colspan='3'>
                  <div>
                  <p class='MsoNormal' style='line-height:14.25pt'><span style='font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060'>This is your reminder notification for PIB Voluntary Payment, with details below : </span><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'><u></u><u></u></span></p>
                  </div>
                  </td>
                 </tr>
                 <tr>
                  <td style='padding:1.0pt 15.0pt 15.0pt 15.0pt' colspan='3'>
                  <table width='100%' class='m_1811720018273078822MsoNormalTable' border='1' cellspacing='0' cellpadding='5' style='background:white; font-size: 12px; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#000000'>
                  <tr style='background-color:black; font-size: 12px; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#ffffff'>
                    <th>ISB Number</th>
                    <th>PIB Number</th>
                    <th>PIB Date</th>
                    <th>Second Payment Due Date</th>
                    <th>Notes</th>
                  </tr> 
                <!-- row data --> "
        For d As Integer = 0 To dtbody.Rows.Count - 1
            body_temp += "
      <td>" + dtbody.Rows(d)("number").ToString() + "</td>
      <td>" + dtbody.Rows(d)("pib_no").ToString() + "</td>
      <td>" + dtbody.Rows(d)("pib_date").ToString() + "</td>
      <td>" + dtbody.Rows(d)("second_due_date").ToString() + "</td>
      <td>" + dtbody.Rows(d)("notif_notes").ToString() + "</td>
      </tr>"
        Next
        body_temp += "</table>
                  </td>
                 </tr>
                  <tr>
                    <td style='padding:15.0pt 15.0pt 15.0pt 15.0pt' colspan='3'>
                    <div>
                    <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Please check attachment for more details.<br /><b> </b><u></u><u></u></span></p>

                    </div>
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
            <span style='text-align:center;font-size:7.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#a0a0a0;letter-spacing:.4pt;'>This email send directly from Volcom ERP. Do not reply.</b><u></u><u></u></span>
          <p class='MsoNormal' align='center' style='margin-bottom:12.0pt;text-align:center;padding-top:0px;'><br></p>
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

    Sub send_mail_email_serah_terima_qc()
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

        Dim Report As New ReportReminderSerahTerima()
        Report.LReminderDate.Text = Date.Parse(Now().ToString).ToString("dd MMMM yyyy")

        ' Create a new memory stream and export the report into it as XLS.
        Dim Mem As New MemoryStream()
        Report.ExportToXls(Mem)

        ' Create a new attachment and put the XLS report into it.
        Mem.Seek(0, SeekOrigin.Begin)
        '
        Dim Att = New Attachment(Mem, "Serah Terima QC Reminder (" & Now.ToString("dd MMMM yyyy") & ").xls", "application/ms-excel")
        '
        Dim from_mail As MailAddress = New MailAddress("system@volcom.co.id", "Serah Terima QC Reminder - Volcom ERP")
        Dim mail As MailMessage = New MailMessage()
        mail.From = from_mail
        mail.Attachments.Add(Att)
        'Send to
        Dim query_send_to As String = "SELECT emp.`email_external`,emp.`employee_name` 
            FROM tb_mail_to md
            INNER JOIN tb_m_user usr ON usr.`id_user`=md.id_user
            INNER JOIN tb_m_employee emp ON emp.`id_employee`=usr.`id_employee`
            WHERE is_to='1' AND md.report_mark_type=346 "
        Dim data_send_to As DataTable = execute_query(query_send_to, -1, True, "", "", "", "")
        For i As Integer = 0 To data_send_to.Rows.Count - 1
            If Not data_send_to.Rows(i)("email_external").ToString = "" Then
                Dim to_mail As MailAddress = New MailAddress(data_send_to.Rows(i)("email_external").ToString, data_send_to.Rows(i)("employee_name").ToString)
                mail.To.Add(to_mail)
            End If
        Next
        'CC
        Dim query_send_cc As String = "SELECT emp.`email_external`,emp.`employee_name` 
            FROM tb_mail_to md
            INNER JOIN tb_m_user usr ON usr.`id_user`=md.id_user
            INNER JOIN tb_m_employee emp ON emp.`id_employee`=usr.`id_employee`
            WHERE is_to='2' AND md.report_mark_type=346 "
        Dim data_send_cc As DataTable = execute_query(query_send_cc, -1, True, "", "", "", "")
        For i As Integer = 0 To data_send_cc.Rows.Count - 1
            If Not data_send_cc.Rows(i)("email_external").ToString = "" Then
                Dim cc_mail As MailAddress = New MailAddress(data_send_cc.Rows(i)("email_external").ToString, data_send_cc.Rows(i)("employee_name").ToString)
                mail.CC.Add(cc_mail)
            End If
        Next

        Dim qmail As String = "SELECT pl.`id_pl_prod_order`,po.`prod_order_number`,pl.`pl_prod_order_number`,pl.`pl_prod_order_date`,d.`design_code`,d.`design_display_name`,rec.`id_pl_prod_order_rec`,pl.`id_comp_contact_to`
,pl.`complete_date`,DATEDIFF(DATE(NOW()),DATE(pl.`complete_date`)) AS diff_date_serah_terima
,cat.`pl_category`
FROM `tb_pl_prod_order` pl 
INNER JOIN tb_prod_order po ON po.`id_prod_order`=pl.`id_prod_order`
INNER JOIN tb_prod_demand_design pdd ON pdd.`id_prod_demand_design`=po.`id_prod_demand_design`
INNER JOIN tb_m_design d ON d.`id_design`=pdd.`id_design`
INNER JOIN tb_m_comp_contact cc ON cc.`id_comp_contact`=pl.`id_comp_contact_to`
INNER JOIN tb_m_comp c ON c.`id_comp`=cc.`id_comp` AND c.id_departement='6'
INNER JOIN tb_lookup_pl_category cat ON cat.`id_pl_category`=pl.`id_pl_category`
LEFT JOIN `tb_pl_prod_order_rec` rec ON rec.`id_pl_prod_order`=pl.`id_pl_prod_order` AND rec.`id_report_status`!=5
WHERE pl.`id_report_status`=6 AND ISNULL(rec.`id_pl_prod_order_rec`)
AND DATEDIFF(DATE(NOW()),DATE(pl.`complete_date`))>18"
        Dim dtmail As DataTable = execute_query(qmail, -1, True, "", "", "", "")

        mail.Subject = "Serah Terima QC Reminder (" & Now.ToString("dd MMMM yyyy") & ")"
        mail.IsBodyHtml = True
        mail.Body = email_serah_terima_qc(dtmail.Rows.Count.ToString)
        client.Send(mail)

        Report.Dispose()
        Mem.Dispose()
        Att.Dispose()
        mail.Dispose()
        client.Dispose()

        'log
        Dim query_log As String = "INSERT INTO tb_scheduler_prod_log(id_log_type,`datetime`,log) VALUES('1',NOW(),'Sending Serah Terima QC Reminder')"
        execute_non_query(query_log, True, "", "", "", "")
    End Sub

    Function email_serah_terima_qc(ByVal jml_expired As String)
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
                          <p class='MsoNormal' style='line-height:14.25pt'><b><span style='font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060'>Dear Team,</span></b><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'><u></u><u></u></span></p>
                          <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>This is daily reminder for Receiving Packing List Serah Terima QC.
                          "
        If Not jml_expired.ToString = "0" Then
            body_temp += "<br/> - " & jml_expired & " packing list(s) overdue for receiving (More than 18 days pending)."
        End If
        body_temp += "<br/>Make sure to follow up immediately. Please see attachment for detail.
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
                <p class='MsoNormal' align='center' style='text-align:center'><a href='http://www.volcom.co.id/' title='Volcom' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.volcom.co.id/&amp;source=gmail&amp;ust=1480121870771000&amp;usg=AFQjCNEjXvEZWgDdR-Wlke7nn0fmc1ZUuA'><span style='text-decoration:none'><img border='0' width='180' id='m_1811720018273078822_x0000_i1025' src='" + get_setup_field("mail_volcom_logo") + "' alt='Volcom' class='CToWUd'></span></a><u></u><u></u></p>
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
                <p class='MsoNormal' align='center' style='text-align:center'><a href='http://www.volcom.co.id/' title='Volcom' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.volcom.co.id/&amp;source=gmail&amp;ust=1480121870771000&amp;usg=AFQjCNEjXvEZWgDdR-Wlke7nn0fmc1ZUuA'><span style='text-decoration:none'><img border='0' width='180' id='m_1811720018273078822_x0000_i1025' src='" + get_setup_field("mail_volcom_logo") + "' alt='Volcom' class='CToWUd'></span></a><u></u><u></u></p>
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

    Sub send_email_eos()
        Dim qry As String = "SELECT p.id_pp_change, p.number, 
        DATE_FORMAT(p.effective_date,'%d-%m-%Y') AS `start_date`, 
        DATE_FORMAT(p.plan_end_date,'%d-%m-%Y') AS `end_date`, 
        p.note, datediff(DATE(NOW()), p.plan_end_date) AS `count_day`, o.eos_body_mail1, o.eos_body_mail2
        FROM tb_pp_change p 
        JOIN tb_opt o
        WHERE p.id_report_status=6 AND p.id_design_mkd=1 AND p.plan_end_date>=NOW()
        GROUP BY p.id_pp_change
        HAVING count_day IN (" + comment_by + ") "
        Dim deos As DataTable = execute_query(qry, -1, True, "", "", "", "")
        If deos.Rows.Count > 0 Then
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

            Dim from_mail As MailAddress = New MailAddress("system@volcom.co.id", "EOS Notifications (" & Now.ToString("dd MMMM yyyy") & ") - Volcom ERP")
            Dim mail As MailMessage = New MailMessage()
            mail.From = from_mail

            'Send to
            Dim query_send_to As String = "SELECT md.id_user,emp.`email_external`,emp.`employee_name`, IFNULL(external_recipient,'-') AS `external_recipient` 
            FROM tb_mail_to md
            LEFT JOIN tb_m_user usr ON usr.`id_user`=md.id_user
            LEFT JOIN tb_m_employee emp ON emp.`id_employee`=usr.`id_employee`
            WHERE is_to='1' AND md.report_mark_type=372 "
            Dim data_send_to As DataTable = execute_query(query_send_to, -1, True, "", "", "", "")
            For i As Integer = 0 To data_send_to.Rows.Count - 1
                If Not data_send_to.Rows(i)("id_user").ToString = "0" Then
                    'internal
                    If Not data_send_to.Rows(i)("email_external").ToString = "" Then
                        Dim to_mail As MailAddress = New MailAddress(data_send_to.Rows(i)("email_external").ToString, data_send_to.Rows(i)("employee_name").ToString)
                        mail.To.Add(to_mail)
                    End If
                Else
                    'external
                    Dim ext_to As String() = Split(data_send_to.Rows(i)("external_recipient").ToString, ";")
                    Dim ext_to_title As String = ext_to(0)
                    Dim ext_to_mail As String = ext_to(1)
                    Dim to_mail As MailAddress = New MailAddress(ext_to_mail, ext_to_title)
                    mail.To.Add(to_mail)
                End If
            Next
            'CC
            Dim query_send_cc As String = "SELECT md.id_user,emp.`email_external`,emp.`employee_name`, IFNULL(external_recipient,'-') AS `external_recipient` 
            FROM tb_mail_to md
            LEFT JOIN tb_m_user usr ON usr.`id_user`=md.id_user
            LEFT JOIN tb_m_employee emp ON emp.`id_employee`=usr.`id_employee`
            WHERE is_to='2' AND md.report_mark_type=372 "
            Dim data_send_cc As DataTable = execute_query(query_send_cc, -1, True, "", "", "", "")
            For i As Integer = 0 To data_send_cc.Rows.Count - 1
                If Not data_send_cc.Rows(i)("id_user").ToString = "0" Then
                    'internal
                    If Not data_send_cc.Rows(i)("email_external").ToString = "" Then
                        Dim cc_mail As MailAddress = New MailAddress(data_send_cc.Rows(i)("email_external").ToString, data_send_cc.Rows(i)("employee_name").ToString)
                        mail.CC.Add(cc_mail)
                    End If
                Else
                    'external
                    Dim ext_cc As String() = Split(data_send_cc.Rows(i)("external_recipient").ToString, ";")
                    Dim ext_cc_title As String = ext_cc(0)
                    Dim ext_cc_mail As String = ext_cc(1)
                    Dim cc_mail As MailAddress = New MailAddress(ext_cc_mail, ext_cc_title)
                    mail.CC.Add(cc_mail)
                End If
            Next

            mail.Subject = "EOS Notification (" & Now.ToString("dd MMMM yyyy") & ")"
            mail.IsBodyHtml = True
            mail.Body = email_eos_notif(deos)
            client.Send(mail)
            '

            mail.Dispose()
            client.Dispose()

            'log
            Dim query_log As String = "INSERT INTO tb_eos_reminder_log(`log_date`,`log_note`) VALUES(NOW(),'Success')"
            execute_non_query(query_log, True, "", "", "", "")
        End If
    End Sub

    Function email_eos_notif(ByVal dt_par As DataTable)
        Dim body As String = "<table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100.0%;background:#eeeeee'>
    <tbody><tr>
      <td style='padding:30.0pt 30.0pt 30.0pt 30.0pt'>
      <div align='center'>

      <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='600' style='width:6.25in;background:white'>
       <tbody><tr>
        <td style='padding:0in 0in 0in 0in'></td>
       </tr>
       <tr>
        <td style='padding:0in 0in 0in 0in'>
        <p class='MsoNormal' align='center' style='text-align:center'><a href='http://www.volcom.co.id/' title='Volcom' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.volcom.co.id/&amp;source=gmail&amp;ust=1480121870771000&amp;usg=AFQjCNEjXvEZWgDdR-Wlke7nn0fmc1ZUuA'><span style='text-decoration:none'><img border='0' width='180' id='m_1811720018273078822_x0000_i1025' src='https://d3k81ch9hvuctc.cloudfront.net/company/VFgA3P/images/de2b6f13-9275-426d-ae31-640f3dcfc744.jpeg' alt='Volcom' class='CToWUd'></span></a><u></u><u></u></p>
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
            <td style='padding:15.0pt 15.0pt 5.0pt 15.0pt' colspan='3'>
                <p style='font-size:10.0pt; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060; border-spacing:0 7px;'>Dear Team,</p>
                <p style='margin-bottom:5pt; line-height:20.25pt; font-size:10.0pt; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060; border-spacing:0 7px;'>" + dt_par.Rows(0)("eos_body_mail1").ToString + "</p>
            
             </td>
         </tr>

        
        <tr>
          <td style='padding:1.0pt 15.0pt 15.0pt 15.0pt' colspan='3'>
            <table width='80%' class='m_1811720018273078822MsoNormalTable' border='1' cellspacing='0' cellpadding='5' style='background:white; font-size: 12px; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#000000'>
            <tr style='background-color:black; font-size: 12px; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#ffffff'>
              <th>No.</th>
              <th>Tanggal Mulai</th>
              <th>Tanggal Berakhir</th>
              <th>Remark</th>
            </tr> 
          <!-- row data --> "
        For i As Integer = 0 To dt_par.Rows.Count - 1
            body += "<tr>
              <td>" + dt_par.Rows(0)("number").ToString + "</td>
              <td>" + dt_par.Rows(0)("start_date").ToString + "</td>
              <td>" + dt_par.Rows(0)("end_date").ToString + "</td>
              <td>H" + dt_par.Rows(0)("count_day").ToString + "</td>
            </tr> "
        Next
        body += "</table>
          </td>

         </tr>

         <tr>
            <td style='padding:5.0pt 15.0pt 5.0pt 15.0pt' colspan='3'>
                <p style='line-height:20.25pt;font-size:10.0pt; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060; border-spacing:0 7px;'>" + dt_par.Rows(0)("eos_body_mail2").ToString + "
             </td>
         </tr>


  <tr>
          <td style='padding:15.0pt 15.0pt 15.0pt 15.0pt' colspan='3'>
          <div>
          <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Terima kasih, <br /><b>Volcom ERP</b><u></u><u></u></span></p>

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
          <p class='MsoNormal' align='center' style='margin-bottom:12.0pt;text-align:center;padding-top:0px;'><br></p>
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
        Return body
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

    Sub send_email_price_eos()
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

        'send email turun harga
        Dim query As String = "SELECT CONCAT('VOLCOM INDONESIA - PRICE LIST VOLCOM EOSS ', UPPER(DATE_FORMAT(pp.effective_date,'%M %Y')),' - ', cg.description) AS `subject`,
        DATE_FORMAT(pp.effective_date,'%d %M %Y') AS `start_date`, cg.description, o.price_eos_body_mail1
        FROM tb_pp_change pp 
        JOIN tb_m_comp_group cg ON cg.id_comp_group=" + par1 + "
        JOIN tb_opt o 
        WHERE pp.id_pp_change=" + id_report + " "
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")
        Dim subj As String = data.Rows(0)("subject").ToString
        Dim start_date As String = data.Rows(0)("start_date").ToString
        Dim store As String = data.Rows(0)("description").ToString
        Dim body_mail As String = data.Rows(0)("price_eos_body_mail1").ToString.Replace("#start_date#", start_date)

        Dim from_mail As MailAddress = New MailAddress("system@volcom.co.id", subj)
        Dim mail As MailMessage = New MailMessage()
        mail.From = from_mail

        'attac
        ReportMkdEOS.id = id_report
        ReportMkdEOS.id_store = par1
        Dim Report As New ReportMkdEOS()
        ' Create a new memory stream and export the report into it as XLS.
        Dim Mem As New MemoryStream()
        Dim opt As DevExpress.XtraPrinting.XlsExportOptions = New DevExpress.XtraPrinting.XlsExportOptions()
        opt.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Value
        Report.ExportToXls(Mem, opt)
        ' Create a new attachment and put the XLS report into it.
        Mem.Seek(0, SeekOrigin.Begin)
        Dim Att = New Attachment(Mem, subj + ".xls", "application/ms-excel")
        mail.Attachments.Add(Att)

        'Send to
        Dim query_send_mail As String = "SELECT IF(md.id_user=0,SUBSTRING_INDEX(external_recipient,';',-1),emp.`email_external`) AS email_external, IF(md.id_user=0,SUBSTRING_INDEX(external_recipient,';',1),emp.`employee_name`) AS employee_name
            FROM tb_mail_to md
            LEFT JOIN tb_m_user usr ON usr.`id_user`=md.id_user
            LEFT JOIN tb_m_employee emp ON emp.`id_employee`=usr.`id_employee`
            WHERE md.report_mark_type='" + report_mark_type + "' AND is_to='1' AND IF(ISNULL(md.id_user),TRUE,IF(IFNULL(emp.id_employee_active,1)=1,TRUE,FALSE))
            UNION ALL
            SELECT IF(ISNULL(emp.employee_name),md.email,emp.email_external) AS email_external, IF(ISNULL(emp.employee_name),md.name,emp.employee_name) AS employee_name
            FROM tb_mail_to_group md
            LEFT JOIN tb_m_employee emp ON emp.id_employee=md.id_employee
            WHERE md.report_mark_type='" + report_mark_type + "' AND id_comp_group='" + par1 + "' AND is_to='1' AND IF(ISNULL(md.id_employee),TRUE,IF(IFNULL(emp.id_employee_active,1)=1,TRUE,FALSE))"
        Dim data_send_mail As DataTable = execute_query(query_send_mail, -1, True, "", "", "", "")
        For i As Integer = 0 To data_send_mail.Rows.Count - 1
            Dim to_mail As MailAddress = New MailAddress(data_send_mail.Rows(i)("email_external").ToString, data_send_mail.Rows(i)("employee_name").ToString)
            mail.To.Add(to_mail)
        Next

        'Send CC
        Dim query_send_cc As String = "SELECT IF(md.id_user=0,SUBSTRING_INDEX(external_recipient,';',-1),emp.`email_external`) AS email_external, IF(md.id_user=0,SUBSTRING_INDEX(external_recipient,';',1),emp.`employee_name`) AS employee_name
            FROM tb_mail_to md
            LEFT JOIN tb_m_user usr ON usr.`id_user`=md.id_user
            LEFT JOIN tb_m_employee emp ON emp.`id_employee`=usr.`id_employee`
            WHERE md.report_mark_type='" + report_mark_type + "' AND is_to='2' AND IF(ISNULL(md.id_user),TRUE,IF(IFNULL(emp.id_employee_active,1)=1,TRUE,FALSE))
            UNION ALL
            SELECT IF(ISNULL(emp.employee_name),md.email,emp.email_external) AS email_external, IF(ISNULL(emp.employee_name),md.name,emp.employee_name) AS employee_name
            FROM tb_mail_to_group md
            LEFT JOIN tb_m_employee emp ON emp.id_employee=md.id_employee
            WHERE md.report_mark_type='" + report_mark_type + "' AND id_comp_group='" + par1 + "' AND is_to='2' AND IF(ISNULL(md.id_employee),TRUE,IF(IFNULL(emp.id_employee_active,1)=1,TRUE,FALSE)) "
        Dim data_send_cc As DataTable = execute_query(query_send_cc, -1, True, "", "", "", "")
        For i As Integer = 0 To data_send_cc.Rows.Count - 1
            Dim to_mail As MailAddress = New MailAddress(data_send_cc.Rows(i)("email_external").ToString, data_send_cc.Rows(i)("employee_name").ToString)
            mail.CC.Add(to_mail)
        Next

        mail.Subject = subj
        mail.IsBodyHtml = True

        mail.Body = "<table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100.0%;background:#eeeeee'>
    <tbody><tr>
      <td style='padding:30.0pt 30.0pt 30.0pt 30.0pt'>
      <div align='center'>

      <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='600' style='width:6.25in;background:white'>
       <tbody><tr>
        <td style='padding:0in 0in 0in 0in'></td>
       </tr>
       <tr>
        <td style='padding:0in 0in 0in 0in'>
        <p class='MsoNormal' align='center' style='text-align:center'><a href='http://www.volcom.co.id/' title='Volcom' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.volcom.co.id/&amp;source=gmail&amp;ust=1480121870771000&amp;usg=AFQjCNEjXvEZWgDdR-Wlke7nn0fmc1ZUuA'><span style='text-decoration:none'><img border='0' width='180' id='m_1811720018273078822_x0000_i1025' src='https://d3k81ch9hvuctc.cloudfront.net/company/VFgA3P/images/de2b6f13-9275-426d-ae31-640f3dcfc744.jpeg' alt='Volcom' class='CToWUd'></span></a><u></u><u></u></p>
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
            <td style='padding:15.0pt 15.0pt 5.0pt 15.0pt' colspan='3'>
                <p style='font-size:10.0pt; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060; border-spacing:0 7px;'>Kepada Yth, Toko " + store + "</p>
                <p style='margin-bottom:5pt; line-height:20.25pt; font-size:10.0pt; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060; border-spacing:0 7px;'>" + body_mail + "</p>
            
             </td>
         </tr>

  <tr>
          <td style='padding:15.0pt 15.0pt 15.0pt 15.0pt' colspan='3'>
          <div>
          <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Terima kasih, <br /><b>Volcom ERP</b><u></u><u></u></span></p>

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
          <p class='MsoNormal' align='center' style='margin-bottom:12.0pt;text-align:center;padding-top:0px;'><br></p>
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
        client.Send(mail)
    End Sub

    Sub send_email_bsp()
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

        'send email turun harga
        Dim query As String = "SELECT CONCAT('VOLCOM INDONESIA - PRICE LIST VOLCOM BIG SALE ', UPPER(DATE_FORMAT(pp.start_date,'%M %Y')),' - ', c.comp_name) AS `subject`,
        DATE_FORMAT(pp.start_date,'%d %M %Y') AS `start_date`, DATE_FORMAT(pp.end_date,'%d %M %Y') AS `end_date`, c.comp_name, o.bsp_body_mail1,
        cg.description AS `store_group`
        FROM tb_bsp pp 
        INNER JOIN tb_m_comp c ON c.id_comp= pp.id_comp
        INNER JOIN tb_m_comp_group cg ON cg.id_comp_group = c.id_comp_group
        JOIN tb_opt o 
        WHERE pp.id_bsp=" + id_report + " "
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")
        Dim subj As String = data.Rows(0)("subject").ToString
        Dim start_date As String = data.Rows(0)("start_date").ToString
        Dim end_date As String = data.Rows(0)("end_date").ToString
        Dim store As String = data.Rows(0)("comp_name").ToString
        Dim store_group As String = data.Rows(0)("store_group").ToString
        Dim body_mail As String = data.Rows(0)("bsp_body_mail1").ToString.Replace("#start_date#", start_date).Replace("#end_date#", end_date).Replace("#store#", store)

        Dim from_mail As MailAddress = New MailAddress("system@volcom.co.id", subj)
        Dim mail As MailMessage = New MailMessage()
        mail.From = from_mail

        'attach
        ReportMkdBSP.id_store = par2
        Dim Report As New ReportMkdBSP()
        ' Create a new memory stream and export the report into it as XLS.
        Dim Mem As New MemoryStream()
        Dim opt As DevExpress.XtraPrinting.XlsExportOptions = New DevExpress.XtraPrinting.XlsExportOptions()
        opt.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Value
        Report.ExportToXls(Mem, opt)
        ' Create a new attachment and put the XLS report into it.
        Mem.Seek(0, SeekOrigin.Begin)
        Dim Att = New Attachment(Mem, subj + ".xls", "application/ms-excel")
        mail.Attachments.Add(Att)

        'Send to
        Dim query_send_mail As String = "SELECT IF(md.id_user=0,SUBSTRING_INDEX(external_recipient,';',-1),emp.`email_external`) AS email_external, IF(md.id_user=0,SUBSTRING_INDEX(external_recipient,';',1),emp.`employee_name`) AS employee_name
            FROM tb_mail_to md
            LEFT JOIN tb_m_user usr ON usr.`id_user`=md.id_user
            LEFT JOIN tb_m_employee emp ON emp.`id_employee`=usr.`id_employee`
            WHERE md.report_mark_type='" + report_mark_type + "' AND is_to='1' AND IF(ISNULL(md.id_user),TRUE,IF(IFNULL(emp.id_employee_active,1)=1,TRUE,FALSE))
            UNION ALL
            SELECT IF(ISNULL(emp.employee_name),md.email,emp.email_external) AS email_external, IF(ISNULL(emp.employee_name),md.name,emp.employee_name) AS employee_name
            FROM tb_mail_to_group md
            LEFT JOIN tb_m_employee emp ON emp.id_employee=md.id_employee
            WHERE md.report_mark_type='" + report_mark_type + "' AND id_comp_group='" + par1 + "' AND is_to='1' AND IF(ISNULL(md.id_employee),TRUE,IF(IFNULL(emp.id_employee_active,1)=1,TRUE,FALSE))"
        Dim data_send_mail As DataTable = execute_query(query_send_mail, -1, True, "", "", "", "")
        For i As Integer = 0 To data_send_mail.Rows.Count - 1
            Dim to_mail As MailAddress = New MailAddress(data_send_mail.Rows(i)("email_external").ToString, data_send_mail.Rows(i)("employee_name").ToString)
            mail.To.Add(to_mail)
        Next

        'Send CC
        Dim query_send_cc As String = "SELECT IF(md.id_user=0,SUBSTRING_INDEX(external_recipient,';',-1),emp.`email_external`) AS email_external, IF(md.id_user=0,SUBSTRING_INDEX(external_recipient,';',1),emp.`employee_name`) AS employee_name
            FROM tb_mail_to md
            LEFT JOIN tb_m_user usr ON usr.`id_user`=md.id_user
            LEFT JOIN tb_m_employee emp ON emp.`id_employee`=usr.`id_employee`
            WHERE md.report_mark_type='" + report_mark_type + "' AND is_to='2' AND IF(ISNULL(md.id_user),TRUE,IF(IFNULL(emp.id_employee_active,1)=1,TRUE,FALSE))
            UNION ALL
            SELECT IF(ISNULL(emp.employee_name),md.email,emp.email_external) AS email_external, IF(ISNULL(emp.employee_name),md.name,emp.employee_name) AS employee_name
            FROM tb_mail_to_group md
            LEFT JOIN tb_m_employee emp ON emp.id_employee=md.id_employee
            WHERE md.report_mark_type='" + report_mark_type + "' AND id_comp_group='" + par1 + "' AND is_to='2' AND IF(ISNULL(md.id_employee),TRUE,IF(IFNULL(emp.id_employee_active,1)=1,TRUE,FALSE)) "
        Dim data_send_cc As DataTable = execute_query(query_send_cc, -1, True, "", "", "", "")
        For i As Integer = 0 To data_send_cc.Rows.Count - 1
            Dim to_mail As MailAddress = New MailAddress(data_send_cc.Rows(i)("email_external").ToString, data_send_cc.Rows(i)("employee_name").ToString)
            mail.CC.Add(to_mail)
        Next

        mail.Subject = subj
        mail.IsBodyHtml = True

        mail.Body = "<table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100.0%;background:#eeeeee'>
    <tbody><tr>
      <td style='padding:30.0pt 30.0pt 30.0pt 30.0pt'>
      <div align='center'>

      <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='600' style='width:6.25in;background:white'>
       <tbody><tr>
        <td style='padding:0in 0in 0in 0in'></td>
       </tr>
       <tr>
        <td style='padding:0in 0in 0in 0in'>
        <p class='MsoNormal' align='center' style='text-align:center'><a href='http://www.volcom.co.id/' title='Volcom' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.volcom.co.id/&amp;source=gmail&amp;ust=1480121870771000&amp;usg=AFQjCNEjXvEZWgDdR-Wlke7nn0fmc1ZUuA'><span style='text-decoration:none'><img border='0' width='180' id='m_1811720018273078822_x0000_i1025' src='https://d3k81ch9hvuctc.cloudfront.net/company/VFgA3P/images/de2b6f13-9275-426d-ae31-640f3dcfc744.jpeg' alt='Volcom' class='CToWUd'></span></a><u></u><u></u></p>
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
            <td style='padding:15.0pt 15.0pt 5.0pt 15.0pt' colspan='3'>
                <p style='font-size:10.0pt; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060; border-spacing:0 7px;'>Kepada Yth, Toko " + store_group + "</p>
                <p style='margin-bottom:5pt; line-height:20.25pt; font-size:10.0pt; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060; border-spacing:0 7px;'>" + body_mail + "</p>
            
             </td>
         </tr>

  <tr>
          <td style='padding:15.0pt 15.0pt 15.0pt 15.0pt' colspan='3'>
          <div>
          <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Terima kasih, <br /><b>Volcom ERP</b><u></u><u></u></span></p>

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
          <p class='MsoNormal' align='center' style='margin-bottom:12.0pt;text-align:center;padding-top:0px;'><br></p>
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
        client.Send(mail)
    End Sub

    Sub send_email_line_list()
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

        'send email turun harga
        Dim query As String = "SELECT o.line_list_subject_mail, o.line_list_body_mail1, o.line_list_body_mail2,
        DATE_FORMAT(DATE(NOW()),'%d %M %Y') AS tgl_notif
        FROM tb_opt o "
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")
        Dim subject_mail As String = data.Rows(0)("line_list_subject_mail").ToString
        Dim tgl_notif As String = data.Rows(0)("tgl_notif").ToString
        Dim body_mail1 As String = data.Rows(0)("line_list_body_mail1").ToString.Replace("#tgl_notif#", tgl_notif)
        Dim body_mail2 As String = data.Rows(0)("line_list_body_mail2").ToString

        Dim from_mail As MailAddress = New MailAddress("system@volcom.co.id", subject_mail + " - Volcom ERP")
        Dim mail As MailMessage = New MailMessage()
        mail.From = from_mail

        'Send to
        Dim query_send_mail As String = "SELECT IF(md.id_user=0,SUBSTRING_INDEX(external_recipient,';',-1),emp.`email_external`) AS email_external, IF(md.id_user=0,SUBSTRING_INDEX(external_recipient,';',1),emp.`employee_name`) AS employee_name
            FROM tb_mail_to md
            LEFT JOIN tb_m_user usr ON usr.`id_user`=md.id_user
            LEFT JOIN tb_m_employee emp ON emp.`id_employee`=usr.`id_employee`
            WHERE md.report_mark_type='" + report_mark_type + "' AND is_to='1' AND IF(ISNULL(md.id_user),TRUE,IF(IFNULL(emp.id_employee_active,1)=1,TRUE,FALSE))
            UNION ALL
            SELECT IF(ISNULL(emp.employee_name),md.email,emp.email_external) AS email_external, IF(ISNULL(emp.employee_name),md.name,emp.employee_name) AS employee_name
            FROM tb_mail_to_group md
            LEFT JOIN tb_m_employee emp ON emp.id_employee=md.id_employee
            WHERE md.report_mark_type='" + report_mark_type + "' AND id_comp_group='" + par1 + "' AND is_to='1' AND IF(ISNULL(md.id_employee),TRUE,IF(IFNULL(emp.id_employee_active,1)=1,TRUE,FALSE))"
        Dim data_send_mail As DataTable = execute_query(query_send_mail, -1, True, "", "", "", "")
        For i As Integer = 0 To data_send_mail.Rows.Count - 1
            Dim to_mail As MailAddress = New MailAddress(data_send_mail.Rows(i)("email_external").ToString, data_send_mail.Rows(i)("employee_name").ToString)
            mail.To.Add(to_mail)
        Next

        'Send CC
        Dim query_send_cc As String = "SELECT IF(md.id_user=0,SUBSTRING_INDEX(external_recipient,';',-1),emp.`email_external`) AS email_external, IF(md.id_user=0,SUBSTRING_INDEX(external_recipient,';',1),emp.`employee_name`) AS employee_name
            FROM tb_mail_to md
            LEFT JOIN tb_m_user usr ON usr.`id_user`=md.id_user
            LEFT JOIN tb_m_employee emp ON emp.`id_employee`=usr.`id_employee`
            WHERE md.report_mark_type='" + report_mark_type + "' AND is_to='2' AND IF(ISNULL(md.id_user),TRUE,IF(IFNULL(emp.id_employee_active,1)=1,TRUE,FALSE))
            UNION ALL
            SELECT IF(ISNULL(emp.employee_name),md.email,emp.email_external) AS email_external, IF(ISNULL(emp.employee_name),md.name,emp.employee_name) AS employee_name
            FROM tb_mail_to_group md
            LEFT JOIN tb_m_employee emp ON emp.id_employee=md.id_employee
            WHERE md.report_mark_type='" + report_mark_type + "' AND id_comp_group='" + par1 + "' AND is_to='2' AND IF(ISNULL(md.id_employee),TRUE,IF(IFNULL(emp.id_employee_active,1)=1,TRUE,FALSE)) "
        Dim data_send_cc As DataTable = execute_query(query_send_cc, -1, True, "", "", "", "")
        For i As Integer = 0 To data_send_cc.Rows.Count - 1
            Dim to_mail As MailAddress = New MailAddress(data_send_cc.Rows(i)("email_external").ToString, data_send_cc.Rows(i)("employee_name").ToString)
            mail.CC.Add(to_mail)
        Next

        mail.Subject = subject_mail
        mail.IsBodyHtml = True

        mail.Body = "<table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100.0%;background:#eeeeee'>
    <tbody><tr>
      <td style='padding:30.0pt 30.0pt 30.0pt 30.0pt'>
      <div align='center'>

      <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='600' style='width:6.25in;background:white'>
       <tbody><tr>
        <td style='padding:0in 0in 0in 0in'></td>
       </tr>
       <tr>
        <td style='padding:0in 0in 0in 0in'>
        <p class='MsoNormal' align='center' style='text-align:center'><a href='http://www.volcom.co.id/' title='Volcom' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.volcom.co.id/&amp;source=gmail&amp;ust=1480121870771000&amp;usg=AFQjCNEjXvEZWgDdR-Wlke7nn0fmc1ZUuA'><span style='text-decoration:none'><img border='0' width='180' id='m_1811720018273078822_x0000_i1025' src='https://d3k81ch9hvuctc.cloudfront.net/company/VFgA3P/images/de2b6f13-9275-426d-ae31-640f3dcfc744.jpeg' alt='Volcom' class='CToWUd'></span></a><u></u><u></u></p>
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
            <td style='padding:15.0pt 15.0pt 5.0pt 15.0pt' colspan='3'>
                <p style='font-size:10.0pt; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060; border-spacing:0 7px;'>Dear Team,</p>
                <p style='margin-bottom:5pt; line-height:20.25pt; font-size:10.0pt; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060; border-spacing:0 7px;'>" + body_mail1 + "</p>
            
             </td>
         </tr>

        
        <tr>
          <td style='padding:1.0pt 15.0pt 15.0pt 15.0pt' colspan='3'>
            <table width='100%' class='m_1811720018273078822MsoNormalTable' border='1' cellspacing='0' cellpadding='5' style='background:white; font-size: 12px; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#000000'>
            <tr style='background-color:black; font-size: 12px; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#ffffff'>
              <th>Season</th>
              <th>Updated Transaction</th>
            </tr> "
        For i As Integer = 0 To dt.Rows.Count - 1
            mail.Body += "<tr>
              <td>" + dt.Rows(i)("season").ToString + "</td>
              <td>" + dt.Rows(i)("updated_trans").ToString + "</td>
            </tr> "
        Next
        mail.Body += "

          <!-- row data -->

          </table>
          </td>

         </tr>

         <tr>
            <td style='padding:5.0pt 15.0pt 5.0pt 15.0pt' colspan='3'>
                <p style='line-height:20.25pt;font-size:10.0pt; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060; border-spacing:0 7px;'>" + body_mail2 + "</b>
             </td>
         </tr>


  <tr>
          <td style='padding:15.0pt 15.0pt 15.0pt 15.0pt' colspan='3'>
          <div>
          <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Terima kasih, <br /><b>Volcom ERP</b><u></u><u></u></span></p>

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
          <p class='MsoNormal' align='center' style='margin-bottom:12.0pt;text-align:center;padding-top:0px;'><br></p>
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
        client.Send(mail)
    End Sub
End Class
