Public Class FormScheduler
    Public connection_problem As Boolean = False

    Private Sub FormScheduler_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor

        ShowInTaskbar = False

        Try
            read_database_configuration()
            check_connection(True, "", "", "", "")
            'show hide login
            'LoginToolStripMenuItem.Visible = True
        Catch ex As Exception
            connection_problem = True

            Visible = False

            FormDatabase.TopMost = True
            FormDatabase.Show()
            FormDatabase.Focus()
            FormDatabase.TopMost = False
        End Try

        load_schedule()
        auto_start()

        Cursor = Cursors.Default
    End Sub

    Sub auto_start()
        Timer.Enabled = True
        BSave.Text = "Stop"
        Linfo.Text = "Schedule Running"
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub SettingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingToolStripMenuItem.Click
        Visible = True
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        Close()
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles BAdd.Click
        FormSchedulerSet.ShowDialog()
    End Sub

    Sub load_schedule()
        Dim query As String = ""
        query = "SELECT * FROM tb_scheduler_attn"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")

        GCSchedule.DataSource = data
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        'Try
        For i As Integer = 0 To GVSchedule.RowCount - 1
            If (Date.Parse(GVSchedule.GetRowCellValue(i, "time_var").ToString).ToString("HH:mm:ss") = Now.ToString("HH:mm:ss")) Then
                MsgBox("aw")
            End If
        Next
        'Catch ex As Exception
        'Timer.Enabled = False
        'MsgBox(ex.ToString)
        'End Try
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        If BSave.Text = "Start" Then
            Timer.Enabled = True
        Else
            Timer.Enabled = False
        End If
    End Sub

    Private Sub BDelete_Click(sender As Object, e As EventArgs) Handles BDelete.Click
        If GVSchedule.RowCount > 0 Then
            Dim query As String = "DELETE FROM tb_scheduler_attn WHERE id_scheduler_attn='" & GVSchedule.GetFocusedRowCellValue("id_scheduler_attn").ToString & "'"
            execute_non_query(query, True, "", "", "", "")
            load_schedule()
        End If
    End Sub
End Class