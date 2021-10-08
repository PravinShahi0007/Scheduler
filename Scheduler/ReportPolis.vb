Public Class ReportPolis
    Private Sub ReportPolis_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Dim q As String = "SELECT p.id_polis,DATEDIFF(p.end_date,DATE(NOW())) AS expired_in,pol_by.comp_name AS comp_name_polis,CONCAT(c.comp_number,' - ',c.`comp_name`) AS polis_object,c.`address_primary` AS polis_object_location,p.`number` AS polis_number,d.`description` AS polis_untuk,p.`premi`,p.`start_date`,p.`end_date` 
FROM tb_polis p 
INNER JOIN tb_m_comp c ON c.`id_comp`=p.`id_reff` AND p.`id_polis_cat`=1
INNER JOIN tb_m_comp pol_by ON pol_by.id_comp=p.id_polis_by
INNER JOIN `tb_lookup_desc_premi` d ON d.`id_desc_premi`=p.`id_desc_premi`
WHERE p.`is_active`=1 AND DATEDIFF(p.end_date,DATE(NOW()))<60"
        Dim dt As DataTable = execute_query(q, -1, True, "", "", "", "")
        GCPolisToko.DataSource = dt
        GVPolisToko.BestFitColumns()
        GVPolisToko.ExpandAllGroups()
    End Sub

    Private Sub GVPolisToko_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles GVPolisToko.CellMerge
        If (e.Column.FieldName = "polis_object" Or e.Column.FieldName = "nilai_total" Or e.Column.FieldName = "polis_object_location" Or e.Column.FieldName = "expired_in" Or e.Column.FieldName = "comp_name_polis" Or e.Column.FieldName = "start_date" Or e.Column.FieldName = "end_date") Then
            Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
            Dim val1 As String = view.GetRowCellValue(e.RowHandle1, "id_polis")
            Dim val2 As String = view.GetRowCellValue(e.RowHandle2, "id_polis")

            e.Merge = (val1.ToString = val2.ToString)
            e.Handled = True
        Else
            e.Merge = False
            e.Handled = True
        End If
    End Sub

    Private Sub GVPolisToko_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GVPolisToko.RowStyle
        If GVPolisToko.GetRowCellValue(e.RowHandle, "expired_in") < 0 Then
            e.Appearance.BackColor = Color.Salmon
            e.Appearance.BackColor2 = Color.Salmon
            e.Appearance.ForeColor = Color.Black
        ElseIf GVPolisToko.GetRowCellValue(e.RowHandle, "expired_in") < 60 Then
            e.Appearance.BackColor = Color.LightYellow
            e.Appearance.BackColor2 = Color.LightYellow
            e.Appearance.ForeColor = Color.Black
        Else
            e.Appearance.BackColor = Color.White
            e.Appearance.BackColor2 = Color.White
            e.Appearance.ForeColor = Color.Black
        End If
    End Sub
End Class