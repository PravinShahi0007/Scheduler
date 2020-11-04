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

    Sub get_order_fail()
        'checked date
        Dim qd As String = "SELECT STR_TO_DATE(CONCAT(DATE_SUB(DATE(NOW()),INTERVAL o.shopify_min_date_order_failed DAY),' 23:59:59'),'%Y-%m-%d %H:%i:%s') AS `check_date`
        FROM tb_opt o "
        Dim dd As DataTable = execute_query(qd, -1, True, "", "", "", "")
        Dim check_date As DateTime = dd.Rows(0)("check_date")

        Net.ServicePointManager.Expect100Continue = True
        Net.ServicePointManager.SecurityProtocol = CType(3072, Net.SecurityProtocolType)
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
                        If financial_status = "pending" And created_at <= check_date Then
                            'check existing
                            Dim id As String = row("id").ToString
                            Dim qcek As String = "SELECT * FROM tb_ol_store_order_fail od WHERE od.id='" + id + "' "
                            Dim dcek As DataTable = execute_query(qcek, -1, True, "", "", "", "")
                            If dcek.Rows.Count = 0 Then
                                Dim checkout_id As String = If(row("checkout_id").ToString = "null", "", row("checkout_id").ToString)
                                Dim order_date As String = DateTime.Parse(row("created_at").ToString).ToString("yyyy-MM-dd HH:mm:ss")
                                Dim order_number As String = row("order_number").ToString
                                Dim order_tag As String = addSlashes(row("tags").ToString)
                                Dim customer_name As String = row("customer")("first_name").ToString + " " + row("customer")("last_name").ToString

                                'detail line item
                                Dim qins As String = "INSERT tb_ol_store_order_fail(id,checkout_id, order_date, order_number, order_tag, customer_name, line_item_id, quantity, input_date) VALUES "
                                Dim line_item_id As String = ""
                                Dim quantity As String = ""
                                Dim j As Integer = 0
                                For Each row_item In row("line_items").ToList
                                    line_item_id = row_item("id").ToString
                                    quantity = decimalSQL(row_item("quantity").ToString)

                                    If j > 0 Then
                                        qins += ","
                                    End If
                                    qins += "('" + id + "', '" + checkout_id + "', '" + order_date + "', '" + order_number + "', '" + order_tag + "', '" + customer_name + "', '" + line_item_id + "', '" + quantity + "', NOW()) "
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

    Public Shared Function decimalSQL(ByVal value As String) 'hanya kalo masuk ke database
        Dim nominal As String

        nominal = value.Replace(",", ".")
        Return nominal
    End Function
End Class
