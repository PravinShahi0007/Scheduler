Imports System.IO
Imports System.Net.Mail

Public Class ClassCashAdvance
    Public Shared Sub check_cash_advance()
        Dim query As String = "
            SELECT ca.id_employee, e.employee_name, ca.id_departement, d.departement, ca.id_cash_advance, ca.number, ca.id_cash_advance_type, ca.val_ca, ca.note, DATE_FORMAT(ca.report_back_date, '%d %M %Y') AS report_back_date, DATE_FORMAT(ca.report_back_due_date, '%d %M %Y') AS report_back_due_date, DATE_FORMAT(can.report_notification_date, '%d %M %Y') AS report_notification_date, cas.total
            FROM tb_cash_advance AS ca
            LEFT JOIN tb_m_employee AS e ON ca.id_employee = e.id_employee
            LEFT JOIN tb_m_departement AS d ON ca.id_departement = d.id_departement
            LEFT JOIN (
	            SELECT id_cash_advance, COUNT(id_cash_advance) AS total
	            FROM tb_cash_advance_report
	            GROUP BY id_cash_advance
            ) AS cas ON ca.id_cash_advance = cas.id_cash_advance
            LEFT JOIN (
	            SELECT id_cash_advance, IF(id_cash_advance_type = 1, report_back_date + INTERVAL 7 DAY, report_back_due_date) AS report_notification_date
	            FROM tb_cash_advance
            ) AS can ON ca.id_cash_advance = can.id_cash_advance
            WHERE cas.total IS NULL AND ca.id_report_status = 6 AND can.report_notification_date = CURDATE()
        "

        Dim dataCashAdvance As DataTable = execute_query(query, -1, True, "", "", "", "")

        For i = 0 To dataCashAdvance.Rows.Count - 1
            send_mail(dataCashAdvance.Rows(i)("id_employee").ToString, dataCashAdvance.Rows(i)("employee_name").ToString, dataCashAdvance.Rows(i)("id_departement").ToString, dataCashAdvance.Rows(i)("departement").ToString, dataCashAdvance.Rows(i)("id_cash_advance").ToString, dataCashAdvance.Rows(i)("number").ToString, dataCashAdvance.Rows(i)("id_cash_advance_type").ToString, dataCashAdvance.Rows(i)("val_ca").ToString, dataCashAdvance.Rows(i)("note").ToString, dataCashAdvance.Rows(i)("report_back_date").ToString, dataCashAdvance.Rows(i)("report_back_due_date").ToString, dataCashAdvance.Rows(i)("report_notification_date").ToString)
        Next
    End Sub

    Public Shared Sub send_mail(id_employee As String, employee_name As String, id_departement As String, departement As String, id_cash_advance As String, number As String, id_cash_advance_type As String, val_ca As String, note As String, report_back_date As String, report_back_due_date As String, report_notification_date As String)
        Dim email As String = "
            <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100.0%;background:#eeeeee'>
                <tbody>
                  <tr>
                    <td style='padding:30.0pt 30.0pt 30.0pt 30.0pt'>
                      <div align='center'>
                        <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='600'
                          style='width:6.25in;background:white'>
                          <tbody>
                            <tr>
                              <td style='padding:0in 0in 0in 0in'></td>
                            </tr>
                            <tr>
                              <td style='padding:0in 0in 0in 0in'>
                                <p class='MsoNormal' align='center' style='text-align:center'>
                                  <a href='http://www.volcom.co.id/' title='Volcom' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.volcom.co.id/&amp;source=gmail&amp;ust=1480121870771000&amp;usg=AFQjCNEjXvEZWgDdR-Wlke7nn0fmc1ZUuA'>
                                    <span style='text-decoration:none'>
                                      <img border='0' width='180' id='m_1811720018273078822_x0000_i1025' src='https://ci3.googleusercontent.com/proxy/x-zXDZUS-2knkEkbTh3HzgyAAusw1Wz7dqV-lbnl39W_4F6T97fJ2_b9doP3nYi0B6KHstdb-tK8VAF_kOaLt2OH=s0-d-e1-ft#http://www.volcom.co.id/enews/img/volcom.jpg' alt='Volcom' class='CToWUd'>
                                    </span>
                                  </a>
                                </p>
                              </td>
                            </tr>
                            <tr>
                              <td style='padding:0in 0in 0in 0in'></td>
                            </tr>
                            <tr>
                              <td style='padding:0in 0in 0in 0in'>
                                <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='600'
                                  style='width:6.25in;background:white'>
                                  <tbody>
                                    <tr>
                                      <td style='padding:0in 0in 0in 0in'>

                                      </td>
                                    </tr>
                                  </tbody>
                                </table>
                                <p class='MsoNormal' style='background-color:#eff0f1'><span style='display:block;background-color:#eff0f1;height: 5px;'><u></u>&nbsp;<u></u></span></p>
                                <p class='MsoNormal'><span style='display:none'><u></u>&nbsp;<u></u></span></p>
                                <table width='100%' class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0'
                                  cellpadding='0' style='background:white'>
                                  <tbody>
                                    <tr>
                                      <td style='padding:15.0pt 15.0pt 15.0pt 15.0pt' colspan='3'>
                                        <div>
                                          <p class='MsoNormal' style='line-height:14.25pt'><b><span style='font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060'>Dear All,</span></b><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'><u></u><u></u></span></p>
                                        </div>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td style='padding:1.0pt 1.0pt 1.0pt 15.0pt' colspan='3'>
                                        <div>
                                          <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Cash advance with detail: </span></b><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'><u></u><u></u></span>
                                        </div>
                                      </td>
                                    </tr>

                                    <tr>
                                      <td style='padding:15.0pt 15.0pt 15.0pt 15.0pt' colspan='3'>
                                        <table width='100%' class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0'
                                          cellpadding='5' style='background:white'>
                                          <tr>
                                            <td style='padding:2.5pt 10pt 2.5pt 0pt'>
                                              <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Number</span>
                                            </td>
                                            <td style='padding:2.5pt 10pt 2.5pt 0pt'>
                                              <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>:</span>
                                            </td>
                                            <td style='padding:2.5pt 0pt'>
                                              <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>" + number + "</span>
                                            </td>
                                          </tr>

                                          <tr>
                                            <td style='padding:2.5pt 10pt 2.5pt 0pt'>
                                              <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Departement</span>
                                            </td>
                                            <td style='padding:2.5pt 10pt 2.5pt 0pt'>
                                              <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>:</span>
                                            </td>
                                            <td style='padding:2.5pt 0pt'>
                                              <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>" + departement + "</span>
                                            </td>
                                          </tr>

                                          <tr>
                                            <td style='padding:2.5pt 10pt 2.5pt 0pt'>
                                              <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Employee</span>
                                            </td>
                                            <td style='padding:2.5pt 10pt 2.5pt 0pt'>
                                              <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>:</span>
                                            </td>
                                            <td style='padding:2.5pt 0pt'>
                                              <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>" + employee_name + "</span>
                                            </td>
                                          </tr>

                                          <tr>
                                            <td style='padding:2.5pt 10pt 2.5pt 0pt'>
                                              <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Cash in Advance</span>
                                            </td>
                                            <td style='padding:2.5pt 10pt 2.5pt 0pt'>
                                              <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>:</span>
                                            </td>
                                            <td style='padding:2.5pt 0pt'>
                                              <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Rp. " + String.Format("{0:#,##0.00}", Convert.ToDouble(val_ca)) + "</span>
                                            </td>
                                          </tr>

                                          <tr>
                                            <td style='padding:2.5pt 10pt 2.5pt 0pt'>
                                              <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Note</span>
                                            </td>
                                            <td style='padding:2.5pt 10pt 2.5pt 0pt'>
                                              <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>:</span>
                                            </td>
                                            <td style='padding:2.5pt 0pt'>
                                              <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>" + note + "</span>
                                            </td>
                                          </tr>

                                          <tr>
                                            <td style='padding:2.5pt 10pt 2.5pt 0pt'>
                                              <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Report Back Date</span>
                                            </td>
                                            <td style='padding:2.5pt 10pt 2.5pt 0pt'>
                                              <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>:</span>
                                            </td>
                                            <td style='padding:2.5pt 0pt'>
                                              <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>" + report_back_date + "</span>
                                            </td>
                                          </tr>

                                          <tr>
                                            <td style='padding:2.5pt 10pt 2.5pt 0pt'>
                                              <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Report Back Due Date</span>
                                            </td>
                                            <td style='padding:2.5pt 10pt 2.5pt 0pt'>
                                              <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>:</span>
                                            </td>
                                            <td style='padding:2.5pt 0pt'>
                                              <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>" + report_back_due_date + "</span>
                                            </td>
                                          </tr>
                                        </table>
                                      </td>
                                    </tr>

                                    <tr>
                                      <td style='padding:15.0pt 15.0pt 8.0pt 15.0pt' colspan='3'>
                                        <div>
                                          <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Have not reported accountability yet. Please check your system.</span></b><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'><u></u><u></u></span>
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
                                <p class='MsoNormal' style='background-color:#eff0f1'><span style='display:block;background-color:#eff0f1;height: 5px;'><u></u>&nbsp;<u></u></span></p>
                                <p class='MsoNormal'><span style='display:none'><u></u>&nbsp;<u></u></span></p>
                                <div align='center'>
                                  <table class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0' cellpadding='0' style='background:white'>
                                    <tbody>
                                      <tr>
                                        <td style='padding:6.0pt 6.0pt 6.0pt 6.0pt;text-align:center;'>
                                          <span style='text-align:center;font-size:7.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#a0a0a0;letter-spacing:.4pt;'>This email send directly from system. Do not reply.</b><u></u><u></u></span>
                                          <p class='MsoNormal' align='center' style='margin-bottom:12.0pt;text-align:center;padding-top:0px;'>
                                            <img border='0' width='300' id='m_1811720018273078822_x0000_i1028' src='https://ci6.googleusercontent.com/proxy/xq6o45mp_D9Z7DHCK5WT7GKuQ2QDaLg1hyMxoHX5ofUIv_m7GwasoczpbAOn6l6Ze-UfLuIUAndSokPvO633nnO9=s0-d-e1-ft#http://www.volcom.co.id/enews/img/footer.jpg' class='CToWUd'><u></u><u></u>
                                          </p>
                                        </td>
                                      </tr>
                                    </tbody>
                                  </table>
                                </div>
                              </td>
                            </tr>
                          </tbody>
                        </table>
                      </div>
                    </td>
                  </tr>
                </tbody>
             </table>
        "

        Dim mail As MailMessage = New MailMessage("system@volcom.mail", "friastana@volcom.mail")
        Dim client As SmtpClient = New SmtpClient()
        client.Port = 25
        client.DeliveryMethod = SmtpDeliveryMethod.Network
        client.UseDefaultCredentials = False
        client.Host = "192.168.1.4"
        client.Credentials = New System.Net.NetworkCredential("system@volcom.mail", "system123")
        mail.Subject = "Cash Advance"
        'mail.CC.Add("friastana@volcom.mail")
        mail.IsBodyHtml = True
        mail.Body = email
        client.Send(mail)

        mail.Dispose()
        client.Dispose()

        'log
        Dim query_log As String = "INSERT INTO tb_cash_advance_log (`id_cash_advance`, `datetime`) VALUES (" + id_cash_advance + ", NOW())"
        execute_non_query(query_log, True, "", "", "", "")
    End Sub
End Class
