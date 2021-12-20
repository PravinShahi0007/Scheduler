<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class ReportMkdBSP
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
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel14 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.LabelNotice = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.GCBSP = New DevExpress.XtraGrid.GridControl()
        Me.GVBSP = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.WinControlContainer1 = New DevExpress.XtraReports.UI.WinControlContainer()
        Me.GridColumnkode_lengkap = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnkode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnsize = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnclass = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumndeskripsi = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnwarna = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumndeskripsi_warna = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnharga_normal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumndisc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnharga_update = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnstatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnqty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumndivision = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnket = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnseason = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.GCBSP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVBSP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.WinControlContainer1})
        Me.Detail.HeightF = 271.875!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 37.5!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo1})
        Me.BottomMargin.HeightF = 39.30553!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel13, Me.XrLabel11, Me.XrLabel9, Me.XrLabel17, Me.XrLabel16, Me.XrLabel15, Me.XrLabel8, Me.XrLabel2, Me.XrLabel3, Me.XrLabel4, Me.XrLabel5, Me.XrLabel6, Me.XrLabel7, Me.XrLabel1, Me.XrLabel10, Me.XrLabel12, Me.XrLabel14})
        Me.PageHeader.HeightF = 148.9583!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrLabel8
        '
        Me.XrLabel8.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel8.LocationFloat = New DevExpress.Utils.PointFloat(127.875!, 82.41669!)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel8.SizeF = New System.Drawing.SizeF(423.9584!, 21.95834!)
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.StylePriority.UseTextAlignment = False
        Me.XrLabel8.Text = "[store]"
        Me.XrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel2
        '
        Me.XrLabel2.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(786.5417!, 0!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(311.4583!, 37.58334!)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.Text = "PT. VOLCOM INDONESIA"
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel3
        '
        Me.XrLabel3.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(0.7916133!, 38.49999!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(110.4167!, 21.95834!)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.Text = "MULAI EVENT"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel4
        '
        Me.XrLabel4.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(111.2083!, 38.49999!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(16.66669!, 21.95834!)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.StylePriority.UseTextAlignment = False
        Me.XrLabel4.Text = ":"
        Me.XrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel5
        '
        Me.XrLabel5.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(127.875!, 38.49999!)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(423.9585!, 21.95834!)
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.StylePriority.UseTextAlignment = False
        Me.XrLabel5.Text = "[start_date_display]"
        Me.XrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel6
        '
        Me.XrLabel6.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(111.2083!, 82.41669!)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(16.66669!, 21.95834!)
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = ":"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel7
        '
        Me.XrLabel7.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(0.7916133!, 82.41669!)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(110.4167!, 21.95834!)
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.StylePriority.UseTextAlignment = False
        Me.XrLabel7.Text = "TOKO"
        Me.XrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel1
        '
        Me.XrLabel1.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(0.7916133!, 0.9166718!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(336.4583!, 37.58334!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "PRICE LIST PRODUK BIG SALE"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel10
        '
        Me.XrLabel10.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(801.1251!, 38.49999!)
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel10.SizeF = New System.Drawing.SizeF(136.4583!, 21.95834!)
        Me.XrLabel10.StylePriority.UseFont = False
        Me.XrLabel10.StylePriority.UseTextAlignment = False
        Me.XrLabel10.Text = "TANGGAL DIBUAT"
        Me.XrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel12
        '
        Me.XrLabel12.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(937.5833!, 38.49999!)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel12.SizeF = New System.Drawing.SizeF(16.66669!, 21.95834!)
        Me.XrLabel12.StylePriority.UseFont = False
        Me.XrLabel12.StylePriority.UseTextAlignment = False
        Me.XrLabel12.Text = ":"
        Me.XrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel14
        '
        Me.XrLabel14.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel14.LocationFloat = New DevExpress.Utils.PointFloat(954.2499!, 38.49999!)
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel14.SizeF = New System.Drawing.SizeF(143.7501!, 21.95834!)
        Me.XrLabel14.StylePriority.UseFont = False
        Me.XrLabel14.StylePriority.UseTextAlignment = False
        Me.XrLabel14.Text = "[created_date_display]"
        Me.XrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel15
        '
        Me.XrLabel15.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(0!, 60.45834!)
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel15.SizeF = New System.Drawing.SizeF(110.4167!, 21.95834!)
        Me.XrLabel15.StylePriority.UseFont = False
        Me.XrLabel15.StylePriority.UseTextAlignment = False
        Me.XrLabel15.Text = "SELESAI EVENT"
        Me.XrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel16
        '
        Me.XrLabel16.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(110.4167!, 60.45834!)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(16.66669!, 21.95834!)
        Me.XrLabel16.StylePriority.UseFont = False
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        Me.XrLabel16.Text = ":"
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel17
        '
        Me.XrLabel17.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel17.LocationFloat = New DevExpress.Utils.PointFloat(127.0834!, 60.45834!)
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel17.SizeF = New System.Drawing.SizeF(424.7501!, 21.95834!)
        Me.XrLabel17.StylePriority.UseFont = False
        Me.XrLabel17.StylePriority.UseTextAlignment = False
        Me.XrLabel17.Text = "[end_date_display]"
        Me.XrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LabelNotice})
        Me.ReportFooter.HeightF = 24.23613!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'LabelNotice
        '
        Me.LabelNotice.Font = New System.Drawing.Font("Segoe UI", 6.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelNotice.LocationFloat = New DevExpress.Utils.PointFloat(0.7916133!, 0!)
        Me.LabelNotice.Name = "LabelNotice"
        Me.LabelNotice.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.LabelNotice.SizeF = New System.Drawing.SizeF(1097.208!, 24.23613!)
        Me.LabelNotice.StylePriority.UseFont = False
        Me.LabelNotice.StylePriority.UseTextAlignment = False
        Me.LabelNotice.Text = "* This is computer generated Price List. Signature not required."
        Me.LabelNotice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrPageInfo1.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPageInfo1.Format = "Page {0} of {1}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(947.9999!, 0!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(150.0!, 18.71793!)
        Me.XrPageInfo1.StylePriority.UseBorders = False
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel9
        '
        Me.XrLabel9.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel9.LocationFloat = New DevExpress.Utils.PointFloat(0!, 104.375!)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel9.SizeF = New System.Drawing.SizeF(111.2083!, 21.95834!)
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.StylePriority.UseTextAlignment = False
        Me.XrLabel9.Text = "LOKASI"
        Me.XrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel11
        '
        Me.XrLabel11.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(111.2083!, 104.375!)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel11.SizeF = New System.Drawing.SizeF(16.66669!, 21.95834!)
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.StylePriority.UseTextAlignment = False
        Me.XrLabel11.Text = ":"
        Me.XrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel13
        '
        Me.XrLabel13.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel13.LocationFloat = New DevExpress.Utils.PointFloat(127.875!, 104.375!)
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel13.SizeF = New System.Drawing.SizeF(423.9584!, 21.95834!)
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.StylePriority.UseTextAlignment = False
        Me.XrLabel13.Text = "[store_address]"
        Me.XrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'GCBSP
        '
        Me.GCBSP.Location = New System.Drawing.Point(0, 41)
        Me.GCBSP.MainView = Me.GVBSP
        Me.GCBSP.Name = "GCBSP"
        Me.GCBSP.Size = New System.Drawing.Size(1053, 251)
        Me.GCBSP.TabIndex = 4
        Me.GCBSP.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVBSP})
        '
        'GVBSP
        '
        Me.GVBSP.AppearancePrint.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GVBSP.AppearancePrint.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.GVBSP.AppearancePrint.HeaderPanel.Options.UseBackColor = True
        Me.GVBSP.AppearancePrint.HeaderPanel.Options.UseFont = True
        Me.GVBSP.AppearancePrint.HeaderPanel.Options.UseForeColor = True
        Me.GVBSP.AppearancePrint.HeaderPanel.Options.UseTextOptions = True
        Me.GVBSP.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GVBSP.AppearancePrint.Row.BackColor = System.Drawing.Color.Transparent
        Me.GVBSP.AppearancePrint.Row.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.GVBSP.AppearancePrint.Row.ForeColor = System.Drawing.Color.Black
        Me.GVBSP.AppearancePrint.Row.Options.UseBackColor = True
        Me.GVBSP.AppearancePrint.Row.Options.UseFont = True
        Me.GVBSP.AppearancePrint.Row.Options.UseForeColor = True
        Me.GVBSP.AppearancePrint.Row.Options.UseTextOptions = True
        Me.GVBSP.AppearancePrint.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GVBSP.ColumnPanelRowHeight = 50
        Me.GVBSP.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumnkode_lengkap, Me.GridColumnkode, Me.GridColumnsize, Me.GridColumnclass, Me.GridColumndeskripsi, Me.GridColumnwarna, Me.GridColumndeskripsi_warna, Me.GridColumnharga_normal, Me.GridColumndisc, Me.GridColumnharga_update, Me.GridColumnstatus, Me.GridColumnqty, Me.GridColumndivision, Me.GridColumnket, Me.GridColumnseason})
        Me.GVBSP.GridControl = Me.GCBSP
        Me.GVBSP.Name = "GVBSP"
        Me.GVBSP.OptionsBehavior.AutoExpandAllGroups = True
        Me.GVBSP.OptionsBehavior.ReadOnly = True
        Me.GVBSP.OptionsFind.AlwaysVisible = True
        Me.GVBSP.OptionsPrint.AllowMultilineHeaders = True
        Me.GVBSP.OptionsView.ColumnAutoWidth = False
        Me.GVBSP.OptionsView.ShowFooter = True
        Me.GVBSP.OptionsView.ShowGroupPanel = False
        Me.GVBSP.RowHeight = 30
        '
        'WinControlContainer1
        '
        Me.WinControlContainer1.LocationFloat = New DevExpress.Utils.PointFloat(0.7916133!, 0!)
        Me.WinControlContainer1.Name = "WinControlContainer1"
        Me.WinControlContainer1.SizeF = New System.Drawing.SizeF(1097.208!, 261.875!)
        Me.WinControlContainer1.WinControl = Me.GCBSP
        '
        'GridColumnkode_lengkap
        '
        Me.GridColumnkode_lengkap.AppearanceHeader.BackColor = System.Drawing.Color.White
        Me.GridColumnkode_lengkap.AppearanceHeader.Options.UseBackColor = True
        Me.GridColumnkode_lengkap.Caption = "KODE LENGKAP"
        Me.GridColumnkode_lengkap.FieldName = "product_full_code"
        Me.GridColumnkode_lengkap.Name = "GridColumnkode_lengkap"
        Me.GridColumnkode_lengkap.Visible = True
        Me.GridColumnkode_lengkap.VisibleIndex = 0
        Me.GridColumnkode_lengkap.Width = 99
        '
        'GridColumnkode
        '
        Me.GridColumnkode.AppearanceHeader.BackColor = System.Drawing.Color.White
        Me.GridColumnkode.AppearanceHeader.Options.UseBackColor = True
        Me.GridColumnkode.Caption = "KODE"
        Me.GridColumnkode.FieldName = "design_code"
        Me.GridColumnkode.Name = "GridColumnkode"
        Me.GridColumnkode.Visible = True
        Me.GridColumnkode.VisibleIndex = 1
        '
        'GridColumnsize
        '
        Me.GridColumnsize.AppearanceHeader.BackColor = System.Drawing.Color.White
        Me.GridColumnsize.AppearanceHeader.Options.UseBackColor = True
        Me.GridColumnsize.Caption = "SIZE"
        Me.GridColumnsize.FieldName = "size"
        Me.GridColumnsize.Name = "GridColumnsize"
        Me.GridColumnsize.Visible = True
        Me.GridColumnsize.VisibleIndex = 2
        Me.GridColumnsize.Width = 51
        '
        'GridColumnclass
        '
        Me.GridColumnclass.AppearanceHeader.BackColor = System.Drawing.Color.White
        Me.GridColumnclass.AppearanceHeader.Options.UseBackColor = True
        Me.GridColumnclass.Caption = "KELAS"
        Me.GridColumnclass.FieldName = "class"
        Me.GridColumnclass.Name = "GridColumnclass"
        Me.GridColumnclass.Visible = True
        Me.GridColumnclass.VisibleIndex = 3
        Me.GridColumnclass.Width = 55
        '
        'GridColumndeskripsi
        '
        Me.GridColumndeskripsi.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumndeskripsi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumndeskripsi.AppearanceHeader.BackColor = System.Drawing.Color.White
        Me.GridColumndeskripsi.AppearanceHeader.Options.UseBackColor = True
        Me.GridColumndeskripsi.Caption = "DESKRIPSI"
        Me.GridColumndeskripsi.FieldName = "name"
        Me.GridColumndeskripsi.Name = "GridColumndeskripsi"
        Me.GridColumndeskripsi.Visible = True
        Me.GridColumndeskripsi.VisibleIndex = 4
        Me.GridColumndeskripsi.Width = 179
        '
        'GridColumnwarna
        '
        Me.GridColumnwarna.AppearanceHeader.BackColor = System.Drawing.Color.White
        Me.GridColumnwarna.AppearanceHeader.Options.UseBackColor = True
        Me.GridColumnwarna.Caption = "WARNA"
        Me.GridColumnwarna.FieldName = "color"
        Me.GridColumnwarna.Name = "GridColumnwarna"
        Me.GridColumnwarna.Visible = True
        Me.GridColumnwarna.VisibleIndex = 5
        Me.GridColumnwarna.Width = 72
        '
        'GridColumndeskripsi_warna
        '
        Me.GridColumndeskripsi_warna.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumndeskripsi_warna.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumndeskripsi_warna.AppearanceHeader.BackColor = System.Drawing.Color.White
        Me.GridColumndeskripsi_warna.AppearanceHeader.Options.UseBackColor = True
        Me.GridColumndeskripsi_warna.Caption = "DESKRIPSI WARNA"
        Me.GridColumndeskripsi_warna.FieldName = "color_desc"
        Me.GridColumndeskripsi_warna.Name = "GridColumndeskripsi_warna"
        Me.GridColumndeskripsi_warna.Visible = True
        Me.GridColumndeskripsi_warna.VisibleIndex = 6
        Me.GridColumndeskripsi_warna.Width = 90
        '
        'GridColumnharga_normal
        '
        Me.GridColumnharga_normal.AppearanceHeader.BackColor = System.Drawing.Color.White
        Me.GridColumnharga_normal.AppearanceHeader.Options.UseBackColor = True
        Me.GridColumnharga_normal.Caption = "HARGA NORMAL"
        Me.GridColumnharga_normal.DisplayFormat.FormatString = "N0"
        Me.GridColumnharga_normal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumnharga_normal.FieldName = "design_price_normal"
        Me.GridColumnharga_normal.Name = "GridColumnharga_normal"
        Me.GridColumnharga_normal.Visible = True
        Me.GridColumnharga_normal.VisibleIndex = 9
        Me.GridColumnharga_normal.Width = 112
        '
        'GridColumndisc
        '
        Me.GridColumndisc.AppearanceHeader.BackColor = System.Drawing.Color.White
        Me.GridColumndisc.AppearanceHeader.Options.UseBackColor = True
        Me.GridColumndisc.Caption = "DISC"
        Me.GridColumndisc.DisplayFormat.FormatString = "{0:n0}%"
        Me.GridColumndisc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumndisc.FieldName = "disc"
        Me.GridColumndisc.Name = "GridColumndisc"
        Me.GridColumndisc.Visible = True
        Me.GridColumndisc.VisibleIndex = 11
        Me.GridColumndisc.Width = 53
        '
        'GridColumnharga_update
        '
        Me.GridColumnharga_update.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GridColumnharga_update.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumnharga_update.AppearanceCell.Options.UseBackColor = True
        Me.GridColumnharga_update.AppearanceCell.Options.UseFont = True
        Me.GridColumnharga_update.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GridColumnharga_update.AppearanceHeader.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GridColumnharga_update.AppearanceHeader.Options.UseBackColor = True
        Me.GridColumnharga_update.Caption = "HARGA BIG SALE"
        Me.GridColumnharga_update.DisplayFormat.FormatString = "N0"
        Me.GridColumnharga_update.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumnharga_update.FieldName = "design_price"
        Me.GridColumnharga_update.ImageAlignment = System.Drawing.StringAlignment.Far
        Me.GridColumnharga_update.Name = "GridColumnharga_update"
        Me.GridColumnharga_update.Visible = True
        Me.GridColumnharga_update.VisibleIndex = 10
        Me.GridColumnharga_update.Width = 109
        '
        'GridColumnstatus
        '
        Me.GridColumnstatus.Caption = "STATUS"
        Me.GridColumnstatus.FieldName = "status"
        Me.GridColumnstatus.Name = "GridColumnstatus"
        Me.GridColumnstatus.Visible = True
        Me.GridColumnstatus.VisibleIndex = 7
        Me.GridColumnstatus.Width = 49
        '
        'GridColumnqty
        '
        Me.GridColumnqty.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumnqty.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumnqty.Caption = "TOTAL QTY"
        Me.GridColumnqty.DisplayFormat.FormatString = "N0"
        Me.GridColumnqty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumnqty.FieldName = "qty"
        Me.GridColumnqty.Name = "GridColumnqty"
        Me.GridColumnqty.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "qty", "{0:N0}")})
        Me.GridColumnqty.Visible = True
        Me.GridColumnqty.VisibleIndex = 8
        Me.GridColumnqty.Width = 77
        '
        'GridColumndivision
        '
        Me.GridColumndivision.Caption = "GENDER"
        Me.GridColumndivision.FieldName = "division"
        Me.GridColumndivision.Name = "GridColumndivision"
        Me.GridColumndivision.Visible = True
        Me.GridColumndivision.VisibleIndex = 12
        '
        'GridColumnket
        '
        Me.GridColumnket.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnket.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumnket.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumnket.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumnket.Caption = "KET"
        Me.GridColumnket.FieldName = "ket"
        Me.GridColumnket.Name = "GridColumnket"
        Me.GridColumnket.Visible = True
        Me.GridColumnket.VisibleIndex = 13
        '
        'GridColumnseason
        '
        Me.GridColumnseason.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnseason.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumnseason.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumnseason.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumnseason.Caption = "SEASON"
        Me.GridColumnseason.FieldName = "season"
        Me.GridColumnseason.Name = "GridColumnseason"
        Me.GridColumnseason.Visible = True
        Me.GridColumnseason.VisibleIndex = 14
        '
        'ReportMkdBSP
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.PageHeader, Me.ReportFooter})
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(41, 30, 38, 39)
        Me.PageHeight = 827
        Me.PageWidth = 1169
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "15.1"
        CType(Me.GCBSP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVBSP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel14 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents LabelNotice As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents WinControlContainer1 As DevExpress.XtraReports.UI.WinControlContainer
    Friend WithEvents GCBSP As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVBSP As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumnkode_lengkap As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnkode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnsize As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnclass As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumndeskripsi As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnwarna As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumndeskripsi_warna As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnharga_normal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumndisc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnharga_update As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnstatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnqty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumndivision As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnket As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnseason As DevExpress.XtraGrid.Columns.GridColumn
End Class
