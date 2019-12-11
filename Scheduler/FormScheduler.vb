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
            load_schedule_weekly_attn_report()

            'monthly
            load_schedule_monthly_leave_report()

            'duty
            load_duty_reminder()


            'cash advance
            load_cash_advance()


            'employee performance appraisal
            load_emp_per_app()

            'evaluation ar time
            load_evaluation_time()

            start_timer()
            WindowState = FormWindowState.Minimized
        End If
    End Sub

    Function get_opt_scheduler_field(ByVal field As String)
        'opt as var choose field
        Dim ret_var, query As String
        ret_var = ""

        Try
            query = "SELECT " & field & " FROM tb_opt_scheduler LIMIT 1"
            ret_var = execute_query(query, 0, True, "", "", "", "")
        Catch ex As Exception
            ret_var = ""
        End Try

        Return ret_var
    End Function

    Sub load_duty_reminder()
        Dim query As String = "SELECT prod_duty_time FROM tb_opt_scheduler LIMIT 1"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")

        TETimeDuty.EditValue = data.Rows(0)("prod_duty_time")
    End Sub
    Sub load_schedule_monthly_leave_report()
        Dim query As String = ""
        query = "SELECT time_stock_leave FROM tb_opt_scheduler LIMIT 1"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")

        TETimeMonthly.EditValue = data.Rows(0)("time_stock_leave")
    End Sub
    Sub load_schedule_weekly_attn_report()
        Dim query As String = ""
        query = "SELECT day_weekly_attn,time_weekly_attn FROM tb_opt_scheduler LIMIT 1"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")

        LEDay.ItemIndex = LEDay.Properties.GetDataSourceRowIndex("id_day", data.Rows(0)("day_weekly_attn").ToString)
        TETime.EditValue = data.Rows(0)("time_weekly_attn")
    End Sub
    Sub load_schedule()
        Dim query As String = ""
        query = "SELECT * FROM tb_scheduler_attn"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")

        GCSchedule.DataSource = data
    End Sub
    Sub load_emp_per_app()
        Dim query As String = "SELECT emp_per_app_time FROM tb_opt_scheduler LIMIT 1"
        Dim time As String = execute_query(query, 0, True, "", "", "", "")

        TEEmpPerApp.EditValue = time
    End Sub
    Sub load_cash_advance()
        Dim query As String = "SELECT cash_advance_time FROM tb_opt_scheduler LIMIT 1"
        Dim time As String = execute_query(query, 0, True, "", "", "", "")

        TECashAdvance.EditValue = time
    End Sub

    Sub load_evaluation_time()
        Dim query As String = "SELECT evaluation_ar_time FROM tb_opt_scheduler LIMIT 1"
        Dim time As String = execute_query(query, 0, True, "", "", "", "")

        TEEvaluationAR.EditValue = time
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        Try
            Dim cur_datetime As Date = Now()
            'For i As Integer = 0 To GVSchedule.RowCount - 1
            '    If (Date.Parse(GVSchedule.GetRowCellValue(i, "time_var").ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss")) Then
            '        exec_process()
            '    End If
            'Next
            ''weekly attendance
            'If LEDay.EditValue = cur_datetime.DayOfWeek And (Date.Parse(TETime.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss")) Then
            '    Dim mail As ClassSendEmail = New ClassSendEmail()
            '    mail.report_mark_type = "weekly_attn"
            '    mail.send_email_html()
            '    'dept head
            '    Dim mail_dept As ClassSendEmail = New ClassSendEmail()
            '    mail_dept.report_mark_type = "weekly_attn_head"
            '    mail_dept.send_email_html()
            'End If
            ''monthly attendance
            'If cur_datetime.Day = 1 And (Date.Parse(TETimeMonthly.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss")) Then
            '    Dim mail As ClassSendEmail = New ClassSendEmail()
            '    mail.report_mark_type = "monthly_leave_remaining"
            '    mail.send_email_html()
            '    'dept head
            '    Dim mail_dept As ClassSendEmail = New ClassSendEmail
            '    mail_dept.report_mark_type = "monthly_leave_remaining_head"
            '    mail_dept.send_email_html()
            'End If
            ''Duty Reminder
            'If (Date.Parse(TETimeDuty.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss")) Then
            '    Dim query As String = "SELECT 
            '                                tb.*,
            '                                (
            '                                tb.past_due_date + tb.soon_due + tb.pr_due
            '                                ) AS total_notif 
            '                            FROM
            '                                (SELECT 
            '                                SUM(
            '                                    IF(
            '                             po.duty_is_pay = '2' 
            '                             AND NOT ISNULL(po.pib_date),
            '                             IF(DATEDIFF(DATE_ADD(po.pib_date, INTERVAL 1 YEAR), NOW()) < 0, 1, 0),
            '                             0
            '                                    )
            '                                ) AS past_due_date,
            '                                SUM(
            '                                    IF(
            '                             po.duty_is_pay = '2'
            '                             AND NOT ISNULL(po.pib_date),
            '                             IF(
            '                                 DATEDIFF(DATE_ADD(po.pib_date, INTERVAL 1 YEAR), NOW()) <= 30 
            '                                 AND DATEDIFF(DATE_ADD(po.pib_date, INTERVAL 1 YEAR), NOW()) >= 0,
            '                                 1,
            '                                 0
            '                             ),
            '                             0
            '                                    )
            '                                ) AS soon_due,
            '                                SUM(
            '                                    IF(
            '                             po.duty_is_pay = '2'
            '                             AND po.duty_is_pr_proposed = '2' 
            '                             AND NOT ISNULL(DATE_ADD(po.pib_date, INTERVAL 1 YEAR)),
            '                             IF(DATEDIFF(DATE_ADD(po.pib_date, INTERVAL 1 YEAR), NOW()) <= 60, 1, 0),
            '                             0
            '                                    )
            '                                ) AS pr_due 
            '                                FROM
            '                                tb_prod_order po) AS tb "
            '    Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")
            '    If Not data.Rows(0)("total_notif").ToString = "0" Then
            '        Dim mail As ClassSendEmail = New ClassSendEmail()
            '        mail.past_due_date = data.Rows(0)("past_due_date")
            '        mail.soon_due = data.Rows(0)("soon_due")
            '        mail.pr_due = data.Rows(0)("pr_due")
            '        mail.total_notif = data.Rows(0)("total_notif")
            '        mail.send_mail_duty()
            '    End If
            'End If

            'If get_opt_scheduler_field("is_active_notif_cash_advance").ToString = "1" Then
            '    'Cash Advance
            '    If Date.Parse(TECashAdvance.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
            '        ClassCashAdvance.check_cash_advance()
            '    End If
            'End If

            'If get_opt_scheduler_field("is_active_notif_emp_per_app").ToString = "1" Then
            '    'Employee performance appraisal
            '    If Date.Parse(TEEmpPerApp.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
            '        ClassEmpPerAppraisal.check_evaluation()
            '    End If
            'End If

            'AR evaluation
            If Date.Parse(TEEvaluationAR.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
                Dim qgd As String = "SELECT * FROM tb_ar_eval_setup_date WHERE ar_eval_setup_date='" + cur_datetime.ToString("yyyy-MM-dd") + "' "
                Dim dgd As DataTable = execute_query(qgd, -1, True, "", "", "", "")
                If dgd.Rows.Count > 0 Then 'ketemu tanggal evaluasi 
                    'jalankan 
                    Dim qins As String = "CALL getEvaluationAR('" + cur_datetime.ToString("yyyy-MM-dd") + " " + Date.Parse(TEEvaluationAR.EditValue.ToString).ToString("HH:mm:ss") + "'); "
                    execute_non_query(qins, True, "", "", "", "")

                    'push email

                End If
            End If
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
                fp.clear_attlog()
            Else
                If Not i = 0 Then
                    string_err += vbNewLine
                End If
                string_err += "- " & data.Rows(i)("name").ToString()
            End If

            fp.disconnect()

            If data.Rows(i)("is_maintenance").ToString = "1" Then
                fp.maintenance_datetime()
            End If
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
        Dim query_log As String = "UPDATE tb_opt_scheduler SET day_weekly_attn='" & LEDay.EditValue.ToString & "',`time_weekly_attn`='" & Date.Parse(TETime.EditValue.ToString).ToString("HH:mm:ss") & "'"
        execute_non_query(query_log, True, "", "", "", "")
        MsgBox("Weekly Schedule saved.")
    End Sub

    Private Sub BSaveMonthly_Click(sender As Object, e As EventArgs) Handles BSaveMonthly.Click
        Dim query As String = "UPDATE tb_opt_scheduler SET time_stock_leave='" & Date.Parse(TETimeMonthly.EditValue.ToString).ToString("HH:mm:ss") & "'"
        execute_non_query(query, True, "", "", "", "")
        MsgBox("Monthly Schedule saved.")
    End Sub

    Private Sub BProdDuty_Click(sender As Object, e As EventArgs) Handles BProdDuty.Click
        Dim query As String = "UPDATE tb_opt_scheduler SET prod_duty_time='" & Date.Parse(TETimeDuty.EditValue.ToString).ToString("HH:mm:ss") & "'"
        execute_non_query(query, True, "", "", "", "")
        MsgBox("Duty Reminder saved.")
    End Sub

    Private Sub SBEmpPerApp_Click(sender As Object, e As EventArgs) Handles SBEmpPerApp.Click
        Dim query As String = "UPDATE tb_opt_scheduler SET emp_per_app_time='" & Date.Parse(TEEmpPerApp.EditValue.ToString).ToString("HH:mm:ss") & "'"
        execute_non_query(query, True, "", "", "", "")
        MsgBox("Employee Performance Appraisal Reminder saved.")
    End Sub

    Private Sub SBCashAdvance_Click(sender As Object, e As EventArgs) Handles SBCashAdvance.Click
        Dim query As String = "UPDATE tb_opt_scheduler SET cash_advance_time='" & Date.Parse(TECashAdvance.EditValue.ToString).ToString("HH:mm:ss") & "'"
        execute_non_query(query, True, "", "", "", "")
        MsgBox("Cash Advance Reminder saved.")
    End Sub

    Private Sub BtnEvaluationAR_Click(sender As Object, e As EventArgs) Handles BtnEvaluationAR.Click
        Dim query As String = "UPDATE tb_opt_scheduler SET evaluation_ar_time='" & Date.Parse(TEEvaluationAR.EditValue.ToString).ToString("HH:mm:ss") & "'"
        execute_non_query(query, True, "", "", "", "")
        MsgBox("Evaluation AR Time saved.")
    End Sub
End Class