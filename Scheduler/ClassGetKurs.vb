Public Class ClassGetKurs
    Shared Sub get_kurs()
        Net.ServicePointManager.SecurityProtocol = DirectCast(3072, Net.SecurityProtocolType)

        Dim webClient As New Net.WebClient
        Dim result As String = webClient.DownloadString("https://fiskal.kemenkeu.go.id/informasi-publik/kurs-pajak")
        Dim str_kurs_dec As String = Between(Between(result, "<img src=""/assets/19bc465b/transparent.gif"" class=""flag flag-us"" alt=""Amerika Serikat"" />", "</td>"), "<div class=""ml-5"">", "</div>").Replace(".", "").Replace(" ", "").Replace(",", "")
        'Dim str_kurs_dec As String = Between(result, "<img src=""/assets/92970f7b/transparent.gif"" class=""flag flag-us"" alt=""Amerika Serikat"" />", "</td>").Replace(".", "").Replace(" ", "").Replace(",", "")
        str_kurs_dec = str_kurs_dec.Substring(0, str_kurs_dec.Length - 2) + "." + str_kurs_dec.Substring(str_kurs_dec.Length - 2, 2)
        '
        Dim query_sel As String = "SELECT CAST('" & str_kurs_dec & "' AS DECIMAL(13,2)) as kurs"
        Dim data_sel As DataTable = execute_query(query_sel, -1, True, "", "", "", "")
        If data_sel.Rows.Count > 0 Then
            Dim kurs As Decimal = data_sel.Rows(0)("kurs")
            'get last fixed floating
            Dim q As String = "SELECT * FROM tb_kurs_trans ORDER BY id_kurs_trans DESC LIMIT 1"
            Dim dt As DataTable = execute_query(q, -1, True, "", "", "", "")
            If dt.Rows.Count > 0 Then
                Dim floating As Decimal = dt.Rows(0)("fixed_floating")

                Dim query As String = "INSERT INTO tb_kurs_trans(created_by,created_date,kurs_trans,fixed_floating) VALUES('0',NOW(),'" & decimalSQL(kurs.ToString) & "','" & decimalSQL(floating.ToString) & "');
                UPDATE tb_opt SET rate_management='" & decimalSQL((kurs + floating).ToString) & "'"
                execute_non_query(query, True, "", "", "", "'")
            End If
        Else
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

            'mail
            Dim mail As Net.Mail.MailMessage = New Net.Mail.MailMessage()

            'from
            Dim from_mail As Net.Mail.MailAddress = New Net.Mail.MailAddress("system@volcom.co.id", "Volcom ERP")

            mail.From = from_mail

            'to
            mail.To.Add(New Net.Mail.MailAddress("catur@volcom.co.id"))
            mail.To.Add(New Net.Mail.MailAddress("septian@volcom.co.id"))
            mail.To.Add(New Net.Mail.MailAddress("friastana@volcom.co.id"))

            mail.Subject = "Get Kurs Error"

            mail.IsBodyHtml = True

            mail.Body = "<p>Get Kurs Error</p>"

            Try
                client.Send(mail)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Public Shared Function Between(ByVal src As String, ByVal findfrom As String, ByVal findto As String) As String
        Dim start As Integer = src.IndexOf(findfrom)
        Dim [to] As Integer = src.IndexOf(findto, start + findfrom.Length)
        If start < 0 OrElse [to] < 0 Then Return ""
        Dim s As String = src.Substring(start + findfrom.Length, [to] - start - findfrom.Length)
        Return s
    End Function

    Public Shared Function decimalSQL(ByVal value As String) 'hanya kalo masuk ke database
        Dim nominal As String

        nominal = value.Replace(",", ".")
        Return nominal
    End Function

    Shared Function get_setup_field(ByVal field As String)
        'opt as var choose field
        Dim ret_var, query As String
        ret_var = ""

        Try
            query = "Select " & field & " FROM tb_opt LIMIT 1"
            ret_var = execute_query(query, 0, True, "", "", "", "")
        Catch ex As Exception
            ret_var = ""
        End Try

        Return ret_var
    End Function
End Class
