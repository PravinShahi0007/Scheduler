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
        viewLookupQuery(LEDayKurs, query, 0, "day_name", "id_day")
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

            'email notice ar
            load_notice_ar_time()

            'warning late
            load_warning_late_time()

            'kurs
            load_kurs()

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

    Sub load_notice_ar_time()
        Dim query As String = "SELECT notice_email_ar_time FROM tb_opt_scheduler LIMIT 1"
        Dim time As String = execute_query(query, 0, True, "", "", "", "")

        TEEmailNoticeAR.EditValue = time
    End Sub

    Sub load_warning_late_time()
        Dim query As String = "SELECT warning_late FROM tb_opt_scheduler LIMIT 1"
        Dim time As String = execute_query(query, 0, True, "", "", "", "")

        TEWaningLate.EditValue = time
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
                'dept head
                Dim mail_dept As ClassSendEmail = New ClassSendEmail()
                mail_dept.report_mark_type = "weekly_attn_head"
                mail_dept.send_email_html()
            End If
            'monthly attendance
            If cur_datetime.Day = 1 And (Date.Parse(TETimeMonthly.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss")) Then
                Dim mail As ClassSendEmail = New ClassSendEmail()
                mail.report_mark_type = "monthly_leave_remaining"
                mail.send_email_html()
                'dept head
                Dim mail_dept As ClassSendEmail = New ClassSendEmail
                mail_dept.report_mark_type = "monthly_leave_remaining_head"
                mail_dept.send_email_html()
            End If
            'Duty Reminder
            If (Date.Parse(TETimeDuty.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss")) Then
                Dim query As String = "SELECT 
                                            tb.*,
                                            (
                                            tb.past_due_date + tb.soon_due + tb.pr_due
                                            ) AS total_notif 
                                        FROM
                                            (SELECT 
                                            SUM(
                                                IF(
                                         po.duty_is_pay = '2' 
                                         AND NOT ISNULL(po.pib_date),
                                         IF(DATEDIFF(DATE_ADD(po.pib_date, INTERVAL 1 YEAR), NOW()) < 0, 1, 0),
                                         0
                                                )
                                            ) AS past_due_date,
                                            SUM(
                                                IF(
                                         po.duty_is_pay = '2'
                                         AND NOT ISNULL(po.pib_date),
                                         IF(
                                             DATEDIFF(DATE_ADD(po.pib_date, INTERVAL 1 YEAR), NOW()) <= 30 
                                             AND DATEDIFF(DATE_ADD(po.pib_date, INTERVAL 1 YEAR), NOW()) >= 0,
                                             1,
                                             0
                                         ),
                                         0
                                                )
                                            ) AS soon_due,
                                            SUM(
                                                IF(
                                         po.duty_is_pay = '2'
                                         AND po.duty_is_pr_proposed = '2' 
                                         AND NOT ISNULL(DATE_ADD(po.pib_date, INTERVAL 1 YEAR)),
                                         IF(DATEDIFF(DATE_ADD(po.pib_date, INTERVAL 1 YEAR), NOW()) <= 60, 1, 0),
                                         0
                                                )
                                            ) AS pr_due 
                                            FROM
                                            tb_prod_order po) AS tb "
                Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")
                If Not data.Rows(0)("total_notif").ToString = "0" Then
                    Dim mail As ClassSendEmail = New ClassSendEmail()
                    mail.past_due_date = data.Rows(0)("past_due_date")
                    mail.soon_due = data.Rows(0)("soon_due")
                    mail.pr_due = data.Rows(0)("pr_due")
                    mail.total_notif = data.Rows(0)("total_notif")
                    mail.send_mail_duty()
                End If
            End If

            If get_opt_scheduler_field("is_active_notif_cash_advance").ToString = "1" Then
                'Cash Advance
                If Date.Parse(TECashAdvance.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
                    ClassCashAdvance.check_cash_advance()
                End If
            End If

            If get_opt_scheduler_field("is_active_notif_emp_per_app").ToString = "1" Then
                'Employee performance appraisal
                If Date.Parse(TEEmpPerApp.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
                    ClassEmpPerAppraisal.check_evaluation()
                End If
            End If

            'AR evaluation
            If get_opt_scheduler_field("is_active_evaluation_ar").ToString = "1" Then
                If Date.Parse(TEEvaluationAR.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
                    Dim qgd As String = "SELECT * FROM tb_ar_eval_setup_date WHERE ar_eval_setup_date='" + cur_datetime.ToString("yyyy-MM-dd") + "' "
                    Dim dgd As DataTable = execute_query(qgd, -1, True, "", "", "", "")
                    If dgd.Rows.Count > 0 Then 'ketemu tanggal evaluasi 
                        Try
                            'jalankan evaluasi
                            Dim qins As String = "CALL getEvaluationAR('" + cur_datetime.ToString("yyyy-MM-dd") + " " + Date.Parse(TEEvaluationAR.EditValue.ToString).ToString("HH:mm:ss") + "'); "
                            execute_non_query(qins, True, "", "", "", "")

                            'log
                            Dim query_log As String = "INSERT INTO tb_ar_eval_log(eval_date, log_time, log) 
                        VALUES('" + cur_datetime.ToString("yyyy-MM-dd") + " " + Date.Parse(TEEvaluationAR.EditValue.ToString).ToString("HH:mm:ss") + "', NOW(), 'Evaluation Success'); "
                            execute_non_query(query_log, True, "", "", "", "")
                        Catch ex As Exception
                            'log
                            Dim query_log As String = "INSERT INTO tb_ar_eval_log(eval_date, log_time, log) 
                        VALUES('" + cur_datetime.ToString("yyyy-MM-dd") + " " + Date.Parse(TEEvaluationAR.EditValue.ToString).ToString("HH:mm:ss") + "', NOW(), 'Evaluation Failed : " + addSlashes(ex.ToString) + "'); "
                            execute_non_query(query_log, True, "", "", "", "")
                        End Try


                        'push email
                        Dim em As New ClassSendEmail()
                        em.report_mark_type = "228"
                        em.par1 = cur_datetime.ToString("yyyy-MM-dd") + " " + Date.Parse(TEEvaluationAR.EditValue.ToString).ToString("HH:mm:ss")
                        em.par2 = cur_datetime.ToString("dd MMMM yyyy")
                        em.send_email_html()
                    End If
                End If
            End If

            'Email Pemberitahuan - AR
            If get_opt_scheduler_field("is_active_email_notice_ar").ToString = "1" Then
                If Date.Parse(TEEmailNoticeAR.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
                    'list group store
                    Dim ar As New ClassAREvaluation()
                    Dim query_grp As String = ar.querylistNoticeInvoice("2", "-1", "-1")
                    Dim dt_grp As DataTable = execute_query(query_grp, -1, True, "", "", "", "")
                    If dt_grp.Rows.Count > 0 Then
                        For g As Integer = 0 To dt_grp.Rows.Count - 1
                            Dim mm As New ClassMailManage()
                            Dim id_mail As String = "-1"
                            Try
                                'create mail var
                                Dim qopt As String = "SELECT mail_head_pemberitahuan,mail_subject_pemberitahuan, mail_title_pemberitahuan , mail_content_head_pemberitahuan, mail_content_pemberitahuan ,mail_content_end_pemberitahuan
                                FROM tb_opt "
                                Dim dopt As DataTable = execute_query(qopt, -1, True, "", "", "", "")
                                Dim mail_head As String = dopt.Rows(0)("mail_head_pemberitahuan").ToString
                                Dim mail_subject As String = dopt.Rows(0)("mail_subject_pemberitahuan").ToString + " " + dt_grp.Rows(g)("sales_pos_due_date").ToString + " - " + dt_grp.Rows(g)("group").ToString
                                Dim mail_title As String = dopt.Rows(0)("mail_title_pemberitahuan").ToString
                                Dim mail_content_head As String = dopt.Rows(0)("mail_content_head_pemberitahuan").ToString
                                Dim mail_content As String = dopt.Rows(0)("mail_content_pemberitahuan").ToString
                                Dim mail_content_end As String = dopt.Rows(0)("mail_content_end_pemberitahuan").ToString

                                'send paramenter class
                                mm.rmt = "226"
                                mm.par1 = "AND cg.id_comp_group=" + dt_grp.Rows(g)("id_comp_group").ToString + " AND c.id_store_company=" + dt_grp.Rows(g)("id_store_company").ToString + " "
                                mm.par2 = dt_grp.Rows(g)("id_comp_group").ToString
                                mm.par3 = dt_grp.Rows(g)("id_store_company").ToString
                                mm.mail_subject = mail_subject
                                mm.createEmail(dt_grp.Rows(g)("id_comp_group").ToString, "0", "NULL", "NULL", "")
                                id_mail = mm.id_mail_manage

                                'data send email
                                Dim qcont As String = "SELECT cgho.comp_name AS `group_company`, UPPER(cg.description) AS `group_store`,
                                sp.id_sales_pos AS `id_report`, sp.sales_pos_number AS report_number, CONCAT(c.comp_number,' - ', c.comp_name) AS `store`,
                                CONCAT(DATE_FORMAT(sp.sales_pos_start_period,'%d-%m-%y'),' s/d ', DATE_FORMAT(sp.sales_pos_end_period,'%d-%m-%y')) AS `period`,
                                DATE_FORMAT(sp.sales_pos_due_date,'%d-%m-%y') AS `sales_pos_due_date`,
                                CAST(IF(typ.`is_receive_payment`=2,-1,1) * ((sp.`sales_pos_total`*((100-sp.sales_pos_discount)/100))-sp.`sales_pos_potongan`) AS DECIMAL(15,2))-IFNULL(pyd.`value`,0.00) AS `amount` 
                                FROM tb_mail_manage_det md
                                INNER JOIN tb_sales_pos sp ON sp.id_sales_pos = md.id_report
                                INNER JOIN tb_m_comp_contact cc ON cc.`id_comp_contact`= IF(sp.id_memo_type=8 OR sp.id_memo_type=9, sp.id_comp_contact_bill,sp.`id_store_contact_from`)
                                INNER JOIN tb_lookup_report_mark_type rmt ON rmt.report_mark_type=sp.report_mark_type
                                INNER JOIN tb_m_comp c ON c.`id_comp`=cc.`id_comp`
                                INNER JOIN tb_m_comp_group cg ON cg.id_comp_group = c.id_comp_group
                                INNER JOIN tb_m_comp cgho ON cgho.id_comp = c.id_store_company
                                INNER JOIN tb_lookup_memo_type typ ON typ.`id_memo_type`=sp.`id_memo_type`
                                LEFT JOIN (
                                   SELECT pyd.id_report, pyd.report_mark_type, 
                                   COUNT(IF(py.id_report_status!=5 AND py.id_report_status!=6,py.id_rec_payment,NULL)) AS `total_pending`,
                                   SUM(pyd.value) AS  `value`
                                   FROM tb_rec_payment_det pyd
                                   INNER JOIN tb_rec_payment py ON py.`id_rec_payment`=pyd.`id_rec_payment`
                                   INNER JOIN tb_sales_pos sp ON sp.id_sales_pos = pyd.id_report AND sp.is_close_rec_payment=2
                                   WHERE py.`id_report_status`=6
                                   GROUP BY pyd.id_report, pyd.report_mark_type
                                ) pyd ON pyd.id_report = sp.id_sales_pos AND pyd.report_mark_type = sp.report_mark_type
                                WHERE md.id_mail_manage=" + id_mail + " "
                                Dim dcont As DataTable = execute_query(qcont, -1, True, "", "", "", "")
                                Dim tot_amo As Double = dt_grp.Rows(g)("amount").ToString

                                'send email
                                Dim sm As New ClassSendEmail()
                                sm.id_report = id_mail
                                sm.report_mark_type = "226"
                                sm.head = mail_head
                                sm.subj = mail_subject
                                sm.titl = mail_title
                                sm.par1 = mail_content_head + " " + dcont.Rows(0)("group_company").ToString
                                sm.par2 = mail_content
                                sm.par3 = mail_content_end
                                sm.par4 = Double.Parse(tot_amo.ToString).ToString("N2")
                                sm.dt = dcont
                                sm.send_email_html()

                                'log
                                Dim querylog As String = "INSERT INTO tb_ar_notice_log(`id_comp_group`, `group`, `log_time`, `log`, `is_success`) 
                                VALUES('" + dt_grp.Rows(g)("id_comp_group").ToString + "', '" + addSlashes(dt_grp.Rows(g)("group").ToString) + "', NOW(), '" + addSlashes(dt_grp.Rows(g)("group").ToString) + " - Email Sent successfully',1); " + mm.queryInsertLog("0", "2", "" + addSlashes(dt_grp.Rows(g)("group").ToString) + " - Sent successfully") + "; "
                                execute_non_query(querylog, True, "", "", "", "")

                                'foolow up
                                mm.insertLogFollowUp("")
                            Catch ex As Exception
                                'Log
                                Dim query_log As String = "INSERT INTO tb_ar_notice_log(`id_comp_group`, `group`, `log_time`, `log`, `is_success`) 
                                VALUES('" + dt_grp.Rows(g)("id_comp_group").ToString + "','" + addSlashes(dt_grp.Rows(g)("group").ToString) + "', NOW(), '" + addSlashes(dt_grp.Rows(g)("group").ToString) + " - Failed send email : " + addSlashes(ex.ToString) + "',2); " + mm.queryInsertLog("0", "3", " + group_name + - " + addSlashes(ex.ToString))
                                execute_non_query(query_log, True, "", "", "", "")
                            End Try
                        Next
                    End If
                End If
            End If

            If get_opt_scheduler_field("is_active_warning_late").ToString = "1" Then
                'Warning Late
                If Date.Parse(TEWaningLate.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
                    ClassLateWarning.check_late()
                End If
            End If

            If LEDayKurs.EditValue = cur_datetime.DayOfWeek And (Date.Parse(TETimeKurs.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss")) Then
                ClassGetKurs.get_kurs()
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

    Private Sub BtnEmailNoticeAR_Click(sender As Object, e As EventArgs) Handles BtnEmailNoticeAR.Click
        Dim query As String = "UPDATE tb_opt_scheduler SET notice_email_ar_time='" & Date.Parse(TEEmailNoticeAR.EditValue.ToString).ToString("HH:mm:ss") & "'"
        execute_non_query(query, True, "", "", "", "")
        MsgBox("Email Notice AR Time saved.")
    End Sub

    Private Sub SBWarningLate_Click(sender As Object, e As EventArgs) Handles SBWarningLate.Click
        Dim query As String = "UPDATE tb_opt_scheduler SET warning_late='" & Date.Parse(TEWaningLate.EditValue.ToString).ToString("HH:mm:ss") & "'"
        execute_non_query(query, True, "", "", "", "")
        MsgBox("Warning Late Time saved.")
    End Sub

    Sub load_kurs()
        Dim query As String = ""
        query = "SELECT get_kurs_day, get_kurs_time FROM tb_opt_scheduler LIMIT 1"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")

        LEDayKurs.ItemIndex = LEDay.Properties.GetDataSourceRowIndex("id_day", data.Rows(0)("get_kurs_day").ToString)
        TETimeKurs.EditValue = data.Rows(0)("get_kurs_time")
    End Sub

    Private Sub BSaveKurs_Click(sender As Object, e As EventArgs) Handles BSaveKurs.Click
        Dim query_log As String = "UPDATE tb_opt_scheduler SET get_kurs_day='" & LEDayKurs.EditValue.ToString & "',`get_kurs_time`='" & Date.Parse(TETimeKurs.EditValue.ToString).ToString("HH:mm:ss") & "'"
        execute_non_query(query_log, True, "", "", "", "")
        MsgBox("Kurs Schedule saved.")
    End Sub
End Class