Public Class ReportReminderSerahTerima
    Private Sub ReportReminderSerahTerima_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Dim q As String = "SELECT pl.`id_pl_prod_order`,po.`prod_order_number`,pl.`pl_prod_order_number`,pl.`pl_prod_order_date`,d.`design_code`,d.`design_display_name`,rec.`id_pl_prod_order_rec`,pl.`id_comp_contact_to`
,pl.`complete_date`,DATEDIFF(DATE(NOW()),DATE(pl.`complete_date`)) AS diff_date_serah_terima
,cat.`pl_category`,det.qty AS qty_pl
FROM `tb_pl_prod_order` pl 
INNER JOIN 
(
	SELECT id_pl_prod_order,SUM(pl_prod_order_det_qty) AS qty FROM `tb_pl_prod_order_det`
	GROUP BY id_pl_prod_order
)det ON det.id_pl_prod_order=pl.`id_pl_prod_order`
INNER JOIN tb_prod_order po ON po.`id_prod_order`=pl.`id_prod_order`
INNER JOIN tb_prod_demand_design pdd ON pdd.`id_prod_demand_design`=po.`id_prod_demand_design`
INNER JOIN tb_m_design d ON d.`id_design`=pdd.`id_design`
INNER JOIN tb_m_comp_contact cc ON cc.`id_comp_contact`=pl.`id_comp_contact_to`
INNER JOIN tb_m_comp c ON c.`id_comp`=cc.`id_comp` AND c.id_departement='6'
INNER JOIN tb_lookup_pl_category cat ON cat.`id_pl_category`=pl.`id_pl_category`
LEFT JOIN `tb_pl_prod_order_rec` rec ON rec.`id_pl_prod_order`=pl.`id_pl_prod_order` AND rec.`id_report_status`!=5
WHERE pl.`id_report_status`=6 AND ISNULL(rec.`id_pl_prod_order_rec`)
AND DATEDIFF(DATE(NOW()),DATE(pl.`complete_date`))>18"
        Dim dt As DataTable = execute_query(q, -1, True, "", "", "", "")
        GCReport.DataSource = dt
        GVReport.BestFitColumns()
        GVReport.ExpandAllGroups()
    End Sub
End Class