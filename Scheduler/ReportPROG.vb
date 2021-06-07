Public Class ReportPROG
    Dim dt As DataTable
    Public id_departement As String = ""

    Private Sub ReportPROG_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Dim q As String = "SELECT IF(DATE(pr.requirement_date)<DATE(NOW()),'Overdue','Due Soon') as group_report,
pr.`id_purc_req`,dep.`id_departement`,dep.`departement`,prd.`id_purc_req_det`,it.item_desc,prd.`item_detail`,pr.`is_cancel`,pr.`purc_req_number`,pr.`requirement_date`,po.po_number,po.report_status,po.est_date_receive,prd.`qty` AS qty_pr,IFNULL(po.qty,0) AS qty_po
,IFNULL(rec.qty,0) AS qty_rec,empc.`employee_name`,empc.`email_external`
FROM tb_purc_req_det prd
INNER JOIN tb_purc_req pr ON pr.`id_purc_req`=prd.`id_purc_req` AND pr.`id_report_status`=6 AND pr.`is_cancel`=2 AND prd.`is_close`=2 AND prd.`is_unable_fulfill`=2
INNER JOIN tb_m_departement dep ON dep.`id_departement`=pr.`id_departement`
INNER JOIN tb_m_user usrc ON usrc.`id_user`=pr.`id_user_created`
INNER JOIN tb_m_employee empc ON empc.`id_employee`=usrc.`id_employee`
INNER JOIN tb_item it ON it.id_item=prd.`id_item`
LEFT JOIN 
(
	SELECT pod.id_purc_req_det,SUM(pod.`qty`) AS qty,GROUP_CONCAT(DISTINCT IF(po.is_submit=1,po.purc_order_number,'Created, Not submitted yet') ORDER BY po.purc_order_number) AS po_number
	,GROUP_CONCAT(DISTINCT IF(po.is_submit=1,sts.report_status,'Created, Not submitted') ORDER BY po.purc_order_number) AS report_status,MAX(po.est_date_receive) AS est_date_receive
	FROM tb_purc_order_det pod 
	INNER JOIN tb_purc_order po ON po.id_purc_order=pod.id_purc_order AND po.id_report_status!=5 AND pod.`is_drop`=2
	INNER JOIN tb_lookup_report_status sts ON sts.`id_report_status`=po.`id_report_status`
	GROUP BY pod.id_purc_req_det
) po ON po.id_purc_req_det =prd.`id_purc_req_det`
LEFT JOIN 
(
	SELECT pod.`id_purc_req_det`,SUM(recd.`qty`) AS qty FROM tb_purc_rec_det recd
	INNER JOIN tb_purc_order_det pod ON recd.id_purc_order_det=pod.id_purc_order_det
	INNER JOIN tb_purc_rec rec ON recd.`id_purc_rec`=rec.id_purc_rec
	WHERE rec.`id_report_status`!='5'
	GROUP BY pod.`id_purc_req_det`
)rec ON rec.id_purc_req_det=prd.`id_purc_req_det`
WHERE (prd.`qty`>IFNULL(po.qty,0) OR prd.`qty`>IFNULL(rec.qty,0)) AND DATE(pr.requirement_date)<DATE_ADD(NOW(),INTERVAL 4 DAY) AND pr.`id_departement`='" & id_departement & "'
ORDER BY pr.requirement_date"
        Dim dt As DataTable = execute_query(q, -1, True, "", "", "", "")

        Dim row_baru As DevExpress.XtraReports.UI.XRTableRow = XRDetail
        Dim group_report As String = ""
        For i = 0 To dt.Rows.Count - 1
            insert_row(row_baru, dt, i)
            '
            If Not dt.Rows(i)("group_report").ToString = group_report Then
                insert_row_head(row_baru, dt, i)
                group_report = dt.Rows(i)("group_report").ToString
            End If
        Next
    End Sub

    Sub insert_row_head(ByRef row_det As DevExpress.XtraReports.UI.XRTableRow, ByVal dt As DataTable, ByVal row_i As Integer)
        Dim font_row_style As New Font(XTDetail.Font.FontFamily, XTDetail.Font.Size, FontStyle.Bold)

        Dim row As New DevExpress.XtraReports.UI.XRTableRow

        row = XTDetail.InsertRowAbove(row_det)

        row.Borders = DevExpress.XtraPrinting.BorderSide.All
        row.BorderWidth = 1
        row.HeightF = 15
        row.Font = font_row_style

        'No
        Dim gr As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(0)
        gr.Text = dt.Rows(row_i)("group_report").ToString
        gr.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        gr.Font = font_row_style

        'pr number
        Dim pr_number As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(1)
        pr_number.Text = ""
        pr_number.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        pr_number.Font = font_row_style

        'requirement date
        Dim req_date As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(2)
        req_date.Text = ""
        req_date.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        req_date.Font = font_row_style

        'po number
        Dim po_number As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(3)
        po_number.Text = ""
        po_number.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        po_number.Font = font_row_style

        'est rec date
        Dim est_date_receive As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(4)
        est_date_receive.Text = ""
        est_date_receive.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        est_date_receive.Font = font_row_style

        'item
        Dim item_desc As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(5)
        item_desc.Text = ""
        item_desc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        item_desc.Font = font_row_style

        'item desc
        Dim item_detail As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(6)
        item_detail.Text = ""
        item_detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        item_detail.Font = font_row_style

        'qty req
        Dim pr_qty As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(7)
        pr_qty.Text = ""
        pr_qty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        pr_qty.Font = font_row_style

        'qty po
        Dim po_qty As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(8)
        po_qty.Text = ""
        po_qty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        po_qty.Font = font_row_style

        'qty rec
        Dim rec_qty As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(9)
        rec_qty.Text = ""
        rec_qty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        rec_qty.Font = font_row_style

        'req by
        Dim req_by As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(10)
        req_by.Text = ""
        req_by.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        req_by.Font = font_row_style

        row.DeleteCell(req_date)
        row.DeleteCell(req_by)
        row.DeleteCell(pr_qty)
        row.DeleteCell(rec_qty)
        row.DeleteCell(po_qty)
        row.DeleteCell(item_detail)
        row.DeleteCell(item_desc)
        row.DeleteCell(est_date_receive)
        row.DeleteCell(pr_number)
        row.DeleteCell(po_number)
    End Sub

    Sub insert_row(ByRef row As DevExpress.XtraReports.UI.XRTableRow, ByVal dt As DataTable, ByVal row_i As Integer)
        Dim font_row_style As New Font(XTDetail.Font.FontFamily, XTDetail.Font.Size - 1, FontStyle.Regular)

        If Not row_i = 0 Then
            row = XTDetail.InsertRowBelow(row)
        End If

        row.Borders = DevExpress.XtraPrinting.BorderSide.All
        row.BorderWidth = 1
        row.HeightF = 15
        row.Font = font_row_style

        'No
        Dim no As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(0)
        no.Text = row_i + 1
        no.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        no.Font = font_row_style

        'pr number
        Dim pr_number As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(1)
        pr_number.Text = dt.Rows(row_i)("purc_req_number").ToString
        pr_number.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        pr_number.Font = font_row_style

        'req date
        Dim requirement_date As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(2)
        requirement_date.Text = Date.Parse(dt.Rows(row_i)("requirement_date").ToString).ToString("dd MMMM yyyy")
        requirement_date.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        requirement_date.Font = font_row_style

        'po number
        Dim po_number As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(3)
        po_number.Text = dt.Rows(row_i)("po_number").ToString
        po_number.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        po_number.Font = font_row_style

        'est rec date
        Dim est_date_receive As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(4)

        If Not dt.Rows(row_i)("est_date_receive").ToString = "" Then
            est_date_receive.Text = Date.Parse(dt.Rows(row_i)("est_date_receive").ToString).ToString("dd MMMM yyyy")
        Else
            est_date_receive.Text = ""
        End If

        est_date_receive.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        est_date_receive.Font = font_row_style

        'item
        Dim item_desc As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(5)
        item_desc.Text = dt.Rows(row_i)("item_desc").ToString
        item_desc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        item_desc.Font = font_row_style

        'item desc
        Dim item_detail As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(6)
        item_detail.Text = dt.Rows(row_i)("item_detail").ToString
        item_detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        item_detail.Font = font_row_style

        'qty req
        Dim pr_qty As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(7)
        pr_qty.Text = Decimal.Parse(dt.Rows(row_i)("qty_pr").ToString).ToString("N2")
        pr_qty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        pr_qty.Font = font_row_style

        'qty po
        Dim po_qty As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(8)
        po_qty.Text = Decimal.Parse(dt.Rows(row_i)("qty_po").ToString).ToString("N2")
        po_qty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        po_qty.Font = font_row_style

        'qty rec
        Dim rec_qty As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(9)
        rec_qty.Text = Decimal.Parse(dt.Rows(row_i)("qty_rec").ToString).ToString("N2")
        rec_qty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        rec_qty.Font = font_row_style

        'req by
        Dim req_by As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(10)
        req_by.Text = dt.Rows(row_i)("employee_name").ToString
        req_by.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        req_by.Font = font_row_style
    End Sub
End Class