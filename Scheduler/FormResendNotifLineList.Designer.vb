<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormResendNotifLineList
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GCData = New DevExpress.XtraGrid.GridControl()
        Me.GVData = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BtnResend = New DevExpress.XtraEditors.SimpleButton()
        Me.GridColumnlog_note = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.GridColumnlog_date = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumncheck_line_list_date = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnid_line_list_notif_type = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnid_log_line_list_mail = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.GCData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GCData
        '
        Me.GCData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GCData.Location = New System.Drawing.Point(0, 0)
        Me.GCData.MainView = Me.GVData
        Me.GCData.Name = "GCData"
        Me.GCData.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemMemoEdit1})
        Me.GCData.Size = New System.Drawing.Size(499, 321)
        Me.GCData.TabIndex = 0
        Me.GCData.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVData})
        '
        'GVData
        '
        Me.GVData.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumnlog_note, Me.GridColumnlog_date, Me.GridColumncheck_line_list_date, Me.GridColumnid_line_list_notif_type, Me.GridColumnid_log_line_list_mail})
        Me.GVData.GridControl = Me.GCData
        Me.GVData.Name = "GVData"
        Me.GVData.OptionsBehavior.Editable = False
        Me.GVData.OptionsFind.AlwaysVisible = True
        Me.GVData.OptionsView.ColumnAutoWidth = False
        Me.GVData.OptionsView.ShowGroupPanel = False
        '
        'BtnResend
        '
        Me.BtnResend.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BtnResend.Location = New System.Drawing.Point(0, 321)
        Me.BtnResend.Name = "BtnResend"
        Me.BtnResend.Size = New System.Drawing.Size(499, 34)
        Me.BtnResend.TabIndex = 1
        Me.BtnResend.Text = "RESEND"
        '
        'GridColumnlog_note
        '
        Me.GridColumnlog_note.Caption = "Note"
        Me.GridColumnlog_note.ColumnEdit = Me.RepositoryItemMemoEdit1
        Me.GridColumnlog_note.FieldName = "log_note"
        Me.GridColumnlog_note.Name = "GridColumnlog_note"
        Me.GridColumnlog_note.Visible = True
        Me.GridColumnlog_note.VisibleIndex = 0
        '
        'RepositoryItemMemoEdit1
        '
        Me.RepositoryItemMemoEdit1.Name = "RepositoryItemMemoEdit1"
        '
        'GridColumnlog_date
        '
        Me.GridColumnlog_date.Caption = "Log Date"
        Me.GridColumnlog_date.DisplayFormat.FormatString = "dd MMMM yyyy HH:mm"
        Me.GridColumnlog_date.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumnlog_date.FieldName = "log_date"
        Me.GridColumnlog_date.Name = "GridColumnlog_date"
        Me.GridColumnlog_date.Visible = True
        Me.GridColumnlog_date.VisibleIndex = 1
        '
        'GridColumncheck_line_list_date
        '
        Me.GridColumncheck_line_list_date.Caption = "Check Date"
        Me.GridColumncheck_line_list_date.DisplayFormat.FormatString = "dd MMMM yyyy"
        Me.GridColumncheck_line_list_date.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumncheck_line_list_date.FieldName = "check_line_list_date"
        Me.GridColumncheck_line_list_date.Name = "GridColumncheck_line_list_date"
        Me.GridColumncheck_line_list_date.Visible = True
        Me.GridColumncheck_line_list_date.VisibleIndex = 2
        '
        'GridColumnid_line_list_notif_type
        '
        Me.GridColumnid_line_list_notif_type.Caption = "id_line_list_notif_type"
        Me.GridColumnid_line_list_notif_type.FieldName = "id_line_list_notif_type"
        Me.GridColumnid_line_list_notif_type.Name = "GridColumnid_line_list_notif_type"
        '
        'GridColumnid_log_line_list_mail
        '
        Me.GridColumnid_log_line_list_mail.Caption = "id_log_line_list_mail"
        Me.GridColumnid_log_line_list_mail.FieldName = "id_log_line_list_mail"
        Me.GridColumnid_log_line_list_mail.Name = "GridColumnid_log_line_list_mail"
        '
        'FormResendNotifLineList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(499, 355)
        Me.Controls.Add(Me.GCData)
        Me.Controls.Add(Me.BtnResend)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormResendNotifLineList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Resend Mail"
        CType(Me.GCData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GCData As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVData As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BtnResend As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridColumnlog_note As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemMemoEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents GridColumnlog_date As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumncheck_line_list_date As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnid_line_list_notif_type As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnid_log_line_list_mail As DevExpress.XtraGrid.Columns.GridColumn
End Class
