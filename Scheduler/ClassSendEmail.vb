﻿Imports System.IO
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
    '
    Sub send_email_html()
        Dim query_opt As String = "SELECT send_weekly_attn,send_stock_leave,send_weekly_attn_headdept,send_stock_leave_headdept,emp.employee_name,emp.email_lokal
                                    FROM tb_opt_scheduler opt 
                                    LEFT JOIN tb_m_employee emp ON emp.id_employee=opt.id_emp_headdept_toreport 
                                    LIMIT 1"
        Dim data_opt As DataTable = execute_query(query_opt, -1, True, "", "", "", "")

        '
        If report_mark_type = "weekly_attn" Then
            If data_opt.Rows(0)("send_weekly_attn").ToString = "1" Then
                'Create a new report. 
                'query dept
                Dim query_dept As String = "SELECT dept.id_departement,dept.departement,emp.id_employee,emp.email_lokal,emp.employee_name FROM tb_m_departement dept
                                            INNER JOIN tb_m_user usr ON dept.id_user_head=usr.id_user
                                            INNER JOIN tb_m_employee emp ON emp.id_employee=usr.id_employee
                                    WHERE is_office_dept='1'"
                Dim data_dept As DataTable = execute_query(query_dept, -1, True, "", "", "", "")
                For i As Integer = 0 To data_dept.Rows.Count - 1
                    Dim ix As Integer = i
                    send_mail_weekly_attn(data_dept.Rows(ix)("id_departement").ToString, data_dept.Rows(ix)("departement").ToString, data_dept.Rows(ix)("employee_name").ToString, data_dept.Rows(ix)("email_lokal").ToString)
                Next
                'query dept kk unit
                Dim query_dept_kkunit As String = "SELECT dept.id_departement,dept.departement,emp.id_employee,emp.email_lokal,emp.employee_name FROM tb_m_departement dept
                                            INNER JOIN tb_m_user usr ON dept.id_user_head=usr.id_user
                                            INNER JOIN tb_m_employee emp ON emp.id_employee=usr.id_employee
                                    WHERE is_office_dept='2' AND is_kk_unit='1'"
                Dim data_dept_kkunit As DataTable = execute_query(query_dept_kkunit, -1, True, "", "", "", "")
                For i As Integer = 0 To data_dept.Rows.Count - 1
                    Dim ix As Integer = i
                    send_mail_weekly_attn(data_dept_kkunit.Rows(ix)("id_departement").ToString, data_dept_kkunit.Rows(ix)("departement").ToString, data_opt.Rows(0)("employee_name").ToString, data_opt.Rows(0)("email_lokal").ToString)
                Next
            End If
        ElseIf report_mark_type = "weekly_attn_head" Then
            If data_opt.Rows(0)("send_weekly_attn_headdept").ToString = "1" Then
                send_mail_weekly_attn_head(data_opt.Rows(0)("employee_name").ToString, data_opt.Rows(0)("email_lokal").ToString)

                'Dim sender_thread = New Thread(Sub() send_mail_weekly_attn_head(data_opt.Rows(0)("employee_name").ToString, data_opt.Rows(0)("email_lokal").ToString))
                'sender_thread.Start()
            End If
        ElseIf report_mark_type = "monthly_leave_remaining" Then
            If data_opt.Rows(0)("send_stock_leave").ToString = "1" Then
                ' Create a new report. 
                'query dept
                Dim query_dept As String = "SELECT dept.id_departement,dept.departement,emp.id_employee,emp.email_lokal,emp.employee_name FROM tb_m_departement dept
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
                    Dim mail As MailMessage = New MailMessage("system@volcom.mail", data_dept.Rows(i)("email_lokal").ToString)
                    'Dim mail As MailMessage = New MailMessage("system@volcom.mail", "septian@volcom.mail")
                    mail.Attachments.Add(Att)
                    Dim client As SmtpClient = New SmtpClient()
                    client.Port = 25
                    client.DeliveryMethod = SmtpDeliveryMethod.Network
                    client.UseDefaultCredentials = False
                    client.Host = "192.168.1.4"
                    client.Credentials = New System.Net.NetworkCredential("system@volcom.mail", "system123")
                    mail.Subject = "Monthly Remaining Leave Report (" & data_dept.Rows(i)("departement").ToString & ")"
                    mail.IsBodyHtml = True
                    mail.Body = email_temp_monthly(data_dept.Rows(i)("employee_name").ToString, False)
                    client.Send(mail)
                    'log
                    Dim query_log As String = "INSERT INTo tb_scheduler_attn_log(id_log_type,`datetime`,log) VALUES('3',NOW(),'Sending Monthly Remaining Leave Report (" & data_dept.Rows(i)("departement").ToString & ") to " & data_dept.Rows(i)("email_lokal").ToString & "')"
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
                Dim mail As MailMessage = New MailMessage("system@volcom.mail", data_opt.Rows(0)("email_lokal").ToString)
                'Dim mail As MailMessage = New MailMessage("system@volcom.mail", "septian@volcom.mail")
                mail.Attachments.Add(Att)
                Dim client As SmtpClient = New SmtpClient()
                client.Port = 25
                client.DeliveryMethod = SmtpDeliveryMethod.Network
                client.UseDefaultCredentials = False
                client.Host = "192.168.1.4"
                client.Credentials = New System.Net.NetworkCredential("system@volcom.mail", "system123")
                mail.Subject = "Monthly Remaining Leave Report (Department Head)"
                mail.IsBodyHtml = True
                mail.Body = email_temp_monthly(data_opt.Rows(0)("employee_name").ToString, True)
                client.Send(mail)
                'log
                Dim query_log As String = "INSERT INTo tb_scheduler_attn_log(id_log_type,`datetime`,log) VALUES('3',NOW(),'Sending Monthly Remaining Leave Report (Department Head) to " & data_opt.Rows(0)("employee_name").ToString & "')"
                execute_non_query(query_log, True, "", "", "", "")
            End If
        End If
    End Sub
    Sub send_mail_weekly_attn(ByVal id_dept As String, ByVal dept As String, ByVal dept_head As String, ByVal dept_head_email As String)
        ReportEmpAttn.id_dept = id_dept
        ReportEmpAttn.is_head_dept = "-1"
        Dim Report As New ReportEmpAttn()

        ' Create a new memory stream and export the report into it as PDF.
        Dim Mem As New MemoryStream()
        Report.ExportToPdf(Mem)

        ' Create a new attachment and put the PDF report into it.
        Mem.Seek(0, SeekOrigin.Begin)
        '
        Dim Att = New Attachment(Mem, "Weekly Attendance Report - " & dept & ".pdf", "application/pdf")
        '
        Dim mail As MailMessage = New MailMessage("system@volcom.mail", dept_head_email)
        'Dim mail As MailMessage = New MailMessage("system@volcom.mail", "septian@volcom.mail")
        mail.Attachments.Add(Att)
        Dim client As SmtpClient = New SmtpClient()
        client.Port = 25
        client.DeliveryMethod = SmtpDeliveryMethod.Network
        client.UseDefaultCredentials = False
        client.Host = "192.168.1.4"
        client.Credentials = New System.Net.NetworkCredential("system@volcom.mail", "system123")
        mail.Subject = "Weekly Attendance Report (" & dept & ")"
        mail.IsBodyHtml = True
        mail.Body = email_temp(dept_head, False)
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
    Sub send_mail_weekly_attn_head(ByVal emp_name As String, ByVal emp_email As String)
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
        Dim mail As MailMessage = New MailMessage("system@volcom.mail", emp_email)
        'Dim mail As MailMessage = New MailMessage("system@volcom.mail", "septian@volcom.mail")
        mail.Attachments.Add(Att)
        Dim client As SmtpClient = New SmtpClient()
        client.Port = 25
        client.DeliveryMethod = SmtpDeliveryMethod.Network
        client.UseDefaultCredentials = False
        client.Host = "192.168.1.4"
        client.Credentials = New System.Net.NetworkCredential("system@volcom.mail", "system123")
        mail.Subject = "Weekly Attendance Report (Department Head)"
        mail.IsBodyHtml = True
        mail.Body = email_temp(emp_name, True)
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
                          <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Here's your weekly attendance report for " & dep & ". Please see attachment.
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
        Dim Report As New ReportDuty()

        ' Create a new memory stream and export the report into it as XLS.
        Dim Mem As New MemoryStream()
        Report.ExportToXls(Mem)

        ' Create a new attachment and put the XLS report into it.
        Mem.Seek(0, SeekOrigin.Begin)
        '
        Dim Att = New Attachment(Mem, "Duty List (" & Now.ToString("dd MMMM yyyy") & ").xls", "application/ms-excel")
        '
        Dim query_email As String = "SELECT opt.id_emp_duty_toreport,emp.employee_name,emp.email_lokal FROM tb_opt_scheduler opt INNER JOIN tb_m_employee emp ON emp.id_employee=opt.id_emp_duty_toreport"
        Dim data_mail As DataTable = execute_query(query_email, -1, True, "", "", "", "")
        '
        Dim mail As MailMessage = New MailMessage("system@volcom.mail", data_mail.Rows(0)("email_lokal").ToString)
        'Dim mail As MailMessage = New MailMessage("system@volcom.mail", "septian@volcom.mail")
        mail.Attachments.Add(Att)
        Dim client As SmtpClient = New SmtpClient()
        client.Port = 25
        client.DeliveryMethod = SmtpDeliveryMethod.Network
        client.UseDefaultCredentials = False
        client.Host = "192.168.1.4"
        client.Credentials = New System.Net.NetworkCredential("system@volcom.mail", "system123")
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
End Class
