<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormScheduler
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormScheduler))
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SettingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.Linfo = New DevExpress.XtraEditors.LabelControl()
        Me.BCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.BSave = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnFailOrder = New DevExpress.XtraEditors.SimpleButton()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TECheckFailOrder = New DevExpress.XtraEditors.TimeEdit()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.BDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.BAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.GCSchedule = New DevExpress.XtraGrid.GridControl()
        Me.GVSchedule = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XTPSchedule = New DevExpress.XtraTab.XtraTabPage()
        Me.XTPAttendance = New DevExpress.XtraTab.XtraTabPage()
        Me.BSaveWAR = New DevExpress.XtraEditors.SimpleButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TETime = New DevExpress.XtraEditors.TimeEdit()
        Me.LEDay = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.XTPLeaveRemaining = New DevExpress.XtraTab.XtraTabPage()
        Me.BSaveMonthly = New DevExpress.XtraEditors.SimpleButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TETimeMonthly = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPProduction = New DevExpress.XtraTab.XtraTabPage()
        Me.BProdDuty = New DevExpress.XtraEditors.SimpleButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TETimeDuty = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPEmpAppraisal = New DevExpress.XtraTab.XtraTabPage()
        Me.SBEmpPerApp = New DevExpress.XtraEditors.SimpleButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TEEmpPerApp = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPCashAdvance = New DevExpress.XtraTab.XtraTabPage()
        Me.SBCashAdvance = New DevExpress.XtraEditors.SimpleButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TECashAdvance = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPEvaulationAR = New DevExpress.XtraTab.XtraTabPage()
        Me.LEDayAREval = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.BtnEvaluationAR = New DevExpress.XtraEditors.SimpleButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TEEvaluationAR = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPNoticeEmail = New DevExpress.XtraTab.XtraTabPage()
        Me.BtnEmailNoticeAR = New DevExpress.XtraEditors.SimpleButton()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TEEmailNoticeAR = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPWarningLate = New DevExpress.XtraTab.XtraTabPage()
        Me.SBWarningLate = New DevExpress.XtraEditors.SimpleButton()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TEWaningLate = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPGetKurs = New DevExpress.XtraTab.XtraTabPage()
        Me.BSaveKurs = New DevExpress.XtraEditors.SimpleButton()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TETimeKurs = New DevExpress.XtraEditors.TimeEdit()
        Me.LEDayKurs = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.XTPFailOrder = New DevExpress.XtraTab.XtraTabPage()
        Me.GCSchCloseOrder = New DevExpress.XtraGrid.GridControl()
        Me.GVSchCloseOrder = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.BtnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.XTPSalesReturn = New DevExpress.XtraTab.XtraTabPage()
        Me.SBSalesReturnOrder = New DevExpress.XtraEditors.SimpleButton()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TESalesReturnOrder = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPMarketplaceOrderStt = New DevExpress.XtraTab.XtraTabPage()
        Me.GCMOS = New DevExpress.XtraGrid.GridControl()
        Me.GVMOS = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PanelControl4 = New DevExpress.XtraEditors.PanelControl()
        Me.BtnRefMOS = New DevExpress.XtraEditors.SimpleButton()
        Me.XTPQC = New DevExpress.XtraTab.XtraTabPage()
        Me.BQC = New DevExpress.XtraEditors.SimpleButton()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.TEQC = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPPolis = New DevExpress.XtraTab.XtraTabPage()
        Me.BPolis = New DevExpress.XtraEditors.SimpleButton()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.TEPolis = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPPOOG = New DevExpress.XtraTab.XtraTabPage()
        Me.BPOOG = New DevExpress.XtraEditors.SimpleButton()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.TEPOOG = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPPROG = New DevExpress.XtraTab.XtraTabPage()
        Me.BPROG = New DevExpress.XtraEditors.SimpleButton()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.TEPROG = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPSerahTerima = New DevExpress.XtraTab.XtraTabPage()
        Me.BSerahTerimaQC = New DevExpress.XtraEditors.SimpleButton()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.TESerahTerimaQC = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPPIBReview = New DevExpress.XtraTab.XtraTabPage()
        Me.BPIBNotif = New DevExpress.XtraEditors.SimpleButton()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.TEPIBNotif = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPEOS = New DevExpress.XtraTab.XtraTabPage()
        Me.GCEOS = New DevExpress.XtraGrid.GridControl()
        Me.GVEOS = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl5 = New DevExpress.XtraEditors.PanelControl()
        Me.BtnRefreshEOS = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnEOS = New DevExpress.XtraEditors.SimpleButton()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.TEEOSNotif = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPEOSPrice = New DevExpress.XtraTab.XtraTabPage()
        Me.GCPriceEOS = New DevExpress.XtraGrid.GridControl()
        Me.GVPriceEOS = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl6 = New DevExpress.XtraEditors.PanelControl()
        Me.BtnRefreshPriceEOS = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnPriceEOS = New DevExpress.XtraEditors.SimpleButton()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.TEPriceEOSNotif = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPBSP = New DevExpress.XtraTab.XtraTabPage()
        Me.GCBSP = New DevExpress.XtraGrid.GridControl()
        Me.GVBSP = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl7 = New DevExpress.XtraEditors.PanelControl()
        Me.BtnRefreshBSP = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnBSP = New DevExpress.XtraEditors.SimpleButton()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.TEBSPNotif = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPLineList = New DevExpress.XtraTab.XtraTabPage()
        Me.BtnResendEmail = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnLineList = New DevExpress.XtraEditors.SimpleButton()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.TELineList = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPKO = New DevExpress.XtraTab.XtraTabPage()
        Me.BReminderKO = New DevExpress.XtraEditors.SimpleButton()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.TEReminderKO = New DevExpress.XtraEditors.TimeEdit()
        Me.XTPSampleTrack = New DevExpress.XtraTab.XtraTabPage()
        Me.ContextMenuStrip.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.TECheckFailOrder.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.GCSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.XTPSchedule.SuspendLayout()
        Me.XTPAttendance.SuspendLayout()
        CType(Me.TETime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LEDay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPLeaveRemaining.SuspendLayout()
        CType(Me.TETimeMonthly.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPProduction.SuspendLayout()
        CType(Me.TETimeDuty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPEmpAppraisal.SuspendLayout()
        CType(Me.TEEmpPerApp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPCashAdvance.SuspendLayout()
        CType(Me.TECashAdvance.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPEvaulationAR.SuspendLayout()
        CType(Me.LEDayAREval.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TEEvaluationAR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPNoticeEmail.SuspendLayout()
        CType(Me.TEEmailNoticeAR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPWarningLate.SuspendLayout()
        CType(Me.TEWaningLate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPGetKurs.SuspendLayout()
        CType(Me.TETimeKurs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LEDayKurs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPFailOrder.SuspendLayout()
        CType(Me.GCSchCloseOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVSchCloseOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        Me.XTPSalesReturn.SuspendLayout()
        CType(Me.TESalesReturnOrder.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPMarketplaceOrderStt.SuspendLayout()
        CType(Me.GCMOS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVMOS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl4.SuspendLayout()
        Me.XTPQC.SuspendLayout()
        CType(Me.TEQC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPPolis.SuspendLayout()
        CType(Me.TEPolis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPPOOG.SuspendLayout()
        CType(Me.TEPOOG.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPPROG.SuspendLayout()
        CType(Me.TEPROG.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPSerahTerima.SuspendLayout()
        CType(Me.TESerahTerimaQC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPPIBReview.SuspendLayout()
        CType(Me.TEPIBNotif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPEOS.SuspendLayout()
        CType(Me.GCEOS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVEOS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl5.SuspendLayout()
        CType(Me.TEEOSNotif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPEOSPrice.SuspendLayout()
        CType(Me.GCPriceEOS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVPriceEOS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl6.SuspendLayout()
        CType(Me.TEPriceEOSNotif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPBSP.SuspendLayout()
        CType(Me.GCBSP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVBSP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl7.SuspendLayout()
        CType(Me.TEBSPNotif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPLineList.SuspendLayout()
        CType(Me.TELineList.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTPKO.SuspendLayout()
        CType(Me.TEReminderKO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer
        '
        Me.Timer.Interval = 1000
        '
        'ContextMenuStrip
        '
        Me.ContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SettingToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.ContextMenuStrip.Name = "ContextMenuStrip"
        Me.ContextMenuStrip.Size = New System.Drawing.Size(112, 48)
        '
        'SettingToolStripMenuItem
        '
        Me.SettingToolStripMenuItem.Name = "SettingToolStripMenuItem"
        Me.SettingToolStripMenuItem.Size = New System.Drawing.Size(111, 22)
        Me.SettingToolStripMenuItem.Text = "Setting"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(111, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'NotifyIcon
        '
        Me.NotifyIcon.BalloonTipText = "Still running here !"
        Me.NotifyIcon.BalloonTipTitle = "Scheduler PHP"
        Me.NotifyIcon.ContextMenuStrip = Me.ContextMenuStrip
        Me.NotifyIcon.Icon = CType(resources.GetObject("NotifyIcon.Icon"), System.Drawing.Icon)
        Me.NotifyIcon.Text = "Scheduler PHP"
        Me.NotifyIcon.Visible = True
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.Linfo)
        Me.PanelControl1.Controls.Add(Me.BCancel)
        Me.PanelControl1.Controls.Add(Me.BSave)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 189)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(878, 36)
        Me.PanelControl1.TabIndex = 3
        '
        'Linfo
        '
        Me.Linfo.Location = New System.Drawing.Point(12, 11)
        Me.Linfo.Name = "Linfo"
        Me.Linfo.Size = New System.Drawing.Size(86, 13)
        Me.Linfo.TabIndex = 5
        Me.Linfo.Text = "Schedule Stopped"
        '
        'BCancel
        '
        Me.BCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.BCancel.Location = New System.Drawing.Point(733, 2)
        Me.BCancel.Name = "BCancel"
        Me.BCancel.Size = New System.Drawing.Size(71, 32)
        Me.BCancel.TabIndex = 1
        Me.BCancel.Text = "Exit"
        '
        'BSave
        '
        Me.BSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.BSave.Location = New System.Drawing.Point(804, 2)
        Me.BSave.Name = "BSave"
        Me.BSave.Size = New System.Drawing.Size(72, 32)
        Me.BSave.TabIndex = 0
        Me.BSave.Text = "Start"
        '
        'BtnFailOrder
        '
        Me.BtnFailOrder.Location = New System.Drawing.Point(174, 3)
        Me.BtnFailOrder.Name = "BtnFailOrder"
        Me.BtnFailOrder.Size = New System.Drawing.Size(66, 23)
        Me.BtnFailOrder.TabIndex = 20
        Me.BtnFailOrder.Text = "Save"
        Me.BtnFailOrder.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(13, 8)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(49, 13)
        Me.Label12.TabIndex = 19
        Me.Label12.Text = "Check at"
        Me.Label12.Visible = False
        '
        'TECheckFailOrder
        '
        Me.TECheckFailOrder.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TECheckFailOrder.Location = New System.Drawing.Point(68, 5)
        Me.TECheckFailOrder.Name = "TECheckFailOrder"
        Me.TECheckFailOrder.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TECheckFailOrder.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TECheckFailOrder.Size = New System.Drawing.Size(100, 20)
        Me.TECheckFailOrder.TabIndex = 18
        Me.TECheckFailOrder.Visible = False
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.BDelete)
        Me.PanelControl2.Controls.Add(Me.BAdd)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl2.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(872, 30)
        Me.PanelControl2.TabIndex = 4
        '
        'BDelete
        '
        Me.BDelete.Dock = System.Windows.Forms.DockStyle.Right
        Me.BDelete.Location = New System.Drawing.Point(788, 2)
        Me.BDelete.Name = "BDelete"
        Me.BDelete.Size = New System.Drawing.Size(42, 26)
        Me.BDelete.TabIndex = 1
        Me.BDelete.Text = "-"
        '
        'BAdd
        '
        Me.BAdd.Dock = System.Windows.Forms.DockStyle.Right
        Me.BAdd.Location = New System.Drawing.Point(830, 2)
        Me.BAdd.Name = "BAdd"
        Me.BAdd.Size = New System.Drawing.Size(40, 26)
        Me.BAdd.TabIndex = 0
        Me.BAdd.Text = "+"
        '
        'GCSchedule
        '
        Me.GCSchedule.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GCSchedule.Location = New System.Drawing.Point(0, 30)
        Me.GCSchedule.MainView = Me.GVSchedule
        Me.GCSchedule.Name = "GCSchedule"
        Me.GCSchedule.Size = New System.Drawing.Size(872, 131)
        Me.GCSchedule.TabIndex = 5
        Me.GCSchedule.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVSchedule})
        '
        'GVSchedule
        '
        Me.GVSchedule.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn1, Me.GridColumn2})
        Me.GVSchedule.GridControl = Me.GCSchedule
        Me.GVSchedule.Name = "GVSchedule"
        Me.GVSchedule.OptionsBehavior.Editable = False
        Me.GVSchedule.OptionsView.ShowGroupPanel = False
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "id"
        Me.GridColumn3.FieldName = "id_scheduler_attn"
        Me.GridColumn3.Name = "GridColumn3"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Schedule"
        Me.GridColumn1.FieldName = "schedule"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Time"
        Me.GridColumn2.DisplayFormat.FormatString = "HH:mm:ss"
        Me.GridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn2.FieldName = "time_var"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XTPSchedule
        Me.XtraTabControl1.Size = New System.Drawing.Size(878, 189)
        Me.XtraTabControl1.TabIndex = 6
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XTPSchedule, Me.XTPAttendance, Me.XTPLeaveRemaining, Me.XTPProduction, Me.XTPEmpAppraisal, Me.XTPCashAdvance, Me.XTPEvaulationAR, Me.XTPNoticeEmail, Me.XTPWarningLate, Me.XTPGetKurs, Me.XTPFailOrder, Me.XTPSalesReturn, Me.XTPMarketplaceOrderStt, Me.XTPQC, Me.XTPPolis, Me.XTPPOOG, Me.XTPPROG, Me.XTPSerahTerima, Me.XTPPIBReview, Me.XTPEOS, Me.XTPEOSPrice, Me.XTPBSP, Me.XTPLineList, Me.XTPKO, Me.XTPSampleTrack})
        '
        'XTPSchedule
        '
        Me.XTPSchedule.Controls.Add(Me.GCSchedule)
        Me.XTPSchedule.Controls.Add(Me.PanelControl2)
        Me.XTPSchedule.Name = "XTPSchedule"
        Me.XTPSchedule.Size = New System.Drawing.Size(872, 161)
        Me.XTPSchedule.Text = "Attendance Fingerprint Data"
        '
        'XTPAttendance
        '
        Me.XTPAttendance.Controls.Add(Me.BSaveWAR)
        Me.XTPAttendance.Controls.Add(Me.Label2)
        Me.XTPAttendance.Controls.Add(Me.TETime)
        Me.XTPAttendance.Controls.Add(Me.LEDay)
        Me.XTPAttendance.Controls.Add(Me.Label1)
        Me.XTPAttendance.Name = "XTPAttendance"
        Me.XTPAttendance.Size = New System.Drawing.Size(872, 161)
        Me.XTPAttendance.Text = "Weekly Attendance Report"
        '
        'BSaveWAR
        '
        Me.BSaveWAR.Location = New System.Drawing.Point(190, 41)
        Me.BSaveWAR.Name = "BSaveWAR"
        Me.BSaveWAR.Size = New System.Drawing.Size(66, 23)
        Me.BSaveWAR.TabIndex = 5
        Me.BSaveWAR.Text = "Save"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "At : "
        '
        'TETime
        '
        Me.TETime.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TETime.Location = New System.Drawing.Point(84, 44)
        Me.TETime.Name = "TETime"
        Me.TETime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TETime.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TETime.Size = New System.Drawing.Size(100, 20)
        Me.TETime.TabIndex = 3
        '
        'LEDay
        '
        Me.LEDay.Location = New System.Drawing.Point(84, 11)
        Me.LEDay.Name = "LEDay"
        Me.LEDay.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LEDay.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_day", "ID Day", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default], DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("day_name", "Day")})
        Me.LEDay.Size = New System.Drawing.Size(445, 20)
        Me.LEDay.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Run Every : "
        '
        'XTPLeaveRemaining
        '
        Me.XTPLeaveRemaining.Controls.Add(Me.BSaveMonthly)
        Me.XTPLeaveRemaining.Controls.Add(Me.Label3)
        Me.XTPLeaveRemaining.Controls.Add(Me.TETimeMonthly)
        Me.XTPLeaveRemaining.Name = "XTPLeaveRemaining"
        Me.XTPLeaveRemaining.Size = New System.Drawing.Size(872, 161)
        Me.XTPLeaveRemaining.Text = "Leave Remaining"
        '
        'BSaveMonthly
        '
        Me.BSaveMonthly.Location = New System.Drawing.Point(284, 16)
        Me.BSaveMonthly.Name = "BSaveMonthly"
        Me.BSaveMonthly.Size = New System.Drawing.Size(66, 23)
        Me.BSaveMonthly.TabIndex = 10
        Me.BSaveMonthly.Text = "Save"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(154, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Beginning of every month At : "
        '
        'TETimeMonthly
        '
        Me.TETimeMonthly.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TETimeMonthly.Location = New System.Drawing.Point(178, 18)
        Me.TETimeMonthly.Name = "TETimeMonthly"
        Me.TETimeMonthly.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TETimeMonthly.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TETimeMonthly.Size = New System.Drawing.Size(100, 20)
        Me.TETimeMonthly.TabIndex = 8
        '
        'XTPProduction
        '
        Me.XTPProduction.Controls.Add(Me.BProdDuty)
        Me.XTPProduction.Controls.Add(Me.Label4)
        Me.XTPProduction.Controls.Add(Me.TETimeDuty)
        Me.XTPProduction.Name = "XTPProduction"
        Me.XTPProduction.Size = New System.Drawing.Size(872, 161)
        Me.XTPProduction.Text = "Production"
        '
        'BProdDuty
        '
        Me.BProdDuty.Location = New System.Drawing.Point(221, 13)
        Me.BProdDuty.Name = "BProdDuty"
        Me.BProdDuty.Size = New System.Drawing.Size(66, 23)
        Me.BProdDuty.TabIndex = 8
        Me.BProdDuty.Text = "Save"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(98, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Duty Reminder at :"
        '
        'TETimeDuty
        '
        Me.TETimeDuty.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TETimeDuty.Location = New System.Drawing.Point(115, 15)
        Me.TETimeDuty.Name = "TETimeDuty"
        Me.TETimeDuty.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TETimeDuty.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TETimeDuty.Size = New System.Drawing.Size(100, 20)
        Me.TETimeDuty.TabIndex = 6
        '
        'XTPEmpAppraisal
        '
        Me.XTPEmpAppraisal.Controls.Add(Me.SBEmpPerApp)
        Me.XTPEmpAppraisal.Controls.Add(Me.Label5)
        Me.XTPEmpAppraisal.Controls.Add(Me.TEEmpPerApp)
        Me.XTPEmpAppraisal.Name = "XTPEmpAppraisal"
        Me.XTPEmpAppraisal.Size = New System.Drawing.Size(872, 161)
        Me.XTPEmpAppraisal.Text = "Employee Performance Appraisal "
        '
        'SBEmpPerApp
        '
        Me.SBEmpPerApp.Location = New System.Drawing.Point(125, 41)
        Me.SBEmpPerApp.Name = "SBEmpPerApp"
        Me.SBEmpPerApp.Size = New System.Drawing.Size(66, 23)
        Me.SBEmpPerApp.TabIndex = 11
        Me.SBEmpPerApp.Text = "Save"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Reminder at:"
        '
        'TEEmpPerApp
        '
        Me.TEEmpPerApp.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TEEmpPerApp.Location = New System.Drawing.Point(19, 43)
        Me.TEEmpPerApp.Name = "TEEmpPerApp"
        Me.TEEmpPerApp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TEEmpPerApp.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TEEmpPerApp.Size = New System.Drawing.Size(100, 20)
        Me.TEEmpPerApp.TabIndex = 9
        '
        'XTPCashAdvance
        '
        Me.XTPCashAdvance.Controls.Add(Me.SBCashAdvance)
        Me.XTPCashAdvance.Controls.Add(Me.Label6)
        Me.XTPCashAdvance.Controls.Add(Me.TECashAdvance)
        Me.XTPCashAdvance.Name = "XTPCashAdvance"
        Me.XTPCashAdvance.Size = New System.Drawing.Size(872, 161)
        Me.XTPCashAdvance.Text = "Cash Advance"
        '
        'SBCashAdvance
        '
        Me.SBCashAdvance.Location = New System.Drawing.Point(222, 10)
        Me.SBCashAdvance.Name = "SBCashAdvance"
        Me.SBCashAdvance.Size = New System.Drawing.Size(66, 23)
        Me.SBCashAdvance.TabIndex = 14
        Me.SBCashAdvance.Text = "Save"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(98, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Daily Reminder at :"
        '
        'TECashAdvance
        '
        Me.TECashAdvance.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TECashAdvance.Location = New System.Drawing.Point(116, 12)
        Me.TECashAdvance.Name = "TECashAdvance"
        Me.TECashAdvance.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TECashAdvance.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TECashAdvance.Size = New System.Drawing.Size(100, 20)
        Me.TECashAdvance.TabIndex = 12
        '
        'XTPEvaulationAR
        '
        Me.XTPEvaulationAR.Controls.Add(Me.LEDayAREval)
        Me.XTPEvaulationAR.Controls.Add(Me.Label18)
        Me.XTPEvaulationAR.Controls.Add(Me.BtnEvaluationAR)
        Me.XTPEvaulationAR.Controls.Add(Me.Label7)
        Me.XTPEvaulationAR.Controls.Add(Me.TEEvaluationAR)
        Me.XTPEvaulationAR.Name = "XTPEvaulationAR"
        Me.XTPEvaulationAR.Size = New System.Drawing.Size(872, 161)
        Me.XTPEvaulationAR.Text = "AR - Evaluation"
        '
        'LEDayAREval
        '
        Me.LEDayAREval.Location = New System.Drawing.Point(90, 20)
        Me.LEDayAREval.Name = "LEDayAREval"
        Me.LEDayAREval.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LEDayAREval.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_day", "ID Day", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default], DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("day_name", "Day")})
        Me.LEDayAREval.Size = New System.Drawing.Size(445, 20)
        Me.LEDayAREval.TabIndex = 19
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(14, 22)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(67, 13)
        Me.Label18.TabIndex = 18
        Me.Label18.Text = "Run Every : "
        '
        'BtnEvaluationAR
        '
        Me.BtnEvaluationAR.Location = New System.Drawing.Point(196, 47)
        Me.BtnEvaluationAR.Name = "BtnEvaluationAR"
        Me.BtnEvaluationAR.Size = New System.Drawing.Size(66, 23)
        Me.BtnEvaluationAR.TabIndex = 17
        Me.BtnEvaluationAR.Text = "Save"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(14, 52)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Evaluation at"
        '
        'TEEvaluationAR
        '
        Me.TEEvaluationAR.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TEEvaluationAR.Location = New System.Drawing.Point(90, 49)
        Me.TEEvaluationAR.Name = "TEEvaluationAR"
        Me.TEEvaluationAR.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TEEvaluationAR.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TEEvaluationAR.Size = New System.Drawing.Size(100, 20)
        Me.TEEvaluationAR.TabIndex = 15
        '
        'XTPNoticeEmail
        '
        Me.XTPNoticeEmail.Controls.Add(Me.BtnEmailNoticeAR)
        Me.XTPNoticeEmail.Controls.Add(Me.Label8)
        Me.XTPNoticeEmail.Controls.Add(Me.TEEmailNoticeAR)
        Me.XTPNoticeEmail.Name = "XTPNoticeEmail"
        Me.XTPNoticeEmail.Size = New System.Drawing.Size(872, 161)
        Me.XTPNoticeEmail.Text = "AR - Email Pemberitahuan"
        '
        'BtnEmailNoticeAR
        '
        Me.BtnEmailNoticeAR.Location = New System.Drawing.Point(173, 14)
        Me.BtnEmailNoticeAR.Name = "BtnEmailNoticeAR"
        Me.BtnEmailNoticeAR.Size = New System.Drawing.Size(66, 23)
        Me.BtnEmailNoticeAR.TabIndex = 20
        Me.BtnEmailNoticeAR.Text = "Save"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(17, 19)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 13)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Email at"
        '
        'TEEmailNoticeAR
        '
        Me.TEEmailNoticeAR.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TEEmailNoticeAR.Location = New System.Drawing.Point(67, 16)
        Me.TEEmailNoticeAR.Name = "TEEmailNoticeAR"
        Me.TEEmailNoticeAR.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TEEmailNoticeAR.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TEEmailNoticeAR.Size = New System.Drawing.Size(100, 20)
        Me.TEEmailNoticeAR.TabIndex = 18
        '
        'XTPWarningLate
        '
        Me.XTPWarningLate.Controls.Add(Me.SBWarningLate)
        Me.XTPWarningLate.Controls.Add(Me.Label9)
        Me.XTPWarningLate.Controls.Add(Me.TEWaningLate)
        Me.XTPWarningLate.Name = "XTPWarningLate"
        Me.XTPWarningLate.Size = New System.Drawing.Size(872, 161)
        Me.XTPWarningLate.Text = "Warning Late"
        '
        'SBWarningLate
        '
        Me.SBWarningLate.Location = New System.Drawing.Point(221, 10)
        Me.SBWarningLate.Name = "SBWarningLate"
        Me.SBWarningLate.Size = New System.Drawing.Size(66, 23)
        Me.SBWarningLate.TabIndex = 14
        Me.SBWarningLate.Text = "Save"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(11, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(98, 13)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "Daily Reminder at :"
        '
        'TEWaningLate
        '
        Me.TEWaningLate.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TEWaningLate.Location = New System.Drawing.Point(115, 12)
        Me.TEWaningLate.Name = "TEWaningLate"
        Me.TEWaningLate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TEWaningLate.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TEWaningLate.Size = New System.Drawing.Size(100, 20)
        Me.TEWaningLate.TabIndex = 12
        '
        'XTPGetKurs
        '
        Me.XTPGetKurs.Controls.Add(Me.BSaveKurs)
        Me.XTPGetKurs.Controls.Add(Me.Label10)
        Me.XTPGetKurs.Controls.Add(Me.TETimeKurs)
        Me.XTPGetKurs.Controls.Add(Me.LEDayKurs)
        Me.XTPGetKurs.Controls.Add(Me.Label11)
        Me.XTPGetKurs.Name = "XTPGetKurs"
        Me.XTPGetKurs.Size = New System.Drawing.Size(872, 161)
        Me.XTPGetKurs.Text = "Kurs"
        '
        'BSaveKurs
        '
        Me.BSaveKurs.Location = New System.Drawing.Point(190, 41)
        Me.BSaveKurs.Name = "BSaveKurs"
        Me.BSaveKurs.Size = New System.Drawing.Size(66, 23)
        Me.BSaveKurs.TabIndex = 10
        Me.BSaveKurs.Text = "Save"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(11, 47)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(28, 13)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "At : "
        '
        'TETimeKurs
        '
        Me.TETimeKurs.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TETimeKurs.Location = New System.Drawing.Point(84, 44)
        Me.TETimeKurs.Name = "TETimeKurs"
        Me.TETimeKurs.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TETimeKurs.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TETimeKurs.Size = New System.Drawing.Size(100, 20)
        Me.TETimeKurs.TabIndex = 8
        '
        'LEDayKurs
        '
        Me.LEDayKurs.Location = New System.Drawing.Point(84, 11)
        Me.LEDayKurs.Name = "LEDayKurs"
        Me.LEDayKurs.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LEDayKurs.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_day", "ID Day", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default], DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("day_name", "Day")})
        Me.LEDayKurs.Size = New System.Drawing.Size(445, 20)
        Me.LEDayKurs.TabIndex = 7
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(11, 14)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(67, 13)
        Me.Label11.TabIndex = 6
        Me.Label11.Text = "Run Every : "
        '
        'XTPFailOrder
        '
        Me.XTPFailOrder.Controls.Add(Me.GCSchCloseOrder)
        Me.XTPFailOrder.Controls.Add(Me.PanelControl3)
        Me.XTPFailOrder.Name = "XTPFailOrder"
        Me.XTPFailOrder.Size = New System.Drawing.Size(872, 161)
        Me.XTPFailOrder.Text = "VIOS - Close Failed Order"
        '
        'GCSchCloseOrder
        '
        Me.GCSchCloseOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GCSchCloseOrder.Location = New System.Drawing.Point(0, 30)
        Me.GCSchCloseOrder.MainView = Me.GVSchCloseOrder
        Me.GCSchCloseOrder.Name = "GCSchCloseOrder"
        Me.GCSchCloseOrder.Size = New System.Drawing.Size(872, 131)
        Me.GCSchCloseOrder.TabIndex = 6
        Me.GCSchCloseOrder.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVSchCloseOrder})
        '
        'GVSchCloseOrder
        '
        Me.GVSchCloseOrder.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.GridColumn5, Me.GridColumn6})
        Me.GVSchCloseOrder.GridControl = Me.GCSchCloseOrder
        Me.GVSchCloseOrder.Name = "GVSchCloseOrder"
        Me.GVSchCloseOrder.OptionsBehavior.Editable = False
        Me.GVSchCloseOrder.OptionsView.ShowGroupPanel = False
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "id"
        Me.GridColumn4.FieldName = "id_scheduler_attn"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Schedule"
        Me.GridColumn5.FieldName = "schedule_desc"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Time"
        Me.GridColumn6.DisplayFormat.FormatString = "HH:mm:ss"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn6.FieldName = "schedule"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 1
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.BtnRefresh)
        Me.PanelControl3.Controls.Add(Me.BtnFailOrder)
        Me.PanelControl3.Controls.Add(Me.Label12)
        Me.PanelControl3.Controls.Add(Me.TECheckFailOrder)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl3.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(872, 30)
        Me.PanelControl3.TabIndex = 5
        '
        'BtnRefresh
        '
        Me.BtnRefresh.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnRefresh.Location = New System.Drawing.Point(795, 2)
        Me.BtnRefresh.Name = "BtnRefresh"
        Me.BtnRefresh.Size = New System.Drawing.Size(75, 26)
        Me.BtnRefresh.TabIndex = 21
        Me.BtnRefresh.Text = "Refresh"
        '
        'XTPSalesReturn
        '
        Me.XTPSalesReturn.Controls.Add(Me.SBSalesReturnOrder)
        Me.XTPSalesReturn.Controls.Add(Me.Label13)
        Me.XTPSalesReturn.Controls.Add(Me.TESalesReturnOrder)
        Me.XTPSalesReturn.Name = "XTPSalesReturn"
        Me.XTPSalesReturn.Size = New System.Drawing.Size(872, 161)
        Me.XTPSalesReturn.Text = "Sales Return"
        '
        'SBSalesReturnOrder
        '
        Me.SBSalesReturnOrder.Location = New System.Drawing.Point(226, 12)
        Me.SBSalesReturnOrder.Name = "SBSalesReturnOrder"
        Me.SBSalesReturnOrder.Size = New System.Drawing.Size(66, 23)
        Me.SBSalesReturnOrder.TabIndex = 13
        Me.SBSalesReturnOrder.Text = "Save"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(16, 17)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(98, 13)
        Me.Label13.TabIndex = 12
        Me.Label13.Text = "Daily Reminder at :"
        '
        'TESalesReturnOrder
        '
        Me.TESalesReturnOrder.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TESalesReturnOrder.Location = New System.Drawing.Point(120, 14)
        Me.TESalesReturnOrder.Name = "TESalesReturnOrder"
        Me.TESalesReturnOrder.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TESalesReturnOrder.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TESalesReturnOrder.Size = New System.Drawing.Size(100, 20)
        Me.TESalesReturnOrder.TabIndex = 11
        '
        'XTPMarketplaceOrderStt
        '
        Me.XTPMarketplaceOrderStt.Controls.Add(Me.GCMOS)
        Me.XTPMarketplaceOrderStt.Controls.Add(Me.PanelControl4)
        Me.XTPMarketplaceOrderStt.Name = "XTPMarketplaceOrderStt"
        Me.XTPMarketplaceOrderStt.Size = New System.Drawing.Size(872, 161)
        Me.XTPMarketplaceOrderStt.Text = "Marketplace Order Status"
        '
        'GCMOS
        '
        Me.GCMOS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GCMOS.Location = New System.Drawing.Point(0, 30)
        Me.GCMOS.MainView = Me.GVMOS
        Me.GCMOS.Name = "GCMOS"
        Me.GCMOS.Size = New System.Drawing.Size(872, 131)
        Me.GCMOS.TabIndex = 8
        Me.GCMOS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVMOS})
        '
        'GVMOS
        '
        Me.GVMOS.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7, Me.GridColumn8, Me.GridColumn9})
        Me.GVMOS.GridControl = Me.GCMOS
        Me.GVMOS.Name = "GVMOS"
        Me.GVMOS.OptionsBehavior.Editable = False
        Me.GVMOS.OptionsView.ShowGroupPanel = False
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "id"
        Me.GridColumn7.FieldName = "id_scheduler_attn"
        Me.GridColumn7.Name = "GridColumn7"
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Schedule"
        Me.GridColumn8.FieldName = "schedule_desc"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 0
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Time"
        Me.GridColumn9.DisplayFormat.FormatString = "HH:mm:ss"
        Me.GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn9.FieldName = "schedule"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 1
        '
        'PanelControl4
        '
        Me.PanelControl4.Controls.Add(Me.BtnRefMOS)
        Me.PanelControl4.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl4.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl4.Name = "PanelControl4"
        Me.PanelControl4.Size = New System.Drawing.Size(872, 30)
        Me.PanelControl4.TabIndex = 7
        '
        'BtnRefMOS
        '
        Me.BtnRefMOS.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnRefMOS.Location = New System.Drawing.Point(795, 2)
        Me.BtnRefMOS.Name = "BtnRefMOS"
        Me.BtnRefMOS.Size = New System.Drawing.Size(75, 26)
        Me.BtnRefMOS.TabIndex = 21
        Me.BtnRefMOS.Text = "Refresh"
        '
        'XTPQC
        '
        Me.XTPQC.Controls.Add(Me.BQC)
        Me.XTPQC.Controls.Add(Me.Label14)
        Me.XTPQC.Controls.Add(Me.TEQC)
        Me.XTPQC.Name = "XTPQC"
        Me.XTPQC.Size = New System.Drawing.Size(872, 161)
        Me.XTPQC.Text = "QC"
        '
        'BQC
        '
        Me.BQC.Location = New System.Drawing.Point(221, 13)
        Me.BQC.Name = "BQC"
        Me.BQC.Size = New System.Drawing.Size(66, 23)
        Me.BQC.TabIndex = 16
        Me.BQC.Text = "Save"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(11, 18)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(98, 13)
        Me.Label14.TabIndex = 15
        Me.Label14.Text = "Daily Reminder at :"
        '
        'TEQC
        '
        Me.TEQC.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TEQC.Location = New System.Drawing.Point(115, 15)
        Me.TEQC.Name = "TEQC"
        Me.TEQC.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TEQC.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TEQC.Size = New System.Drawing.Size(100, 20)
        Me.TEQC.TabIndex = 14
        '
        'XTPPolis
        '
        Me.XTPPolis.Controls.Add(Me.BPolis)
        Me.XTPPolis.Controls.Add(Me.Label15)
        Me.XTPPolis.Controls.Add(Me.TEPolis)
        Me.XTPPolis.Name = "XTPPolis"
        Me.XTPPolis.Size = New System.Drawing.Size(872, 161)
        Me.XTPPolis.Text = "Polis"
        '
        'BPolis
        '
        Me.BPolis.Location = New System.Drawing.Point(221, 13)
        Me.BPolis.Name = "BPolis"
        Me.BPolis.Size = New System.Drawing.Size(66, 23)
        Me.BPolis.TabIndex = 16
        Me.BPolis.Text = "Save"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(11, 18)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(98, 13)
        Me.Label15.TabIndex = 15
        Me.Label15.Text = "Daily Reminder at :"
        '
        'TEPolis
        '
        Me.TEPolis.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TEPolis.Location = New System.Drawing.Point(115, 15)
        Me.TEPolis.Name = "TEPolis"
        Me.TEPolis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TEPolis.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TEPolis.Size = New System.Drawing.Size(100, 20)
        Me.TEPolis.TabIndex = 14
        '
        'XTPPOOG
        '
        Me.XTPPOOG.Controls.Add(Me.BPOOG)
        Me.XTPPOOG.Controls.Add(Me.Label16)
        Me.XTPPOOG.Controls.Add(Me.TEPOOG)
        Me.XTPPOOG.Name = "XTPPOOG"
        Me.XTPPOOG.Size = New System.Drawing.Size(872, 161)
        Me.XTPPOOG.Text = "PO OG Reminder"
        '
        'BPOOG
        '
        Me.BPOOG.Location = New System.Drawing.Point(224, 16)
        Me.BPOOG.Name = "BPOOG"
        Me.BPOOG.Size = New System.Drawing.Size(66, 23)
        Me.BPOOG.TabIndex = 19
        Me.BPOOG.Text = "Save"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(14, 21)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(98, 13)
        Me.Label16.TabIndex = 18
        Me.Label16.Text = "Daily Reminder at :"
        '
        'TEPOOG
        '
        Me.TEPOOG.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TEPOOG.Location = New System.Drawing.Point(118, 18)
        Me.TEPOOG.Name = "TEPOOG"
        Me.TEPOOG.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TEPOOG.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TEPOOG.Size = New System.Drawing.Size(100, 20)
        Me.TEPOOG.TabIndex = 17
        '
        'XTPPROG
        '
        Me.XTPPROG.Controls.Add(Me.BPROG)
        Me.XTPPROG.Controls.Add(Me.Label17)
        Me.XTPPROG.Controls.Add(Me.TEPROG)
        Me.XTPPROG.Name = "XTPPROG"
        Me.XTPPROG.Size = New System.Drawing.Size(872, 161)
        Me.XTPPROG.Text = "PR OG Reminder"
        '
        'BPROG
        '
        Me.BPROG.Location = New System.Drawing.Point(221, 18)
        Me.BPROG.Name = "BPROG"
        Me.BPROG.Size = New System.Drawing.Size(66, 23)
        Me.BPROG.TabIndex = 22
        Me.BPROG.Text = "Save"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(11, 23)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(98, 13)
        Me.Label17.TabIndex = 21
        Me.Label17.Text = "Daily Reminder at :"
        '
        'TEPROG
        '
        Me.TEPROG.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TEPROG.Location = New System.Drawing.Point(115, 20)
        Me.TEPROG.Name = "TEPROG"
        Me.TEPROG.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TEPROG.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TEPROG.Size = New System.Drawing.Size(100, 20)
        Me.TEPROG.TabIndex = 20
        '
        'XTPSerahTerima
        '
        Me.XTPSerahTerima.Controls.Add(Me.BSerahTerimaQC)
        Me.XTPSerahTerima.Controls.Add(Me.Label19)
        Me.XTPSerahTerima.Controls.Add(Me.TESerahTerimaQC)
        Me.XTPSerahTerima.Name = "XTPSerahTerima"
        Me.XTPSerahTerima.Size = New System.Drawing.Size(872, 161)
        Me.XTPSerahTerima.Text = "Serah Terima QC"
        '
        'BSerahTerimaQC
        '
        Me.BSerahTerimaQC.Location = New System.Drawing.Point(226, 11)
        Me.BSerahTerimaQC.Name = "BSerahTerimaQC"
        Me.BSerahTerimaQC.Size = New System.Drawing.Size(66, 23)
        Me.BSerahTerimaQC.TabIndex = 25
        Me.BSerahTerimaQC.Text = "Save"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(16, 16)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(98, 13)
        Me.Label19.TabIndex = 24
        Me.Label19.Text = "Daily Reminder at :"
        '
        'TESerahTerimaQC
        '
        Me.TESerahTerimaQC.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TESerahTerimaQC.Location = New System.Drawing.Point(120, 13)
        Me.TESerahTerimaQC.Name = "TESerahTerimaQC"
        Me.TESerahTerimaQC.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TESerahTerimaQC.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TESerahTerimaQC.Size = New System.Drawing.Size(100, 20)
        Me.TESerahTerimaQC.TabIndex = 23
        '
        'XTPPIBReview
        '
        Me.XTPPIBReview.Controls.Add(Me.BPIBNotif)
        Me.XTPPIBReview.Controls.Add(Me.Label20)
        Me.XTPPIBReview.Controls.Add(Me.TEPIBNotif)
        Me.XTPPIBReview.Name = "XTPPIBReview"
        Me.XTPPIBReview.Size = New System.Drawing.Size(872, 161)
        Me.XTPPIBReview.Text = "PIB Notification"
        '
        'BPIBNotif
        '
        Me.BPIBNotif.Location = New System.Drawing.Point(221, 16)
        Me.BPIBNotif.Name = "BPIBNotif"
        Me.BPIBNotif.Size = New System.Drawing.Size(66, 23)
        Me.BPIBNotif.TabIndex = 25
        Me.BPIBNotif.Text = "Save"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(11, 21)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(98, 13)
        Me.Label20.TabIndex = 24
        Me.Label20.Text = "Daily Reminder at :"
        '
        'TEPIBNotif
        '
        Me.TEPIBNotif.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TEPIBNotif.Location = New System.Drawing.Point(115, 18)
        Me.TEPIBNotif.Name = "TEPIBNotif"
        Me.TEPIBNotif.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TEPIBNotif.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TEPIBNotif.Size = New System.Drawing.Size(100, 20)
        Me.TEPIBNotif.TabIndex = 23
        '
        'XTPEOS
        '
        Me.XTPEOS.Controls.Add(Me.GCEOS)
        Me.XTPEOS.Controls.Add(Me.PanelControl5)
        Me.XTPEOS.Name = "XTPEOS"
        Me.XTPEOS.Size = New System.Drawing.Size(872, 161)
        Me.XTPEOS.Text = "EOS Reminder"
        '
        'GCEOS
        '
        Me.GCEOS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GCEOS.Location = New System.Drawing.Point(0, 41)
        Me.GCEOS.MainView = Me.GVEOS
        Me.GCEOS.Name = "GCEOS"
        Me.GCEOS.Size = New System.Drawing.Size(872, 120)
        Me.GCEOS.TabIndex = 1
        Me.GCEOS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVEOS})
        '
        'GVEOS
        '
        Me.GVEOS.GridControl = Me.GCEOS
        Me.GVEOS.Name = "GVEOS"
        Me.GVEOS.OptionsView.ShowGroupPanel = False
        '
        'PanelControl5
        '
        Me.PanelControl5.Controls.Add(Me.BtnRefreshEOS)
        Me.PanelControl5.Controls.Add(Me.BtnEOS)
        Me.PanelControl5.Controls.Add(Me.Label21)
        Me.PanelControl5.Controls.Add(Me.TEEOSNotif)
        Me.PanelControl5.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl5.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl5.Name = "PanelControl5"
        Me.PanelControl5.Size = New System.Drawing.Size(872, 41)
        Me.PanelControl5.TabIndex = 0
        '
        'BtnRefreshEOS
        '
        Me.BtnRefreshEOS.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnRefreshEOS.Location = New System.Drawing.Point(795, 2)
        Me.BtnRefreshEOS.Name = "BtnRefreshEOS"
        Me.BtnRefreshEOS.Size = New System.Drawing.Size(75, 37)
        Me.BtnRefreshEOS.TabIndex = 1
        Me.BtnRefreshEOS.Text = "Refresh"
        '
        'BtnEOS
        '
        Me.BtnEOS.Location = New System.Drawing.Point(223, 9)
        Me.BtnEOS.Name = "BtnEOS"
        Me.BtnEOS.Size = New System.Drawing.Size(66, 23)
        Me.BtnEOS.TabIndex = 28
        Me.BtnEOS.Text = "Save"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(13, 14)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(98, 13)
        Me.Label21.TabIndex = 27
        Me.Label21.Text = "Daily Reminder at :"
        '
        'TEEOSNotif
        '
        Me.TEEOSNotif.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TEEOSNotif.Location = New System.Drawing.Point(117, 11)
        Me.TEEOSNotif.Name = "TEEOSNotif"
        Me.TEEOSNotif.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TEEOSNotif.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TEEOSNotif.Size = New System.Drawing.Size(100, 20)
        Me.TEEOSNotif.TabIndex = 26
        '
        'XTPEOSPrice
        '
        Me.XTPEOSPrice.Controls.Add(Me.GCPriceEOS)
        Me.XTPEOSPrice.Controls.Add(Me.PanelControl6)
        Me.XTPEOSPrice.Name = "XTPEOSPrice"
        Me.XTPEOSPrice.Size = New System.Drawing.Size(872, 161)
        Me.XTPEOSPrice.Text = "EOS Price"
        '
        'GCPriceEOS
        '
        Me.GCPriceEOS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GCPriceEOS.Location = New System.Drawing.Point(0, 41)
        Me.GCPriceEOS.MainView = Me.GVPriceEOS
        Me.GCPriceEOS.Name = "GCPriceEOS"
        Me.GCPriceEOS.Size = New System.Drawing.Size(872, 120)
        Me.GCPriceEOS.TabIndex = 3
        Me.GCPriceEOS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVPriceEOS})
        '
        'GVPriceEOS
        '
        Me.GVPriceEOS.GridControl = Me.GCPriceEOS
        Me.GVPriceEOS.Name = "GVPriceEOS"
        Me.GVPriceEOS.OptionsView.ShowGroupPanel = False
        '
        'PanelControl6
        '
        Me.PanelControl6.Controls.Add(Me.BtnRefreshPriceEOS)
        Me.PanelControl6.Controls.Add(Me.BtnPriceEOS)
        Me.PanelControl6.Controls.Add(Me.Label22)
        Me.PanelControl6.Controls.Add(Me.TEPriceEOSNotif)
        Me.PanelControl6.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl6.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl6.Name = "PanelControl6"
        Me.PanelControl6.Size = New System.Drawing.Size(872, 41)
        Me.PanelControl6.TabIndex = 2
        '
        'BtnRefreshPriceEOS
        '
        Me.BtnRefreshPriceEOS.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnRefreshPriceEOS.Location = New System.Drawing.Point(795, 2)
        Me.BtnRefreshPriceEOS.Name = "BtnRefreshPriceEOS"
        Me.BtnRefreshPriceEOS.Size = New System.Drawing.Size(75, 37)
        Me.BtnRefreshPriceEOS.TabIndex = 1
        Me.BtnRefreshPriceEOS.Text = "Refresh"
        '
        'BtnPriceEOS
        '
        Me.BtnPriceEOS.Location = New System.Drawing.Point(223, 9)
        Me.BtnPriceEOS.Name = "BtnPriceEOS"
        Me.BtnPriceEOS.Size = New System.Drawing.Size(66, 23)
        Me.BtnPriceEOS.TabIndex = 28
        Me.BtnPriceEOS.Text = "Save"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(13, 14)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(98, 13)
        Me.Label22.TabIndex = 27
        Me.Label22.Text = "Daily Reminder at :"
        '
        'TEPriceEOSNotif
        '
        Me.TEPriceEOSNotif.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TEPriceEOSNotif.Location = New System.Drawing.Point(117, 11)
        Me.TEPriceEOSNotif.Name = "TEPriceEOSNotif"
        Me.TEPriceEOSNotif.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TEPriceEOSNotif.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TEPriceEOSNotif.Size = New System.Drawing.Size(100, 20)
        Me.TEPriceEOSNotif.TabIndex = 26
        '
        'XTPBSP
        '
        Me.XTPBSP.Controls.Add(Me.GCBSP)
        Me.XTPBSP.Controls.Add(Me.PanelControl7)
        Me.XTPBSP.Name = "XTPBSP"
        Me.XTPBSP.Size = New System.Drawing.Size(872, 161)
        Me.XTPBSP.Text = "Big Sale Price"
        '
        'GCBSP
        '
        Me.GCBSP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GCBSP.Location = New System.Drawing.Point(0, 41)
        Me.GCBSP.MainView = Me.GVBSP
        Me.GCBSP.Name = "GCBSP"
        Me.GCBSP.Size = New System.Drawing.Size(872, 120)
        Me.GCBSP.TabIndex = 4
        Me.GCBSP.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVBSP})
        '
        'GVBSP
        '
        Me.GVBSP.GridControl = Me.GCBSP
        Me.GVBSP.Name = "GVBSP"
        Me.GVBSP.OptionsBehavior.Editable = False
        Me.GVBSP.OptionsView.ShowGroupPanel = False
        '
        'PanelControl7
        '
        Me.PanelControl7.Controls.Add(Me.BtnRefreshBSP)
        Me.PanelControl7.Controls.Add(Me.BtnBSP)
        Me.PanelControl7.Controls.Add(Me.Label23)
        Me.PanelControl7.Controls.Add(Me.TEBSPNotif)
        Me.PanelControl7.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl7.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl7.Name = "PanelControl7"
        Me.PanelControl7.Size = New System.Drawing.Size(872, 41)
        Me.PanelControl7.TabIndex = 3
        '
        'BtnRefreshBSP
        '
        Me.BtnRefreshBSP.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnRefreshBSP.Location = New System.Drawing.Point(795, 2)
        Me.BtnRefreshBSP.Name = "BtnRefreshBSP"
        Me.BtnRefreshBSP.Size = New System.Drawing.Size(75, 37)
        Me.BtnRefreshBSP.TabIndex = 1
        Me.BtnRefreshBSP.Text = "Refresh"
        '
        'BtnBSP
        '
        Me.BtnBSP.Location = New System.Drawing.Point(223, 9)
        Me.BtnBSP.Name = "BtnBSP"
        Me.BtnBSP.Size = New System.Drawing.Size(66, 23)
        Me.BtnBSP.TabIndex = 28
        Me.BtnBSP.Text = "Save"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(13, 14)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(98, 13)
        Me.Label23.TabIndex = 27
        Me.Label23.Text = "Daily Reminder at :"
        '
        'TEBSPNotif
        '
        Me.TEBSPNotif.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TEBSPNotif.Location = New System.Drawing.Point(117, 11)
        Me.TEBSPNotif.Name = "TEBSPNotif"
        Me.TEBSPNotif.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TEBSPNotif.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TEBSPNotif.Size = New System.Drawing.Size(100, 20)
        Me.TEBSPNotif.TabIndex = 26
        '
        'XTPLineList
        '
        Me.XTPLineList.Controls.Add(Me.BtnResendEmail)
        Me.XTPLineList.Controls.Add(Me.BtnLineList)
        Me.XTPLineList.Controls.Add(Me.Label24)
        Me.XTPLineList.Controls.Add(Me.TELineList)
        Me.XTPLineList.Name = "XTPLineList"
        Me.XTPLineList.Size = New System.Drawing.Size(872, 161)
        Me.XTPLineList.Text = "Line List"
        '
        'BtnResendEmail
        '
        Me.BtnResendEmail.Location = New System.Drawing.Point(291, 14)
        Me.BtnResendEmail.LookAndFeel.SkinMaskColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnResendEmail.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.BtnResendEmail.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BtnResendEmail.Name = "BtnResendEmail"
        Me.BtnResendEmail.Size = New System.Drawing.Size(99, 23)
        Me.BtnResendEmail.TabIndex = 29
        Me.BtnResendEmail.Text = "Resend Email"
        '
        'BtnLineList
        '
        Me.BtnLineList.Location = New System.Drawing.Point(221, 14)
        Me.BtnLineList.Name = "BtnLineList"
        Me.BtnLineList.Size = New System.Drawing.Size(66, 23)
        Me.BtnLineList.TabIndex = 28
        Me.BtnLineList.Text = "Save"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(11, 19)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(98, 13)
        Me.Label24.TabIndex = 27
        Me.Label24.Text = "Daily Reminder at :"
        '
        'TELineList
        '
        Me.TELineList.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TELineList.Location = New System.Drawing.Point(115, 16)
        Me.TELineList.Name = "TELineList"
        Me.TELineList.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TELineList.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TELineList.Size = New System.Drawing.Size(100, 20)
        Me.TELineList.TabIndex = 26
        '
        'XTPKO
        '
        Me.XTPKO.Controls.Add(Me.BReminderKO)
        Me.XTPKO.Controls.Add(Me.Label25)
        Me.XTPKO.Controls.Add(Me.TEReminderKO)
        Me.XTPKO.Name = "XTPKO"
        Me.XTPKO.Size = New System.Drawing.Size(872, 161)
        Me.XTPKO.Text = "Reminder KO"
        '
        'BReminderKO
        '
        Me.BReminderKO.Location = New System.Drawing.Point(221, 11)
        Me.BReminderKO.Name = "BReminderKO"
        Me.BReminderKO.Size = New System.Drawing.Size(66, 23)
        Me.BReminderKO.TabIndex = 31
        Me.BReminderKO.Text = "Save"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(11, 16)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(98, 13)
        Me.Label25.TabIndex = 30
        Me.Label25.Text = "Daily Reminder at :"
        '
        'TEReminderKO
        '
        Me.TEReminderKO.EditValue = New Date(2016, 9, 26, 0, 0, 0, 0)
        Me.TEReminderKO.Location = New System.Drawing.Point(115, 13)
        Me.TEReminderKO.Name = "TEReminderKO"
        Me.TEReminderKO.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TEReminderKO.Properties.Mask.EditMask = "HH:mm:ss"
        Me.TEReminderKO.Size = New System.Drawing.Size(100, 20)
        Me.TEReminderKO.TabIndex = 29
        '
        'XTPSampleTrack
        '
        Me.XTPSampleTrack.Name = "XTPSampleTrack"
        Me.XTPSampleTrack.Size = New System.Drawing.Size(872, 161)
        Me.XTPSampleTrack.Text = "Sample Development Tracking"
        '
        'FormScheduler
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(878, 225)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormScheduler"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Scheduler"
        Me.ContextMenuStrip.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.TECheckFailOrder.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.GCSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.XTPSchedule.ResumeLayout(False)
        Me.XTPAttendance.ResumeLayout(False)
        Me.XTPAttendance.PerformLayout()
        CType(Me.TETime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LEDay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPLeaveRemaining.ResumeLayout(False)
        Me.XTPLeaveRemaining.PerformLayout()
        CType(Me.TETimeMonthly.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPProduction.ResumeLayout(False)
        Me.XTPProduction.PerformLayout()
        CType(Me.TETimeDuty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPEmpAppraisal.ResumeLayout(False)
        Me.XTPEmpAppraisal.PerformLayout()
        CType(Me.TEEmpPerApp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPCashAdvance.ResumeLayout(False)
        Me.XTPCashAdvance.PerformLayout()
        CType(Me.TECashAdvance.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPEvaulationAR.ResumeLayout(False)
        Me.XTPEvaulationAR.PerformLayout()
        CType(Me.LEDayAREval.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TEEvaluationAR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPNoticeEmail.ResumeLayout(False)
        Me.XTPNoticeEmail.PerformLayout()
        CType(Me.TEEmailNoticeAR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPWarningLate.ResumeLayout(False)
        Me.XTPWarningLate.PerformLayout()
        CType(Me.TEWaningLate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPGetKurs.ResumeLayout(False)
        Me.XTPGetKurs.PerformLayout()
        CType(Me.TETimeKurs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LEDayKurs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPFailOrder.ResumeLayout(False)
        CType(Me.GCSchCloseOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVSchCloseOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        Me.PanelControl3.PerformLayout()
        Me.XTPSalesReturn.ResumeLayout(False)
        Me.XTPSalesReturn.PerformLayout()
        CType(Me.TESalesReturnOrder.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPMarketplaceOrderStt.ResumeLayout(False)
        CType(Me.GCMOS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVMOS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl4.ResumeLayout(False)
        Me.XTPQC.ResumeLayout(False)
        Me.XTPQC.PerformLayout()
        CType(Me.TEQC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPPolis.ResumeLayout(False)
        Me.XTPPolis.PerformLayout()
        CType(Me.TEPolis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPPOOG.ResumeLayout(False)
        Me.XTPPOOG.PerformLayout()
        CType(Me.TEPOOG.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPPROG.ResumeLayout(False)
        Me.XTPPROG.PerformLayout()
        CType(Me.TEPROG.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPSerahTerima.ResumeLayout(False)
        Me.XTPSerahTerima.PerformLayout()
        CType(Me.TESerahTerimaQC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPPIBReview.ResumeLayout(False)
        Me.XTPPIBReview.PerformLayout()
        CType(Me.TEPIBNotif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPEOS.ResumeLayout(False)
        CType(Me.GCEOS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVEOS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl5.ResumeLayout(False)
        Me.PanelControl5.PerformLayout()
        CType(Me.TEEOSNotif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPEOSPrice.ResumeLayout(False)
        CType(Me.GCPriceEOS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVPriceEOS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl6.ResumeLayout(False)
        Me.PanelControl6.PerformLayout()
        CType(Me.TEPriceEOSNotif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPBSP.ResumeLayout(False)
        CType(Me.GCBSP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVBSP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl7.ResumeLayout(False)
        Me.PanelControl7.PerformLayout()
        CType(Me.TEBSPNotif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPLineList.ResumeLayout(False)
        Me.XTPLineList.PerformLayout()
        CType(Me.TELineList.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTPKO.ResumeLayout(False)
        Me.XTPKO.PerformLayout()
        CType(Me.TEReminderKO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Timer As Timer
    Friend WithEvents ContextMenuStrip As ContextMenuStrip
    Friend WithEvents SettingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NotifyIcon As NotifyIcon
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents Linfo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GCSchedule As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVSchedule As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XTPSchedule As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XTPAttendance As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents Label1 As Label
    Friend WithEvents LEDay As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label2 As Label
    Friend WithEvents TETime As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents BSaveWAR As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents XTPLeaveRemaining As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents BSaveMonthly As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label3 As Label
    Friend WithEvents TETimeMonthly As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPProduction As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents BProdDuty As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label4 As Label
    Friend WithEvents TETimeDuty As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPEmpAppraisal As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SBEmpPerApp As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label5 As Label
    Friend WithEvents TEEmpPerApp As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPCashAdvance As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SBCashAdvance As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label6 As Label
    Friend WithEvents TECashAdvance As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPEvaulationAR As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents BtnEvaluationAR As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label7 As Label
    Friend WithEvents TEEvaluationAR As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPNoticeEmail As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents BtnEmailNoticeAR As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label8 As Label
    Friend WithEvents TEEmailNoticeAR As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPWarningLate As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SBWarningLate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label9 As Label
    Friend WithEvents TEWaningLate As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPGetKurs As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents BSaveKurs As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label10 As Label
    Friend WithEvents TETimeKurs As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents LEDayKurs As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label11 As Label
    Friend WithEvents XTPFailOrder As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents BtnFailOrder As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label12 As Label
    Friend WithEvents TECheckFailOrder As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPSalesReturn As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SBSalesReturnOrder As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label13 As Label
    Friend WithEvents TESalesReturnOrder As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents GCSchCloseOrder As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVSchCloseOrder As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents XTPMarketplaceOrderStt As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GCMOS As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVMOS As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PanelControl4 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtnRefMOS As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents XTPQC As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XTPPolis As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents BQC As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label14 As Label
    Friend WithEvents TEQC As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents BPolis As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label15 As Label
    Friend WithEvents TEPolis As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPPOOG As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents BPOOG As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label16 As Label
    Friend WithEvents TEPOOG As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPPROG As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents BPROG As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label17 As Label
    Friend WithEvents TEPROG As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents LEDayAREval As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label18 As Label
    Friend WithEvents XTPSerahTerima As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents BSerahTerimaQC As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label19 As Label
    Friend WithEvents TESerahTerimaQC As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPPIBReview As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents BPIBNotif As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label20 As Label
    Friend WithEvents TEPIBNotif As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPEOS As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GCEOS As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVEOS As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PanelControl5 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtnRefreshEOS As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnEOS As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label21 As Label
    Friend WithEvents TEEOSNotif As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPEOSPrice As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GCPriceEOS As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVPriceEOS As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PanelControl6 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtnRefreshPriceEOS As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnPriceEOS As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label22 As Label
    Friend WithEvents TEPriceEOSNotif As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPBSP As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GCBSP As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVBSP As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PanelControl7 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtnRefreshBSP As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnBSP As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label23 As Label
    Friend WithEvents TEBSPNotif As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPLineList As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents BtnLineList As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label24 As Label
    Friend WithEvents TELineList As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPKO As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents BReminderKO As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label25 As Label
    Friend WithEvents TEReminderKO As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents XTPSampleTrack As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents BtnResendEmail As DevExpress.XtraEditors.SimpleButton
End Class
