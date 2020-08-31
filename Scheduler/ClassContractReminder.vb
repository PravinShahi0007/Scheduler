Public Class ClassContractReminder
    Public Shared Sub contract_reminder()
        Dim date_range As DataTable = execute_query("SELECT DATE(CONCAT(YEAR(DATE_ADD(DATE(NOW()), INTERVAL 2 MONTH)), '-', MONTH(DATE_ADD(DATE(NOW()), INTERVAL 2 MONTH)), '-01')) first_date, LAST_DAY(DATE_ADD(DATE(NOW()), INTERVAL 2 MONTH)) last_date", -1, True, "", "", "", "")

        Dim query_employee As String = "
            SELECT emp.id_employee, emp.employee_name, emp.employee_position, lvl.employee_level, sts.employee_status, dep.id_departement, dep.departement, emp.employee_join_date, emp.start_period, emp.end_period, DATE_ADD(DATE(CONCAT(YEAR(NOW()), '-', MONTH(NOW()), '-01')), INTERVAL -1 DAY) AS end_appraisal, lvl.grup_penilaian, emp_head.id_employee AS id_employee_head, emp_head.employee_name AS employee_name_head, emp_head.email_external AS email_external_head, dep.is_store
            FROM tb_m_employee AS emp
            LEFT JOIN tb_lookup_employee_level AS lvl ON emp.id_employee_level = lvl.id_employee_level
            LEFT JOIN tb_lookup_employee_status AS sts ON emp.id_employee_status = sts.id_employee_status
            LEFT JOIN tb_m_departement AS dep ON emp.id_departement = dep.id_departement
            LEFT JOIN tb_m_user AS usr_head ON dep.id_user_head = usr_head.id_user
            LEFT JOIN tb_m_employee AS emp_head ON usr_head.id_employee = emp_head.id_employee
            WHERE emp.id_employee_status IN (1, 3) AND emp.id_employee_active = 1 AND emp.end_period BETWEEN '" + Date.Parse(date_range.Rows(0)("first_date").ToString).ToString("yyyy-MM-dd") + "' AND '" + Date.Parse(date_range.Rows(0)("last_date").ToString).ToString("yyyy-MM-dd") + "'
            ORDER BY dep.id_departement ASC
        "

        Dim data_employee As DataTable = execute_query(query_employee, -1, True, "", "", "", "")

        'separate by department
        Dim list_data As List(Of DataTable) = New List(Of DataTable)

        Dim last_departement As String = ""

        For i = 0 To data_employee.Rows.Count - 1
            If Not data_employee.Rows(i)("id_departement").ToString = last_departement Then
                list_data.Add(data_employee.Clone())
            End If

            list_data(list_data.Count - 1).ImportRow(data_employee.Rows(i))

            last_departement = data_employee.Rows(i)("id_departement").ToString
        Next

        'send mail
        For i = 0 To list_data.Count - 1
            Dim c As ClassContractReminder = New ClassContractReminder

            c.send_email(list_data(i))

            c = Nothing
        Next
    End Sub

    Sub send_email(data As DataTable)
        Dim is_ssl = get_setup_field("system_email_is_ssl").ToString

        Dim client As Net.Mail.SmtpClient = New Net.Mail.SmtpClient()

        If is_ssl = "1" Then
            client.Port = get_setup_field("system_email_ssl_port").ToString
            client.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
            client.UseDefaultCredentials = False
            client.Host = get_setup_field("system_email_ssl_server").ToString
            client.EnableSsl = True
            client.Credentials = New System.Net.NetworkCredential(get_setup_field("system_email_ssl").ToString, get_setup_field("system_email_ssl_pass").ToString)
        Else
            client.Port = get_setup_field("system_email_port").ToString
            client.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
            client.UseDefaultCredentials = False
            client.Host = get_setup_field("system_email_server").ToString
            client.Credentials = New System.Net.NetworkCredential(get_setup_field("system_email").ToString, get_setup_field("system_email_pass").ToString)
        End If

        'mail
        Dim mail As Net.Mail.MailMessage = New Net.Mail.MailMessage()

        'from
        Dim from_mail As Net.Mail.MailAddress = New Net.Mail.MailAddress("system@volcom.co.id", "Volcom ERP")

        mail.From = from_mail

        'to
        Dim send_name As String = data.Rows(0)("employee_name_head").ToString
        Dim send_mail As String = data.Rows(0)("email_external_head").ToString

        Dim to_query As String = "
            SELECT emp.employee_name, emp.email_external
            FROM tb_appraisal_mail_mapping AS map
            LEFT JOIN tb_m_employee AS emp ON map.id_employee = emp.id_employee
            WHERE map.send_type = 'to' AND map.id_departement IN (" + data.Rows(0)("id_departement").ToString + ")
        "

        Dim to_data As DataTable = execute_query(to_query, -1, True, "", "", "", "")

        If to_data.Rows.Count > 0 Then
            send_name = to_data.Rows(0)("employee_name").ToString
            send_mail = to_data.Rows(0)("email_external").ToString
        End If

        Dim to_mail As Net.Mail.MailAddress = New Net.Mail.MailAddress(send_mail, send_name)

        mail.To.Add(to_mail)

        'cc
        Dim cc_query As String = "
            (SELECT emp.employee_name, emp.email_external
            FROM tb_appraisal_mail_mapping AS map
            LEFT JOIN tb_m_employee AS emp ON map.id_employee = emp.id_employee
            WHERE map.send_type = 'cc' AND map.id_departement IN (0, " + data.Rows(0)("id_departement").ToString + ") AND emp.email_external <> '" + send_mail + "')
        "

        Dim is_cc_management As String = execute_query("SELECT emp_per_app_is_cc_management FROM tb_opt_scheduler LIMIT 1", 0, True, "", "", "", "")

        If is_cc_management = "1" Then
            cc_query += "
                UNION ALL
                (SELECT management_mail AS employee_name, management_mail AS email_external FROM tb_opt_scheduler LIMIT 1)
            "
        End If

        Dim cc_data As DataTable = execute_query(cc_query, -1, True, "", "", "", "")

        For j = 0 To cc_data.Rows.Count - 1
            Dim cc_mail As Net.Mail.MailAddress = New Net.Mail.MailAddress(cc_data.Rows(j)("email_external").ToString, cc_data.Rows(j)("employee_name").ToString)

            mail.CC.Add(cc_mail)
        Next

        'subject & body
        mail.Subject = "Evaluasi Staff - " + data.Rows(0)("departement").ToString

        mail.IsBodyHtml = True

        Dim bdy As String = "
            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Dear " + data.Rows(0)("employee_name_head").ToString + ",</p>
            <br>
            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Sehubungan dengan kontrak staffnya akan berakhir, maka mohon dapat dibuatkan evaluasi kerja atas nama sbb:</p>
            <br>
        "

        For i = 0 To data.Rows.Count - 1
            bdy += "
                <table border='0'>
                    <tr>
                        <td valign='top' style='width: 35px;'>
                            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>" + (i + 1).ToString + ".</p>
                        </td>
                        <td>
                            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Nama Karyawan: " + data.Rows(i)("employee_name").ToString + "</p>
                            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Posisi: " + data.Rows(i)("employee_position").ToString + "</p>
                            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Level: " + data.Rows(i)("employee_level").ToString + "</p>
                            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Dept: " + data.Rows(i)("departement").ToString + "</p>
                            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Tgl Mulai Bekerja: " + Date.Parse(data.Rows(i)("employee_join_date").ToString).ToString("dd MMMM yyyy") + "</p>
                            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Tgl Berakhir Kontrak: " + Date.Parse(data.Rows(i)("end_period").ToString).ToString("dd MMMM yyyy") + "</p>
                            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Status Karyawan: " + data.Rows(i)("employee_status").ToString + "</p>
                            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Periode Penilaian: " + Date.Parse(data.Rows(i)("start_period").ToString).ToString("dd MMMM yyyy") + " s/d " + Date.Parse(data.Rows(i)("end_appraisal").ToString).ToString("dd MMMM yyyy") + "</p>
                        </td>
                </table>
                <br>
            "
        Next

        bdy += "
            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Demikian kami sampaikan, atas perhatian dan kerjasamanya kami ucapkan terimakasih.</p>
            <br>
            <br>
            <br>
            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Thank You</p>
            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Volcom ERP</p>
        "

        mail.Body = bdy

        Dim already_1 As Boolean = False
        Dim already_2 As Boolean = False

        For i = 0 To data.Rows.Count - 1
            If data.Rows(i)("grup_penilaian").ToString = "1" Then
                If Not already_1 Then
                    'staff - team leader
                    Dim path As String = "\\192.168.1.2\dataapp$\emp\document\Form_Performance_Appraisal_level_Staff_-_Team_Leader.xlsx"

                    mail.Attachments.Add(New Net.Mail.Attachment(path))

                    already_1 = True
                End If
            Else
                If Not already_2 Then
                    'asst. spv - sr. manager
                    Dim path As String = "\\192.168.1.2\dataapp$\emp\document\Form_Performance_Appraisal_level_Asst_Spv_-_Sr._Spv.xlsx"

                    mail.Attachments.Add(New Net.Mail.Attachment(path))
                End If
            End If
        Next

        'to
        Dim values_to As String = ""

        For i = 0 To mail.To.Count - 1
            values_to += mail.To(i).Address + ", "
        Next

        'cc
        Dim values_cc As String = ""

        For i = 0 To mail.CC.Count - 1
            values_cc += mail.CC(i).Address + ", "
        Next

        'body
        Dim values_body As String = mail.Body

        'att
        Dim values_att As String = ""

        For i = 0 To mail.Attachments.Count - 1
            values_att += mail.Attachments(i).Name + ", "
        Next

        'query log
        Dim query_log As String = "INSERT INTO tb_appraisal_mail_log (departement, `to`, `cc`, `body`, `att`, created_date) VALUES ('" + data.Rows(0)("departement").ToString + "', '" + values_to.Substring(0, values_to.Length - 2) + "', '" + values_cc.Substring(0, values_cc.Length - 2) + "', '" + addSlashes(values_body) + "', '" + values_att.Substring(0, values_att.Length - 2) + "', NOW())"

        Dim status As String = ""

        Try
            client.Send(mail)

            mail.Dispose()

            execute_non_query(query_log, True, "", "", "", "")
        Catch ex As Exception
            status = "Failed send contract reminder: " + data.Rows(0)("departement").ToString + "; Error: " + ex.Message

            execute_non_query("INSERT INTO tb_error_mail (date, description) VALUES (NOW(), '" + addSlashes(status) + "')", True, "", "", "", "")
        End Try
    End Sub

    Shared Function get_setup_field(ByVal field As String)
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
End Class
