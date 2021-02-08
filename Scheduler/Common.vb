Module Common
    Public Function addSlashes(ByVal InputTxt As String) As String
        ' List of characters handled:
        ' \000 null
        ' \010 backspace
        ' \011 horizontal tab
        ' \012 new line
        ' \015 carriage return
        ' \032 substitute
        ' \042 double quote
        ' \047 single quote
        ' \134 backslash
        ' \140 grave accent

        Dim Result As String = InputTxt

        Try
            Result = System.Text.RegularExpressions.Regex.Replace(InputTxt, "[\000\010\011\012\015\032\042\047\134\140]", "\$0")
        Catch Ex As Exception
            ' handle any exception here
            Console.WriteLine(Ex.Message)
        End Try
        Return Result
    End Function

    Public Function getMailManagement(ByVal rmt As String) As String
        Dim qmm As String = "SELECT o.management_mail 
        FROM tb_lookup_report_mark_type rmt 
        JOIN tb_opt_scheduler o
        WHERE rmt.report_mark_type=" + rmt + " AND rmt.is_mail_management=1 "
        Dim data As DataTable = execute_query(qmm, -1, True, "", "", "", "")
        Dim management_mail As String = ""
        If data.Rows.Count > 0 Then
            management_mail = data.Rows(0)("management_mail").ToString
        End If
        Return management_mail
    End Function

    Function unixMiliSecondsToDatetime(ByVal unix_time As Double) As DateTime
        Dim dt As DateTime = New DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
        dt = dt.AddMilliseconds(unix_time).AddHours(8)
        Return dt
    End Function
End Module
