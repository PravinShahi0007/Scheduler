Public Class ReportEmpAttn
    Public Shared id_dept As String = "-1"

    Public Shared is_head_dept As String = "-1"

    Public Shared date_label As String = ""
    Public Shared dept_label As String = ""

    Sub load_report()
        Dim dept As String

        If id_dept = "0" Then
            dept = "%%"
        Else
            dept = id_dept
        End If

        Dim query As String = ""

        If is_head_dept = "-1" Then
            query = "SELECT tb.*,(tb.over-tb.late-tb.over_break) AS balance,IF(NOT ISNULL(tb.att_in) AND NOT ISNULL(tb.att_out),'Yes','No') AS present FROM"
            query += " ("
            query += " SELECT sch.id_schedule,lvl.employee_level,emp.employee_position,ket.id_leave_type,ket.leave_type,sch.info_leave,active.employee_active,active.id_employee_active,sch.id_employee,emp.employee_name,emp.employee_code,emp.id_departement,dept.departement,sch.date, "
            query += " sch.in,sch.in_tolerance,MIN(at_in.datetime) As `att_in`, "
            query += " sch.out,MAX(at_out.datetime) AS `att_out`, "
            query += " sch.break_out,MIN(at_brout.datetime) As start_break, "
            query += " sch.break_in,MAX(at_brin.datetime) AS end_break, "
            query += " scht.schedule_type,note ,"
            query += " IF(MIN(at_in.datetime)>sch.in_tolerance,TIMESTAMPDIFF(MINUTE,sch.in_tolerance,MIN(at_in.datetime)),0) AS late ,"
            query += " TIMESTAMPDIFF(MINUTE,sch.out,MAX(at_out.datetime)) As over ,"
            query += " IF(TIMESTAMPDIFF(MINUTE,MIN(at_brout.datetime),MAX(at_brin.datetime))>TIMESTAMPDIFF(MINUTE,sch.break_out,sch.break_in),"
            query += " TIMESTAMPDIFF(MINUTE,MIN(at_brout.datetime),MAX(at_brin.datetime))-TIMESTAMPDIFF(MINUTE,sch.break_out,sch.break_in),0) As over_break ,"
            query += " TIMESTAMPDIFF(MINUTE,MIN(at_in.datetime),MAX(at_out.datetime)) AS actual_work_hour ,"
            query += " TIMESTAMPDIFF(MINUTE,If(MIN(at_in.datetime)<sch.In,sch.In,If(MIN(at_in.datetime)<sch.in_tolerance,sch.In,MIN(at_in.datetime))),If(MAX(at_out.datetime)>sch.out,sch.out,MAX(at_out.datetime))) As work_hour "
            query += " FROM tb_emp_schedule sch "
            query += " LEFT JOIN tb_lookup_leave_type ket ON ket.id_leave_type=sch.id_leave_type "
            query += " INNER JOIN tb_m_employee emp On emp.id_employee=sch.id_employee "
            query += " INNER JOIN tb_lookup_employee_level lvl ON lvl.id_employee_level=emp.id_employee_level "
            query += " INNER JOIN tb_m_departement dept On dept.id_departement=emp.id_departement "
            query += " INNER JOIN tb_lookup_schedule_type scht On scht.id_schedule_type=sch.id_schedule_type "
            query += " INNER JOIN tb_lookup_employee_active active on emp.id_employee_active=active.id_employee_active"
            query += " LEFT JOIN tb_emp_attn at_in ON at_in.id_employee = sch.id_employee AND (at_in.datetime>=(sch.out - INTERVAL 1 DAY) AND at_in.datetime<=sch.out) AND at_in.type_log = 1 "
            query += " LEFT JOIN tb_emp_attn at_out ON at_out.id_employee = sch.id_employee AND (at_out.datetime>=sch.in AND at_out.datetime<=(sch.in + INTERVAL 1 DAY)) AND at_out.type_log = 2 "
            query += " LEFT JOIN tb_emp_attn at_brout On at_brout.id_employee=sch.id_employee And Date(at_brout.datetime) = sch.Date And at_brout.type_log = 3 "
            query += " LEFT JOIN tb_emp_attn at_brin On at_brin.id_employee=sch.id_employee And Date(at_brin.datetime) = sch.Date And at_brin.type_log = 4 "
            query += " WHERE emp.id_departement Like '" & dept & "' AND emp.id_employee_active='1'"
            'this is last week from monday till sunday
            query += " AND YEARWEEK(sch.`date`,1) = YEARWEEK(NOW(),1) - 1"
            query += " GROUP BY sch.id_schedule"
            query += " ) tb"
        Else
            query = "SELECT tb.*,(tb.over-tb.late-tb.over_break) AS balance,IF(NOT ISNULL(tb.att_in) AND NOT ISNULL(tb.att_out),'Yes','No') AS present FROM
                    (
                    SELECT sch.id_schedule,lvl.employee_level,emp.employee_position,ket.id_leave_type,ket.leave_type,sch.info_leave,active.employee_active,active.id_employee_active,sch.id_employee,emp.employee_name,emp.employee_code,emp.id_departement,dept.departement,sch.date, 
                    sch.in,sch.in_tolerance,MIN(at_in.datetime) AS `att_in`, 
                    sch.out,MAX(at_out.datetime) AS `att_out`, 
                    sch.break_out,MIN(at_brout.datetime) AS start_break, 
                    sch.break_in,MAX(at_brin.datetime) AS end_break, 
                    scht.schedule_type,note ,
                    IF(MIN(at_in.datetime)>sch.in_tolerance,TIMESTAMPDIFF(MINUTE,sch.in_tolerance,MIN(at_in.datetime)),0) AS late ,
                    TIMESTAMPDIFF(MINUTE,sch.out,MAX(at_out.datetime)) AS over ,
                    IF(TIMESTAMPDIFF(MINUTE,MIN(at_brout.datetime),MAX(at_brin.datetime))>TIMESTAMPDIFF(MINUTE,sch.break_out,sch.break_in),
                    TIMESTAMPDIFF(MINUTE,MIN(at_brout.datetime),MAX(at_brin.datetime))-TIMESTAMPDIFF(MINUTE,sch.break_out,sch.break_in),0) AS over_break ,
                    TIMESTAMPDIFF(MINUTE,MIN(at_in.datetime),MAX(at_out.datetime)) AS actual_work_hour ,
                    TIMESTAMPDIFF(MINUTE,IF(MIN(at_in.datetime)<sch.In,sch.In,IF(MIN(at_in.datetime)<sch.in_tolerance,sch.In,MIN(at_in.datetime))),IF(MAX(at_out.datetime)>sch.out,sch.out,MAX(at_out.datetime))) AS work_hour
                    FROM tb_emp_schedule sch
                    LEFT JOIN tb_lookup_leave_type ket ON ket.id_leave_type=sch.id_leave_type 
                    INNER JOIN tb_m_employee emp ON emp.id_employee=sch.id_employee 
                    INNER JOIN tb_lookup_employee_level lvl ON lvl.id_employee_level=emp.id_employee_level 
                    INNER JOIN tb_m_departement dept ON dept.id_departement=emp.id_departement 
                    INNER JOIN tb_lookup_schedule_type scht ON scht.id_schedule_type=sch.id_schedule_type 
                    INNER JOIN tb_lookup_employee_active active ON emp.id_employee_active=active.id_employee_active
                    LEFT JOIN tb_emp_attn at_in ON at_in.id_employee = sch.id_employee AND (at_in.datetime>=(sch.out - INTERVAL 1 DAY) AND at_in.datetime<=sch.out) AND at_in.type_log = 1
                    LEFT JOIN tb_emp_attn at_out ON at_out.id_employee = sch.id_employee AND (at_out.datetime>=sch.in AND at_out.datetime<=(sch.in + INTERVAL 1 DAY)) AND at_out.type_log = 2
                    LEFT JOIN tb_emp_attn at_brout ON at_brout.id_employee=sch.id_employee AND DATE(at_brout.datetime) = sch.Date AND at_brout.type_log = 3
                    LEFT JOIN tb_emp_attn at_brin ON at_brin.id_employee=sch.id_employee AND DATE(at_brin.datetime) = sch.Date AND at_brin.type_log = 4
                    INNER JOIN 
                    (
	                    SELECT emp.id_employee FROM tb_m_departement dep
	                    INNER JOIN tb_m_user usr ON usr.id_user=dep.id_user_head
	                    INNER JOIN tb_m_employee emp ON emp.id_employee = usr.id_employee
	                    WHERE dep.is_office_dept='1'
	                    GROUP BY emp.id_employee
                    ) dept_head ON dept_head.id_employee=emp.id_employee
                    WHERE emp.id_employee_active='1'
                    AND YEARWEEK(sch.`date`,1) = YEARWEEK(NOW(),1) - 1
                    GROUP BY sch.id_schedule
                    ) tb"
        End If

        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")
        GCSchedule.DataSource = data
        GVSchedule.BestFitColumns()
        GVSchedule.ExpandAllGroups()
        '
        If id_dept = "0" Then
            LDept.Text = "All Departement"
        ElseIf id_dept = "dept_head" Then
            LDept.Text = "Departement Head"
        Else
            Dim query_labelx As String = "SELECT departement FROM tb_m_departement WHERE id_departement='" & id_dept & "'"
            Dim data_labelx As DataTable = execute_query(query_labelx, -1, True, "", "", "", "")
            '
            LDept.Text = data_labelx.Rows(0)("departement").ToString
            '
        End If

        Dim query_label As String = "SELECT DATE_SUB(DATE(NOW()), INTERVAL DAYOFWEEK(NOW())+5 DAY) AS mon_last_week,DATE_SUB(DATE(NOW()), INTERVAL DAYOFWEEK(NOW())-1 DAY) AS sun_last_week;"
        Dim data_label As DataTable = execute_query(query_label, -1, True, "", "", "", "")

        LDateRange.Text = Date.Parse(data_label.Rows(0)("mon_last_week").ToString).ToString("dd MMMM yyyy") & " until " & Date.Parse(data_label.Rows(0)("sun_last_week").ToString).ToString("dd MMMM yyyy")
    End Sub

    Private Sub ReportEmpAttn_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles MyBase.BeforePrint
        load_report()
    End Sub
End Class