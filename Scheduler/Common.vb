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
End Module
