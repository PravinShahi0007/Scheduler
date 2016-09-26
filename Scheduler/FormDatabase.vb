Public Class FormDatabase
    Private connect_state As Boolean = False
    Public id_type As String = "-1"

    Sub view_database(ByVal host As String, ByVal username As String, ByVal password As String)
        Dim data As DataTable = show_databases(False, host, username, password)
        ListBoxControlDatabase.DataSource = data
        ListBoxControlDatabase.DisplayMember = "Database"
        ListBoxControlDatabase.ValueMember = "Database"
    End Sub

    Private Sub SimpleButtonConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButtonConnect.Click
        Cursor = Cursors.WaitCursor

        Try
            If connect_state = False Then
                Dim host As String = TextEditHost.Text
                Dim username As String = TextEditUsername.Text
                Dim password As String = TextEditPassword.Text

                view_database(host, username, password)

                TextEditHost.Enabled = False
                TextEditUsername.Enabled = False
                TextEditPassword.Enabled = False
                SimpleButtonConnect.Text = "Change"
                SimpleButtonSave.Enabled = True

                connect_state = True
            Else
                ListBoxControlDatabase.DataSource = Nothing
                TextEditHost.Enabled = True
                TextEditUsername.Enabled = True
                TextEditPassword.Enabled = True
                SimpleButtonConnect.Text = "Connect"
                SimpleButtonSave.Enabled = False

                connect_state = False
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        Cursor = Cursors.Default
    End Sub

    Private Sub SimpleButtonSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButtonSave.Click
        Cursor = Cursors.WaitCursor

        Try
            write_database_configuration(TextEditHost.Text, TextEditUsername.Text, TextEditPassword.Text, ListBoxControlDatabase.SelectedValue.ToString)
            read_database_configuration()

            FormScheduler.Visible = True

            Close()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        Cursor = Cursors.Default
    End Sub
End Class