Public Class ClassLateWarning
    Public Shared Sub check_late()
        Dim query As String = "CALL view_warning_late();"

        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")

        Dim employee_late As DataTable = New DataTable

        employee_late.Columns.Add("id_employee", GetType(Integer))
        employee_late.Columns.Add("employee_code", GetType(String))
        employee_late.Columns.Add("employee_name", GetType(String))
        employee_late.Columns.Add("employee_position", GetType(String))
        employee_late.Columns.Add("from_date", GetType(Date))
        employee_late.Columns.Add("to_date", GetType(Date))

        Dim last_employee As Integer = data.Rows(0)("id_employee")
        Dim total_late As Integer = 0

        For i = 0 To data.Rows.Count - 1
            If last_employee <> data.Rows(i)("id_employee") Then
                total_late = 0
            End If

            If data.Rows(i)("late") > 0 Then
                total_late = total_late + 1
            End If

            If total_late >= 3 Then
                employee_late.Rows.Add(data.Rows(i)("id_employee"), data.Rows(i)("employee_code").ToString, data.Rows(i)("employee_name").ToString, data.Rows(i)("employee_position").ToString, data.Rows(i)("date"), data.Rows(i - 2)("date"))
            End If

            last_employee = data.Rows(i)("id_employee")
        Next

        'send email
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

        For i = 0 To employee_late.Rows.Count - 1
            Dim id_warning_type As String = execute_query("SELECT (IFNULL((SELECT MAX(id_warning_type) FROM tb_emp_late_warning WHERE id_employee = " + employee_late.Rows(i)("id_employee").ToString + " AND MONTH(NOW()) = MONTH(sent_date) AND YEAR(NOW()) AND YEAR(sent_date)), 0) + 1) AS id_warning_type", 0, True, "", "", "", "")

            Dim warning_type As String = execute_query("
                SELECT warning_type
                FROM tb_lookup_warning_type
                WHERE id_warning_type = IF(" + id_warning_type + " > (SELECT MAX(id_warning_type) FROM tb_lookup_warning_type), (SELECT MAX(id_warning_type) FROM tb_lookup_warning_type), " + id_warning_type + ")
            ", 0, True, "", "", "", "")

            'mail
            Dim mail As Net.Mail.MailMessage = New Net.Mail.MailMessage()

            'from
            Dim from_mail As Net.Mail.MailAddress = New Net.Mail.MailAddress("system@volcom.co.id", "Volcom ERP")

            mail.From = from_mail

            Dim email_to As String = "friastana@volcom.co.id"
            Dim name_to As String = "I Putu Agus Friastana"

            'to
            Dim to_mail As Net.Mail.MailAddress = New Net.Mail.MailAddress(email_to, name_to)

            mail.To.Add(to_mail)

            mail.Subject = "Notifikasi Keterlambatan - " + warning_type

            mail.IsBodyHtml = True

            mail.Body = "
                <table cellpadding='0' cellspacing='0' width='100%' style='background-color: #EEEEEE; border-collapse: collapse; padding: 30pt;'>
                    <tr>
                        <td align='center'>
                            <table cellpadding='0' cellspacing='0' width='700' style='background-color: #FFFFFF; border-collapse: collapse;'>
                                <tr>
                                    <td style='text-align: center; padding: 30pt 0pt;'>
                                        <a href='http://www.volcom.co.id' title='Volcom' target='_blank'>
                                            <img src='http://www.volcom.co.id/enews/img/volcom.jpg' alt='Volcom' height='142' width='200'>
                                        </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td style='background-color: #EEEEEE; padding: 5pt 0pt;'></td>
                                </tr>
                                <tr>
                                    <td style='padding: 30pt;'>
                                        <p style='font-size: 12pt; font-family: Arial, sans-serif; font-weight: bold; margin: 0pt 0pt 10pt 0pt;'>Dear " + employee_late.Rows(i)("employee_name").ToString + ",</p>
                                        <p style='font-size: 10pt; font-family: Arial, sans-serif; margin: 0pt 0pt 5pt 0pt;'>Anda terlambat tiga hari berturut-turut dari tanggal " + Date.Parse(employee_late.Rows(i)("from_date").ToString).ToString("dd MMMM yyyy") + " - " + Date.Parse(employee_late.Rows(i)("to_date").ToString).ToString("dd MMMM yyyy") + ".</p>
                                        <p style='font-size: 10pt; font-family: Arial, sans-serif; margin: 25pt 0pt 10pt 0pt;'>Thank you</p>
                                        <p style='font-size: 12pt; font-family: Arial, sans-serif; font-weight: bold; margin: 0pt;'>Volcom ERP</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td style='background-color: #EEEEEE; padding: 5pt 0pt;'></td>
                                </tr>
                                <tr>
                                    <td style='text-align: center; padding: 30pt 0pt;'>
                                        <p style='font-size: 9pt; font-family: Arial, sans-serif; color: #A0A0A0; margin-bottom: 15pt;'>This email send directly from system. Do not reply.</p>
                                        <img src='http://www.volcom.co.id/enews/img/footer.jpg' alt='Volcom' height='56' width='250'>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            "

            Dim messages As String = ""

            Try
                client.Send(mail)
            Catch ex As Exception
                messages = ex.ToString
            End Try

            Dim insert_query As String = "INSERT INTO tb_emp_late_warning (id_warning_type, id_employee, from_date, to_date, sent_date, messages) VALUES (" + id_warning_type + ", " + employee_late.Rows(i)("id_employee").ToString + ", '" + Date.Parse(employee_late.Rows(i)("from_date").ToString).ToString("yyyy-MM-dd") + "', '" + Date.Parse(employee_late.Rows(i)("to_date").ToString).ToString("yyyy-MM-dd") + "', NOW(), '" + addSlashes(messages) + "')"

            execute_non_query(insert_query, True, "", "", "", "")
        Next
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
