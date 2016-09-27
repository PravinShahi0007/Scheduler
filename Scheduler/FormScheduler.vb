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
        start_timer()

        Cursor = Cursors.Default
    End Sub

    Sub load_schedule()
        Dim query As String = ""
        query = "SELECT * FROM tb_scheduler_attn"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")

        GCSchedule.DataSource = data
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        Try
            For i As Integer = 0 To GVSchedule.RowCount - 1
                If (Date.Parse(GVSchedule.GetRowCellValue(i, "time_var").ToString).ToString("HH:mm:ss") = Now.ToString("HH:mm:ss")) Then
                    exec_process()
                End If
            Next
        Catch ex As Exception
            stop_timer()
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        If BSave.Text = "Start" Then
            start_timer()
        Else
            stop_timer()
        End If
    End Sub

    Sub stop_timer()
        Timer.Enabled = False
        Linfo.Text = "Schedule Stopped"
        BSave.Text = "Start"
        RemoveValue(Application.ProductName)
    End Sub

    Sub start_timer()
        Timer.Enabled = True
        Linfo.Text = "Schedule Running"
        BSave.Text = "Stop"
        RunAtStartup(Application.ProductName, Application.ExecutablePath.ToString)
    End Sub

    Private Sub BDelete_Click(sender As Object, e As EventArgs) Handles BDelete.Click
        If GVSchedule.RowCount > 0 Then
            Dim query As String = "DELETE FROM tb_scheduler_attn WHERE id_scheduler_attn='" & GVSchedule.GetFocusedRowCellValue("id_scheduler_attn").ToString & "'"
            execute_non_query(query, True, "", "", "", "")
            load_schedule()
        End If
    End Sub
    Private Sub Scheduler_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If WindowState = FormWindowState.Minimized Then
            NotifyIcon.ShowBalloonTip(500)
            Hide()
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub SettingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingToolStripMenuItem.Click
        Show()
        WindowState = FormWindowState.Normal
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        Close()
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles BAdd.Click
        FormSchedulerSet.ShowDialog()
    End Sub

    Public Sub RunAtStartup(ByVal ApplicationName As String, ByVal ApplicationPath As String)
        Dim CU As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
        With CU
            .CreateSubKey(ApplicationName)
            .SetValue(ApplicationName, """" & ApplicationPath.ToString & """")
        End With
    End Sub

    Public Sub RemoveValue(ByVal ApplicationName As String)
        Dim CU As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
        With CU
            .OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
            .DeleteValue(ApplicationName, False)
        End With
    End Sub

    Sub exec_process()
        get_data()
    End Sub

    Sub get_data()
        Dim fp As New ClassFingerPrint

        Dim query As String = "SELECT * FROM tb_m_fingerprint"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")

        Dim string_err As String = ""

        For i As Integer = 0 To data.Rows.Count - 1
            fp.ip = data.Rows(i)("ip").ToString()
            fp.port = data.Rows(i)("port").ToString()

            fp.connect()

            If fp.bIsConnected Then
                fp.get_attlog(data.Rows(i)("id_fingerprint").ToString())
                'fp.clear_attlog()
            Else
                If Not i = 0 Then
                    string_err += vbNewLine
                End If
                string_err += "- " & data.Rows(i)("name").ToString()
            End If

            fp.disconnect()
        Next

        If Not string_err = "" Then
            string_err = "Fingerprint not connected : " & string_err
            Dim query_log As String = "INSERT INTO tb_scheduler_attn_log(datetime,log) VALUES(NOW(),'" & string_err & "')"
            execute_non_query(query_log, True, "", "", "", "")
        Else
            Dim query_log As String = "INSERT INTO tb_scheduler_attn_log(datetime,log) VALUES(NOW(),'Success get attendance record.')"
            execute_non_query(query_log, True, "", "", "", "")
        End If
    End Sub
End Class