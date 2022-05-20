Public Class FormResendNotifLineList
    Private Sub FormResendNotifLineList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        viewData()
    End Sub

    Private Sub FormResendNotifLineList_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Dispose()
    End Sub

    Sub viewData()
        Cursor = Cursors.WaitCursor
        Dim query As String = "SELECT l.id_log_line_list_mail, l.log_note, l.log_date, l.check_line_list_date, l.id_line_list_notif_type 
        FROM tb_log_line_list_mail l WHERE l.is_resend=2
        ORDER BY l.check_line_list_date ASC "
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")
        GCData.DataSource = data
        Cursor = Cursors.Default
    End Sub

    Private Sub BtnResend_Click(sender As Object, e As EventArgs) Handles BtnResend.Click
        If GVData.RowCount > 0 And GVData.FocusedRowHandle >= 0 Then
            Dim id_log_line_list_mail As String = GVData.GetFocusedRowCellValue("id_log_line_list_mail").ToString
            Dim id_line_list_notif_type As String = GVData.GetFocusedRowCellValue("id_line_list_notif_type").ToString
            Dim check_date As String = DateTime.Parse(GVData.GetFocusedRowCellValue("check_line_list_date").ToString).ToString("yyyy-MM-dd")
            If id_line_list_notif_type = "1" Then
                Try
                    Dim ll As New ClassLineList()
                    Dim dtl As DataTable = ll.dataNotifSummary(check_date)
                    If dtl.Rows.Count > 0 Then
                        Dim sm As New ClassSendEmail()
                        sm.report_mark_type = "397"
                        sm.dt = dtl
                        sm.send_email_line_list()
                    End If
                    execute_non_query("UPDATE tb_log_line_list_mail SET is_resend=1, resend_date=NOW() WHERE id_log_line_list_mail='" + id_log_line_list_mail + "' ", True, "", "", "", "")
                    viewData()
                Catch ex As Exception
                    MsgBox("error resend")
                End Try
            ElseIf id_line_list_notif_type = "2" Then
                'drop changes
                Try
                    Dim qcek As String = "CALL view_drop_changes_list_v2('" + check_date + "')"
                    Dim dcek As DataTable = execute_query(qcek, -1, True, "", "", "", "")
                    If dcek.Rows.Count > 0 Then
                        Dim sm As New ClassSendEmail()
                        sm.report_mark_type = "394"
                        sm.par1 = check_date
                        sm.send_email_drop_changes()
                    End If
                    execute_non_query("UPDATE tb_log_line_list_mail SET is_resend=1, resend_date=NOW() WHERE id_log_line_list_mail='" + id_log_line_list_mail + "' ", True, "", "", "", "")
                    viewData()
                Catch ex As Exception
                    MsgBox("error resend")
                End Try
            ElseIf id_line_list_notif_type = "3" Then

            End If
        End If
    End Sub
End Class