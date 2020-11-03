Public Class ClassShopifyAPI
    Public username As String = get_setup_field("shopify_api_username")
    Public password As String = get_setup_field("shopify_api_password")
    Public shop As String = get_setup_field("shopify_api_shop")
    Public id_comp_group As String = get_setup_field("shopify_comp_group")

    Function get_setup_field(ByVal field As String)
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
        Net.ServicePointManager.Expect100Continue = True
        Net.ServicePointManager.SecurityProtocol = CType(3072, Net.SecurityProtocolType)
        Dim limit_order As String = get_setup_field("shopify_limit_order_failed")
        Dim url As String = "https://" + username + ":" + password + "@" + shop + "/admin/api/2020-04/orders.json?fulfillment_status=unshipped&financial_status=pending&status=open&limit=" + limit_order + ""
        Dim page_info As String = ""
        Dim i As Integer = 0
        Dim is_loop As Boolean = True
        While is_loop
            Dim url_page_info As String = url + (If(Not page_info = "", "&page_info=" + page_info, ""))
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
                        Dim created_at As String = DateTime.Parse(row("created_at").ToString).ToString("yyyy-MM-dd HH:mm:ss")
                        If financial_status = "pending" Then

                        End If
                    Next
                End Using
                is_loop = True
            Else
                is_loop = False
            End If
            i += 1
        End While
    End Sub
End Class
