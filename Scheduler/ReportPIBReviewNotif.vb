Public Class ReportPIBReviewNotif
    Private Sub ReportPIBReviewNotif_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Dim q As String = "CALL pib_analisa_send()"
        Dim dt As DataTable = execute_query(q, -1, True, "", "", "", "")
        GCAnalisa.DataSource = dt
        GVAnalisa.BestFitColumns()
    End Sub
End Class