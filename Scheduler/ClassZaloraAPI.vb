Public Class ClassZaloraAPI
    Public api_key As String = ""
    Public user_id As String = ""
    Dim status_order As String = ""
    Dim id_store_group As String = ""
    Dim data_size As New DataTable

    Sub New()
        Dim query_main As String = "SELECT o.zalora_api_key,
        o.zalora_user_id,
        o.zalora_comp_group, o.id_code_product_size
        FROM tb_opt o "
        Dim data_main As DataTable = execute_query(query_main, -1, True, "", "", "", "")
        api_key = data_main.Rows(0)("zalora_api_key").ToString
        user_id = data_main.Rows(0)("zalora_user_id").ToString
        id_store_group = data_main.Rows(0)("zalora_comp_group").ToString

        'size
        Dim id_code_detail_size As String = data_main.Rows(0)("id_code_product_size").ToString
        Dim query As String = "SELECT cd.id_code_detail,cd.code, cd.code_detail_name FROM tb_m_code_detail cd WHERE cd.id_code=" + id_code_detail_size + " "
        data_size = execute_query(query, -1, True, "", "", "", "")
    End Sub

    Function get_signature(ByVal parameter As DataTable) As String
        Dim url As String = ""

        For i = 0 To parameter.Rows.Count - 1
            url += parameter.Rows(i)("key").ToString + "=" + parameter.Rows(i)("value").ToString + "&"
        Next

        url = url.Substring(0, url.Length - 1)

        Dim encoding As New System.Text.UTF8Encoding

        Dim key() As Byte = encoding.GetBytes(api_key)
        Dim text() As Byte = encoding.GetBytes(url)

        Dim sha265 As New Security.Cryptography.HMACSHA256(key)
        Dim hash_code As Byte() = sha265.ComputeHash(text)
        Dim hash As String = Replace(BitConverter.ToString(hash_code), "-", "")

        Return hash.ToLower
    End Function

    Function getSKU(ByVal design_code_par As String, ByVal size_par As String) As String
        Dim code9 As String = design_code_par
        Dim variation As String = size_par
        If Not isNumber(variation) Then
            variation = variation
        Else
            variation = variation.PadLeft(2, "0")
        End If
        If variation < "10" Then
            variation = variation
        Else
            variation = If(variation = "One Size", "One Size", variation.Replace("in", ""))
        End If
        Dim data_size_cek As DataRow() = data_size.Select("[code_detail_name]='" + variation + "' ")
        Dim code_size As String = ""
        If data_size_cek.Length <= 0 Then
            code_size = ""
        Else
            code_size = data_size_cek(0)("code").ToString
        End If
        Dim sku As String = code9 + code_size
        Return sku
    End Function

    Function get_order_detail(ByVal id As String) As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("ol_store_id", GetType(String))
        dt.Columns.Add("item_id", GetType(String))
        dt.Columns.Add("sku", GetType(String))
        dt.Columns.Add("ol_store_sku", GetType(String))
        dt.Columns.Add("design_price", GetType(Decimal))
        dt.Columns.Add("tracking_code", GetType(String))
        dt.Columns.Add("shipment_provider", GetType(String))
        dt.Columns.Add("status", GetType(String))
        dt.Columns.Add("updated_at", GetType(String))

        Dim parameter_det As DataTable = New DataTable

        parameter_det.Columns.Add("key", GetType(String))
        parameter_det.Columns.Add("value", GetType(String))

        parameter_det.Rows.Add("Action", "GetOrderItems")
        parameter_det.Rows.Add("Format", "JSON")
        parameter_det.Rows.Add("OrderId", id)
        parameter_det.Rows.Add("Timestamp", Uri.EscapeDataString(DateTime.Parse(Now().ToUniversalTime().ToString).ToString("yyyy-MM-ddTHH:mm:ss+00:00")))
        parameter_det.Rows.Add("UserID", Uri.EscapeDataString(user_id))
        parameter_det.Rows.Add("Version", "1.0")

        Dim signature_det As String = get_signature(parameter_det)

        parameter_det.Rows.Add("Signature", signature_det)

        Dim url_det As String = "https://sellercenter-api.zalora.co.id?"

        For i = 0 To parameter_det.Rows.Count - 1
            url_det += parameter_det.Rows(i)("key").ToString + "=" + parameter_det.Rows(i)("value").ToString + "&"
        Next

        url_det = url_det.Substring(0, url_det.Length - 1)

        Dim request_det As Net.HttpWebRequest = Net.WebRequest.Create(url_det)

        request_det.Method = "GET"
        Dim response_det As Net.HttpWebResponse = request_det.GetResponse()
        Using dataStreamDet As IO.Stream = response_det.GetResponseStream()
            Dim reader_det As IO.StreamReader = New IO.StreamReader(dataStreamDet)

            Dim responseFromServerDet As String = reader_det.ReadToEnd()

            'array
            Dim json_det As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.Linq.JObject.Parse(responseFromServerDet)
            If json_det("SuccessResponse")("Body")("OrderItems")("OrderItem").Count > 0 Then
                Dim sku As String = ""

                If responseFromServerDet.Contains("""OrderItem"":[") Then
                    'array
                    For Each row_det In json_det("SuccessResponse")("Body")("OrderItems")("OrderItem").ToList
                        sku = ""
                        sku = getSKU(row_det("Sku").ToString.Substring(0, 9), row_det("Variation").ToString)
                        dt.Rows.Add(row_det("ShopId").ToString, row_det("OrderItemId").ToString, sku, row_det("ShopSku").ToString, row_det("ItemPrice"), row_det("TrackingCode").ToString, row_det("ShipmentProvider").ToString, row_det("Status").ToString, row_det("UpdatedAt").ToString)
                    Next
                Else
                    'non array
                    sku = ""
                    sku = getSKU(json_det("SuccessResponse")("Body")("OrderItems")("OrderItem")("Sku").ToString.Substring(0, 9), json_det("SuccessResponse")("Body")("OrderItems")("OrderItem")("Variation").ToString)
                    dt.Rows.Add(json_det("SuccessResponse")("Body")("OrderItems")("OrderItem")("ShopId").ToString, json_det("SuccessResponse")("Body")("OrderItems")("OrderItem")("OrderItemId").ToString, sku, json_det("SuccessResponse")("Body")("OrderItems")("OrderItem")("ShopSku").ToString, json_det("SuccessResponse")("Body")("OrderItems")("OrderItem")("ItemPrice"), json_det("SuccessResponse")("Body")("OrderItems")("OrderItem")("TrackingCode").ToString, json_det("SuccessResponse")("Body")("OrderItems")("OrderItem")("ShipmentProvider").ToString, json_det("SuccessResponse")("Body")("OrderItems")("OrderItem")("Status").ToString, json_det("SuccessResponse")("Body")("OrderItems")("OrderItem")("UpdatedAt").ToString)
                End If
            End If
        End Using
        response_det.Close()
        Return dt
    End Function

    Function get_status_update(ByVal id_order_par As String, ByVal item_id_par As String) As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("order_status", GetType(String))
        dt.Columns.Add("order_status_date", GetType(String))
        Dim data As DataTable = get_order_detail(id_order_par)
        If data.Rows.Count > 0 Then
            Dim data_filter_cek As DataRow() = data.Select("[item_id]='" + item_id_par + "' ")
            If data_filter_cek.Length <= 0 Then
                dt = Nothing
            Else
                dt.Rows.Add(data_filter_cek(0)("status").ToString, data_filter_cek(0)("updated_at").ToString)
            End If
        Else
            dt = Nothing
        End If
        Return dt
    End Function
End Class
