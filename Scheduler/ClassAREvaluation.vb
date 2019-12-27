Public Class ClassAREvaluation
    Function listNoticeInvoice(ByVal typ As String) As DataTable
        'query amount
        Dim q_amo As String = ""
        Dim q_group_by As String = ""
        If typ = "1" Then
            q_amo = "CAST(IF(typ.`is_receive_payment`=2,-1,1) * ((sp.`sales_pos_total`*((100-sp.sales_pos_discount)/100))-sp.`sales_pos_potongan`) AS DECIMAL(15,2))-IFNULL(pyd.`value`,0.00) AS amount "
            q_group_by = "sp.`id_sales_pos` "
        ElseIf typ = "2" Then
            q_amo = "SUM(CAST(IF(typ.`is_receive_payment`=2,-1,1) * ((sp.`sales_pos_total`*((100-sp.sales_pos_discount)/100))-sp.`sales_pos_potongan`) AS DECIMAL(15,2))-IFNULL(pyd.`value`,0.00)) AS amount "
            q_group_by = "c.id_comp_group "
        End If

        Dim query As String = "SELECT cg.id_comp_group, cg.description AS `group`,cho.comp_name AS `group_company`, CONCAT(c.comp_number,' - ', c.comp_name) AS `store`, sp.sales_pos_number,
        CONCAT(DATE_FORMAT(sp.sales_pos_start_period,'%d-%m-%y'),' s/d ', DATE_FORMAT(sp.sales_pos_end_period,'%d-%m-%y')) AS `period`,
        DATE_FORMAT(sp.sales_pos_due_date,'%d-%m-%y') AS `sales_pos_due_date`,
        " + q_amo + "
        FROM tb_sales_pos sp 
        INNER JOIN tb_m_comp_contact cc ON cc.`id_comp_contact`= IF(sp.id_memo_type=8 OR sp.id_memo_type=9, sp.id_comp_contact_bill,sp.`id_store_contact_from`)
        INNER JOIN tb_lookup_report_mark_type rmt ON rmt.report_mark_type=sp.report_mark_type
        INNER JOIN tb_m_comp c ON c.`id_comp`=cc.`id_comp`
        INNER JOIN tb_m_comp_group cg ON cg.id_comp_group = c.id_comp_group
        INNER JOIN tb_m_comp cho ON cho.id_comp = cg.id_comp
        INNER JOIN tb_lookup_memo_type typ ON typ.`id_memo_type`=sp.`id_memo_type`
        LEFT JOIN (
           SELECT pyd.id_report, pyd.report_mark_type, 
           COUNT(IF(py.id_report_status!=5 AND py.id_report_status!=6,py.id_rec_payment,NULL)) AS `total_pending`,
           SUM(pyd.value) AS  `value`
           FROM tb_rec_payment_det pyd
           INNER JOIN tb_rec_payment py ON py.`id_rec_payment`=pyd.`id_rec_payment`
           WHERE py.`id_report_status`=6
           GROUP BY pyd.id_report, pyd.report_mark_type
        ) pyd ON pyd.id_report = sp.id_sales_pos AND pyd.report_mark_type = sp.report_mark_type
        WHERE sp.`id_report_status`='6' AND sp.is_close_rec_payment=2 
        AND DATEDIFF(NOW(),sp.`sales_pos_due_date`)=-5
        GROUP BY " + q_group_by + "
        HAVING amount>0 
        ORDER BY id_sales_pos ASC "
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")
        Return data
    End Function
End Class