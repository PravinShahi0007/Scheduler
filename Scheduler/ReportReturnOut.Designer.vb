<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class ReportReturnOut
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.WinControlContainer1 = New DevExpress.XtraReports.UI.WinControlContainer()
        Me.GCTemp = New DevExpress.XtraGrid.GridControl()
        Me.GVTemp = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColRetOutNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColShipTo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColRecDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColDueDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColPSONumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnRetIn = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.LTitle = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.LReminderDate = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        CType(Me.GCTemp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVTemp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.WinControlContainer1})
        Me.Detail.HeightF = 184.375!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'WinControlContainer1
        '
        Me.WinControlContainer1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.WinControlContainer1.Name = "WinControlContainer1"
        Me.WinControlContainer1.SizeF = New System.Drawing.SizeF(1000.0!, 184.375!)
        Me.WinControlContainer1.WinControl = Me.GCTemp
        '
        'GCTemp
        '
        Me.GCTemp.Location = New System.Drawing.Point(0, 0)
        Me.GCTemp.MainView = Me.GVTemp
        Me.GCTemp.Name = "GCTemp"
        Me.GCTemp.Size = New System.Drawing.Size(960, 177)
        Me.GCTemp.TabIndex = 0
        Me.GCTemp.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVTemp})
        '
        'GVTemp
        '
        Me.GVTemp.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColRetOutNumber, Me.ColShipTo, Me.ColRecDate, Me.ColDueDate, Me.ColPSONumber, Me.GridColumn7, Me.GridColumn9, Me.GridColumn10, Me.GridColumnRetIn, Me.GridColumn1, Me.GridColumn2, Me.GridColumn3})
        Me.GVTemp.GridControl = Me.GCTemp
        Me.GVTemp.GroupCount = 1
        Me.GVTemp.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "qty", Me.GridColumn10, "{0:n0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "qty_retin", Me.GridColumnRetIn, "{0:N0}")})
        Me.GVTemp.Name = "GVTemp"
        Me.GVTemp.OptionsBehavior.AutoExpandAllGroups = True
        Me.GVTemp.OptionsBehavior.Editable = False
        Me.GVTemp.OptionsView.ShowFooter = True
        Me.GVTemp.OptionsView.ShowGroupPanel = False
        Me.GVTemp.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn3, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'ColRetOutNumber
        '
        Me.ColRetOutNumber.Caption = "Number"
        Me.ColRetOutNumber.FieldName = "prod_order_ret_out_number"
        Me.ColRetOutNumber.Name = "ColRetOutNumber"
        Me.ColRetOutNumber.Visible = True
        Me.ColRetOutNumber.VisibleIndex = 0
        Me.ColRetOutNumber.Width = 78
        '
        'ColShipTo
        '
        Me.ColShipTo.Caption = "To"
        Me.ColShipTo.FieldName = "comp_to"
        Me.ColShipTo.Name = "ColShipTo"
        Me.ColShipTo.Visible = True
        Me.ColShipTo.VisibleIndex = 4
        Me.ColShipTo.Width = 139
        '
        'ColRecDate
        '
        Me.ColRecDate.Caption = "Create Date"
        Me.ColRecDate.DisplayFormat.FormatString = "dd MMMM yyyy"
        Me.ColRecDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ColRecDate.FieldName = "prod_order_ret_out_date"
        Me.ColRecDate.Name = "ColRecDate"
        Me.ColRecDate.Width = 136
        '
        'ColDueDate
        '
        Me.ColDueDate.Caption = "Est. Return In Date"
        Me.ColDueDate.DisplayFormat.FormatString = "dd MMMM yyyy"
        Me.ColDueDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ColDueDate.FieldName = "prod_order_ret_out_due_date"
        Me.ColDueDate.Name = "ColDueDate"
        Me.ColDueDate.Visible = True
        Me.ColDueDate.VisibleIndex = 8
        Me.ColDueDate.Width = 148
        '
        'ColPSONumber
        '
        Me.ColPSONumber.Caption = "FGPO No."
        Me.ColPSONumber.FieldName = "prod_order_number"
        Me.ColPSONumber.Name = "ColPSONumber"
        Me.ColPSONumber.Visible = True
        Me.ColPSONumber.VisibleIndex = 1
        Me.ColPSONumber.Width = 80
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Code"
        Me.GridColumn7.FieldName = "code"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 2
        Me.GridColumn7.Width = 80
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Name"
        Me.GridColumn9.FieldName = "name"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 3
        Me.GridColumn9.Width = 139
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Qty"
        Me.GridColumn10.DisplayFormat.FormatString = "N0"
        Me.GridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn10.FieldName = "qty"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "qty", "{0:N0}")})
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 5
        Me.GridColumn10.Width = 50
        '
        'GridColumnRetIn
        '
        Me.GridColumnRetIn.Caption = "Qty Return In"
        Me.GridColumnRetIn.DisplayFormat.FormatString = "N0"
        Me.GridColumnRetIn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumnRetIn.FieldName = "qty_retin"
        Me.GridColumnRetIn.Name = "GridColumnRetIn"
        Me.GridColumnRetIn.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "qty_retin", "{0:N0}")})
        Me.GridColumnRetIn.Visible = True
        Me.GridColumnRetIn.VisibleIndex = 6
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Different (pcs)"
        Me.GridColumn1.FieldName = "diff_qty"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 7
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Overdue (days)"
        Me.GridColumn2.FieldName = "overdue"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 9
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Status"
        Me.GridColumn3.FieldName = "sts"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.UnboundExpression = "Iif([overdue] > 0, 'Overdue', 'Due Soon')"
        Me.GridColumn3.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 50.0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.HeightF = 19.0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrPageInfo1.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPageInfo1.Format = "Page {0} of {1}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(850.0!, 0!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(150.0!, 18.71793!)
        Me.XrPageInfo1.StylePriority.UseBorders = False
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'LTitle
        '
        Me.LTitle.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LTitle.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.LTitle.Name = "LTitle"
        Me.LTitle.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.LTitle.SizeF = New System.Drawing.SizeF(1000.0!, 32.37501!)
        Me.LTitle.StylePriority.UseFont = False
        Me.LTitle.StylePriority.UseTextAlignment = False
        Me.LTitle.Text = "RETURN OUT REMINDER"
        Me.LTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LReminderDate, Me.XrLabel2, Me.XrLabel1, Me.LTitle})
        Me.ReportHeader.HeightF = 99.04169!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'LReminderDate
        '
        Me.LReminderDate.LocationFloat = New DevExpress.Utils.PointFloat(109.375!, 66.04169!)
        Me.LReminderDate.Name = "LReminderDate"
        Me.LReminderDate.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.LReminderDate.SizeF = New System.Drawing.SizeF(248.9583!, 23.0!)
        '
        'XrLabel2
        '
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(92.70834!, 66.04169!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(16.66666!, 23.0!)
        Me.XrLabel2.Text = ":"
        '
        'XrLabel1
        '
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 66.04169!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(92.70834!, 23.0!)
        Me.XrLabel1.Text = "Reminder Date"
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo1})
        Me.PageFooter.HeightF = 18.71793!
        Me.PageFooter.Name = "PageFooter"
        '
        'ReportReturnOut
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.PageFooter})
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(47, 53, 50, 19)
        Me.PageHeight = 850
        Me.PageWidth = 1100
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "15.1"
        CType(Me.GCTemp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVTemp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents LTitle As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents WinControlContainer1 As DevExpress.XtraReports.UI.WinControlContainer
    Friend WithEvents GCTemp As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVTemp As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ColRetOutNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColShipTo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColRecDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColDueDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColPSONumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnRetIn As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LReminderDate As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
End Class
