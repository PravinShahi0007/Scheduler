Public Class FormSchedulerSet
    Private Sub FormSchedulerSet_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Dispose()
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        Close()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        Dim query As String = "INSERT INTO tb_scheduler_attn(schedule,time_var) VALUES('" & TESchedule.Text & "','" & Date.Parse(TETime.EditValue.ToString).ToString("yyyy-MM-dd HH:mm:ss") & "')"
        execute_non_query(query, True, "", "", "", "")
        FormScheduler.load_schedule()
        Close()
    End Sub

End Class