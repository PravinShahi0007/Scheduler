Imports System.IO
Imports System.Net.Mail

Public Class ClassEmpPerAppraisal
    Public Shared Sub check_evaluation()
        Dim first_run As New System.DateTime(2019, 02, 18)

        Dim query As String = "
            SELECT tb.*
            FROM (
	            SELECT emp.id_departement, dept.departement, IFNULL(dept.id_employee_head, 0) id_employee_head, IFNULL(dept.employee_head_name, '') employee_head_name, IFNULL(dept.employee_head_email, '') employee_head_email,
                    IFNULL(emp.id_employee_status, 0) id_employee_status, IFNULL(stt.employee_status, '') employee_status,
		            emp.id_employee, emp.employee_code, emp.employee_name,
		            emp.employee_position, emp.id_employee_level, lvl.employee_level, IF(lvl.grup_penilaian = 0, 1, lvl.grup_penilaian) grup_penilaian,
		            IFNULL(qp.id_question_period, 0) id_question_period, IFNULL(qp.id_question_status, 1) id_question_status, IFNULL(qps.status, 'Belum Dinilai') status,
		            IFNULL(qp.appraiser_check, 0) appraiser_check,
		            IFNULL(DATE_FORMAT(qp.appraiser_check_date, '%d %M %Y'), '') appraiser_check_date,
		            IFNULL(qp.hrd_check, 0) hrd_check,
		            IFNULL(DATE_FORMAT(qp.hrd_check_date, '%d %M %Y'), '') hrd_check_date,
		            DATE_FORMAT(emp.employee_join_date, '%d %M %Y') employee_join_date,
		            DATE_FORMAT(jdate.start_period, '%d %M %Y') start_period,
		            DATE_FORMAT(jdate.end_period, '%d %M %Y') end_period,
		            DATE_FORMAT((jdate.end_period - INTERVAL 45 DAY), '%d %M %Y') start_evaluation_date,
		            DATE_FORMAT((jdate.end_period - INTERVAL 45 DAY) + INTERVAL 3 DAY, '%d %M %Y') end_evaluation_date,
		            IF(TIMESTAMPDIFF(DAY, ((jdate.end_period - INTERVAL 45 DAY)), DATE(CURDATE())) = 0, 1, 0) now,
		            TIMESTAMPDIFF(DAY, ((jdate.end_period - INTERVAL 45 DAY) + INTERVAL 3 DAY), DATE(CURDATE())) late
	            FROM tb_m_employee emp
	            LEFT JOIN (
		            SELECT d.id_departement, d.departement, e.id_employee id_employee_head, e.employee_name employee_head_name, e.email_lokal employee_head_email
		            FROM tb_m_departement d
		            LEFT JOIN tb_m_user u ON d.id_user_head = u.id_user
		            LEFT JOIN tb_m_employee e ON u.id_employee = e.id_employee
	            ) dept ON dept.id_departement = emp.id_departement 
	            LEFT JOIN tb_lookup_employee_status stt ON stt.id_employee_status = emp.id_employee_status 
	            LEFT JOIN tb_lookup_employee_level lvl ON lvl.id_employee_level = emp.id_employee_level
	            LEFT JOIN (
		            SELECT e.id_employee, 
			            IF(CURDATE() >= j.first_evaluation_start AND CURDATE() <= j.first_evaluation_end, j.first_evaluation_start, j.second_evaluation_start) start_period,
			            IF(CURDATE() >= j.first_evaluation_start AND CURDATE() <= j.first_evaluation_end, j.first_evaluation_end, j.second_evaluation_end) end_period,
			            j.first_evaluation_start,
			            j.first_evaluation_end,
			            j.second_evaluation_start,
			            j.second_evaluation_end
		            FROM tb_m_employee e
		            LEFT JOIN (
			            SELECT id_employee,
				            DATE_ADD(employee_join_date, INTERVAL (TIMESTAMPDIFF(YEAR, employee_join_date, DATE(CURDATE()))) YEAR) first_evaluation_start,
				            DATE_ADD(DATE_ADD(employee_join_date, INTERVAL (TIMESTAMPDIFF(YEAR, employee_join_date, DATE(CURDATE()))) YEAR), INTERVAL 6 MONTH) - INTERVAL 1 DAY first_evaluation_end,
				            DATE_ADD(DATE_ADD(employee_join_date, INTERVAL (TIMESTAMPDIFF(YEAR, employee_join_date, DATE(CURDATE()))) YEAR), INTERVAL 6 MONTH) second_evaluation_start,
				            DATE_ADD(DATE_ADD(employee_join_date, INTERVAL (TIMESTAMPDIFF(YEAR, employee_join_date, DATE(CURDATE()))) YEAR), INTERVAL 12 MONTH) - INTERVAL 1 DAY second_evaluation_end
			            FROM tb_m_employee
		            ) j ON e.id_employee = j.id_employee
	            ) jdate ON emp.id_employee = jdate.id_employee
	            LEFT JOIN tb_question_period qp ON emp.id_employee = qp.id_employee AND jdate.start_period = qp.from_period AND jdate.end_period = qp.until_period
	            LEFT JOIN tb_lookup_question_status qps ON qp.id_question_status = qps.id_question_status
	            WHERE emp.id_employee_active = 1 AND (CURDATE() >= (jdate.end_period - INTERVAL 45 DAY) AND CURDATE() <= jdate.end_period) AND dept.id_employee_head <> emp.id_employee
            ) tb
            WHERE tb.id_question_status = 1
            ORDER BY tb.id_employee ASC
        "

        Dim data_employee As DataTable = execute_query(query, -1, True, "", "", "", "")

        For i = 0 To data_employee.Rows.Count - 1
            Dim period As String = data_employee.Rows(i)("start_period").ToString + " s/d " + data_employee.Rows(i)("end_period").ToString

            If data_employee.Rows(i)("now") = 1 Then
                'Reminder evaluation
                send_mail(0, data_employee.Rows(i)("employee_head_email").ToString, data_employee.Rows(i)("employee_head_name").ToString, data_employee.Rows(i)("id_employee").ToString, data_employee.Rows(i)("employee_name").ToString, data_employee.Rows(i)("employee_position").ToString, data_employee.Rows(i)("departement").ToString, period)
            End If

            If data_employee.Rows(i)("late") = 1 Then
                'HRD to Depthead
                If first_run < Date.Parse(data_employee.Rows(i)("start_evaluation_date").ToString) Then
                    send_mail(1, data_employee.Rows(i)("employee_head_email").ToString, data_employee.Rows(i)("employee_head_name").ToString, data_employee.Rows(i)("id_employee").ToString, data_employee.Rows(i)("employee_name").ToString, data_employee.Rows(i)("employee_position").ToString, data_employee.Rows(i)("departement").ToString, period)
                End If
            End If

            If data_employee.Rows(i)("late") = 3 Then
                'HRD to Depthead cc Management
                If first_run < Date.Parse(data_employee.Rows(i)("start_evaluation_date").ToString) Then
                    send_mail(3, data_employee.Rows(i)("employee_head_email").ToString, data_employee.Rows(i)("employee_head_name").ToString, data_employee.Rows(i)("id_employee").ToString, data_employee.Rows(i)("employee_name").ToString, data_employee.Rows(i)("employee_position").ToString, data_employee.Rows(i)("departement").ToString, period)
                End If
            End If
        Next
    End Sub

    Public Shared Sub send_mail(late As Integer, head_mail As String, head_name As String, id_employee As String, name As String, position As String, departement As String, period As String)
        Dim status As String = ""

        If late = 0 Then
            status = "sudah dapat dilakukan"
        End If

        If late = 1 Then
            status = "sudah telat 1 hari"
        End If

        If late = 3 Then
            status = "sudah telat 3 hari"
        End If

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
                                        <p class='MsoNormal' style='line-height:14.25pt'><b><span style='font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060'>Dear " + head_name + ",</span></b><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'><u></u><u></u></span></p>
                                      </div>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td style='padding:1.0pt 1.0pt 1.0pt 15.0pt' colspan='3'>
                                      <div>
                                        <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Karyawan dengan detail dibawah: </span></b><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'><u></u><u></u></span>
                                      </div>
                                    </td>
                                  </tr>

                                  <tr>
                                    <td style='padding:15.0pt 15.0pt 15.0pt 15.0pt' colspan='3'>
                                      <table width='100%' class='m_1811720018273078822MsoNormalTable' border='0' cellspacing='0'
                                        cellpadding='5' style='background:white'>
                                        <tr>
                                          <td style='padding:2.5pt 10pt 2.5pt 0pt'>
                                            <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Nama</span>
                                          </td>
                                          <td style='padding:2.5pt 10pt 2.5pt 0pt'>
                                            <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>:</span>
                                          </td>
                                          <td style='padding:2.5pt 0pt'>
                                            <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>" + name + "</span>
                                          </td>
                                        </tr>
                                        
                                        <tr>
                                          <td style='padding:2.5pt 10pt 2.5pt 0pt'>
                                            <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Posisi</span>
                                          </td>
                                          <td style='padding:2.5pt 10pt 2.5pt 0pt'>
                                            <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>:</span>
                                          </td>
                                          <td style='padding:2.5pt 0pt'>
                                            <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>" + position + "</span>
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
                                      </table>
                                    </td>
                                  </tr>

                                  <tr>
                                    <td style='padding:15.0pt 15.0pt 8.0pt 15.0pt' colspan='3'>
                                      <div>
                                        <p class='MsoNormal' style='line-height:14.25pt'><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'>Evaluasi kinerja karyawan periode " + period + ", <b>" + status + "</b>. Mohon untuk mengecek sistem anda.</span></b><span style='font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#606060;letter-spacing:.4pt'><u></u><u></u></span>
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

        Dim log_id_employee As String = id_employee
        Dim log_to As String = head_mail
        Dim log_cc As String = "hrd@volcom.co.id"
        Dim log_late As String = late

        'head_mail
        Dim mail As MailMessage = New MailMessage("system@volcom.mail", "friastana@volcom.mail")
        Dim client As SmtpClient = New SmtpClient()
        client.Port = 25
        client.DeliveryMethod = SmtpDeliveryMethod.Network
        client.UseDefaultCredentials = False
        client.Host = "192.168.1.4"
        client.Credentials = New System.Net.NetworkCredential("system@volcom.mail", "system123")
        mail.Subject = "Penilaian Kinerja Karyawan"
        'CC hrd
        'hrd@volcom.co.id
        mail.CC.Add("friastana@volcom.mail")
        If late = 3 Then
            'CC management
            'management@volcom.co.id
            mail.CC.Add("friastana@volcom.mail")

            log_cc += ", management@volcom.co.id"
        End If
        mail.IsBodyHtml = True
        mail.Body = email
        client.Send(mail)

        'log
        Dim query_log As String = "INSERT INTO tb_question_log (`id_employee`, `datetime`, `to`, `cc`, `late`) VALUES (" + log_id_employee + ", NOW(), '" + log_to + "', '" + log_cc + "', " + log_late + ")"
        execute_non_query(query_log, True, "", "", "", "")
    End Sub
End Class
