Public Class ReportMkdEOS
    Public Shared id As String = "-1"
    Public Shared id_store As String = "-1"
    Private Sub ReportMkdEOS_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles MyBase.BeforePrint
        'printed date & approved date
        Dim qpd As String = "SELECT pp.id_pp_change,  pp.number, 
        DATE_FORMAT(pp.created_date,'%d %M %Y') AS `created_date`, 
        DATE_FORMAT(pp.effective_date,'%d %M %Y') AS `start_date`, 
        DATE_FORMAT(pp.plan_end_date,'%d %M %Y') AS `end_date`,
        cg.description AS `store_name`, DATE_FORMAT(NOW(), '%d/%m/%Y %H:%i') AS `printed_date`
        FROM tb_pp_change pp 
        JOIN tb_m_comp_group cg ON cg.id_comp_group=" + id_store + "
        WHERE pp.id_pp_change=" + id + "
        LIMIT 1 "
        Dim dpd As DataTable = execute_query(qpd, -1, True, "", "", "", "")
        DataSource = dpd

        'detail
        Dim qdet As String = "SELECT p.product_full_code AS `kode_lengkap`, d.design_code AS `kode`, sz.display_name	AS `size`, 
        cd.class, d.design_display_name AS `deskripsi`, cd.color AS `warna`, cd.color_desc AS `deskripsi_warna`,
        normal_prc.design_price_normal AS `harga_normal`,CONCAT(CAST(ppd.propose_discount AS DECIMAL(10,0)),'%')AS `disc`, CAST(ppd.propose_price_final AS DECIMAL(15,0)) AS `harga_update`,
        IF(ppd.id_extended_eos=1, UPPER(exos.extended_eos), 'EOSS') AS `ket`
        FROM tb_pp_change_det ppd
        INNER JOIN tb_m_design d ON d.id_design = ppd.id_design
        LEFT JOIN (
	        SELECT dc.id_design, 
	        MAX(CASE WHEN cd.id_code=32 THEN cd.id_code_detail END) AS `id_division`,
	        MAX(CASE WHEN cd.id_code=32 THEN cd.code_detail_name END) AS `division`,
	        MAX(CASE WHEN cd.id_code=30 THEN cd.id_code_detail END) AS `id_class`,
	        MAX(CASE WHEN cd.id_code=30 THEN cd.display_name END) AS `class`,
	        MAX(CASE WHEN cd.id_code=14 THEN cd.id_code_detail END) AS `id_color`,
	        MAX(CASE WHEN cd.id_code=14 THEN cd.display_name END) AS `color`,
	        MAX(CASE WHEN cd.id_code=14 THEN cd.code_detail_name END) AS `color_desc`,
	        MAX(CASE WHEN cd.id_code=43 THEN cd.id_code_detail END) AS `id_sht`,
	        MAX(CASE WHEN cd.id_code=43 THEN cd.code_detail_name END) AS `sht`
	        FROM tb_m_design_code dc
	        INNER JOIN tb_m_code_detail cd ON cd.id_code_detail = dc.id_code_detail 
	        AND cd.id_code IN (32,30,14, 43)
	        GROUP BY dc.id_design
        ) cd ON cd.id_design = d.id_design
        INNER JOIN tb_m_product p ON p.id_design = d.id_design
        INNER JOIN tb_m_product_code pc ON pc.id_product = p.id_product
        INNER JOIN tb_m_code_detail sz ON sz.id_code_detail = pc.id_code_detail
        LEFT JOIN tb_m_product_void pv ON pv.id_product = p.id_product
        INNER JOIN tb_lookup_extended_eos exos ON exos.id_extended_eos = ppd.id_extended_eos
        LEFT JOIN (
		    SELECT p.id_design_price AS `id_design_price_normal`,
		    p.design_price AS `design_price_normal`, p.id_design
		    FROM tb_m_design_price p
		    INNER JOIN tb_lookup_design_price_type pt ON pt.id_design_price_type = p.id_design_price_type
		    INNER JOIN tb_lookup_design_cat cat ON cat.id_design_cat = pt.id_design_cat
		    WHERE p.id_design_price IN (
			    SELECT MAX(p.id_design_price) FROM tb_m_design_price p
			    WHERE  p.id_design_price_type=1
			    GROUP BY p.id_design
		    )
	    ) normal_prc ON normal_prc.id_design = ppd.id_design
        WHERE ppd.id_pp_change=" + id + " AND ISNULL(pv.id_product) AND (ppd.propose_price_final>0 AND !ISNULL(ppd.propose_price_final))
        ORDER BY ket ASC, class ASC, deskripsi ASC, kode_lengkap ASC  "
        Dim ddet As DataTable = execute_query(qdet, -1, True, "", "", "", "")
        GCData.DataSource = ddet
        GVData.BestFitColumns()

        'opt
        GridColumnkode_lengkap.Width = 100
        GridColumnkode.Width = 80
        GridColumnsize.Width = 40
        GridColumnclass.Width = 50
        GridColumndeskripsi.Width = 120
        GridColumnwarna.Width = 60
        GridColumndeskripsi_warna.Width = 60
        GridColumnharga_normal.Width = 75
        GridColumndisc.Width = 40
        GridColumnharga_update.Width = 75
        GridColumnket.Width = 50
    End Sub

    Sub printStyleReport(ByVal gv As DevExpress.XtraGrid.Views.Grid.GridView)
        'gv.OptionsPrint.UsePrintStyles = True

        'gv.AppearancePrint.HeaderPanel.BackColor = Color.Transparent
        'gv.AppearancePrint.HeaderPanel.ForeColor = Color.Black
        'gv.AppearancePrint.HeaderPanel.Font = New Font("Tahoma", 11, FontStyle.Bold)

        'gv.AppearancePrint.Row.Font = New Font("Tahoma", 11, FontStyle.Regular)

        'gv.OptionsPrint.ExpandAllDetails = True
        'gv.OptionsPrint.UsePrintStyles = True
        'gv.OptionsPrint.PrintDetails = True
        'gv.OptionsPrint.PrintFooter = True
    End Sub
End Class