Public Class FormScheduler
    Public connection_problem As Boolean = False

    Public dom As String = "-1"


    Private Sub FormScheduler_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        load_form()
        'Console.WriteLine(Now().AddDays(1).DayOfWeek())
        Cursor = Cursors.Default
    End Sub
    Sub load_days()
        For Each t As DevExpress.XtraTab.XtraTabPage In XtraTabControl1.TabPages
            XtraTabControl1.SelectedTabPage = t
        Next t
        XtraTabControl1.SelectedTabPage = XtraTabControl1.TabPages(0)

        Dim query As String = "SELECT '1' AS id_day, 'Monday' AS day_name
                               UNION SELECT '2' AS id_day, 'Tuesday' AS day_name
                               UNION SELECT '3' AS id_day, 'Wednesday' AS day_name
                               UNION SELECT '4' AS id_day, 'Thursday' AS day_name
                               UNION SELECT '5' AS id_day, 'Friday' AS day_name
                               UNION SELECT '6' AS id_day, 'Saturday' AS day_name
                               UNION SELECT '0' AS id_day, 'Sunday' AS day_name"
        viewLookupQuery(LEDay, query, 0, "day_name", "id_day")
    End Sub
    Public Sub viewLookupQuery(ByVal LE As DevExpress.XtraEditors.LookUpEdit, ByVal query As String, ByVal index_selected As Integer, ByVal display As String, ByVal value As String)
        Try
            Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")
            LE.Properties.DataSource = Nothing
            LE.Properties.DataSource = data
            LE.Properties.DisplayMember = display
            LE.Properties.ValueMember = value
            LE.ItemIndex = index_selected
        Catch ex As Exception
            'errorConnection()
            Console.WriteLine(ex.ToString)
        End Try
    End Sub
    Sub load_form()
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

        If connection_problem = False Then
            load_schedule()
            'weekly
            load_days()
            load_schedule_wekkly_attn_report()
            '
            start_timer()
            WindowState = FormWindowState.Minimized
        End If
    End Sub
    Sub load_schedule_wekkly_attn_report()
        Dim query As String = ""
        query = "SELECT * FROM tb_opt_scheduler LIMIT 1"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")

        LEDay.ItemIndex = LEDay.Properties.GetDataSourceRowIndex("id_day", data.Rows(0)("day").ToString)
        TETime.EditValue = data.Rows(0)("time")
    End Sub
    Sub load_schedule()
        Dim query As String = ""
        query = "SELECT * FROM tb_scheduler_attn"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")

        GCSchedule.DataSource = data
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        Try
            Dim cur_datetime As Date = Now()
            For i As Integer = 0 To GVSchedule.RowCount - 1
                If (Date.Parse(GVSchedule.GetRowCellValue(i, "time_var").ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss")) Then
                    exec_process()
                End If
            Next
            'weekly attendance
            If LEDay.EditValue = cur_datetime.DayOfWeek And (Date.Parse(TETime.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss")) Then
                Dim mail As ClassSendEmail = New ClassSendEmail()
                mail.report_mark_type = "weekly_attn"
                mail.send_email_html()
            End If
        Catch ex As Exception
            stop_timer()
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub exec_weekly_attn()

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
                fp.clear_attlog()
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

    Private Sub BSaveWAR_Click(sender As Object, e As EventArgs) Handles BSaveWAR.Click
        Dim query_log As String = "UPDATE tb_opt_scheduler SET day='" & LEDay.EditValue.ToString & "',`time`='" & Date.Parse(TETime.EditValue.ToString).ToString("HH:mm:ss") & "'"
        execute_non_query(query_log, True, "", "", "", "")
        MsgBox("Schedule saved.")
    End Sub
End Class