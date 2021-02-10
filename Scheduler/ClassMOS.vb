Public Class ClassMOS
    Sub insertLog(ByVal sch As String, ByVal log As String)
        Dim query As String = "INSERT INTO tb_ol_store_status_log(schedule, log_time, log) VALUES('" + sch + "', NOW(), '" + addSlashes(log) + "'); "
        execute_non_query(query, True, "", "", "", "")
    End Sub

    Sub insertStatusOrder(ByVal id_sales_order_det_par As String, ByVal status_par As String, ByVal status_date_par As String)
        Dim query_ins As String = "INSERT IGNORE INTO tb_sales_order_det_status(id_sales_order_det, status, status_date, input_status_date) 
        VALUES('" + id_sales_order_det_par + "', '" + status_par + "', '" + status_date_par + "', NOW()) "
        execute_non_query(query_ins, True, "", "", "", "")
    End Sub
End Class
