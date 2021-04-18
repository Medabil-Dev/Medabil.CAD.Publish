<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Merc2
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
        Me.TIPO = New System.Windows.Forms.ComboBox()
        Me.GRUPO = New System.Windows.Forms.ListBox()
        Me.VALOR = New System.Windows.Forms.ListBox()
        Me.FILTRO = New System.Windows.Forms.TextBox()
        Me.Aplicar = New System.Windows.Forms.Button()
        Me.FILTRO2 = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'TIPO
        '
        Me.TIPO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TIPO.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.TIPO.FormattingEnabled = True
        Me.TIPO.Items.AddRange(New Object() {"MERCADORIAS", "MATERIAIS", "TRATAMENTOS"})
        Me.TIPO.Location = New System.Drawing.Point(13, 12)
        Me.TIPO.Name = "TIPO"
        Me.TIPO.Size = New System.Drawing.Size(198, 21)
        Me.TIPO.TabIndex = 0
        '
        'GRUPO
        '
        Me.GRUPO.FormattingEnabled = True
        Me.GRUPO.Location = New System.Drawing.Point(13, 65)
        Me.GRUPO.Name = "GRUPO"
        Me.GRUPO.Size = New System.Drawing.Size(198, 251)
        Me.GRUPO.TabIndex = 1
        '
        'VALOR
        '
        Me.VALOR.FormattingEnabled = True
        Me.VALOR.Location = New System.Drawing.Point(217, 38)
        Me.VALOR.Name = "VALOR"
        Me.VALOR.Size = New System.Drawing.Size(198, 225)
        Me.VALOR.TabIndex = 1
        '
        'FILTRO
        '
        Me.FILTRO.Location = New System.Drawing.Point(13, 39)
        Me.FILTRO.Name = "FILTRO"
        Me.FILTRO.Size = New System.Drawing.Size(198, 20)
        Me.FILTRO.TabIndex = 2
        '
        'Aplicar
        '
        Me.Aplicar.Location = New System.Drawing.Point(218, 270)
        Me.Aplicar.Name = "Aplicar"
        Me.Aplicar.Size = New System.Drawing.Size(197, 46)
        Me.Aplicar.TabIndex = 3
        Me.Aplicar.Text = "Aplicar!"
        Me.Aplicar.UseVisualStyleBackColor = True
        '
        'FILTRO2
        '
        Me.FILTRO2.Location = New System.Drawing.Point(217, 12)
        Me.FILTRO2.Name = "FILTRO2"
        Me.FILTRO2.Size = New System.Drawing.Size(198, 20)
        Me.FILTRO2.TabIndex = 2
        '
        'Merc2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(433, 333)
        Me.Controls.Add(Me.Aplicar)
        Me.Controls.Add(Me.FILTRO2)
        Me.Controls.Add(Me.FILTRO)
        Me.Controls.Add(Me.VALOR)
        Me.Controls.Add(Me.GRUPO)
        Me.Controls.Add(Me.TIPO)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Merc2"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Merc2"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TIPO As System.Windows.Forms.ComboBox
    Friend WithEvents GRUPO As System.Windows.Forms.ListBox
    Friend WithEvents VALOR As System.Windows.Forms.ListBox
    Friend WithEvents FILTRO As System.Windows.Forms.TextBox
    Friend WithEvents Aplicar As System.Windows.Forms.Button
    Friend WithEvents FILTRO2 As System.Windows.Forms.TextBox
End Class
