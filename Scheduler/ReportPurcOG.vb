Public Class ReportPurcOG
    Dim dt As DataTable

    Private Sub ReportPurcOG_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Dim q As String = "SELECT IF(DATE(po.est_date_receive)<DATE(NOW()),'Overdue','Due Soon') as group_report,po.`id_purc_order`,po.`purc_order_number`,po.`date_created`,po.`est_date_receive`,reqd.`id_item`,it.`item_desc`,reqd.`item_detail`,req.id_user_created,emp.`employee_name` AS req_by,req.`requirement_date`
,IFNULL(rec.qty,0) AS rec_qty,reqd.`qty` AS req_qty,pod.`qty` AS po_qty,c.`comp_name`
FROM tb_purc_order_det pod
INNER JOIN tb_purc_req_det reqd ON reqd.`id_purc_req_det`=pod.`id_purc_req_det`
INNER JOIN tb_purc_req req ON req.`id_purc_req`=reqd.`id_purc_req` AND req.`id_report_status`='6'
INNER JOIN tb_purc_order po ON po.id_purc_order=pod.id_purc_order AND po.`id_report_status`='6' AND po.`is_close_rec`=2
INNER JOIN tb_item it ON it.`id_item`=reqd.`id_item`
INNER JOIN tb_m_uom uom ON uom.id_uom=it.id_uom
INNER JOIN tb_m_comp_contact cc ON po.`id_comp_contact`=cc.`id_comp_contact`
INNER JOIN tb_m_comp c ON c.`id_comp`=cc.`id_comp`
INNER JOIN tb_m_user usr_req ON usr_req.`id_user`=req.id_user_created
INNER JOIN tb_m_employee emp ON emp.`id_employee`=usr_req.`id_employee`
LEFT JOIN
(
	SELECT pod.`id_purc_order_det`,pod.`id_purc_req_det`,SUM(recd.`qty`) AS qty 
	FROM tb_purc_rec_det recd
	INNER JOIN tb_purc_order_det pod ON recd.id_purc_order_det=pod.id_purc_order_det
	INNER JOIN tb_purc_rec rec ON recd.`id_purc_rec`=rec.id_purc_rec
	WHERE rec.`id_report_status`!='5'
	GROUP BY pod.`id_purc_req_det`
)rec  ON rec.id_purc_req_det=reqd.`id_purc_req_det`
WHERE DATE(po.est_date_receive)<DATE_ADD(NOW(),INTERVAL 4 DAY) AND reqd.`qty`>IFNULL(rec.qty,0)
GROUP BY reqd.`id_purc_req_det`
ORDER BY po.est_date_receive"
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

        'po number
        Dim po_number As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(1)
        po_number.Text = ""
        po_number.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        po_number.Font = font_row_style

        'vendor
        Dim comp_name As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(2)
        comp_name.Text = ""
        comp_name.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        comp_name.Font = font_row_style

        'est rec date
        Dim est_date_receive As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(3)
        est_date_receive.Text = ""
        est_date_receive.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        est_date_receive.Font = font_row_style

        'item
        Dim item_desc As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(4)
        item_desc.Text = ""
        item_desc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        item_desc.Font = font_row_style

        'item desc
        Dim item_detail As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(5)
        item_detail.Text = ""
        item_detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        item_detail.Font = font_row_style

        'qty po
        Dim po_qty As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(6)
        po_qty.Text = ""
        po_qty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        po_qty.Font = font_row_style

        'qty rec
        Dim rec_qty As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(7)
        rec_qty.Text = ""
        rec_qty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        rec_qty.Font = font_row_style

        'req by
        Dim req_by As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(8)
        req_by.Text = ""
        req_by.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        req_by.Font = font_row_style

        'req date
        Dim requirement_date As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(9)
        requirement_date.Text = ""
        requirement_date.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        requirement_date.Font = font_row_style

        row.DeleteCell(requirement_date)
        row.DeleteCell(req_by)
        row.DeleteCell(rec_qty)
        row.DeleteCell(po_qty)
        row.DeleteCell(item_detail)
        row.DeleteCell(item_desc)
        row.DeleteCell(est_date_receive)
        row.DeleteCell(comp_name)
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

        'po number
        Dim po_number As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(1)
        po_number.Text = dt.Rows(row_i)("purc_order_number").ToString
        po_number.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        po_number.Font = font_row_style

        'vendor
        Dim comp_name As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(2)
        comp_name.Text = dt.Rows(row_i)("comp_name").ToString
        comp_name.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        comp_name.Font = font_row_style

        'est rec date
        Dim est_date_receive As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(3)
        est_date_receive.Text = Date.Parse(dt.Rows(row_i)("est_date_receive").ToString).ToString("dd MMMM yyyy")
        est_date_receive.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        est_date_receive.Font = font_row_style

        'item
        Dim item_desc As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(4)
        item_desc.Text = dt.Rows(row_i)("item_desc").ToString
        item_desc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        item_desc.Font = font_row_style

        'item desc
        Dim item_detail As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(5)
        item_detail.Text = dt.Rows(row_i)("item_detail").ToString
        item_detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        item_detail.Font = font_row_style

        'qty po
        Dim po_qty As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(6)
        po_qty.Text = Decimal.Parse(dt.Rows(row_i)("po_qty").ToString).ToString("N2")
        po_qty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        po_qty.Font = font_row_style

        'qty rec
        Dim rec_qty As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(7)
        rec_qty.Text = Decimal.Parse(dt.Rows(row_i)("rec_qty").ToString).ToString("N2")
        rec_qty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        rec_qty.Font = font_row_style

        'req by
        Dim req_by As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(8)
        req_by.Text = dt.Rows(row_i)("req_by").ToString
        req_by.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        req_by.Font = font_row_style

        'req date
        Dim requirement_date As DevExpress.XtraReports.UI.XRTableCell = row.Cells.Item(9)
        requirement_date.Text = Date.Parse(dt.Rows(row_i)("requirement_date").ToString).ToString("dd MMMM yyyy")
        requirement_date.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        requirement_date.Font = font_row_style
    End Sub
End Class