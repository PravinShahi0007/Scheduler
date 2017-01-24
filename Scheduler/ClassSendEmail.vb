Imports System.IO
Imports System.Net.Mail

Public Class ClassSendEmail
    Public id_report As String = "-1"
    Public report_mark_type As String = "-1"

    Sub send_email_html()
        Dim query_opt As String = "SELECT send_weekly_attn,send_stock_leave,send_weekly_attn_headdept,send_stock_leave_headdept,emp.employee_name,emp.email_lokal
                                    FROM tb_opt_scheduler opt 
                                    LEFT JOIN tb_m_employee emp ON emp.id_employee=opt.id_emp_headdept_toreport 
                                    LIMIT 1"
        Dim data_opt As DataTable = execute_query(query_opt, -1, True, "", "", "", "")
        'query dept
        Dim query_dept As String = "SELECT dept.id_departement,dept.departement,emp.id_employee,emp.email_lokal,emp.employee_name FROM tb_m_departement dept
                                            INNER JOIN tb_m_user usr ON dept.id_user_head=usr.id_user
                                            INNER JOIN tb_m_employee emp ON emp.id_employee=usr.id_employee
                                    WHERE is_office_dept='1'"
        '
        If report_mark_type = "weekly_attn" Then
            If data_opt.Rows(0)("send_weekly_attn").ToString = "1" Then
                'Create a new report. 
                Dim data_dept As DataTable = execute_query(query_dept, -1, True, "", "", "", "")
                For i As Integer = 0 To data_dept.Rows.Count - 1
                    ReportEmpAttn.id_dept = data_dept.Rows(i)("id_departement").ToString
                    ReportEmpAttn.is_head_dept = "-1"
                    Dim Report As New ReportEmpAttn()

                    ' Create a new memory stream and export the report into it as PDF.
                    Dim Mem As New MemoryStream()
                    Report.ExportToPdf(Mem)

                    ' Create a new attachment and put the PDF report into it.
                    Mem.Seek(0, System.IO.SeekOrigin.Begin)
                    '
                    Dim Att = New Attachment(Mem, "Weekly Attendance Report - " & data_dept.Rows(i)("departement").ToString & ".pdf", "application/pdf")
                    '
                    Dim mail As MailMessage = New MailMessage("system@volcom.mail", data_dept.Rows(i)("email_lokal").ToString)
                    ' Dim mail As MailMessage = New MailMessage("system@volcom.mail", "septian@volcom.mail")
                    mail.Attachments.Add(Att)
                    Dim client As SmtpClient = New SmtpClient()
                    client.Port = 25
                    client.DeliveryMethod = SmtpDeliveryMethod.Network
                    client.UseDefaultCredentials = False
                    client.Host = "192.168.1.4"
                    client.Credentials = New System.Net.NetworkCredential("system@volcom.mail", "system123")
                    mail.Subject = "Weekly Attendance Report (" & data_dept.Rows(i)("departement").ToString & ")"
                    mail.IsBodyHtml = True
                    mail.Body = email_temp(data_dept.Rows(i)("employee_name").ToString, False)
                    client.Send(mail)
                    'log
                    Dim query_log As String = "INSERT INTo tb_scheduler_attn_log(id_log_type,`datetime`,log) VALUES('3',NOW(),'Sending Weekly Attendance Report (" & data_dept.Rows(i)("departement").ToString & ") to " & data_dept.Rows(i)("email_lokal").ToString & "')"
                    execute_non_query(query_log, True, "", "", "", "")
                Next
            End If
        ElseIf report_mark_type = "weekly_attn_head" Then
            If data_opt.Rows(0)("send_weekly_attn_headdept").ToString = "1" Then
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
                Dim mail As MailMessage = New MailMessage("system@volcom.mail", data_opt.Rows(0)("email_lokal").ToString)
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
                mail.Body = email_temp(data_opt.Rows(0)("employee_name").ToString, True)
                client.Send(mail)
                'log
                Dim query_log As String = "INSERT INTo tb_scheduler_attn_log(id_log_type,`datetime`,log) VALUES('3',NOW(),'Sending Weekly Attendance Report (Department Head) to " & data_opt.Rows(0)("employee_name").ToString & "')"
                execute_non_query(query_log, True, "", "", "", "")
            End If
        ElseIf report_mark_type = "monthly_leave_remaining" Then
            If data_opt.Rows(0)("send_stock_leave").ToString = "1" Then
                ' Create a new report. 
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
End Class
