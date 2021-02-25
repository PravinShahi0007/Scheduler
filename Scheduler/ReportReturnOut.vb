Public Class ReportReturnOut
    Private Sub ReportReturnOut_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Dim q As String = "SELECT h.id_report_status, h.report_status, a.id_prod_order_ret_out, a.prod_order_ret_out_date, a.prod_order_ret_out_due_date, a.prod_order_ret_out_note,  
a.prod_order_ret_out_number , b.prod_order_number, c.id_season, c.season, CONCAT(e.comp_number,' - ',e.comp_name) AS comp_from, CONCAT(g.comp_number,' - ',g.comp_name) AS comp_to, dsg.design_code AS `code`, dsg.design_display_name AS `name`, SUM(ad.prod_order_ret_out_det_qty) AS `qty` 
,IFNULL(retin.qty_ret_in,0) AS qty_retin
,SUM(ad.prod_order_ret_out_det_qty)-IFNULL(retin.qty_ret_in,0) AS diff_qty
,DATEDIFF(DATE(NOW()),a.`prod_order_ret_out_due_date`) AS overdue
FROM tb_prod_order_ret_out a 
INNER JOIN tb_prod_order_ret_out_det ad ON ad.id_prod_order_ret_out = a.id_prod_order_ret_out 
INNER JOIN tb_prod_order b ON a.id_prod_order = b.id_prod_order 
INNER JOIN tb_prod_demand_design b1 ON b.id_prod_demand_design = b1.id_prod_demand_design 
INNER JOIN tb_m_design dsg ON dsg.id_design = b1.id_design 
INNER JOIN tb_prod_demand b2 ON b2.id_prod_demand = b1.id_prod_demand 
INNER JOIN tb_season c ON b2.id_season = c.id_season 
INNER JOIN tb_m_comp_contact d ON d.id_comp_contact = a.id_comp_contact_from 
INNER JOIN tb_m_comp e ON d.id_comp = e.id_comp 
INNER JOIN tb_m_comp_contact f ON f.id_comp_contact = a.id_comp_contact_to 
INNER JOIN tb_m_comp g ON f.id_comp = g.id_comp 
INNER JOIN tb_lookup_report_status h ON a.id_report_status = h.id_report_status 
LEFT JOIN 
(
	SELECT id_prod_order_ret_out,SUM(prod_order_ret_in_det_qty) AS qty_ret_in
	FROM `tb_prod_order_ret_in_det` retid
	INNER JOIN `tb_prod_order_ret_in` reti ON reti.`id_prod_order_ret_in`=retid.`id_prod_order_ret_in`
	WHERE reti.`id_report_status`!=5
	GROUP BY reti.`id_prod_order_ret_out`
)retin ON retin.id_prod_order_ret_out=a.`id_prod_order_ret_out`
WHERE a.`prod_order_ret_out_due_date` >= '2020-07-01' AND a.`prod_order_ret_out_due_date` <= DATE_ADD(DATE(NOW()),INTERVAL 3 DAY) AND a.`id_report_status`=6
GROUP BY a.id_prod_order_ret_out 
HAVING qty > qty_retin
ORDER BY a.id_prod_order_ret_out DESC"
        Dim dt As DataTable = execute_query(q, -1, True, "", "", "", "")
        GCTemp.DataSource = dt
        GVTemp.BestFitColumns()
        GVTemp.ExpandAllGroups()
    End Sub
End Class