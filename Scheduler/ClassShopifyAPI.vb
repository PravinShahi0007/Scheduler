Public Class ClassShopifyAPI
    Public username As String = get_setup_field("shopify_api_username")
    Public password As String = get_setup_field("shopify_api_password")
    Public shop As String = get_setup_field("shopify_api_shop")
    Public id_comp_group As String = get_setup_field("shopify_comp_group")

    Private Function get_setup_field(ByVal field As String)
        'opt as var choose field
        Dim ret_var, query As String
        ret_var = ""

        Try
            query = "SELECT " & field & " FROM tb_opt LIMIT 1"
            ret_var = execute_query(query, 0, True, "", "", "", "")
        Catch ex As Exception
            ret_var = ""
        End Try

        Return ret_var
    End Function

    Sub get_order_fail(ByVal schedule_cek As DateTime)
        Net.ServicePointManager.Expect100Continue = True
        Net.ServicePointManager.SecurityProtocol = CType(3072, Net.SecurityProtocolType)
        Dim check_time_set = get_setup_field("shopify_min_date_order_failed")
        Dim limit_order As String = get_setup_field("shopify_limit_order_failed")
        Dim url_first As String = "https://" + username + ":" + password + "@" + shop + "/admin/api/2020-04/orders.json?fulfillment_status=unshipped&financial_status=pending&status=open&limit=" + limit_order + ""
        Dim url As String = "https://" + username + ":" + password + "@" + shop + "/admin/api/2020-04/orders.json?limit=" + limit_order + ""
        Dim page_info As String = ""
        Dim i As Integer = 0
        Dim is_loop As Boolean = True
        While is_loop
            Dim url_page_info As String = ""
            If i = 0 Then
                url_page_info = url_first + (If(Not page_info = "", "&page_info=" + page_info, ""))
            Else
                url_page_info = url + (If(Not page_info = "", "&page_info=" + page_info, ""))
            End If
            'Console.WriteLine(url_page_info)

            Dim request As Net.WebRequest = Net.WebRequest.Create(url_page_info)
            request.Method = "GET"
            request.Credentials = New Net.NetworkCredential(username, password)
            Dim response As Net.WebResponse = request.GetResponse()
            If Not page_info = "" Or i = 0 Then
                Using dataStream As IO.Stream = response.GetResponseStream()
                    Dim reader As IO.StreamReader = New IO.StreamReader(dataStream)
                    Dim responseFromServer As String = reader.ReadToEnd()
                    Dim json As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.Linq.JObject.Parse(responseFromServer)
                    For Each row In json("orders").ToList
                        Dim financial_status As String = row("financial_status").ToString
                        Dim created_at As DateTime = DateTime.Parse(row("created_at").ToString)
                        Dim created_at_cek As DateTime = New DateTime(created_at.Year, created_at.Month, created_at.Day, created_at.Hour, created_at.Minute, 0)
                        Dim diff_time As Long = (schedule_cek - created_at_cek).TotalMinutes
                        Console.WriteLine(schedule_cek.ToString)
                        Console.WriteLine(created_at.ToString)
                        Console.WriteLine(diff_time.ToString)
                        If financial_status = "pending" And diff_time >= check_time_set Then
                            'check existing
                            Dim id As String = row("id").ToString
                            Dim qcek As String = "SELECT * FROM tb_ol_store_order_fail od WHERE od.id='" + id + "' "
                            Dim dcek As DataTable = execute_query(qcek, -1, True, "", "", "", "")
                            If dcek.Rows.Count = 0 Then
                                Dim schedule_time As String = DateTime.Parse(schedule_cek).ToString("yyyy-MM-dd HH:mm:ss")
                                Dim checkout_id As String = If(row("checkout_id").ToString = "null", "", row("checkout_id").ToString)
                                Dim order_date As String = DateTime.Parse(row("created_at").ToString).ToString("yyyy-MM-dd HH:mm:ss")
                                Dim order_number As String = row("order_number").ToString
                                Dim order_tag As String = addSlashes(row("tags").ToString)
                                Dim customer_name As String = addSlashes(row("customer")("first_name").ToString + " " + row("customer")("last_name").ToString)

                                'detail line item
                                Dim qins As String = "INSERT tb_ol_store_order_fail(id,schedule_time,checkout_id, order_date, order_number, order_tag, customer_name, line_item_id, quantity, input_date) VALUES "
                                Dim line_item_id As String = ""
                                Dim quantity As String = ""
                                Dim j As Integer = 0
                                For Each row_item In row("line_items").ToList
                                    line_item_id = row_item("id").ToString
                                    quantity = decimalSQL(row_item("quantity").ToString)

                                    If j > 0 Then
                                        qins += ","
                                    End If
                                    qins += "('" + id + "', '" + schedule_time + "','" + checkout_id + "', '" + order_date + "', '" + order_number + "', '" + order_tag + "', '" + customer_name + "', '" + line_item_id + "', '" + quantity + "', NOW()) "
                                    j += 1
                                Next
                                'insert ortder
                                If j > 0 Then
                                    execute_non_query(qins, True, "", "", "", "")
                                End If
                            End If
                        End If
                    Next
                End Using
                is_loop = True
            Else
                is_loop = False
            End If
            i += 1

            'get next page
            Dim link As String() = response.Headers.GetValues(16)
            Dim j1 As Integer = link(link.Count - 1).LastIndexOf(">; rel=""next")
            Dim j2 As Integer = link(link.Count - 1).LastIndexOf("o=") + 2
            If j1 > 0 And j2 > 0 Then
                page_info = link(link.Count - 1).Substring(0, j1).Substring(j2)
            Else
                page_info = ""
            End If

            response.Close()
        End While
    End Sub

    Sub proceed_cancel_fail_order()
        Dim is_process As String = get_opt_scheduler_field("is_process_vios_close_fail_order")
        If is_process = "2" Then
            'on process
            set_process_closed_order("1")

            'get on process fail order
            Dim qor As String = "SELECT of.id,of.order_number 
            FROM tb_ol_store_order_fail of WHERE of.is_process=2
            GROUP BY of.id
            ORDER BY of.id ASC "
            Dim dor As DataTable = execute_query(qor, -1, True, "", "", "", "")
            For d As Integer = 0 To dor.Rows.Count - 1
                Dim id As String = dor.Rows(d)("id").ToString

                Try
                    'closed order
                    cancel_order(id)
                    'set tag
                    set_tag_order(id)
                    execute_non_query("UPDATE tb_ol_store_order_fail SET is_process=1,process_date=NOW() WHERE id='" + id + "'", True, "", "", "", "")
                Catch ex As Exception
                    'update error note
                    execute_non_query("UPDATE tb_ol_store_order_fail SET error_process='" + addSlashes(ex.ToString) + "' WHERE id='" + id + "'", True, "", "", "", "")
                End Try
            Next

            'end process
            set_process_closed_order("2")
        End If
    End Sub

    Sub cancel_order(ByVal id_order As String)
        Dim location_id As String = get_setup_field("shopify_location_id")
        Dim query As String = "SELECT * FROM tb_ol_store_order_fail of WHERE of.id='" + id_order + "' "
        Dim data As DataTable = execute_query(query, -1, True, "", "", "", "")
        Dim str As String = ""
        For i As Integer = 0 To data.Rows.Count - 1
            Dim line_item_id As String = data.Rows(i)("line_item_id").ToString
            Dim quantity As String = data.Rows(i)("quantity").ToString

            If i > 0 Then
                str += ","
            End If
            str += " {
        ""line_item_id"": " + line_item_id + ",
        ""quantity"": " + quantity + ",
        ""restock_type"": ""cancel"",
        ""location_id"": " + location_id + "
      }"
        Next

        Dim dt = Text.Encoding.UTF8.GetBytes("{
  ""refund"": {
    ""note"": ""Expired Order"",
    ""shipping"": {
      ""full_refund"": true
    },
    ""refund_line_items"": [
      " + str + "
    ]
  }
}")
        Dim result_post As String = SendRequest("https://" & username & ":" & password & "@" & shop & "/admin/api/2020-04/orders/" & id_order & "/cancel.json", dt, "application/json", "POST", username, password)
    End Sub

    Sub set_tag_order(ByVal id_order As String)
        Dim tag As String = get_setup_field("shopify_fail_order_tag")
        Dim curr_tag As String = execute_query("SELECT of.order_tag FROM tb_ol_store_order_fail of WHERE of.id='" + id_order + "' LIMIT 1", 0, True, "", "", "", "")
        If curr_tag = "" Then
            curr_tag = tag
        Else
            curr_tag = curr_tag + "," + tag
        End If
        Dim dt = Text.Encoding.UTF8.GetBytes("{
  ""order"": {
    ""id"": " + id_order + ",
    ""tags"": """ + curr_tag + """
  }
}")
        Dim result_post As String = SendRequest("https://" & username & ":" & password & "@" & shop & "/admin/api/2020-04/orders/" & id_order & ".json", dt, "application/json", "PUT", username, password)
    End Sub

    Private Function SendRequest(str_url As String, jsonDataBytes As Byte(), contentType As String, method As String, ByVal username As String, ByVal pass As String) As String
        Net.ServicePointManager.Expect100Continue = True
        Net.ServicePointManager.SecurityProtocol = CType(3072, Net.SecurityProtocolType)

        Dim response As String
        Dim request As Net.WebRequest


        Dim url As Uri = New Uri(str_url)

        request = Net.WebRequest.Create(url)
        request.ContentLength = jsonDataBytes.Length
        request.ContentType = contentType
        request.Method = method
        request.Credentials = New Net.NetworkCredential(username, password)


        Using requestStream = request.GetRequestStream
            requestStream.Write(jsonDataBytes, 0, jsonDataBytes.Length)
            requestStream.Close()

            Using responseStream = request.GetResponse.GetResponseStream
                Using reader As New IO.StreamReader(responseStream)
                    response = reader.ReadToEnd()
                End Using
            End Using
        End Using

        Return response
    End Function

    Public Shared Function decimalSQL(ByVal value As String) 'hanya kalo masuk ke database
        Dim nominal As String

        nominal = value.Replace(",", ".")
        Return nominal
    End Function

    Private Function get_opt_scheduler_field(ByVal field As String)
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

    Private Sub set_process_closed_order(ByVal par As String)
        execute_non_query("UPDATE tb_opt_scheduler SET is_process_vios_close_fail_order=" + par + "; ", True, "", "", "", "")
    End Sub
End Class
