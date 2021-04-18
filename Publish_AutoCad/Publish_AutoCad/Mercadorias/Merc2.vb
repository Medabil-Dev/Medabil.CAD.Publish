Public Class Merc2

    Private Sub TIPO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TIPO.SelectedIndexChanged
        DB_M.ALIMENTA(GRUPO, TIPO.Text, FILTRO.Text)
    End Sub

    Private Sub GRUPO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GRUPO.SelectedIndexChanged
        DB_M.ALIMENTA(VALOR, GRUPO.Text, FILTRO2.Text)
    End Sub

    Private Sub VALOR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles VALOR.SelectedIndexChanged

    End Sub

    Private Sub FILTRO_TextChanged(sender As Object, e As EventArgs) Handles FILTRO.TextChanged
        DB_M.ALIMENTA(GRUPO, TIPO.Text, FILTRO.Text)
    End Sub

    Private Sub FILTRO2_TextChanged(sender As Object, e As EventArgs) Handles FILTRO2.TextChanged
        DB_M.ALIMENTA(VALOR, GRUPO.Text, FILTRO2.Text)
    End Sub

    Private Sub Aplicar_Click(sender As Object, e As EventArgs) Handles Aplicar.Click
        Me.Hide()
        Dim MARCAS As String() = {"M8_COM", "M8_PRO", "M8_LAM", "M8_ELE", "M8_ELU", "P8_PRO", "P8_PRO_M", "P8_LAM_M", "P8_ELU_M", "P8_ELU", "P8_ELE_M", "P8_LAM"}
        Dim POSICOES As String() = {""}
        If TIPO.Text = "MERCADORIAS" Then
            _Core.Seta_Atributos2(MARCAS, "DES_PEZ", VALOR.Text)
        ElseIf TIPO.Text = "TRATAMENTOS" Then
            _Core.Seta_Atributos2(MARCAS, "TRA_PEZ", VALOR.Text)
        ElseIf TIPO.Text = "MATERIAIS" Then
            _Core.Seta_Atributos2(MARCAS, "MAT_PRO", VALOR.Text)
        End If


    End Sub

    Private Sub Merc2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TIPO.SelectedIndex = 0
    End Sub
End Class