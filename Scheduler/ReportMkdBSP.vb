Public Class ReportMkdBSP
    Public Shared id As String = "-1"
    Public Shared id_store As String = "-1"

    Private Sub ReportMkdBSP_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles MyBase.BeforePrint
        'printed date & approved date
        Dim qpd As String = "SELECT c.id_comp, CONCAT(c.comp_number, ' - ', c.comp_name) AS `store`, c.address_primary AS `store_address`, 
        bsp.start_date,DATE_FORMAT(bsp.start_date,'%d %M %Y') AS `start_date_display`,DATE_FORMAT(bsp.end_date,'%d %M %Y') AS `end_date_display`, DATE_FORMAT(NOW(),'%d %M %Y') AS `created_date_display`
        FROM tb_bsp bsp
        INNER JOIN tb_m_comp c ON c.id_comp = bsp.id_comp
        WHERE bsp.id_report_status=6 AND bsp.id_comp='" + id_store + "' LIMIT 1 "
        Dim dpd As DataTable = execute_query(qpd, -1, True, "", "", "", "")
        DataSource = dpd

        'detail
        Dim start_date_ori As String = Date.Parse(dpd.Rows(0)("start_date")).ToString("yyyy-MM-dd")
        Dim qdet As String = "SELECT c.id_comp, c.comp_number, c.comp_name,
        p.product_full_code, dsg.design_code, sz.display_name AS `size`, cd.class, dsg.design_display_name AS `name`, cd.color, cd.color_desc,
        LEFT(prc.design_cat,1) AS `status`, SUM(dd.pl_sales_order_del_det_qty) AS `qty`, 
        normal_prc.design_price AS `design_price_normal`, prc.design_price, ((normal_prc.design_price-prc.design_price)/normal_prc.design_price)*100 AS `disc`,
        cd.division, SUBSTRING(cd.class_desc ,(LENGTH(cd.division)+2)) AS `ket`, ss.season
        FROM tb_pl_sales_order_del_det dd
        INNER JOIN tb_pl_sales_order_del d ON d.id_pl_sales_order_del = dd.id_pl_sales_order_del
        INNER JOIN tb_m_product p ON p.id_product = dd.id_product
        INNER JOIN tb_m_product_code pc ON pc.id_product = p.id_product
        LEFT JOIN tb_m_product_void pv ON pv.id_product = p.id_product
        INNER JOIN tb_m_code_detail sz ON sz.id_code_detail = pc.id_code_detail
        INNER JOIN tb_m_design dsg ON dsg.id_design = p.id_design
        INNER JOIN tb_season ss ON ss.id_season = dsg.id_season
        LEFT JOIN (
          SELECT dc.id_design, 
          MAX(CASE WHEN cd.id_code=32 THEN cd.id_code_detail END) AS `id_division`,
          MAX(CASE WHEN cd.id_code=32 THEN cd.code_detail_name END) AS `division`,
          MAX(CASE WHEN cd.id_code=30 THEN cd.id_code_detail END) AS `id_class`,
          MAX(CASE WHEN cd.id_code=30 THEN cd.display_name END) AS `class`,
          MAX(CASE WHEN cd.id_code=30 THEN cd.code_detail_name END) AS `class_desc`,
          MAX(CASE WHEN cd.id_code=14 THEN cd.id_code_detail END) AS `id_color`,
          MAX(CASE WHEN cd.id_code=14 THEN cd.display_name END) AS `color`,
          MAX(CASE WHEN cd.id_code=14 THEN cd.code_detail_name END) AS `color_desc`,
          MAX(CASE WHEN cd.id_code=43 THEN cd.id_code_detail END) AS `id_sht`,
          MAX(CASE WHEN cd.id_code=43 THEN cd.code_detail_name END) AS `sht`
          FROM tb_m_design_code dc
          INNER JOIN tb_m_code_detail cd ON cd.id_code_detail = dc.id_code_detail 
          AND cd.id_code IN (32,30,14, 43)
          GROUP BY dc.id_design
        ) cd ON cd.id_design = dsg.id_design
        LEFT JOIN (
	        SELECT p.* , LEFT(pt.design_price_type,1) AS `price_type`, cat.id_design_cat, LEFT(cat.design_cat,1) AS `design_cat`
	        FROM tb_m_design_price p
	        INNER JOIN tb_lookup_design_price_type pt ON pt.id_design_price_type = p.id_design_price_type
	        INNER JOIN tb_lookup_design_cat cat ON cat.id_design_cat = pt.id_design_cat
	        WHERE p.id_design_price IN (
		        SELECT MAX(p.id_design_price) FROM tb_m_design_price p
		        WHERE p.design_price_start_date<='" + start_date_ori + "' AND is_active_wh=1 AND is_design_cost=0
		        GROUP BY p.id_design
	        )
        ) prc ON prc.id_design = dsg.id_design
        LEFT JOIN (
	        SELECT prc.id_design, prc.id_design_price, prc.design_price, prc.id_design_cat,prc.design_cat
	        FROM (
		        SELECT prc.id_design, prc.id_design_price, prc.design_price, cat.id_design_cat, cat.design_cat
		        FROM tb_m_design_price prc
		        INNER JOIN tb_lookup_design_price_type pt ON pt.id_design_price_type = prc.id_design_price_type
		        INNER JOIN tb_lookup_design_cat cat ON cat.id_design_cat = pt.id_design_cat
		        WHERE design_price_start_date<=NOW() AND is_active_wh=1 AND is_design_cost=0
		        AND prc.id_design_price_type=1
		        ORDER BY design_price_start_date DESC, id_design_price DESC
	        ) prc
	        GROUP BY id_design
        ) normal_prc ON normal_prc.id_design = dsg.id_design
        INNER JOIN tb_m_comp_contact cc ON cc.id_comp_contact = d.id_store_contact_to
        INNER JOIN tb_m_comp c ON c.id_comp = cc.id_comp
        WHERE d.id_report_status=6 AND c.id_comp=" + id_store + " 
        AND ISNULL(pv.id_product)
        GROUP BY p.id_product
        ORDER BY class ASC, name ASC, product_full_code ASC "
        Dim ddet As DataTable = execute_query(qdet, -1, True, "", "", "", "")
        GCBSP.DataSource = ddet
        GVBSP.BestFitColumns()

        'opt
        GridColumnkode_lengkap.Width = 90
        GridColumnkode.Width = 70
        GridColumnsize.Width = 35
        GridColumnclass.Width = 45
        GridColumndeskripsi.Width = 150
        GridColumnwarna.Width = 50
        GridColumndeskripsi_warna.Width = 60
        GridColumnstatus.Width = 40
        GridColumnqty.Width = 50
        GridColumnharga_normal.Width = 75
        GridColumnharga_update.Width = 75
        GridColumndisc.Width = 40
        GridColumndivision.Width = 50
        GridColumnket.Width = 60
        GridColumnseason.Width = 50
    End Sub
End Class