Public Class ReportEmpLeaveStock
    Public Shared id_dept As String = "-1"
    '
    Public Shared is_head_dept As String = "-1"
    '
    Public Shared date_label As String = ""
    Public Shared dept_label As String = ""

    Sub load_report()
        Dim query As String = ""
        If is_head_dept = "-1" Then
            query = "SELECT emp.id_employee,emp.employee_position,emp.employee_code,emp.employee_name,emp.id_departement,dep.departement,lvl.employee_level,active.employee_active
                    ,SUM(IF(emp_sl.plus_minus=1,emp_sl.qty,-emp_sl.qty)) AS qty_leave
                    ,emp_sl.type,IF(emp_sl.type='1','Leave','DP') AS type_ket,emp_sl.date_expired
                    FROM tb_emp_stock_leave emp_sl
                    INNER JOIN tb_m_employee emp ON emp.id_employee=emp_sl.id_emp
                    INNER JOIN tb_lookup_employee_level lvl ON lvl.id_employee_level=emp.id_employee_level
                    INNER JOIN tb_m_departement dep ON dep.id_departement=emp.id_departement
                    INNER JOIN tb_lookup_employee_active active ON active.id_employee_active=emp.id_employee_active
                    WHERE emp_sl.is_process_exp = '2' AND emp.id_employee_active='1'
                    AND emp.id_departement='" & id_dept & "'
                    GROUP BY emp_sl.id_emp,emp_sl.type,emp_sl.date_expired
                    HAVING SUM(IF(emp_sl.plus_minus=1,emp_sl.qty,-emp_sl.qty)) > 0
                    UNION
                    SELECT emp.id_employee,emp.employee_position,emp.employee_code,emp.employee_name,emp.id_departement,dep.departement,lvl.employee_level,active.employee_active
                    ,-(SUM(adv.qty)) AS qty_leave
                    ,3 AS `type`,'Advance Leave' AS type_ket,NULL AS date_expired
                    FROM tb_emp_stock_leave_adv adv
                    INNER JOIN tb_m_employee emp ON emp.id_employee=adv.id_emp
                    INNER JOIN tb_lookup_employee_level lvl ON lvl.id_employee_level=emp.id_employee_level
                    INNER JOIN tb_m_departement dep ON dep.id_departement=emp.id_departement
                    INNER JOIN tb_lookup_employee_active active ON active.id_employee_active=emp.id_employee_active
                    WHERE emp.id_departement='" & id_dept & "'
                    GROUP BY id_emp
                    HAVING -(SUM(adv.qty)) < 0"
        Else
            query = "SELECT emp.id_employee,emp.employee_position,emp.employee_code,emp.employee_name,emp.id_departement,dep.departement,lvl.employee_level,active.employee_active
                    ,SUM(IF(emp_sl.plus_minus=1,emp_sl.qty,-emp_sl.qty)) AS qty_leave
                    ,emp_sl.type,IF(emp_sl.type='1','Leave','DP') AS type_ket,emp_sl.date_expired
                    FROM tb_emp_stock_leave emp_sl
                    INNER JOIN tb_m_employee emp ON emp.id_employee=emp_sl.id_emp
                    INNER JOIN tb_lookup_employee_level lvl ON lvl.id_employee_level=emp.id_employee_level
                    INNER JOIN tb_m_departement dep ON dep.id_departement=emp.id_departement
                    INNER JOIN tb_lookup_employee_active active ON active.id_employee_active=emp.id_employee_active
                    INNER JOIN 
                    (
	                    SELECT emp.id_employee 
                        FROM tb_m_departement dep
                        INNER JOIN tb_m_user usr ON usr.id_user=dep.id_user_head
                        INNER JOIN tb_m_employee emp ON emp.id_employee = usr.id_employee
                        WHERE dep.is_office_dept='1' AND dep.is_store='2'
                        UNION
                        SELECT id_employee FROM tb_emp_attn_spec
                        GROUP BY id_employee
                    ) dept_head ON dept_head.id_employee=emp.id_employee
                    WHERE emp_sl.is_process_exp = '2' AND emp.id_employee_active='1'
                    GROUP BY emp_sl.id_emp,emp_sl.type,emp_sl.date_expired
                    HAVING SUM(IF(emp_sl.plus_minus=1,emp_sl.qty,-emp_sl.qty)) > 0
                    UNION
                    SELECT emp.id_employee,emp.employee_position,emp.employee_code,emp.employee_name,emp.id_departement,dep.departement,lvl.employee_level,active.employee_active
                    ,-(SUM(adv.qty)) AS qty_leave
                    ,3 AS `type`,'Advance Leave' AS type_ket,NULL AS date_expired
                    FROM tb_emp_stock_leave_adv adv
                    INNER JOIN tb_m_employee emp ON emp.id_employee=adv.id_emp
                    INNER JOIN tb_lookup_employee_level lvl ON lvl.id_employee_level=emp.id_employee_level
                    INNER JOIN tb_m_departement dep ON dep.id_departement=emp.id_departement
                    INNER JOIN tb_lookup_employee_active active ON active.id_employee_active=emp.id_employee_active
                    INNER JOIN 
                    (
	                    SELECT emp.id_employee 
                        FROM tb_m_departement dep
                        INNER JOIN tb_m_user usr ON usr.id_user=dep.id_user_head
                        INNER JOIN tb_m_employee emp ON emp.id_employee = usr.id_employee
                        WHERE dep.is_office_dept='1' AND dep.is_store='2'
                        UNION
                        SELECT id_employee FROM tb_emp_attn_spec
                        GROUP BY id_employee
                    ) dept_head ON dept_head.id_employee=emp.id_employee
                    GROUP BY id_emp
                    HAVING -(SUM(adv.qty)) < 0"
        End If

        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")
        GCSchedule.DataSource = data
        GVSchedule.BestFitColumns()
        GVSchedule.ExpandAllGroups()
        '
        If id_dept = "0" Then
            LDept.Text = "All Departement"
        Else
            Dim query_labelx As String = "SELECT departement FROM tb_m_departement WHERE id_departement='" & id_dept & "'"
            Dim data_labelx As DataTable = execute_query(query_labelx, -1, True, "", "", "", "")
            '
            LDept.Text = data_labelx.Rows(0)("departement").ToString
            '
        End If

        LDateRange.Text = Date.Parse(Now().ToString).ToString("dd MMMM yyyy")
    End Sub
    Private Sub ReportEmpLeaveStock_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles MyBase.BeforePrint
        load_report()
    End Sub
End Class