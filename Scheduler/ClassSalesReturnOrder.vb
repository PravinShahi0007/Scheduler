Public Class ClassSalesReturnOrder
    Shared Sub check_empty_pickup_date()
        Dim query As String = "
            SELECT CONCAT(d.comp_number, ' - ', d.comp_name) AS store_name_to, CONCAT(wh.comp_number, ' - ', wh.comp_name) AS wh_name_to, a.sales_return_order_number, a.sales_return_order_date, a.sales_return_order_est_date
            FROM tb_mail_manage_det AS m
            LEFT JOIN tb_mail_manage AS h ON m.id_mail_manage = h.id_mail_manage
            LEFT JOIN tb_sales_return_order AS a ON m.id_report = a.id_sales_return_order
            LEFT JOIN tb_m_comp_contact AS c ON c.id_comp_contact = a.id_store_contact_to
            LEFT JOIN tb_m_comp AS d ON c.id_comp = d.id_comp
            LEFT JOIN tb_m_comp_contact AS whc ON whc.id_comp_contact = a.id_wh_contact_to
            LEFT JOIN tb_m_comp AS wh ON wh.id_comp = whc.id_comp
            WHERE h.report_mark_type = 45 AND h.id_mail_status = 2 AND m.id_report NOT IN (
	            SELECT d.id_sales_order_return
	            FROM tb_sales_return_order_mail_3pl_det AS d
	            LEFT JOIN tb_sales_return_order_mail_3pl AS p ON d.id_mail_3pl = p.id_mail_3pl
	            WHERE p.id_status <> 5
            )
        "

        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")

        If data.Rows.Count > 0 Then
            Dim html As String = "
                <table cellpadding='0' cellspacing='0' width='100%' style='background-color: #EEEEEE; padding: 30pt;'>
                    <tr>
                        <td align='center'>
                            <table cellpadding='0' cellspacing='0' width='700' style='background-color: #FFFFFF;'>
                                <tr>
                                    <td style='text-align: center; padding: 30pt 0pt;'>
                                        <a href='http://www.volcom.co.id' title='Volcom' target='_blank'>
                                            <img src='https://d3k81ch9hvuctc.cloudfront.net/company/VFgA3P/images/de2b6f13-9275-426d-ae31-640f3dcfc744.jpeg' alt='Volcom' height='142' width='200'>
                                        </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td style='background-color: #EEEEEE; padding: 5pt 0pt;'></td>
                                </tr>
                                <tr>
                                    <td style='padding: 30pt;'>
                                        <p style='font-size: 12pt; font-family: Arial, sans-serif; font-weight: bold; margin: 0pt 0pt 10pt 0pt;'>Dear Team,</p>
                                        <p style='font-size: 10pt; font-family: Arial, sans-serif; margin: 0pt 0pt 5pt 0pt;'>Please create pickup order for propose return below :</p>
                                        <table border='1' cellpadding='0' cellspacing='0' width='100%' style='margin: 15pt 0;'>
                                            <tr>
                                                <th style='padding: 5pt 10pt;'><p style='font-size: 10pt; font-family: Arial, sans-serif; margin: 0pt 0pt 0pt 0pt;'>No</p></th>
                                                <th style='padding: 5pt 10pt;'><p style='font-size: 10pt; font-family: Arial, sans-serif; margin: 0pt 0pt 0pt 0pt;'>Store</p></th>
                                                <th style='padding: 5pt 10pt;'><p style='font-size: 10pt; font-family: Arial, sans-serif; margin: 0pt 0pt 0pt 0pt;'>No Sales Return</p></th>
                                                <th style='padding: 5pt 10pt;'><p style='font-size: 10pt; font-family: Arial, sans-serif; margin: 0pt 0pt 0pt 0pt;'>Created Date</p></th>
                                                <th style='padding: 5pt 10pt;'><p style='font-size: 10pt; font-family: Arial, sans-serif; margin: 0pt 0pt 0pt 0pt;'>Estimate Return</p></th>
                                            </tr>
            "

            For i = 0 To data.Rows.Count - 1
                html += "
                    <tr>
                        <td style='padding: 5pt 10pt;'><p style='font-size: 10pt; font-family: Arial, sans-serif; margin: 0pt 0pt 0pt 0pt;'>" + (i + 1).ToString + "</p></td>
                        <td style='padding: 5pt 10pt;'><p style='font-size: 10pt; font-family: Arial, sans-serif; margin: 0pt 0pt 0pt 0pt;'>" + data.Rows(i)("store_name_to").ToString + "</p></td>
                        <td style='padding: 5pt 10pt;'><p style='font-size: 10pt; font-family: Arial, sans-serif; margin: 0pt 0pt 0pt 0pt;'>" + data.Rows(i)("sales_return_order_number").ToString + "</p></td>
                        <td style='padding: 5pt 10pt;'><p style='font-size: 10pt; font-family: Arial, sans-serif; margin: 0pt 0pt 0pt 0pt;'>" + Date.Parse(data.Rows(i)("sales_return_order_date").ToString).ToString("dd MMMM yyyy") + "</p></td>
                        <td style='padding: 5pt 10pt;'><p style='font-size: 10pt; font-family: Arial, sans-serif; margin: 0pt 0pt 0pt 0pt;'>" + Date.Parse(data.Rows(i)("sales_return_order_est_date").ToString).ToString("dd MMMM yyyy") + "</p></td>
                    </tr>
                "
            Next

            html += "                   </table>
                                        <p style='font-size: 10pt; font-family: Arial, sans-serif; margin: 25pt 0pt 10pt 0pt;'>Thank you</p>
                                        <p style='font-size: 12pt; font-family: Arial, sans-serif; font-weight: bold; margin: 0pt;'>Volcom ERP</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td style='background-color: #EEEEEE; padding: 5pt 0pt;'></td>
                                </tr>
                                <tr>
                                    <td style='text-align: center; padding: 30pt 0pt;'>
                                        <p style='font-size: 9pt; font-family: Arial, sans-serif; color: #A0A0A0;'>This email send directly from system. Do not reply.</p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            "

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
            Dim to_query As String = "
                SELECT emp.employee_name, emp.email_external
                FROM tb_sales_return_order_mail_mapping AS map
                LEFT JOIN tb_m_employee AS emp ON map.id_employee = emp.id_employee
                WHERE map.use_in = 'pickupdate' AND map.type = 'to'
            "

            Dim to_data As DataTable = execute_query(to_query, -1, True, "", "", "", "")

            If to_data.Rows.Count > 0 Then
                Dim to_mail As Net.Mail.MailAddress = New Net.Mail.MailAddress(to_data.Rows(0)("email_external").ToString, to_data.Rows(0)("employee_name").ToString)

                mail.To.Add(to_mail)
            End If

            'cc
            Dim cc_query As String = "
                SELECT emp.employee_name, emp.email_external
                FROM tb_sales_return_order_mail_mapping AS map
                LEFT JOIN tb_m_employee AS emp ON map.id_employee = emp.id_employee
                WHERE map.use_in = 'pickupdate' AND map.type = 'cc'
            "

            Dim cc_data As DataTable = execute_query(cc_query, -1, True, "", "", "", "")

            For j = 0 To cc_data.Rows.Count - 1
                Dim cc_mail As Net.Mail.MailAddress = New Net.Mail.MailAddress(cc_data.Rows(j)("email_external").ToString, cc_data.Rows(j)("employee_name").ToString)

                mail.CC.Add(cc_mail)
            Next

            'subject & body
            mail.Subject = "Sales Return Order - Pick Up Date"

            mail.IsBodyHtml = True

            mail.Body = html

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

            'query log
            Dim query_log As String = "INSERT INTO tb_sales_return_order_mail_log (`to`, `cc`, `body`, created_date) VALUES ('" + values_to.Substring(0, values_to.Length - 2) + "', '" + values_cc.Substring(0, values_cc.Length - 2) + "', '" + addSlashes(values_body) + "', NOW())"

            Try
                client.Send(mail)

                mail.Dispose()

                execute_non_query(query_log, True, "", "", "", "")
            Catch ex As Exception
                Dim status As String = "Failed send sales return order pick up date; Error: " + ex.Message

                execute_non_query("INSERT INTO tb_error_mail (date, description) VALUES (NOW(), '" + addSlashes(status) + "')", True, "", "", "", "")
            End Try
        End If
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
