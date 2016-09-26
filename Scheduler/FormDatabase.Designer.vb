<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormDatabase
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormDatabase))
        Me.GroupControlDatabase = New DevExpress.XtraEditors.GroupControl()
        Me.ListBoxControlDatabase = New DevExpress.XtraEditors.ListBoxControl()
        Me.GroupControlConnection = New DevExpress.XtraEditors.GroupControl()
        Me.PictureEditIcon = New DevExpress.XtraEditors.PictureEdit()
        Me.TextEditHost = New DevExpress.XtraEditors.TextEdit()
        Me.TextEditPassword = New DevExpress.XtraEditors.TextEdit()
        Me.SimpleButtonSave = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControlPassword = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControlHost = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControlUsername = New DevExpress.XtraEditors.LabelControl()
        Me.SimpleButtonConnect = New DevExpress.XtraEditors.SimpleButton()
        Me.TextEditUsername = New DevExpress.XtraEditors.TextEdit()
        CType(Me.GroupControlDatabase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControlDatabase.SuspendLayout()
        CType(Me.ListBoxControlDatabase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControlConnection, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControlConnection.SuspendLayout()
        CType(Me.PictureEditIcon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditHost.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEditUsername.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControlDatabase
        '
        Me.GroupControlDatabase.Controls.Add(Me.ListBoxControlDatabase)
        Me.GroupControlDatabase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControlDatabase.Location = New System.Drawing.Point(361, 0)
        Me.GroupControlDatabase.Name = "GroupControlDatabase"
        Me.GroupControlDatabase.Size = New System.Drawing.Size(214, 262)
        Me.GroupControlDatabase.TabIndex = 21
        Me.GroupControlDatabase.Text = "Database"
        '
        'ListBoxControlDatabase
        '
        Me.ListBoxControlDatabase.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.ListBoxControlDatabase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBoxControlDatabase.Location = New System.Drawing.Point(2, 20)
        Me.ListBoxControlDatabase.Name = "ListBoxControlDatabase"
        Me.ListBoxControlDatabase.Size = New System.Drawing.Size(210, 240)
        Me.ListBoxControlDatabase.TabIndex = 5
        '
        'GroupControlConnection
        '
        Me.GroupControlConnection.Controls.Add(Me.PictureEditIcon)
        Me.GroupControlConnection.Controls.Add(Me.TextEditHost)
        Me.GroupControlConnection.Controls.Add(Me.TextEditPassword)
        Me.GroupControlConnection.Controls.Add(Me.SimpleButtonSave)
        Me.GroupControlConnection.Controls.Add(Me.LabelControlPassword)
        Me.GroupControlConnection.Controls.Add(Me.LabelControlHost)
        Me.GroupControlConnection.Controls.Add(Me.LabelControlUsername)
        Me.GroupControlConnection.Controls.Add(Me.SimpleButtonConnect)
        Me.GroupControlConnection.Controls.Add(Me.TextEditUsername)
        Me.GroupControlConnection.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupControlConnection.Location = New System.Drawing.Point(0, 0)
        Me.GroupControlConnection.Name = "GroupControlConnection"
        Me.GroupControlConnection.Size = New System.Drawing.Size(361, 262)
        Me.GroupControlConnection.TabIndex = 20
        Me.GroupControlConnection.Text = "Information"
        '
        'PictureEditIcon
        '
        Me.PictureEditIcon.EditValue = CType(resources.GetObject("PictureEditIcon.EditValue"), Object)
        Me.PictureEditIcon.Location = New System.Drawing.Point(12, 47)
        Me.PictureEditIcon.Name = "PictureEditIcon"
        Me.PictureEditIcon.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PictureEditIcon.Properties.Appearance.Options.UseBackColor = True
        Me.PictureEditIcon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PictureEditIcon.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.PictureEditIcon.Size = New System.Drawing.Size(124, 113)
        Me.PictureEditIcon.TabIndex = 17
        '
        'TextEditHost
        '
        Me.TextEditHost.Location = New System.Drawing.Point(147, 50)
        Me.TextEditHost.Name = "TextEditHost"
        Me.TextEditHost.Size = New System.Drawing.Size(199, 20)
        Me.TextEditHost.TabIndex = 1
        '
        'TextEditPassword
        '
        Me.TextEditPassword.Location = New System.Drawing.Point(147, 140)
        Me.TextEditPassword.Name = "TextEditPassword"
        Me.TextEditPassword.Properties.UseSystemPasswordChar = True
        Me.TextEditPassword.Size = New System.Drawing.Size(199, 20)
        Me.TextEditPassword.TabIndex = 3
        '
        'SimpleButtonSave
        '
        Me.SimpleButtonSave.Enabled = False
        Me.SimpleButtonSave.Location = New System.Drawing.Point(147, 166)
        Me.SimpleButtonSave.Name = "SimpleButtonSave"
        Me.SimpleButtonSave.Size = New System.Drawing.Size(50, 23)
        Me.SimpleButtonSave.TabIndex = 6
        Me.SimpleButtonSave.Text = "Save"
        '
        'LabelControlPassword
        '
        Me.LabelControlPassword.Location = New System.Drawing.Point(147, 121)
        Me.LabelControlPassword.Name = "LabelControlPassword"
        Me.LabelControlPassword.Size = New System.Drawing.Size(46, 13)
        Me.LabelControlPassword.TabIndex = 10
        Me.LabelControlPassword.Text = "Password"
        '
        'LabelControlHost
        '
        Me.LabelControlHost.Location = New System.Drawing.Point(147, 31)
        Me.LabelControlHost.Name = "LabelControlHost"
        Me.LabelControlHost.Size = New System.Drawing.Size(22, 13)
        Me.LabelControlHost.TabIndex = 12
        Me.LabelControlHost.Text = "Host"
        '
        'LabelControlUsername
        '
        Me.LabelControlUsername.Location = New System.Drawing.Point(147, 76)
        Me.LabelControlUsername.Name = "LabelControlUsername"
        Me.LabelControlUsername.Size = New System.Drawing.Size(48, 13)
        Me.LabelControlUsername.TabIndex = 9
        Me.LabelControlUsername.Text = "Username"
        '
        'SimpleButtonConnect
        '
        Me.SimpleButtonConnect.Location = New System.Drawing.Point(203, 166)
        Me.SimpleButtonConnect.Name = "SimpleButtonConnect"
        Me.SimpleButtonConnect.Size = New System.Drawing.Size(69, 23)
        Me.SimpleButtonConnect.TabIndex = 4
        Me.SimpleButtonConnect.Text = "Connect"
        '
        'TextEditUsername
        '
        Me.TextEditUsername.Location = New System.Drawing.Point(147, 95)
        Me.TextEditUsername.Name = "TextEditUsername"
        Me.TextEditUsername.Size = New System.Drawing.Size(199, 20)
        Me.TextEditUsername.TabIndex = 2
        '
        'FormDatabase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(575, 262)
        Me.Controls.Add(Me.GroupControlDatabase)
        Me.Controls.Add(Me.GroupControlConnection)
        Me.Name = "FormDatabase"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Database"
        CType(Me.GroupControlDatabase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControlDatabase.ResumeLayout(False)
        CType(Me.ListBoxControlDatabase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControlConnection, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControlConnection.ResumeLayout(False)
        Me.GroupControlConnection.PerformLayout()
        CType(Me.PictureEditIcon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditHost.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEditUsername.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupControlDatabase As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ListBoxControlDatabase As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents GroupControlConnection As DevExpress.XtraEditors.GroupControl
    Friend WithEvents PictureEditIcon As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents TextEditHost As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEditPassword As DevExpress.XtraEditors.TextEdit
    Friend WithEvents SimpleButtonSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControlPassword As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControlHost As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControlUsername As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SimpleButtonConnect As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TextEditUsername As DevExpress.XtraEditors.TextEdit
End Class
