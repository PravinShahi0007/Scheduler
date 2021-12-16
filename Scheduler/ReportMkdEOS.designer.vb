<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class ReportMkdEOS
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportMkdEOS))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.WinControlContainer1 = New DevExpress.XtraReports.UI.WinControlContainer()
        Me.GCData = New DevExpress.XtraGrid.GridControl()
        Me.GVData = New DevExpress.XtraGrid.Views.Grid.GridView()
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
        Me.GridColumnket = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepoLinkNumber = New DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrLabel14 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.LabelNotice = New DevExpress.XtraReports.UI.XRLabel()
        CType(Me.GCData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepoLinkNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.WinControlContainer1})
        Me.Detail.HeightF = 100.0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'WinControlContainer1
        '
        Me.WinControlContainer1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.WinControlContainer1.Name = "WinControlContainer1"
        Me.WinControlContainer1.SizeF = New System.Drawing.SizeF(761.0001!, 89.99999!)
        Me.WinControlContainer1.WinControl = Me.GCData
        '
        'GCData
        '
        Me.GCData.Location = New System.Drawing.Point(0, 0)
        Me.GCData.MainView = Me.GVData
        Me.GCData.Name = "GCData"
        Me.GCData.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepoLinkNumber})
        Me.GCData.Size = New System.Drawing.Size(731, 86)
        Me.GCData.TabIndex = 0
        Me.GCData.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVData})
        '
        'GVData
        '
        Me.GVData.AppearancePrint.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GVData.AppearancePrint.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.GVData.AppearancePrint.HeaderPanel.Options.UseBackColor = True
        Me.GVData.AppearancePrint.HeaderPanel.Options.UseFont = True
        Me.GVData.AppearancePrint.HeaderPanel.Options.UseForeColor = True
        Me.GVData.AppearancePrint.HeaderPanel.Options.UseTextOptions = True
        Me.GVData.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GVData.AppearancePrint.Row.BackColor = System.Drawing.Color.Transparent
        Me.GVData.AppearancePrint.Row.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GVData.AppearancePrint.Row.ForeColor = System.Drawing.Color.Black
        Me.GVData.AppearancePrint.Row.Options.UseBackColor = True
        Me.GVData.AppearancePrint.Row.Options.UseFont = True
        Me.GVData.AppearancePrint.Row.Options.UseForeColor = True
        Me.GVData.AppearancePrint.Row.Options.UseTextOptions = True
        Me.GVData.AppearancePrint.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GVData.ColumnPanelRowHeight = 50
        Me.GVData.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumnkode_lengkap, Me.GridColumnkode, Me.GridColumnsize, Me.GridColumnclass, Me.GridColumndeskripsi, Me.GridColumnwarna, Me.GridColumndeskripsi_warna, Me.GridColumnharga_normal, Me.GridColumndisc, Me.GridColumnharga_update, Me.GridColumnket})
        Me.GVData.GridControl = Me.GCData
        Me.GVData.Name = "GVData"
        Me.GVData.OptionsBehavior.AutoExpandAllGroups = True
        Me.GVData.OptionsBehavior.ReadOnly = True
        Me.GVData.OptionsFind.AlwaysVisible = True
        Me.GVData.OptionsPrint.AllowMultilineHeaders = True
        Me.GVData.OptionsView.ColumnAutoWidth = False
        Me.GVData.OptionsView.ShowGroupPanel = False
        Me.GVData.RowHeight = 30
        '
        'GridColumnkode_lengkap
        '
        Me.GridColumnkode_lengkap.AppearanceHeader.BackColor = System.Drawing.Color.White
        Me.GridColumnkode_lengkap.AppearanceHeader.Options.UseBackColor = True
        Me.GridColumnkode_lengkap.Caption = "KODE LENGKAP"
        Me.GridColumnkode_lengkap.FieldName = "kode_lengkap"
        Me.GridColumnkode_lengkap.Name = "GridColumnkode_lengkap"
        Me.GridColumnkode_lengkap.Visible = True
        Me.GridColumnkode_lengkap.VisibleIndex = 0
        Me.GridColumnkode_lengkap.Width = 113
        '
        'GridColumnkode
        '
        Me.GridColumnkode.AppearanceHeader.BackColor = System.Drawing.Color.White
        Me.GridColumnkode.AppearanceHeader.Options.UseBackColor = True
        Me.GridColumnkode.Caption = "KODE"
        Me.GridColumnkode.FieldName = "kode"
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
        '
        'GridColumndeskripsi
        '
        Me.GridColumndeskripsi.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumndeskripsi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumndeskripsi.AppearanceHeader.BackColor = System.Drawing.Color.White
        Me.GridColumndeskripsi.AppearanceHeader.Options.UseBackColor = True
        Me.GridColumndeskripsi.Caption = "DESKRIPSI"
        Me.GridColumndeskripsi.FieldName = "deskripsi"
        Me.GridColumndeskripsi.Name = "GridColumndeskripsi"
        Me.GridColumndeskripsi.Visible = True
        Me.GridColumndeskripsi.VisibleIndex = 4
        '
        'GridColumnwarna
        '
        Me.GridColumnwarna.AppearanceHeader.BackColor = System.Drawing.Color.White
        Me.GridColumnwarna.AppearanceHeader.Options.UseBackColor = True
        Me.GridColumnwarna.Caption = "WARNA"
        Me.GridColumnwarna.FieldName = "warna"
        Me.GridColumnwarna.Name = "GridColumnwarna"
        Me.GridColumnwarna.Visible = True
        Me.GridColumnwarna.VisibleIndex = 5
        Me.GridColumnwarna.Width = 118
        '
        'GridColumndeskripsi_warna
        '
        Me.GridColumndeskripsi_warna.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumndeskripsi_warna.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumndeskripsi_warna.AppearanceHeader.BackColor = System.Drawing.Color.White
        Me.GridColumndeskripsi_warna.AppearanceHeader.Options.UseBackColor = True
        Me.GridColumndeskripsi_warna.Caption = "DESKRIPSI WARNA"
        Me.GridColumndeskripsi_warna.FieldName = "deskripsi_warna"
        Me.GridColumndeskripsi_warna.Name = "GridColumndeskripsi_warna"
        Me.GridColumndeskripsi_warna.Visible = True
        Me.GridColumndeskripsi_warna.VisibleIndex = 6
        '
        'GridColumnharga_normal
        '
        Me.GridColumnharga_normal.AppearanceHeader.BackColor = System.Drawing.Color.White
        Me.GridColumnharga_normal.AppearanceHeader.Options.UseBackColor = True
        Me.GridColumnharga_normal.Caption = "HARGA NORMAL"
        Me.GridColumnharga_normal.DisplayFormat.FormatString = "N0"
        Me.GridColumnharga_normal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumnharga_normal.FieldName = "harga_normal"
        Me.GridColumnharga_normal.Name = "GridColumnharga_normal"
        Me.GridColumnharga_normal.Visible = True
        Me.GridColumnharga_normal.VisibleIndex = 7
        '
        'GridColumndisc
        '
        Me.GridColumndisc.AppearanceHeader.BackColor = System.Drawing.Color.White
        Me.GridColumndisc.AppearanceHeader.Options.UseBackColor = True
        Me.GridColumndisc.Caption = "DISC"
        Me.GridColumndisc.FieldName = "disc"
        Me.GridColumndisc.Name = "GridColumndisc"
        Me.GridColumndisc.Visible = True
        Me.GridColumndisc.VisibleIndex = 8
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
        Me.GridColumnharga_update.Caption = "HARGA UPDATE"
        Me.GridColumnharga_update.DisplayFormat.FormatString = "N0"
        Me.GridColumnharga_update.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumnharga_update.FieldName = "harga_update"
        Me.GridColumnharga_update.Image = CType(resources.GetObject("GridColumnharga_update.Image"), System.Drawing.Image)
        Me.GridColumnharga_update.ImageAlignment = System.Drawing.StringAlignment.Far
        Me.GridColumnharga_update.Name = "GridColumnharga_update"
        Me.GridColumnharga_update.Visible = True
        Me.GridColumnharga_update.VisibleIndex = 9
        Me.GridColumnharga_update.Width = 109
        '
        'GridColumnket
        '
        Me.GridColumnket.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnket.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumnket.AppearanceHeader.BackColor = System.Drawing.Color.White
        Me.GridColumnket.AppearanceHeader.Options.UseBackColor = True
        Me.GridColumnket.Caption = "KET"
        Me.GridColumnket.FieldName = "ket"
        Me.GridColumnket.Name = "GridColumnket"
        Me.GridColumnket.Visible = True
        Me.GridColumnket.VisibleIndex = 10
        '
        'RepoLinkNumber
        '
        Me.RepoLinkNumber.AutoHeight = False
        Me.RepoLinkNumber.Name = "RepoLinkNumber"
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 38.0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo1})
        Me.BottomMargin.HeightF = 38.0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrPageInfo1.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPageInfo1.Format = "Page {0} of {1}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(611.0!, 0!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(150.0!, 18.71793!)
        Me.XrPageInfo1.StylePriority.UseBorders = False
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel14, Me.XrLabel13, Me.XrLabel12, Me.XrLabel11, Me.XrLabel10, Me.XrLabel9, Me.XrLabel8, Me.XrLabel7, Me.XrLabel6, Me.XrLabel5, Me.XrLabel4, Me.XrLabel3, Me.XrLabel2, Me.XrLabel1})
        Me.PageHeader.HeightF = 112.5!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrLabel14
        '
        Me.XrLabel14.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel14.LocationFloat = New DevExpress.Utils.PointFloat(541.2084!, 59.54167!)
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel14.SizeF = New System.Drawing.SizeF(219.7917!, 21.95834!)
        Me.XrLabel14.StylePriority.UseFont = False
        Me.XrLabel14.StylePriority.UseTextAlignment = False
        Me.XrLabel14.Text = "[created_date]"
        Me.XrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel13
        '
        Me.XrLabel13.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel13.LocationFloat = New DevExpress.Utils.PointFloat(541.2084!, 37.58334!)
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel13.SizeF = New System.Drawing.SizeF(219.7916!, 21.95834!)
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.StylePriority.UseTextAlignment = False
        Me.XrLabel13.Text = "[number]"
        Me.XrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel12
        '
        Me.XrLabel12.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(524.5417!, 59.54167!)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel12.SizeF = New System.Drawing.SizeF(16.66669!, 21.95834!)
        Me.XrLabel12.StylePriority.UseFont = False
        Me.XrLabel12.StylePriority.UseTextAlignment = False
        Me.XrLabel12.Text = ":"
        Me.XrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel11
        '
        Me.XrLabel11.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(524.5417!, 37.58334!)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel11.SizeF = New System.Drawing.SizeF(16.66669!, 21.95834!)
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.StylePriority.UseTextAlignment = False
        Me.XrLabel11.Text = ":"
        Me.XrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel10
        '
        Me.XrLabel10.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(449.5417!, 59.54167!)
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel10.SizeF = New System.Drawing.SizeF(75.00003!, 21.95834!)
        Me.XrLabel10.StylePriority.UseFont = False
        Me.XrLabel10.StylePriority.UseTextAlignment = False
        Me.XrLabel10.Text = "TANGGAL"
        Me.XrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel9
        '
        Me.XrLabel9.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel9.LocationFloat = New DevExpress.Utils.PointFloat(449.5417!, 37.58334!)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel9.SizeF = New System.Drawing.SizeF(75.00003!, 21.95834!)
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.StylePriority.UseTextAlignment = False
        Me.XrLabel9.Text = "NOMOR"
        Me.XrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel8
        '
        Me.XrLabel8.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel8.LocationFloat = New DevExpress.Utils.PointFloat(127.0834!, 59.54167!)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel8.SizeF = New System.Drawing.SizeF(209.375!, 21.95834!)
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.StylePriority.UseTextAlignment = False
        Me.XrLabel8.Text = "[store_name]"
        Me.XrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel7
        '
        Me.XrLabel7.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(0!, 59.54167!)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(110.4167!, 21.95834!)
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.StylePriority.UseTextAlignment = False
        Me.XrLabel7.Text = "TOKO"
        Me.XrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel6
        '
        Me.XrLabel6.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(110.4167!, 59.54167!)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(16.66669!, 21.95834!)
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = ":"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel5
        '
        Me.XrLabel5.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(127.0834!, 37.58334!)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(209.375!, 21.95834!)
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.StylePriority.UseTextAlignment = False
        Me.XrLabel5.Text = "[start_date]"
        Me.XrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel4
        '
        Me.XrLabel4.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(110.4167!, 37.58334!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(16.66669!, 21.95834!)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.StylePriority.UseTextAlignment = False
        Me.XrLabel4.Text = ":"
        Me.XrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel3
        '
        Me.XrLabel3.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(0!, 37.58334!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(110.4167!, 21.95834!)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.Text = "PERIODE EVENT"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel2
        '
        Me.XrLabel2.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(449.5417!, 0!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(311.4583!, 37.58334!)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.Text = "PT. VOLCOM INDONESIA"
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel1
        '
        Me.XrLabel1.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(248.9584!, 37.58334!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "PRICE LIST PRODUK EOSS"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
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
        Me.LabelNotice.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.LabelNotice.Name = "LabelNotice"
        Me.LabelNotice.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.LabelNotice.SizeF = New System.Drawing.SizeF(761.0!, 24.23613!)
        Me.LabelNotice.StylePriority.UseFont = False
        Me.LabelNotice.StylePriority.UseTextAlignment = False
        Me.LabelNotice.Text = "* This is computer generated Price List. Signature not required."
        Me.LabelNotice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'ReportMkdEOS
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.PageHeader, Me.ReportFooter})
        Me.Margins = New System.Drawing.Printing.Margins(36, 30, 38, 38)
        Me.PageHeight = 1169
        Me.PageWidth = 827
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "15.1"
        CType(Me.GCData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepoLinkNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel14 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents LabelNotice As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents WinControlContainer1 As DevExpress.XtraReports.UI.WinControlContainer
    Friend WithEvents GCData As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVData As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepoLinkNumber As DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit
    Friend WithEvents GridColumnkode_lengkap As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnkode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnsize As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnclass As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumndeskripsi As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnwarna As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumndeskripsi_warna As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumndisc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnharga_update As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnket As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnharga_normal As DevExpress.XtraGrid.Columns.GridColumn
End Class
