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
        viewLookupQuery(LEDayAREval, query, 0, "day_name", "id_day")
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
            'disable when developed
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

            'closed order vios
            'load_check_fail_order_time()
            load_schedule_close_ol_order()

            'sales return order
            load_sales_return_order()

            'marketplace order status
            load_schedule_mos()

            load_qc()

            load_polis()

            load_po_og()

            load_pr_og()

            load_serah_terima_qc()

            load_pib_review_notif()

            load_eos()

            load_price_eos()

            load_bsp()

            load_line_list()

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

    Sub load_qc()
        Dim query As String = "SELECT qc_time FROM tb_opt_scheduler LIMIT 1"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")

        TEQC.EditValue = data.Rows(0)("qc_time")
    End Sub

    Sub load_polis()
        Dim query As String = "SELECT polis_time FROM tb_opt_scheduler LIMIT 1"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")

        TEPolis.EditValue = data.Rows(0)("polis_time")
    End Sub

    Sub load_po_og()
        Dim query As String = "SELECT po_og_time FROM tb_opt_scheduler LIMIT 1"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")

        TEPOOG.EditValue = data.Rows(0)("po_og_time")
    End Sub

    Sub load_pr_og()
        Dim query As String = "SELECT pr_og_time FROM tb_opt_scheduler LIMIT 1"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")

        TEPROG.EditValue = data.Rows(0)("pr_og_time")
    End Sub

    Sub load_serah_terima_qc()
        Dim query As String = "SELECT serah_terima_qc_time FROM tb_opt_scheduler LIMIT 1"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")

        TESerahTerimaQC.EditValue = data.Rows(0)("serah_terima_qc_time")
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
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")

        TEEmpPerApp.EditValue = data.Rows(0)("emp_per_app_time")
    End Sub
    Sub load_cash_advance()
        Dim query As String = "SELECT cash_advance_time FROM tb_opt_scheduler LIMIT 1"
        Dim time As String = execute_query(query, 0, True, "", "", "", "")

        TECashAdvance.EditValue = time
    End Sub

    Sub load_evaluation_time()
        Dim query As String = "SELECT evaluation_ar_time, evaluation_ar_day FROM tb_opt_scheduler LIMIT 1"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")
        LEDayAREval.ItemIndex = LEDayAREval.Properties.GetDataSourceRowIndex("id_day", data.Rows(0)("evaluation_ar_day"))
        TEEvaluationAR.EditValue = data.Rows(0)("evaluation_ar_time")
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

            'disable when developed from here
            For i As Integer = 0 To GVSchedule.RowCount - 1
                If (Date.Parse(GVSchedule.GetRowCellValue(i, "time_var").ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss")) Then
                    exec_process()
                End If
            Next
            'weekly attendance
            If LEDay.EditValue = cur_datetime.DayOfWeek And (Date.Parse(TETime.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss")) Then
                Dim mail As ClassSendEmail = New ClassSendEmail()
                mail.report_mark_type = "weekly_attn"
                mail.is_daily = "-1"
                mail.send_email_html()
                'dept head
                Dim mail_dept As ClassSendEmail = New ClassSendEmail()
                mail_dept.report_mark_type = "weekly_attn_head"
                mail_dept.is_daily = "-1"
                mail_dept.send_email_html()
            End If
            'daily attendance
            If get_opt_scheduler_field("is_active_daily_attendance").ToString = "1" And (Date.Parse(TETime.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss")) Then
                Dim mail As ClassSendEmail = New ClassSendEmail()
                mail.report_mark_type = "weekly_attn"
                mail.is_daily = "1"
                mail.send_email_html()
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
            'If (Date.Parse(TETimeDuty.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss")) Then
            '    Dim query As String = "SELECT 
            '                                                                    tb.*,
            '                                                                    (
            '                                                                    tb.past_due_date + tb.soon_due + tb.pr_due
            '                                                                    ) AS total_notif 
            '                                                                FROM
            '                                                                    (SELECT 
            '                                                                    SUM(
            '                                                                        IF(
            '                                                                 po.duty_is_pay = '2' 
            '                                                                 AND NOT ISNULL(po.pib_date),
            '                                                                 IF(DATEDIFF(DATE_ADD(po.pib_date, INTERVAL 1 YEAR), NOW()) < 0, 1, 0),
            '                                                                 0
            '                                                                        )
            '                                                                    ) AS past_due_date,
            '                                                                    SUM(
            '                                                                        IF(
            '                                                                 po.duty_is_pay = '2'
            '                                                                 AND NOT ISNULL(po.pib_date),
            '                                                                 IF(
            '                                                                     DATEDIFF(DATE_ADD(po.pib_date, INTERVAL 1 YEAR), NOW()) <= 30 
            '                                                                     AND DATEDIFF(DATE_ADD(po.pib_date, INTERVAL 1 YEAR), NOW()) >= 0,
            '                                                                     1,
            '                                                                     0
            '                                                                 ),
            '                                                                 0
            '                                                                        )
            '                                                                    ) AS soon_due,
            '                                                                    SUM(
            '                                                                        IF(
            '                                                                 po.duty_is_pay = '2'
            '                                                                 AND po.duty_is_pr_proposed = '2' 
            '                                                                 AND NOT ISNULL(DATE_ADD(po.pib_date, INTERVAL 1 YEAR)),
            '                                                                 IF(DATEDIFF(DATE_ADD(po.pib_date, INTERVAL 1 YEAR), NOW()) <= 60, 1, 0),
            '                                                                 0
            '                                                                        )
            '                                                                    ) AS pr_due 
            '                                                                    FROM
            '                                                                    tb_prod_order po) AS tb "
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

            If get_opt_scheduler_field("is_active_notif_cash_advance").ToString = "1" Then
                'Cash Advance
                If Date.Parse(TECashAdvance.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
                    ClassCashAdvance.check_cash_advance()
                End If
            End If

            If get_opt_scheduler_field("is_active_notif_emp_per_app").ToString = "1" Then
                If Date.Parse(TEEmpPerApp.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
                    ClassContractReminder.contract_reminder()
                End If
            End If

            'AR evaluation
            If get_opt_scheduler_field("is_active_evaluation_ar").ToString = "1" Then
                If LEDayAREval.EditValue = cur_datetime.DayOfWeek And Date.Parse(TEEvaluationAR.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
                    'Pake tanggal evaluasi
                    'Dim qgd As String = "SELECT * FROM tb_ar_eval_setup_date WHERE ar_eval_setup_date='" + cur_datetime.ToString("yyyy-MM-dd") + "' "
                    'Dim dgd As DataTable = execute_query(qgd, -1, True, "", "", "", "")
                    'If dgd.Rows.Count > 0 Then 'ketemu tanggal evaluasi 
                    '    Try
                    '        'jalankan evaluasi
                    '        Dim qins As String = "CALL getEvaluationAR('" + cur_datetime.ToString("yyyy-MM-dd") + " " + Date.Parse(TEEvaluationAR.EditValue.ToString).ToString("HH:mm:ss") + "'); "
                    '        execute_non_query(qins, True, "", "", "", "")

                    '        'log
                    '        Dim query_log As String = "INSERT INTO tb_ar_eval_log(eval_date, log_time, log) 
                    '                            VALUES('" + cur_datetime.ToString("yyyy-MM-dd") + " " + Date.Parse(TEEvaluationAR.EditValue.ToString).ToString("HH:mm:ss") + "', NOW(), 'Evaluation Success'); "
                    '        execute_non_query(query_log, True, "", "", "", "")
                    '    Catch ex As Exception
                    '        'log
                    '        Dim query_log As String = "INSERT INTO tb_ar_eval_log(eval_date, log_time, log) 
                    '                            VALUES('" + cur_datetime.ToString("yyyy-MM-dd") + " " + Date.Parse(TEEvaluationAR.EditValue.ToString).ToString("HH:mm:ss") + "', NOW(), 'Evaluation Failed : " + addSlashes(ex.ToString) + "'); "
                    '        execute_non_query(query_log, True, "", "", "", "")
                    '    End Try


                    '    'push email
                    '    Dim em As New ClassSendEmail()
                    '    em.report_mark_type = "228"
                    '    em.par1 = cur_datetime.ToString("yyyy-MM-dd") + " " + Date.Parse(TEEvaluationAR.EditValue.ToString).ToString("HH:mm:ss")
                    '    em.par2 = cur_datetime.ToString("dd MMMM yyyy")
                    '    em.send_email_html()
                    'End If
                    'setiap rabu
                    Dim err As String = ""
                    Dim date_now_origin As DateTime = cur_datetime
                    Dim date_now As String = DateTime.Parse(date_now_origin).ToString("yyyy-MM-dd HH:mm:ss")
                    Dim date_now_display As String = DateTime.Parse(date_now_origin).ToString("dd MMMM yyyy")
                    Dim ev As New ClassAREvaluation()

                    'hold delivery
                    ev.holdDelivery(date_now)

                    'sending mail hold delivery
                    ev.sendEmailHoldDelivery(date_now, date_now_display)

                    'sendiing email peringatan
                    Dim qcg As String = "SELECT e.id_comp_group, e.id_store_company AS `id_ho`, cg.description AS `group`
                                            FROM tb_ar_eval e 
                                            INNER JOIN tb_m_comp_group cg ON cg.id_comp_group = e.id_comp_group
                                            WHERE e.eval_date='" + date_now + "'
                                            GROUP BY e.id_comp_group, e.id_store_company "
                    Dim dcg As DataTable = execute_query(qcg, -1, True, "", "", "", "")
                    For g As Integer = 0 To dcg.Rows.Count - 1
                        Dim id_group As String = dcg.Rows(g)("id_comp_group").ToString
                        Dim id_store_company As String = dcg.Rows(g)("id_ho").ToString
                        Dim group As String = addSlashes(dcg.Rows(g)("group").ToString.ToUpper)
                        ev.sendEmailPeringatan(date_now, id_group, id_store_company, group)
                    Next
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
                                                                                            CAST(IF(typ.`is_receive_payment`=2,-1,1) * sp.netto AS DECIMAL(15,2))-IFNULL(pyd.`value`,0.00) AS `amount` 
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

            'CLOSED SHOPIFY ORDER
            If get_opt_scheduler_field("is_active_vios_close_fail_order").ToString = "1" Then
                For i As Integer = 0 To GVSchCloseOrder.RowCount - 1
                    If (Date.Parse(GVSchCloseOrder.GetRowCellValue(i, "schedule").ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss")) Then
                        'split par
                        Dim time_split As String() = Split(cur_datetime.ToString("HH:mm"), ":")
                        Dim hour As Integer = Integer.Parse(time_split(0).ToString)
                        Dim minute As Integer = Integer.Parse(time_split(1).ToString)
                        Dim sch_cek As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, 0)

                        Dim fo As New ClassShopifyAPI()
                        Try
                            fo.get_order_fail(sch_cek)
                        Catch ex As Exception
                            fo.orderFailLog("(" + DateTime.Parse(sch_cek).ToString("yyyy-MM-dd HH:mm:ss") + "):" + ex.ToString)
                        End Try
                        fo.proceed_cancel_fail_order()
                    End If
                Next
            End If

            If get_opt_scheduler_field("is_active_sales_return_order").ToString = "1" Then
                'Sales Return Order
                If Date.Parse(TESalesReturnOrder.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
                    ClassSalesReturnOrder.check_empty_pickup_date()
                End If
            End If

            'qc
            If get_opt_scheduler_field("is_active_qc").ToString = "1" Then
                'Return Out reminder
                If Date.Parse(TEQC.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
                    Dim qqc As String = "SELECT h.id_report_status, h.report_status, a.id_prod_order_ret_out, a.prod_order_ret_out_date, a.prod_order_ret_out_due_date, a.prod_order_ret_out_note,  
                                                                        a.prod_order_ret_out_number , b.prod_order_number, c.id_season, c.season, CONCAT(e.comp_number,' - ',e.comp_name) AS comp_from, CONCAT(g.comp_number,' - ',g.comp_name) AS comp_to, dsg.design_code AS `code`, dsg.design_display_name AS `name`, SUM(ad.prod_order_ret_out_det_qty) AS `qty` 
                                                                        ,IFNULL(retin.qty_ret_in,0) AS qty_retin
                                                                        ,SUM(ad.prod_order_ret_out_det_qty)-IFNULL(retin.qty_ret_in,0) AS diff_qty
                                                                        ,DATEDIFF(DATE(NOW()),a.`prod_order_ret_out_due_date`) AS overdue
                                                                        FROM tb_prod_order_ret_out a 
                                                                        INNER JOIN tb_prod_order_ret_out_det ad ON ad.id_prod_order_ret_out = a.id_prod_order_ret_out AND a.is_closed=2
                                                                        INNER JOIN tb_prod_order b ON a.id_prod_order = b.id_prod_order 
                                                                        INNER JOIN tb_prod_demand_design b1 ON b.id_prod_demand_design = b1.id_prod_demand_design 
                                                                        INNER JOIN tb_m_design dsg ON dsg.id_design = b1.id_design 
                                                                        INNER JOIN tb_prod_demand b2 ON b2.id_prod_demand = b1.id_prod_demand 
                                                                        INNER JOIN tb_season c ON b2.id_season = c.id_season 
                                                                        INNER JOIN tb_m_comp_contact d ON d.id_comp_contact = a.id_comp_contact_from 
                                                                        INNER JOIN tb_m_comp e ON d.id_comp = e.id_comp 
                                                                        INNER JOIN tb_m_comp_contact f ON f.id_comp_contact = a.id_comp_contact_to 
                                                                        INNER JOIN tb_m_comp g ON f.id_comp = g.id_comp 
                                                                        INNER JOIN tb_lookup_report_status h ON a.id_report_status = h.id_report_status 
                                                                        LEFT JOIN 
                                                                        (
                                                                        	SELECT id_prod_order_ret_out,SUM(prod_order_ret_in_det_qty) AS qty_ret_in
                                                                        	FROM `tb_prod_order_ret_in_det` retid
                                                                        	INNER JOIN `tb_prod_order_ret_in` reti ON reti.`id_prod_order_ret_in`=retid.`id_prod_order_ret_in`
                                                                        	WHERE reti.`id_report_status`!=5
                                                                        	GROUP BY reti.`id_prod_order_ret_out`
                                                                        )retin ON retin.id_prod_order_ret_out=a.`id_prod_order_ret_out`
                                                                        WHERE a.`prod_order_ret_out_due_date` >= '2020-07-01' AND a.`prod_order_ret_out_due_date` <= DATE_ADD(DATE(NOW()),INTERVAL 3 DAY) AND a.`id_report_status`=6
                                                                        GROUP BY a.id_prod_order_ret_out 
                                                                        HAVING qty > qty_retin
                                                                        ORDER BY a.id_prod_order_ret_out DESC"
                    Dim dtqc As DataTable = execute_query(qqc, -1, True, "", "", "", "")
                    If dtqc.Rows.Count > 0 Then
                        Dim mail As ClassSendEmail = New ClassSendEmail()
                        mail.par1 = dtqc.Rows.Count.ToString
                        mail.send_mail_qc()
                    End If
                End If
            End If

            'polis
            If get_opt_scheduler_field("is_active_polis").ToString = "1" Then
                'Polis reminder
                If Date.Parse(TEPolis.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
                    Dim qpolis As String = "SELECT p.id_polis,DATEDIFF(p.end_date,DATE(NOW())) AS expired_in,pol_by.comp_name AS comp_name_polis,CONCAT(c.comp_number,' - ',c.`comp_name`) AS polis_object,c.`address_primary` AS polis_object_location,p.`number` AS polis_number,d.`description` AS polis_untuk,p.`premi`,p.`start_date`,p.`end_date` 
                        FROM tb_polis p 
                        INNER JOIN tb_m_comp c ON c.`id_comp`=p.`id_reff` AND p.`id_polis_cat`=1
                        INNER JOIN tb_m_comp pol_by ON pol_by.id_comp=p.id_polis_by
                        INNER JOIN `tb_lookup_desc_premi` d ON d.`id_desc_premi`=p.`id_desc_premi`
                        WHERE p.`is_active`=1 AND DATEDIFF(p.end_date,DATE(NOW()))<60"
                    Dim dtpolis As DataTable = execute_query(qpolis, -1, True, "", "", "", "")
                    If dtpolis.Rows.Count > 0 Then
                        Dim mail As ClassSendEmail = New ClassSendEmail()
                        mail.par1 = dtpolis.Rows.Count.ToString
                        mail.send_mail_polis()
                    End If
                End If
            End If

            'PO OG
            If get_opt_scheduler_field("is_active_po_og").ToString = "1" Then
                If Date.Parse(TEPOOG.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
                    Dim qpoog As String = "SELECT po.`id_purc_order`,po.`purc_order_number`,po.`date_created`,po.`est_date_receive`,reqd.`id_item`,it.`item_desc`,reqd.`item_detail`,req.id_user_created,emp.`employee_name` AS req_by,req.`requirement_date`
                                                ,IFNULL(rec.qty,0) AS rec_qty,reqd.`qty` AS req_qty,pod.`qty` AS po_qty,c.`comp_name`,DATEDIFF(po.`est_date_receive`,DATE(NOW())) AS expired_in
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
                                                GROUP BY po.`id_purc_order`"
                    Dim dtpog As DataTable = execute_query(qpoog, -1, True, "", "", "", "")
                    If dtpog.Rows.Count > 0 Then
                        Dim mail As ClassSendEmail = New ClassSendEmail()
                        mail.par1 = dtpog.Rows.Count.ToString
                        mail.send_mail_po_og()
                    End If
                End If
            End If

            'Reminder Serah Terima
            If get_opt_scheduler_field("is_active_serah_terima_qc").ToString = "1" Then
                If Date.Parse(TESerahTerimaQC.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
                    Dim qserah_terima As String = "SELECT pl.`id_pl_prod_order`,po.`prod_order_number`,pl.`pl_prod_order_number`,pl.`pl_prod_order_date`,d.`design_code`,d.`design_display_name`,rec.`id_pl_prod_order_rec`,pl.`id_comp_contact_to`
                                                                    ,pl.`complete_date`,DATEDIFF(DATE(NOW()),DATE(pl.`complete_date`)) AS diff_date_serah_terima
                                                                    ,cat.`pl_category`
                                                                    FROM `tb_pl_prod_order` pl 
                                                                    INNER JOIN tb_prod_order po ON po.`id_prod_order`=pl.`id_prod_order`
                                                                    INNER JOIN tb_prod_demand_design pdd ON pdd.`id_prod_demand_design`=po.`id_prod_demand_design`
                                                                    INNER JOIN tb_m_design d ON d.`id_design`=pdd.`id_design`
                                                                    INNER JOIN tb_m_comp_contact cc ON cc.`id_comp_contact`=pl.`id_comp_contact_to`
                                                                    INNER JOIN tb_m_comp c ON c.`id_comp`=cc.`id_comp` AND c.id_departement='6'
                                                                    INNER JOIN tb_lookup_pl_category cat ON cat.`id_pl_category`=pl.`id_pl_category`
                                                                    LEFT JOIN `tb_pl_prod_order_rec` rec ON rec.`id_pl_prod_order`=pl.`id_pl_prod_order` AND rec.`id_report_status`!=5
                                                                    WHERE pl.`id_report_status`=6 AND ISNULL(rec.`id_pl_prod_order_rec`)
                                                                    AND DATEDIFF(DATE(NOW()),DATE(pl.`complete_date`))>18"
                    Dim dt_serah_terima As DataTable = execute_query(qserah_terima, -1, True, "", "", "", "")
                    If dt_serah_terima.Rows.Count > 0 Then
                        Dim mail As ClassSendEmail = New ClassSendEmail()
                        mail.par1 = dt_serah_terima.Rows.Count.ToString
                        mail.send_mail_email_serah_terima_qc()
                    End If
                End If
            End If

            'PR OG
            If get_opt_scheduler_field("is_active_pr_og").ToString = "1" Then
                If Date.Parse(TEPROG.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
                    Dim qprog As String = "SELECT pr.`id_purc_req`
                                    FROM tb_purc_req_det prd
                                    INNER JOIN tb_purc_req pr ON pr.`id_purc_req`=prd.`id_purc_req` AND pr.`id_report_status`=6 AND pr.`is_cancel`=2 AND prd.`is_close`=2 AND prd.`is_unable_fulfill`=2
                                    INNER JOIN tb_m_departement dep ON dep.`id_departement`=pr.`id_departement`
                                    INNER JOIN tb_m_user usrc ON usrc.`id_user`=pr.`id_user_created`
                                    INNER JOIN tb_m_employee empc ON empc.`id_employee`=usrc.`id_employee` AND empc.`id_employee_active`=1
                                    LEFT JOIN 
                                    (
                                    	SELECT pod.id_purc_req_det,SUM(pod.`qty`) AS qty,GROUP_CONCAT(DISTINCT po.purc_order_number ORDER BY po.purc_order_number) AS po_number
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
                                    WHERE (prd.`qty`>IFNULL(po.qty,0) OR prd.`qty`>IFNULL(rec.qty,0)) AND DATE(pr.requirement_date)<DATE_ADD(NOW(),INTERVAL 4 DAY) "
                    Dim dtprog As DataTable = execute_query(qprog, -1, True, "", "", "", "")
                    If dtprog.Rows.Count > 0 Then
                        Dim mail As ClassSendEmail = New ClassSendEmail()
                        mail.par1 = dtprog.Rows.Count.ToString
                        mail.send_mail_pr_og()
                    End If
                End If
            End If

            'pib review notif
            If get_opt_scheduler_field("is_active_pib_review_notif").ToString = "1" Then
                If Date.Parse(TEPIBNotif.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
                    Dim qpib As String = "SELECT * 
            FROM tb_pib_review
            WHERE is_active=1 AND is_notified=1 AND notif_triggered_date=DATE_SUB(DATE(NOW()),INTERVAL 1 DAY)"
                    Dim dtpib As DataTable = execute_query(qpib, -1, True, "", "", "", "")
                    If dtpib.Rows.Count > 0 Then
                        Dim mail As ClassSendEmail = New ClassSendEmail()
                        mail.send_mail_pib_notif()
                    End If
                End If
            End If

            'eos reminder
            If get_opt_scheduler_field("is_active_eos_reminder").ToString = "1" Then
                If Date.Parse(TEEOSNotif.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
                    Dim day_coll As String = ""
                    For i As Integer = 0 To GVEOS.RowCount - 1
                        If i > 0 Then
                            day_coll += ","
                        End If
                        day_coll += GVEOS.GetRowCellValue(i, "eos_day").ToString
                    Next
                    Try
                        Dim mail As ClassSendEmail = New ClassSendEmail()
                        mail.comment_by = day_coll
                        mail.send_email_eos()
                    Catch ex As Exception
                        'log
                        Dim query_log As String = "INSERT INTO tb_eos_reminder_log(`log_date`,`log_note`) VALUES(NOW(),'Error : " + addSlashes(ex.ToString) + "')"
                        execute_non_query(query_log, True, "", "", "", "")
                    End Try
                End If
            End If

            'eos send mail price
            If get_opt_scheduler_field("is_active_eos_mail_sch").ToString = "1" Then
                If Date.Parse(TEPriceEOSNotif.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
                    Dim day_coll As String = ""
                    For i As Integer = 0 To GVPriceEOS.RowCount - 1
                        If i > 0 Then
                            day_coll += ","
                        End If
                        day_coll += GVPriceEOS.GetRowCellValue(i, "sch_day").ToString
                    Next

                    'cari yang sesuai jadwl
                    Dim qry As String = "SELECT p.id_pp_change, p.number, 
                    DATE_FORMAT(p.effective_date,'%d-%m-%Y') AS `start_date`,
                    DATE_FORMAT(p.effective_date,'%d %M %Y') AS `start_date_display`, 
                    DATE_FORMAT(p.plan_end_date,'%d-%m-%Y') AS `end_date`, 
                    p.note, datediff(DATE(NOW()), p.effective_date) AS `count_day`, o.price_eos_body_mail1
                    FROM tb_pp_change p 
                    JOIN tb_opt o
                    WHERE p.id_report_status=6 AND p.id_design_mkd=1 
                    -- AND p.effective_date>=NOW()
                    GROUP BY p.id_pp_change
                    HAVING count_day IN (" + day_coll + ") "
                    Dim deos As DataTable = execute_query(qry, -1, True, "", "", "", "")
                    If deos.Rows.Count > 0 Then
                        For ep As Integer = 0 To deos.Rows.Count - 1
                            Dim id_pp_eos As String = deos.Rows(ep)("id_pp_change").ToString
                            Dim qmail As String = "SELECT cg.id_comp_group, cg.comp_group, cg.description 
                            FROM tb_mail_to_group mtg 
                            INNER JOIN tb_m_comp_group cg ON cg.id_comp_group = mtg.id_comp_group
                            WHERE mtg.report_mark_type=373
                            GROUP BY mtg.id_comp_group "
                            Dim dmail As DataTable = execute_query(qmail, -1, True, "", "", "", "")
                            For dx As Integer = 0 To dmail.Rows.Count - 1
                                Try
                                    Dim sm As New ClassSendEmail()
                                    sm.report_mark_type = "373"
                                    sm.id_report = id_pp_eos
                                    sm.par1 = dmail.Rows(dx)("id_comp_group").ToString
                                    sm.send_email_price_eos()
                                    Dim qlog As String = "INSERT INTO tb_pp_change_email_log(id_pp_change, log_note, log_date) VALUES('" + id_pp_eos + "', 'Success: Sending mail to " + addSlashes(dmail.Rows(dx)("description").ToString) + "', NOW()); "
                                    execute_non_query(qlog, True, "", "", "", "")
                                Catch ex As Exception
                                    Dim qlog As String = "INSERT INTO tb_pp_change_email_log(id_pp_change, log_note, log_date) VALUES('" + id_pp_eos + "', 'Error:" + addSlashes(ex.ToString) + "', NOW()); "
                                    execute_non_query(qlog, True, "", "", "", "")
                                End Try
                            Next
                        Next
                    End If
                End If
            End If

            'big sale mail price
            If get_opt_scheduler_field("is_active_bsp_mail_sch").ToString = "1" Then
                If Date.Parse(TEBSPNotif.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
                    Dim day_coll As String = ""
                    For i As Integer = 0 To GVBSP.RowCount - 1
                        If i > 0 Then
                            day_coll += ","
                        End If
                        day_coll += GVBSP.GetRowCellValue(i, "sch_day").ToString
                    Next

                    'cari yang sesuai jadwl
                    Dim qry As String = "SELECT p.id_bsp, p.number, p.id_comp, CONCAT(c.comp_number, ' - ', c.comp_name) AS `comp`,c.id_comp_group, cg.description,
                    DATE_FORMAT(p.start_date,'%d-%m-%Y') AS `start_date`, 
                    DATE_FORMAT(p.start_date,'%d %M %Y') AS `start_date_display`,
                    DATE_FORMAT(p.end_date,'%d-%m-%Y') AS `end_date`, 
                    DATE_FORMAT(p.end_date,'%d %M %Y') AS `end_date_display`,
                    p.note, datediff(DATE(NOW()), p.start_date) AS `count_day`, o.bsp_body_mail1
                    FROM tb_bsp p 
                    INNER JOIN tb_m_comp c ON c.id_comp = p.id_comp 
                    INNER JOIN tb_m_comp_group cg ON cg.id_comp_group = c.id_comp_group
                    JOIN tb_opt o
                    WHERE p.id_report_status=6
                    GROUP BY p.id_bsp
                    HAVING count_day IN (" + day_coll + ") "
                    Dim dbs As DataTable = execute_query(qry, -1, True, "", "", "", "")
                    If dbs.Rows.Count > 0 Then
                        For b As Integer = 0 To dbs.Rows.Count - 1
                            Dim id_bsp As String = dbs.Rows(b)("id_bsp").ToString
                            Dim id_comp As String = dbs.Rows(b)("id_comp").ToString
                            Dim comp As String = dbs.Rows(b)("comp").ToString
                            Dim id_comp_group As String = dbs.Rows(b)("id_comp_group").ToString
                            Dim comp_group As String = dbs.Rows(b)("description").ToString
                            Try
                                Dim sm As New ClassSendEmail()
                                sm.report_mark_type = "373"
                                sm.id_report = id_bsp
                                sm.par1 = id_comp_group
                                sm.par2 = id_comp
                                sm.send_email_bsp()
                                Dim qlog As String = "INSERT INTO tb_bsp_mail_log(id_bsp, log_note, log_date) VALUES(" + id_bsp + ", 'Success: Sending mail to " + addSlashes(comp_group) + "', NOW()); "
                                execute_non_query(qlog, True, "", "", "", "")
                            Catch ex As Exception
                                Dim qlog As String = "INSERT INTO tb_bsp_mail_log(id_bsp, log_note, log_date) VALUES(" + id_bsp + ", 'Error:" + addSlashes(ex.ToString) + "', NOW()); "
                                execute_non_query(qlog, True, "", "", "", "")
                            End Try
                        Next
                    End If
                End If
            End If

            'line list notif
            If get_opt_scheduler_field("is_active_line_list").ToString = "1" Then
                If Date.Parse(TELineList.EditValue.ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss") Then
                    Dim ll As New ClassLineList()
                    Dim dtl As DataTable = ll.dataNotifSummary
                    If dtl.Rows.Count > 0 Then
                        'line list notif
                        Try
                            Dim sm As New ClassSendEmail()
                            sm.report_mark_type = "397"
                            sm.dt = dtl
                            sm.send_email_line_list()
                        Catch ex As Exception
                            Dim qlog As String = "INSERT INTO tb_log_line_list_mail(log_note, log_date) VALUES('Error Notif Line List:" + addSlashes(ex.ToString) + "', NOW()); "
                            execute_non_query(qlog, True, "", "", "", "")
                        End Try

                        'drop/changes
                        Try
                            Dim qcek As String = "CALL view_drop_changes_list()"
                            Dim dcek As DataTable = execute_query(qcek, -1, True, "", "", "", "")
                            If dcek.Rows.Count > 0 Then
                                Dim sm As New ClassSendEmail()
                                sm.report_mark_type = "394"
                                sm.send_email_drop_changes()
                            End If
                        Catch ex As Exception
                            Dim qlog As String = "INSERT INTO tb_log_line_list_mail(log_note, log_date) VALUES('Error Notif Drop/Changes:" + addSlashes(ex.ToString) + "', NOW()); "
                            execute_non_query(qlog, True, "", "", "", "")
                        End Try

                        'eta update
                        Try
                            Dim qeta As String = "CALL view_eta_changes_list()"
                            Dim deta As DataTable = execute_query(qeta, -1, True, "", "", "", "")
                            If deta.Rows.Count > 0 Then
                                Dim sm As New ClassSendEmail()
                                sm.report_mark_type = "406"
                                sm.send_email_eta_changes()
                            End If
                        Catch ex As Exception
                            Dim qlog As String = "INSERT INTO tb_log_line_list_mail(log_note, log_date) VALUES('Error Notif ETA Update:" + addSlashes(ex.ToString) + "', NOW()); "
                            execute_non_query(qlog, True, "", "", "", "")
                        End Try
                    End If
                End If
            End If

            'biarkan di eksekusi terakhir, kalo mau tambah insert diatas ini
            'markatplace order status
            If get_opt_scheduler_field("is_active_mos").ToString = "1" Then
                For i As Integer = 0 To GVMOS.RowCount - 1
                    If (Date.Parse(GVMOS.GetRowCellValue(i, "schedule").ToString).ToString("HH:mm:ss") = cur_datetime.ToString("HH:mm:ss")) Then
                        Dim is_need_sync As String = execute_query("CALL view_ol_store_status_check_sync();", 0, True, "", "", "", "")
                        If is_need_sync = "1" Then
                            'split par
                            Dim time_split As String() = Split(cur_datetime.ToString("HH:mm"), ":")
                            Dim hour As Integer = Integer.Parse(time_split(0).ToString)
                            Dim minute As Integer = Integer.Parse(time_split(1).ToString)
                            Dim sch_cek As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, 0)
                            Dim sch_input As String = DateTime.Parse(sch_cek).ToString("yyyy-MM-dd HH:mm:ss")

                            Dim cmos As New ClassMOS()
                            cmos.insertLog(sch_input, "start")

                            'general
                            Dim qopt As String = "SELECT o.zalora_comp_group, o.zalora_sleep_req_time, o.blibli_comp_group FROM tb_opt o "
                            Dim dopt As DataTable = execute_query(qopt, -1, True, "", "", "", "")

                            'zalora order status
                            cmos.insertLog(sch_input, "sync status order : zalora")
                            Dim qzo As String = "CALL view_status_ol_store(" + dopt.Rows(0)("zalora_comp_group").ToString + ")"
                            Dim dzo As DataTable = execute_query(qzo, -1, True, "", "", "", "")
                            For z As Integer = 0 To dzo.Rows.Count - 1
                                Try
                                    Dim za As New ClassZaloraAPI()
                                    Dim dt As DataTable = za.get_status_update(dzo.Rows(z)("id_order").ToString, dzo.Rows(z)("item_id").ToString)
                                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                        Dim id_sales_order_det As String = dzo.Rows(z)("id_sales_order_det").ToString
                                        Dim status As String = dt.Rows(0)("order_status").ToString
                                        Dim status_date As String = dt.Rows(0)("order_status_date").ToString
                                        cmos.insertStatusOrder(id_sales_order_det, status, status_date)

                                        'for auto cn/ror
                                        If dzo.Rows(z)("id_sales_pos").ToString <> "" And (dt.Rows(0)("order_status").ToString = "canceled" Or dt.Rows(0)("order_status").ToString = "failed" Or dt.Rows(0)("order_status").ToString = "returned") Then
                                            Dim qiz As String = "INSERT INTO tb_ol_store_return_order(id_comp_group, created_date, order_number, ol_store_id, item_id, qty, id_sales_order, id_sales_order_det, id_sales_pos_det, id_sales_pos)
                                                                    VALUES('" + dopt.Rows(0)("zalora_comp_group").ToString + "', NOW(), '" + dzo.Rows(z)("order_no").ToString + "', '" + dzo.Rows(z)("ol_store_id").ToString + "', '" + dzo.Rows(z)("item_id").ToString + "','1','" + dzo.Rows(z)("id_sales_order").ToString + "', '" + dzo.Rows(z)("id_sales_order_det").ToString + "', '" + dzo.Rows(z)("id_sales_pos_det").ToString + "', '" + dzo.Rows(z)("id_sales_pos").ToString + "'); "
                                            execute_non_query(qiz, True, "", "", "", "")
                                        End If
                                    End If
                                    If (z + 1) Mod 30 = 0 Then
                                        Threading.Thread.Sleep(dopt.Rows(0)("zalora_sleep_req_time"))
                                    End If
                                Catch ex As Exception
                                    cmos.insertLog(sch_input, "err_stt_zal;item:" + dzo.Rows(z)("item_id").ToString + ";" + ex.ToString + "")
                                End Try
                            Next

                            'blibli order status
                            cmos.insertLog(sch_input, "sync status order : blibli")
                            Dim qbl As String = "CALL view_status_ol_store(" + dopt.Rows(0)("blibli_comp_group").ToString + ")"
                            Dim dbl As DataTable = execute_query(qbl, -1, True, "", "", "", "")
                            For b As Integer = 0 To dbl.Rows.Count - 1
                                Try
                                    Dim bli As New ClassBliApi()
                                    Dim dt As DataTable = bli.get_status(dbl.Rows(b)("order_no").ToString, dbl.Rows(b)("ol_store_id").ToString)
                                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                        Dim id_sales_order_det As String = dbl.Rows(b)("id_sales_order_det").ToString
                                        Dim status As String = dt.Rows(0)("order_status").ToString
                                        Dim status_date As String = DateTime.Parse(dt.Rows(0)("order_status_date").ToString).ToString("yyyy-MM-dd HH:mm")
                                        cmos.insertStatusOrder(id_sales_order_det, status, status_date)

                                        'for auto cn/ror
                                        If dbl.Rows(b)("id_sales_pos").ToString <> "" And (dt.Rows(0)("order_status").ToString = "cancelled" Or dt.Rows(0)("order_status").ToString = "failed") Then
                                            Dim qib As String = "INSERT INTO tb_ol_store_return_order(id_comp_group, created_date, order_number, ol_store_id, item_id, qty, id_sales_order, id_sales_order_det, id_sales_pos_det, id_sales_pos)
                                                                    VALUES('" + dopt.Rows(0)("blibli_comp_group").ToString + "', NOW(), '" + dbl.Rows(b)("order_no").ToString + "', '" + dbl.Rows(b)("ol_store_id").ToString + "', '" + dbl.Rows(b)("item_id").ToString + "','1','" + dbl.Rows(b)("id_sales_order").ToString + "', '" + dbl.Rows(b)("id_sales_order_det").ToString + "', '" + dbl.Rows(b)("id_sales_pos_det").ToString + "', '" + dbl.Rows(b)("id_sales_pos").ToString + "'); "
                                            execute_non_query(qib, True, "", "", "", "")
                                        End If
                                    End If
                                Catch ex As Exception
                                    cmos.insertLog(sch_input, "err_stt_bli;ol_id:" + dbl.Rows(b)("ol_store_id").ToString + ";" + ex.ToString + "")
                                End Try
                            Next

                            'blibli ror
                            cmos.insertLog(sch_input, "sync ROR : blibli")
                            Dim bliror As New ClassBliApi()
                            Try
                                bliror.get_ror_list()
                            Catch ex As Exception
                                cmos.insertLog(sch_input, "err_ror_bli;" + ex.ToString)
                            End Try


                            'action set return
                            cmos.insertLog(sch_input, "set status returned : blibli")
                            Try
                                bliror.set_to_returned()
                            Catch ex As Exception
                                cmos.insertLog(sch_input, "err_set_returned_bli;" + ex.ToString)
                            End Try


                            'processing auto cn/ror
                            If get_opt_scheduler_field("is_active_auto_cn_ror").ToString = "1" Then
                                cmos.insertLog(sch_input, "auto cn & ror")
                                Try
                                    Dim qcr As String = "SELECT olr.id_sales_order, olr.id_sales_pos, olr.order_number
                                                            FROM tb_ol_store_return_order olr
                                                            WHERE olr.is_process=2 AND olr.is_manual_sync=2 AND !ISNULL(olr.id_sales_order) AND !ISNULL(olr.id_sales_pos)
                                                            GROUP BY olr.id_sales_order, olr.id_sales_pos
                                                            ORDER BY olr.created_date ASC "
                                    Dim dcr As DataTable = execute_query(qcr, -1, True, "", "", "", "")
                                    For c As Integer = 0 To dcr.Rows.Count - 1
                                        cmos.insertLog(sch_input, "auto_cn_ror_" + dcr.Rows(c)("order_number").ToString)
                                        execute_non_query_long("CALL create_ol_store_cn_ror(" + dcr.Rows(c)("id_sales_order").ToString + ", " + dcr.Rows(c)("id_sales_pos").ToString + "); ", True, "", "", "", "")
                                    Next
                                Catch ex As Exception
                                    cmos.insertLog(sch_input, "err_cn_ror;" + ex.ToString)
                                End Try
                            End If
                            cmos.insertLog(sch_input, "end")
                        End If
                    End If
                Next
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

        Dim query As String = "SELECT * FROM tb_m_fingerprint WHERE is_active='1'"
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
        Dim query As String = "UPDATE tb_opt_scheduler SET evaluation_ar_time='" & Date.Parse(TEEvaluationAR.EditValue.ToString).ToString("HH:mm:ss") & "',
        evaluation_ar_day='" + LEDayAREval.EditValue.ToString + "' "
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

    Sub load_check_fail_order_time()
        'not used
        'Dim query As String = ""
        'query = "SELECT check_vios_fail_order_time FROM tb_opt_scheduler LIMIT 1"
        'Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")
        'TECheckFailOrder.EditValue = data.Rows(0)("check_vios_fail_order_time")
    End Sub

    Sub load_schedule_close_ol_order()
        Dim query As String = "SELECT s.id_schedule, s.schedule_desc, s.`schedule` FROM tb_ol_store_order_fail_schedule s
        ORDER BY s.`schedule` ASC "
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")
        GCSchCloseOrder.DataSource = data
    End Sub

    Private Sub BSaveKurs_Click(sender As Object, e As EventArgs) Handles BSaveKurs.Click
        Dim query_log As String = "UPDATE tb_opt_scheduler SET get_kurs_day='" & LEDayKurs.EditValue.ToString & "',`get_kurs_time`='" & Date.Parse(TETimeKurs.EditValue.ToString).ToString("HH:mm:ss") & "'"
        execute_non_query(query_log, True, "", "", "", "")
        MsgBox("Kurs Schedule saved.")
    End Sub

    Private Sub BtnFailOrder_Click(sender As Object, e As EventArgs) Handles BtnFailOrder.Click
        'Dim query_log As String = "UPDATE tb_opt_scheduler SET `check_vios_fail_order_time`='" & Date.Parse(TECheckFailOrder.EditValue.ToString).ToString("HH:mm:ss") & "'"
        'execute_non_query(query_log, True, "", "", "", "")
        'MsgBox("Check Fail Order Schedule saved.")
        'Dim date_cek As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 30, 0)
        'Dim date_order As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 33, 30)
        'Dim diff As Long = (date_cek - date_order).TotalMinutes
        'MsgBox(diff.ToString)
        load_schedule_close_ol_order()
    End Sub

    Sub load_sales_return_order()
        Dim query As String = ""
        query = "SELECT sales_return_order FROM tb_opt_scheduler LIMIT 1"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")

        TESalesReturnOrder.EditValue = data.Rows(0)("sales_return_order")
    End Sub

    Private Sub SBSalesReturnOrder_Click(sender As Object, e As EventArgs) Handles SBSalesReturnOrder.Click
        Dim query As String = "UPDATE tb_opt_scheduler SET sales_return_order='" & Date.Parse(TESalesReturnOrder.EditValue.ToString).ToString("HH:mm:ss") & "'"
        execute_non_query(query, True, "", "", "", "")
        MsgBox("Sales Return Order Time saved.")
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        load_schedule_close_ol_order()
    End Sub

    Private Sub BtnRefMOS_Click(sender As Object, e As EventArgs) Handles BtnRefMOS.Click
        load_schedule_mos()
    End Sub

    Sub load_schedule_mos()
        Dim query As String = "SELECT s.id_schedule, s.schedule_desc, s.`schedule` 
        FROM tb_ol_store_status_schedule s
        ORDER BY s.`schedule` ASC "
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")
        GCMOS.DataSource = data
    End Sub

    Private Sub BPOOG_Click(sender As Object, e As EventArgs) Handles BPOOG.Click
        Dim query As String = "UPDATE tb_opt_scheduler SET po_og_time='" & Date.Parse(TEPOOG.EditValue.ToString).ToString("HH:mm:ss") & "'"
        execute_non_query(query, True, "", "", "", "")
        MsgBox("Email Notice PO OG Time saved.")
    End Sub

    Private Sub BPROG_Click(sender As Object, e As EventArgs) Handles BPROG.Click
        Dim query As String = "UPDATE tb_opt_scheduler SET pr_og_time='" & Date.Parse(TEPROG.EditValue.ToString).ToString("HH:mm:ss") & "'"
        execute_non_query(query, True, "", "", "", "")
        MsgBox("Email Notice PR OG Time saved.")
    End Sub

    Private Sub BSerahTerimaQC_Click(sender As Object, e As EventArgs) Handles BSerahTerimaQC.Click
        Dim query As String = "UPDATE tb_opt_scheduler SET serah_terima_qc_time='" & Date.Parse(TESerahTerimaQC.EditValue.ToString).ToString("HH:mm:ss") & "'"
        execute_non_query(query, True, "", "", "", "")
        MsgBox("Email Notice Serah Terima QC saved.")
    End Sub

    Private Sub BPIBNotif_Click(sender As Object, e As EventArgs) Handles BPIBNotif.Click
        Dim query As String = "UPDATE tb_opt_scheduler SET pib_review_notif_time='" & Date.Parse(TEPIBNotif.EditValue.ToString).ToString("HH:mm:ss") & "'"
        execute_non_query(query, True, "", "", "", "")
        MsgBox("PIB Notif saved.")
    End Sub

    Sub load_pib_review_notif()
        Dim query As String = ""
        query = "SELECT pib_review_notif_time FROM tb_opt_scheduler LIMIT 1"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")

        TEPIBNotif.EditValue = data.Rows(0)("pib_review_notif_time")
    End Sub

    Sub load_eos()
        'time
        Dim query As String = ""
        query = "SELECT o.eos_reminder_time FROM tb_opt_scheduler o LIMIT 1"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")
        TEEOSNotif.EditValue = data.Rows(0)("eos_reminder_time")

        'range days
        Dim qlist As String = "SELECT er.eos_day, er.note FROM tb_eos_reminder er "
        Dim dlist As DataTable = execute_query(qlist, -1, True, "", "", "", "")
        GCEOS.DataSource = dlist
        GVEOS.BestFitColumns()
    End Sub

    Private Sub BtnEOS_Click(sender As Object, e As EventArgs) Handles BtnEOS.Click
        Dim query As String = "UPDATE tb_opt_scheduler SET eos_reminder_time='" & Date.Parse(TEEOSNotif.EditValue.ToString).ToString("HH:mm:ss") & "'"
        execute_non_query(query, True, "", "", "", "")
        MsgBox("EOS Notif saved.")
    End Sub

    Private Sub BtnRefreshEOS_Click(sender As Object, e As EventArgs) Handles BtnRefreshEOS.Click
        load_eos()
    End Sub

    Sub load_price_eos()
        'time
        Dim query As String = "SELECT o.eos_mail_sch_time FROM tb_opt_scheduler o LIMIT 1"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")
        TEPriceEOSNotif.EditValue = data.Rows(0)("eos_mail_sch_time")

        'range days
        Dim qlist As String = "SELECT er.sch_day, er.note FROM tb_eos_mail_sch er "
        Dim dlist As DataTable = execute_query(qlist, -1, True, "", "", "", "")
        GCPriceEOS.DataSource = dlist
        GVPriceEOS.BestFitColumns()
    End Sub

    Private Sub BtnPriceEOS_Click(sender As Object, e As EventArgs) Handles BtnPriceEOS.Click
        Dim query As String = "UPDATE tb_opt_scheduler SET eos_mail_sch_time='" & Date.Parse(TEPriceEOSNotif.EditValue.ToString).ToString("HH:mm:ss") & "'"
        execute_non_query(query, True, "", "", "", "")
        MsgBox("EOS Price Time saved.")
    End Sub

    Private Sub BtnRefreshPriceEOS_Click(sender As Object, e As EventArgs) Handles BtnRefreshPriceEOS.Click
        load_price_eos()
    End Sub

    Sub load_bsp()
        'time
        Dim query As String = "SELECT o.bsp_mail_sch_time FROM tb_opt_scheduler o LIMIT 1"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")
        TEBSPNotif.EditValue = data.Rows(0)("bsp_mail_sch_time")

        'range days
        Dim qlist As String = "SELECT er.sch_day, er.note FROM tb_bsp_mail_sch er ORDER BY er.sch_day ASC"
        Dim dlist As DataTable = execute_query(qlist, -1, True, "", "", "", "")
        GCBSP.DataSource = dlist
        GVBSP.BestFitColumns()
    End Sub

    Sub load_line_list()
        'time
        Dim query As String = "SELECT o.line_list_sch_time FROM tb_opt_scheduler o LIMIT 1"
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")
        TELineList.EditValue = data.Rows(0)("line_list_sch_time")
    End Sub

    Private Sub BtnBSP_Click(sender As Object, e As EventArgs) Handles BtnBSP.Click
        Dim query As String = "UPDATE tb_opt_scheduler SET bsp_mail_sch_time='" & Date.Parse(TEBSPNotif.EditValue.ToString).ToString("HH:mm:ss") & "'"
        execute_non_query(query, True, "", "", "", "")
        MsgBox("Big Sale Price Time saved.")
    End Sub

    Private Sub BtnRefreshBSP_Click(sender As Object, e As EventArgs) Handles BtnRefreshBSP.Click
        load_bsp()
    End Sub

    Private Sub BtnLineList_Click(sender As Object, e As EventArgs) Handles BtnLineList.Click
        Dim query As String = "UPDATE tb_opt_scheduler SET line_list_sch_time='" & Date.Parse(TELineList.EditValue.ToString).ToString("HH:mm:ss") & "'"
        execute_non_query(query, True, "", "", "", "")
        MsgBox("Email Line List Notif saved.")
    End Sub
End Class