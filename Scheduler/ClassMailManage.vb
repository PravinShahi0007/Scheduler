Public Class ClassMailManage
    Public id_mail_manage As String = "-1"
    Public mail_subject As String = ""
    Public mail_title As String = ""
    Public rmt As String = "-1"
    Public par1 As String = ""


    Sub createEmail()
        Dim query_mail_manage As String = "INSERT INTO tb_mail_manage(number, created_date, created_by, updated_date, updated_by, report_mark_type, id_mail_status, mail_status_note, mail_subject, mail_parameter) 
        VALUES('', NOW(), NULL, NOW(), NULL, 228, 1, 'Draft', '" + mail_subject + "', '" + par1 + "'); SELECT LAST_INSERT_ID(); "
        id_mail_manage = execute_query(query_mail_manage, 0, True, "", "", "", "")
        'update number mail
        execute_non_query("CALL gen_number(" + id_mail_manage + ", 228);", True, "", "", "", "")
        'insert member & detil
        Dim query_mail_detail As String = "/*member*/
        INSERT INTO tb_mail_manage_member(id_mail_manage, id_mail_member_type, id_user, id_comp_contact, mail_address) 
        SELECT " + id_mail_manage + " AS `id_mail_manage`, m.id_mail_member_type, m.id_user, NULL AS `id_comp_contact`, e.email_external AS `mail_address`
        FROM tb_mail_manage_mapping_intern m
        INNER JOIN tb_m_user u ON u.id_user = m.id_user
        INNER JOIN tb_m_employee e ON e.id_employee = u.id_employee
        WHERE m.report_mark_type=228;
        /*detil*/
        INSERT INTO tb_mail_manage_det(id_mail_manage, report_mark_type, id_report, report_number) 
        SELECT " + id_mail_manage + " AS `id_mail_manage`, e.report_mark_type, e.id_sales_pos, e.report_number
        FROM tb_ar_eval e WHERE e.eval_date='" + par1 + "'; "
        execute_non_query(query_mail_detail, True, "", "", "", "")
    End Sub

    Function getDetailData() As DataTable
        Dim dt As New DataTable

        'get id_report
        Dim query_get_id As String = "SELECT GROUP_CONCAT(md.id_report) AS `id_report`
        FROM tb_mail_manage_det md
        WHERE md.id_mail_manage=" + id_mail_manage + " "
        Dim id_trans As String = execute_query(query_get_id, 0, True, "", "", "", "")

        If rmt = "228" Then
            Dim query As String = "SELECT  g.id_comp_group,g.description AS `group_store`,
            DATE_FORMAT(sp.sales_pos_due_date,'%d %M %Y') AS `sales_pos_due_date`,
            SUM(CAST(IF(typ.`is_receive_payment`=2,-1,1) * ((sp.`sales_pos_total`*((100-sp.sales_pos_discount)/100))-sp.`sales_pos_potongan`) AS DECIMAL(15,2))-IFNULL(pyd.`value`,0.00)) AS `amount`
            FROM tb_sales_pos sp 
            INNER JOIN tb_m_comp_contact cc ON cc.`id_comp_contact`= IF(sp.id_memo_type=8 OR sp.id_memo_type=9, sp.id_comp_contact_bill,sp.`id_store_contact_from`)
            INNER JOIN tb_m_comp c ON c.`id_comp`=cc.`id_comp`
            INNER JOIN tb_m_comp_group g ON g.id_comp_group = c.id_comp_group
            INNER JOIN tb_lookup_memo_type typ ON typ.`id_memo_type`=sp.`id_memo_type`
            LEFT JOIN (
                SELECT pyd.id_report, pyd.report_mark_type, 
                COUNT(IF(py.id_report_status!=5 AND py.id_report_status!=6,py.id_rec_payment,NULL)) AS `total_pending`,
                SUM(pyd.value) AS  `value`
                FROM tb_rec_payment_det pyd
                INNER JOIN tb_rec_payment py ON py.`id_rec_payment`=pyd.`id_rec_payment`
                WHERE py.`id_report_status`=6
                GROUP BY pyd.id_report, pyd.report_mark_type
            ) pyd ON pyd.id_report = sp.id_sales_pos AND pyd.report_mark_type = sp.report_mark_type
            WHERE sp.id_sales_pos IN (" + id_trans + ") 
            GROUP BY g.id_comp_group, sp.sales_pos_due_date
            ORDER BY g.id_comp_group ASC, sp.id_sales_pos ASC "
            dt = execute_query(query, -1, True, "", "", "", "")
        End If
        Return dt
    End Function

    Function queryInsertLog(ByVal id_status_par As String, ByVal note_par As String) As String
        Dim query As String = ""
        If id_mail_manage <> "-1" Then
            query = "UPDATE tb_mail_manage SET updated_date=NOW(), updated_by=NULL, 
            id_mail_status=" + id_status_par + ", mail_status_note='" + addSlashes(note_par) + "' WHERE id_mail_manage='" + id_mail_manage + "'; 
            INSERT INTO tb_mail_manage_log(id_mail_manage, log_date, id_user, id_mail_status, note) VALUES 
            ('" + id_mail_manage + "', NOW(),NULL, '" + id_status_par + "', '" + addSlashes(note_par) + "'); "
        End If
        Return query
    End Function
End Class
