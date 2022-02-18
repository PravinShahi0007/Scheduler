Public Class ClassLineList
    Function dataNotifSummary() As DataTable
        Dim query As String = "SELECT ss.season, 
        GROUP_CONCAT(DISTINCT rmt.report_mark_type_name ORDER BY rmt.report_mark_type_name ASC SEPARATOR ', ') AS `updated_trans`
        FROM tb_log_line_list ll 
        INNER JOIN tb_m_design d ON d.id_design = ll.id_design
        INNER JOIN tb_season ss ON ss.id_season = d.id_season 
        INNER JOIN tb_season_delivery sd ON sd.id_season = ss.id_season
        INNER JOIN tb_lookup_report_mark_type rmt ON rmt.report_mark_type = ll.report_mark_type
        WHERE DATE(ll.log_date)=DATE(NOW())
        GROUP BY d.id_season
        ORDER BY sd.delivery_date ASC "
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")
        Return data
    End Function
End Class
