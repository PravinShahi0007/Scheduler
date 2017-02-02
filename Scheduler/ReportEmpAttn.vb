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
            query = "SELECT tb.*,IF(NOT ISNULL(tb.att_in) AND NOT ISNULL(tb.att_out),(tb.minutes_work-tb.over_break-tb.late+IF(tb.over<0,tb.over,0)),0) AS work_hour,(tb.over-tb.late-tb.over_break) AS balance,IF(NOT ISNULL(tb.att_in) AND NOT ISNULL(tb.att_out),'yes','no') AS present FROM
                    (
                     SELECT sch.id_schedule,lvl.employee_level,emp.employee_position,ket.id_leave_type,ket.leave_type,sch.info_leave,active.employee_active,active.id_employee_active,sch.id_employee,emp.employee_name,emp.employee_code,emp.id_departement,dept.departement,sch.date, 
                     sch.in,sch.in_tolerance,MIN(at_in.datetime) AS `att_in`, 
                     sch.out,MAX(at_out.datetime) AS `att_out`, 
                     sch.break_out,MIN(at_brout.datetime) AS start_break, 
                     sch.break_in,MAX(at_brin.datetime) AS end_break, 
                     scht.schedule_type,note ,
                     sch.minutes_work,
                     IF(MIN(at_in.datetime)>sch.in_tolerance,TIMESTAMPDIFF(MINUTE,sch.in_tolerance,MIN(at_in.datetime)),0) AS late ,
                     TIMESTAMPDIFF(MINUTE,sch.out,MAX(at_out.datetime)) AS over ,
                    IF(TIMESTAMPDIFF(MINUTE,MIN(at_brout.datetime),MAX(at_brin.datetime))>TIMESTAMPDIFF(MINUTE,sch.break_out,sch.break_in),
                    TIMESTAMPDIFF(MINUTE,MIN(at_brout.datetime),MAX(at_brin.datetime))-TIMESTAMPDIFF(MINUTE,sch.break_out,sch.break_in),0) AS over_break ,
                    TIMESTAMPDIFF(MINUTE,MIN(at_in.datetime),MAX(at_out.datetime)) AS actual_work_hour 
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
                    WHERE emp.id_departement Like '" & dept & "' 
                    AND emp.id_employee_active='1'
                    AND YEARWEEK(sch.`date`,1) = YEARWEEK(NOW(),1) - 1
                    GROUP BY sch.id_schedule
                    ) tb"
            'this is last week from monday till sunday
        Else
            query = "SELECT tb.*,IF(NOT ISNULL(tb.att_in) AND NOT ISNULL(tb.att_out),(tb.minutes_work-tb.over_break-tb.late+IF(tb.over<0,tb.over,0)),0) AS work_hour,(tb.over-tb.late-tb.over_break) AS balance,IF(NOT ISNULL(tb.att_in) AND NOT ISNULL(tb.att_out),'yes','no') AS present FROM(
                     SELECT sch.id_schedule,lvl.employee_level,emp.employee_position,ket.id_leave_type,ket.leave_type,sch.info_leave,active.employee_active,active.id_employee_active,sch.id_employee,emp.employee_name,emp.employee_code,emp.id_departement,dept.departement,sch.date, 
                     sch.in,sch.in_tolerance,MIN(at_in.datetime) AS `att_in`, 
                     sch.out,MAX(at_out.datetime) AS `att_out`, 
                     sch.break_out,MIN(at_brout.datetime) AS start_break, 
                     sch.break_in,MAX(at_brin.datetime) AS end_break, 
                     scht.schedule_type,note ,
                     sch.minutes_work,
                     IF(MIN(at_in.datetime)>sch.in_tolerance,TIMESTAMPDIFF(MINUTE,sch.in_tolerance,MIN(at_in.datetime)),0) AS late ,
                     TIMESTAMPDIFF(MINUTE,sch.out,MAX(at_out.datetime)) AS over ,
                    IF(TIMESTAMPDIFF(MINUTE,MIN(at_brout.datetime),MAX(at_brin.datetime))>TIMESTAMPDIFF(MINUTE,sch.break_out,sch.break_in),
                    TIMESTAMPDIFF(MINUTE,MIN(at_brout.datetime),MAX(at_brin.datetime))-TIMESTAMPDIFF(MINUTE,sch.break_out,sch.break_in),0) AS over_break ,
                    TIMESTAMPDIFF(MINUTE,MIN(at_in.datetime),MAX(at_out.datetime)) AS actual_work_hour 
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
	                    SELECT emp.id_employee 
                        FROM tb_m_departement dep
                        INNER JOIN tb_m_user usr ON usr.id_user=dep.id_user_head
                        INNER JOIN tb_m_employee emp ON emp.id_employee = usr.id_employee
                        WHERE dep.is_office_dept='1'
                        UNION
                        SELECT id_employee FROM tb_emp_attn_spec
                        GROUP BY id_employee
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