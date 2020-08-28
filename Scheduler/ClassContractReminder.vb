Public Class ClassContractReminder
    Public Shared Sub contract_reminder()
        Dim query As String = "
            SELECT emp.id_employee, emp.employee_name, emp.employee_position, sts.employee_status, dep.departement, emp.employee_join_date, emp.start_period, emp.end_period, lvl.grup_penilaian, emp_head.id_employee AS id_employee_head, emp_head.employee_name AS employee_name_head, emp_head.email_external AS email_external_head
            FROM tb_m_employee AS emp
            LEFT JOIN tb_lookup_employee_level AS lvl ON emp.id_employee_level = lvl.id_employee_level
            LEFT JOIN tb_lookup_employee_status AS sts ON emp.id_employee_status = sts.id_employee_status
            LEFT JOIN tb_m_departement AS dep ON emp.id_departement = dep.id_departement
            LEFT JOIN tb_m_user AS usr_head ON dep.id_user_head = usr_head.id_user
            LEFT JOIN tb_m_employee AS emp_head ON usr_head.id_employee = emp_head.id_employee
            WHERE emp.id_employee_status IN (1, 3) AND emp.id_employee_active = 1 AND
                (emp.end_period = DATE_ADD(DATE(NOW()), INTERVAL 45 DAY) OR
                emp.end_period = DATE_ADD(DATE(NOW()), INTERVAL 30 DAY) OR
                emp.end_period = DATE_ADD(DATE(NOW()), INTERVAL 15 DAY) OR
                emp.end_period = DATE_ADD(DATE(NOW()), INTERVAL 55 DAY))
        "

        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")

        For i = 0 To data.Rows.Count - 1
            Dim cls As ClassContractReminder = New ClassContractReminder()

            cls.send_email(data, i)

            cls = Nothing
        Next
    End Sub

    Sub send_email(data As DataTable, i As Integer)
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
        Dim to_data As DataTable = execute_query("
            SELECT emp.employee_name, emp.email_external
            FROM tb_m_employee AS emp
            WHERE emp.id_employee IN (134)
        ", -1, True, "", "", "", "")

        Dim send_name As String = to_data.Rows(0)("employee_name").ToString
        Dim send_mail As String = to_data.Rows(0)("email_external").ToString

        'data.Rows(i)("employee_name_head").ToString
        'data.Rows(i)("email_external_head").ToString

        Dim to_mail As Net.Mail.MailAddress = New Net.Mail.MailAddress(send_mail, send_name)

        mail.To.Add(to_mail)

        'cc
        Dim cc_query As String = "
            SELECT emp.employee_name, emp.email_external
            FROM tb_m_employee AS emp
            WHERE emp.id_employee IN (506)
        "

        Dim data_query As DataTable = execute_query(cc_query, -1, True, "", "", "", "")

        For j = 0 To data_query.Rows.Count - 1
            Dim cc_mail As Net.Mail.MailAddress = New Net.Mail.MailAddress(data_query.Rows(j)("email_external").ToString, data_query.Rows(j)("employee_name").ToString)

            mail.CC.Add(cc_mail)
        Next

        'subject & body
        mail.Subject = "Evaluasi Staff - " + data.Rows(i)("employee_name").ToString

        mail.IsBodyHtml = True

        mail.Body = "
            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Dear " + data.Rows(i)("employee_name_head").ToString + ",</p>
            <br>
            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Sehubungan dengan kontrak staffnya akan berakhir, maka mohon dapat dibuatkan evaluasi kerja atas nama sbb:</p>
            <br>
            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Nama Karyawan: " + data.Rows(i)("employee_name").ToString + "</p>
            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Posisi: " + data.Rows(i)("employee_position").ToString + "</p>
            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Dept: " + data.Rows(i)("departement").ToString + "</p>
            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Tgl Mulai Bekerja: " + Date.Parse(data.Rows(i)("employee_join_date").ToString).ToString("dd MMMM yyyy") + "</p>
            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Status Karyawan: " + data.Rows(i)("employee_status").ToString + "</p>
            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Periode Penilaian: " + Date.Parse(data.Rows(i)("start_period").ToString).ToString("dd MMMM yyyy") + " s/d " + Date.Parse(data.Rows(i)("end_period").ToString).ToString("dd MMMM yyyy") + "</p>
            <br>
            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Demikian kami sampaikan,  atas perhatian dan kerjasamanya kami ucapkan terimakasih.</p>
            <br>
            <br>
            <br>
            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Thank You</p>
            <p style='font: normal 10.00pt/14.25pt Arial; margin: 0;'>Volcom ERP</p>
        "

        'Dim path_temp As String = "\\192.168.1.2\dataapp$\emp\document\Form_Penilaian_-_" + data.Rows(i)("employee_name").ToString.Replace(" ", "_") + ".xlsx"

        If data.Rows(i)("grup_penilaian").ToString = "1" Then
            'staff - team leader
            Dim path As String = "\\192.168.1.2\dataapp$\emp\document\Form_Performance_Appraisal_level_Staff_-_Team_Leader.xlsx"

            'Dim excel As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application

            'excel.DisplayAlerts = False

            'excel.Workbooks.Open(path)

            'Dim workbook As Microsoft.Office.Interop.Excel.Workbook = excel.ActiveWorkbook

            'Dim sheet As Microsoft.Office.Interop.Excel.Worksheet = workbook.Worksheets(1)

            'sheet.Cells(4, 4) = ": " + data.Rows(i)("employee_name").ToString
            'sheet.Cells(5, 4) = ": " + data.Rows(i)("employee_position").ToString
            'sheet.Cells(6, 4) = ": " + data.Rows(i)("departement").ToString
            'sheet.Cells(8, 4) = ": " + Date.Parse(data.Rows(i)("employee_join_date").ToString).ToString("dd MMMM yyyy")

            'sheet.Cells(7, 10) = ": " + Date.Parse(data.Rows(i)("start_period").ToString).ToString("dd MMMM yyyy") + " s/d " + Date.Parse(data.Rows(i)("end_period").ToString).ToString("dd MMMM yyyy")

            'workbook.SaveAs(path_temp)

            'workbook.Close()

            'excel.Quit()

            'Runtime.InteropServices.Marshal.ReleaseComObject(excel) : excel = Nothing
            'Runtime.InteropServices.Marshal.ReleaseComObject(workbook) : workbook = Nothing
            'Runtime.InteropServices.Marshal.ReleaseComObject(sheet) : sheet = Nothing

            mail.Attachments.Add(New Net.Mail.Attachment(path))
        Else
            'asst. spv - sr. manager
            Dim path As String = "\\192.168.1.2\dataapp$\emp\document\Form_Performance_Appraisal_level_Asst_Spv_-_Sr._Spv.xlsx"

            'Dim excel As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application

            'excel.DisplayAlerts = False

            'excel.Workbooks.Open(path)

            'Dim workbook As Microsoft.Office.Interop.Excel.Workbook = excel.ActiveWorkbook

            'Dim sheet As Microsoft.Office.Interop.Excel.Worksheet = workbook.Worksheets(1)

            'sheet.Cells(4, 4) = ": " + data.Rows(i)("employee_name").ToString
            'sheet.Cells(5, 4) = ": " + data.Rows(i)("employee_position").ToString
            'sheet.Cells(6, 4) = ": " + data.Rows(i)("departement").ToString
            'sheet.Cells(8, 4) = ": " + Date.Parse(data.Rows(i)("employee_join_date").ToString).ToString("dd MMMM yyyy")

            'sheet.Cells(7, 10) = ": " + Date.Parse(data.Rows(i)("start_period").ToString).ToString("dd MMMM yyyy") + " s/d " + Date.Parse(data.Rows(i)("end_period").ToString).ToString("dd MMMM yyyy")

            'workbook.SaveAs(path_temp)

            'workbook.Close()

            'excel.Quit()

            'Runtime.InteropServices.Marshal.ReleaseComObject(excel) : excel = Nothing
            'Runtime.InteropServices.Marshal.ReleaseComObject(workbook) : workbook = Nothing
            'Runtime.InteropServices.Marshal.ReleaseComObject(sheet) : sheet = Nothing

            mail.Attachments.Add(New Net.Mail.Attachment(path))
        End If

        Dim status As String = ""

        Try
            client.Send(mail)

            mail.Dispose()

            'IO.File.Delete(path_temp)
        Catch ex As Exception
            status = "Failed send contract reminder: " + data.Rows(i)("employee_name").ToString + "; Error: " + ex.Message

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
