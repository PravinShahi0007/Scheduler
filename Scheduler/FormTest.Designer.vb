<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormTest
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
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton2 = New DevExpress.XtraEditors.SimpleButton()
        Me.GCReport = New DevExpress.XtraGrid.GridControl()
        Me.GVReport = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.GCReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Location = New System.Drawing.Point(12, 12)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(136, 23)
        Me.SimpleButton1.TabIndex = 0
        Me.SimpleButton1.Text = "Report Dept Head"
        '
        'SimpleButton2
        '
        Me.SimpleButton2.Location = New System.Drawing.Point(12, 41)
        Me.SimpleButton2.Name = "SimpleButton2"
        Me.SimpleButton2.Size = New System.Drawing.Size(136, 23)
        Me.SimpleButton2.TabIndex = 1
        Me.SimpleButton2.Text = "Report Departement"
        '
        'GCReport
        '
        Me.GCReport.Location = New System.Drawing.Point(172, 12)
        Me.GCReport.MainView = Me.GVReport
        Me.GCReport.Name = "GCReport"
        Me.GCReport.Size = New System.Drawing.Size(400, 200)
        Me.GCReport.TabIndex = 2
        Me.GCReport.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVReport})
        '
        'GVReport
        '
        Me.GVReport.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn7, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6})
        Me.GVReport.GridControl = Me.GCReport
        Me.GVReport.Name = "GVReport"
        Me.GVReport.OptionsView.ShowFooter = True
        Me.GVReport.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Packing List QC"
        Me.GridColumn1.FieldName = "pl_prod_order_number"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "pl_prod_order_number", "Jumlah pending = {0}")})
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 230
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "FGPO Number"
        Me.GridColumn2.FieldName = "prod_order_number"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
        Me.GridColumn2.Width = 208
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "PL QC Complete Date"
        Me.GridColumn3.DisplayFormat.FormatString = "dd MMMM yyyy"
        Me.GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn3.FieldName = "complete_date"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 3
        Me.GridColumn3.Width = 250
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Design Code"
        Me.GridColumn4.FieldName = "design_code"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 4
        Me.GridColumn4.Width = 136
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Design"
        Me.GridColumn5.FieldName = "design_display_name"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 5
        Me.GridColumn5.Width = 443
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn6.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn6.Caption = "Jumlah pending (Hari)"
        Me.GridColumn6.DisplayFormat.FormatString = "N0"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn6.FieldName = "diff_date_serah_terima"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 6
        Me.GridColumn6.Width = 185
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "PL Category"
        Me.GridColumn7.FieldName = "pl_category"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 1
        Me.GridColumn7.Width = 164
        '
        'FormTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 215)
        Me.Controls.Add(Me.GCReport)
        Me.Controls.Add(Me.SimpleButton2)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Name = "FormTest"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Test Bug"
        CType(Me.GCReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GCReport As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVReport As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
End Class
