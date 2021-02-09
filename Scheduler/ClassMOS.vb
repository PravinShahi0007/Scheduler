Public Class ClassMOS
    Sub insertLog(ByVal sch As String, ByVal log As String)
        Dim query As String = "INSERT INTO tb_ol_store_status_log(schedule, log_time, log) VALUES('" + sch + "', NOW(), '" + addSlashes(log) + "'); "
        execute_non_query(query, True, "", "", "", "")
    End Sub
End Class
