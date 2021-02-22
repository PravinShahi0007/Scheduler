Public Class ClassBliApi
    Dim api_seller_key As String = ""
    Dim username As String = ""
    Dim password As String = ""
    Dim request_id As String = ""
    Dim channel_id As String = ""
    Dim business_partner As String = ""
    Dim store_id As String = ""
    Dim id_store_group As String = ""
    Dim limit_ror As String = ""

    Sub New()
        Dim query_main As String = "SELECT o.blibli_api_seller_key,
        o.blibli_api_username,
        o.blibli_api_password,
        o.blibli_api_request_id,
        o.blibli_channel_id,
        o.blibli_business_partner,
        o.blibli_store_id,
        o.blibli_comp_group,
        o.blibli_limit_ror
        FROM tb_opt o "
        Dim data_main As DataTable = execute_query(query_main, -1, True, "", "", "", "")
        api_seller_key = data_main.Rows(0)("blibli_api_seller_key").ToString
        username = data_main.Rows(0)("blibli_api_username").ToString
        password = data_main.Rows(0)("blibli_api_password").ToString
        request_id = data_main.Rows(0)("blibli_api_request_id").ToString
        channel_id = data_main.Rows(0)("blibli_channel_id").ToString
        business_partner = data_main.Rows(0)("blibli_business_partner").ToString
        store_id = data_main.Rows(0)("blibli_store_id").ToString
        id_store_group = data_main.Rows(0)("blibli_comp_group").ToString
        limit_ror = data_main.Rows(0)("blibli_limit_ror").ToString
    End Sub

    Function get_status(ByVal order_no_par As String, ByVal ol_store_id_par As String) As DataTable
        Net.ServicePointManager.Expect100Continue = True
        Net.ServicePointManager.SecurityProtocol = CType(3072, Net.SecurityProtocolType)

        Dim dt As DataTable = New DataTable
        dt.Columns.Add("order_status", GetType(String))
        dt.Columns.Add("order_status_date", GetType(DateTime))

        Dim auth As String = Convert.ToBase64String(Text.Encoding.UTF8.GetBytes(username + ":" + password))
        Dim request As Net.HttpWebRequest = Net.WebRequest.Create("https://api.blibli.com/v2/proxy/mta/api/businesspartner/v1/order/orderDetail?storeId=" + store_id + "&requestId=" + request_id + "&businessPartnerCode=" + business_partner + "&channelId=" + channel_id + "&orderNo=" + order_no_par + "&orderItemNo=" + ol_store_id_par)
        request.Method = "GET"
        request.Accept = "application/json"
        request.ContentType = "application/json"
        request.Headers.Add("Authorization", "Basic " + auth)
        request.Headers.Add("API-Seller-Key", api_seller_key)
        Dim response As Net.HttpWebResponse = request.GetResponse()
        Using dataStream As IO.Stream = response.GetResponseStream()
            Dim reader As IO.StreamReader = New IO.StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            Dim json As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.Linq.JObject.Parse(responseFromServer)
            If json("success").ToString = True AndAlso json("value").Count > 0 Then
                For Each row In json("value")("orderHistory").ToList
                    Dim stt As String = ""
                    If row("orderStatus").ToString = "D" Then
                        stt = "delivered"
                    ElseIf row("orderStatus").ToString = "X" Then
                        stt = "cancelled"
                    ElseIf row("orderStatus").ToString = "DF" Then
                        stt = "failed"
                    Else
                        stt = "on_process"
                    End If
                    dt.Rows.Add(stt, unixMiliSecondsToDatetime(row("createdTimestamp")))
                    Exit For
                Next
            Else
                dt = Nothing
            End If
        End Using
        response.Close()
        Return dt
    End Function

    Function get_page_ror() As Integer
        Net.ServicePointManager.Expect100Continue = True
        Net.ServicePointManager.SecurityProtocol = CType(3072, Net.SecurityProtocolType)

        Dim auth As String = Convert.ToBase64String(Text.Encoding.UTF8.GetBytes(username + ":" + password))
        Dim request As Net.HttpWebRequest = Net.WebRequest.Create("https://api.blibli.com/v2/proxy/mta/api/businesspartner/v1/order/getReturnedOrderSummary?requestId=" + request_id + "&businessPartnerCode=" + business_partner + "&channelId=" + channel_id + "&status=RMA_PROCESS_FINISHED")
        request.Method = "GET"

        request.Accept = "application/json"
        request.ContentType = "application/json"

        request.Headers.Add("Authorization", "Basic " + auth)
        request.Headers.Add("API-Seller-Key", api_seller_key)

        Dim response As Net.HttpWebResponse = request.GetResponse()
        Dim page As Decimal = 0
        Using dataStream As IO.Stream = response.GetResponseStream()
            Dim reader As IO.StreamReader = New IO.StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            Dim json As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.Linq.JObject.Parse(responseFromServer)
            If json("success").ToString = True AndAlso json("pageMetaData").Count > 0 Then
                page = Math.Ceiling(Decimal.Parse(json("pageMetaData")("totalRecords")) / limit_ror)
            End If
        End Using
        response.Close()

        Return page
    End Function

    Sub get_ror_list()
        Net.ServicePointManager.Expect100Continue = True
        Net.ServicePointManager.SecurityProtocol = CType(3072, Net.SecurityProtocolType)

        Dim page As Integer = get_page_ror()
        For i As Integer = 0 To page - 1
            Dim auth As String = Convert.ToBase64String(Text.Encoding.UTF8.GetBytes(username + ":" + password))
            Dim request As Net.HttpWebRequest = Net.WebRequest.Create("https://api.blibli.com/v2/proxy/mta/api/businesspartner/v1/order/getReturnedOrderSummary?requestId=" + request_id + "&businessPartnerCode=" + business_partner + "&channelId=" + channel_id + "&status=RMA_PROCESS_FINISHED" + "&size=" + limit_ror + "&page=" + i.ToString)
            request.Method = "GET"

            request.Accept = "application/json"
            request.ContentType = "application/json"

            request.Headers.Add("Authorization", "Basic " + auth)
            request.Headers.Add("API-Seller-Key", api_seller_key)

            Dim response As Net.HttpWebResponse = request.GetResponse()
            Using dataStream As IO.Stream = response.GetResponseStream()
                Dim reader As IO.StreamReader = New IO.StreamReader(dataStream)
                Dim responseFromServer As String = reader.ReadToEnd()
                Dim json As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.Linq.JObject.Parse(responseFromServer)
                If json("success").ToString = True AndAlso json("content").Count > 0 Then
                    For Each row In json("content").ToList
                        Dim id As String = row("returnId").ToString
                        Dim ror_date As String = DateTime.Parse(unixMiliSecondsToDatetime(Decimal.Parse(row("createdDate").ToString))).ToString("yyyy-MM-dd HH:mm:ss")

                        'check first
                        Dim q_check As String = "SELECT * FROM tb_ol_store_ror_bli r WHERE r.id='" + id + "' "
                        Dim dt_check As DataTable = execute_query(q_check, -1, True, "", "", "", "")
                        If Not dt_check.Rows.Count > 0 Then
                            Dim query As String = "INSERT INTO tb_ol_store_ror_bli(sync_date, ror_date, id, ror_number, order_number, item_id, customer_name, qty, price) 
                            VALUES(NOW(),'" + ror_date + "', '" + id + "', '" + addSlashes(row("returnNo").ToString) + "', '" + addSlashes(row("orderNo").ToString) + "', '" + addSlashes(row("orderItemNo").ToString) + "', '" + addSlashes(row("customerName").ToString) + "', '" + decimalSQL(row("orderQuantity").ToString) + "', '" + decimalSQL(row("productSalePrice").ToString) + "'); "
                            execute_non_query(query, True, "", "", "", "")
                        End If
                    Next
                End If
            End Using
            response.Close()
        Next
    End Sub

    Sub set_to_returned()
        Dim query As String = "SELECT b.id_ol_store_ror_bli, b.sync_date, b.id, b.ror_date, b.ror_number, b.order_number, b.item_id, b.customer_name,
        b.qty, b.price 
        FROM tb_ol_store_ror_bli b
        WHERE b.is_process=2
        ORDER BY b.ror_date ASC "
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")
        For i As Integer = 0 To data.Rows.Count - 1
            Dim stt_date As String = DateTime.Parse(data.Rows(i)("ror_date").ToString).ToString("yyyy-MM-dd HH:mm:ss")
            Dim qcek As String = "SELECT so.sales_order_ol_shop_number AS `order_no`, sod.id_sales_order, sod.id_sales_order_det, spd.id_sales_pos_det, spd.id_sales_pos,
            sod.ol_store_id, sod.item_id
            FROM  tb_sales_order so
            INNER JOIN tb_m_comp_contact sc ON sc.id_comp_contact = so.id_store_contact_to
            INNER JOIN tb_m_comp s ON s.id_comp = sc.id_comp
            INNER JOIN tb_sales_order_det sod ON sod.id_sales_order = so.id_sales_order
            LEFT JOIN tb_pl_sales_order_del_det dd ON dd.id_sales_order_det = sod.id_sales_order_det
            LEFT JOIN tb_pl_sales_order_del d ON d.id_pl_sales_order_del = dd.id_pl_sales_order_del AND d.id_report_status=6
            LEFT JOIN tb_sales_pos_det spd ON spd.id_pl_sales_order_del_det = dd.id_pl_sales_order_del_det
            LEFT JOIN tb_sales_pos sp ON sp.id_sales_pos = spd.id_sales_pos AND sp.id_report_status=6
            WHERE so.id_report_status=6  AND s.id_comp_group='" + id_store_group + "' AND so.sales_order_ol_shop_number='" + data.Rows(i)("order_number").ToString + "' AND sod.ol_store_id='" + data.Rows(i)("item_id").ToString + "'
            ORDER BY sod.id_sales_order_det ASC LIMIT " + data.Rows(i)("qty").ToString + " "
            Dim dcek As DataTable = execute_query(qcek, -1, True, "", "", "", "")
            Dim found_item As Boolean = False
            For j As Integer = 0 To dcek.Rows.Count - 1
                'set status
                Dim cmos As New ClassMOS()
                cmos.insertStatusOrder(dcek.Rows(j)("id_sales_order_det").ToString, "returned", stt_date)
                found_item = True
                If dcek.Rows(j)("id_sales_pos").ToString <> "" Then
                    Dim qib As String = "INSERT INTO tb_ol_store_return_order(id_comp_group, created_date, order_number, ol_store_id, item_id, qty, id_sales_order, id_sales_order_det, id_sales_pos_det, id_sales_pos)
                                            VALUES('" + id_store_group + "', NOW(), '" + dcek.Rows(j)("order_no").ToString + "', '" + dcek.Rows(j)("ol_store_id").ToString + "', '" + dcek.Rows(j)("item_id").ToString + "','1','" + dcek.Rows(j)("id_sales_order").ToString + "', '" + dcek.Rows(j)("id_sales_order_det").ToString + "', '" + dcek.Rows(j)("id_sales_pos_det").ToString + "', '" + dcek.Rows(j)("id_sales_pos").ToString + "'); "
                    execute_non_query(qib, True, "", "", "", "")
                End If
            Next
            If found_item Then
                execute_non_query("UPDATE tb_ol_store_ror_bli SET is_process=1, process_date=NOW() WHERE id_ol_store_ror_bli='" + data.Rows(i)("id_ol_store_ror_bli").ToString + "' ", True, "", "", "", "")
            End If
        Next
    End Sub

    Public Shared Function decimalSQL(ByVal value As String) 'hanya kalo masuk ke database
        Dim nominal As String

        nominal = value.Replace(",", ".")
        Return nominal
    End Function
End Class
