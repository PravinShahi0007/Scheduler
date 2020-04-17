Public Class FormTest
    Public connection_problem As Boolean

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim Report As New ReportEmpAttn()
        Report.id_dept = "dept_head"
        Report.is_head_dept = "1"
        '
        Dim Tool As DevExpress.XtraReports.UI.ReportPrintTool = New DevExpress.XtraReports.UI.ReportPrintTool(Report)
        Tool.ShowPreview()
    End Sub

    Private Sub FormTest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            read_database_configuration()
            check_connection(True, "", "", "", "")
            'show hide login
            'LoginToolStripMenuItem.Visible = True
        Catch ex As Exception
            connection_problem = True

            Visible = False

            FormDatabase.TopMost = True
            FormDatabase.Show()
            FormDatabase.Focus()
            FormDatabase.TopMost = False
        End Try

        If connection_problem = False Then
            WindowState = FormWindowState.Minimized
        End If
    End Sub
End Class