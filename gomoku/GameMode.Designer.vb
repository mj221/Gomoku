<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GameMode
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GameMode))
        Me.btnSinglePlayer = New System.Windows.Forms.Button()
        Me.btnTwoPlayer = New System.Windows.Forms.Button()
        Me.btnMainMenu = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnSinglePlayer
        '
        Me.btnSinglePlayer.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.btnSinglePlayer.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSinglePlayer.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnSinglePlayer.Location = New System.Drawing.Point(13, 109)
        Me.btnSinglePlayer.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSinglePlayer.Name = "btnSinglePlayer"
        Me.btnSinglePlayer.Size = New System.Drawing.Size(174, 75)
        Me.btnSinglePlayer.TabIndex = 0
        Me.btnSinglePlayer.Text = "Single Player"
        Me.btnSinglePlayer.UseVisualStyleBackColor = False
        '
        'btnTwoPlayer
        '
        Me.btnTwoPlayer.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.btnTwoPlayer.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTwoPlayer.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnTwoPlayer.Location = New System.Drawing.Point(233, 109)
        Me.btnTwoPlayer.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnTwoPlayer.Name = "btnTwoPlayer"
        Me.btnTwoPlayer.Size = New System.Drawing.Size(174, 75)
        Me.btnTwoPlayer.TabIndex = 1
        Me.btnTwoPlayer.Text = "Two Player"
        Me.btnTwoPlayer.UseVisualStyleBackColor = False
        '
        'btnMainMenu
        '
        Me.btnMainMenu.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.btnMainMenu.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMainMenu.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnMainMenu.Location = New System.Drawing.Point(150, 238)
        Me.btnMainMenu.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnMainMenu.Name = "btnMainMenu"
        Me.btnMainMenu.Size = New System.Drawing.Size(124, 45)
        Me.btnMainMenu.TabIndex = 2
        Me.btnMainMenu.Text = "Main Menu"
        Me.btnMainMenu.UseVisualStyleBackColor = False
        '
        'GameMode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(420, 409)
        Me.Controls.Add(Me.btnMainMenu)
        Me.Controls.Add(Me.btnTwoPlayer)
        Me.Controls.Add(Me.btnSinglePlayer)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.Name = "GameMode"
        Me.Text = "Gomoku - Choose Game Mode"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnSinglePlayer As Button
    Friend WithEvents btnTwoPlayer As Button
    Friend WithEvents btnMainMenu As Button
End Class
