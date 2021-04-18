Imports System.Windows.Forms

Public Class DB_M




    Public Shared Function RETORNA_PRODUTO(ByVal TIPO As String) As String()
        TIPO = TIPO.ToUpper
        If TIPO = "PERFIS LAMINADOS" Then
            Return {"VIGA LAM W", "PERFIL DE ROLAMENTO LAM", "VIGA LAM GEN"}
        ElseIf TIPO = "PERFIS SOLDADOS" Then
            Return {"VIGA SOLDADA", "VIGA DE ROLAMENTO SOLD", "VIGA CAIXAO SOLD"}
        ElseIf TIPO = "PERFIS MISCELANEA" Then
            Return {"PILARETE", "PONTALETE"}
        ElseIf TIPO = "CANTONEIRA" Then
            Return {"TERCA COM ACABAMENTO", "TERCA PURLIN C", "TERCA PURLIN Z"}
        ElseIf TIPO = "CANTONEIRA" Then
            Return {"CANTONMEIRA"}
        ElseIf TIPO = "CHUMBADOR" Then
            Return {"CHUMBADOR", "INSERTO"}
        ElseIf TIPO = "MISCELANEA" Then
            Return {"CHAPA", "PERFIL DOBRADO", "SUPORTE", "GRADE DE PISO", "CHAPA DE PISO", "TIRANTE", "GUARDA CORPO", "PLATAFORMA/PASSARELA"}
        ElseIf TIPO = "ESCADA" Then
            Return {"LATERAL DE ESCADA", "DEGRAU", "GUARDA CORPO", "ESCADA", "ESCADA MARINHEIRO", "PLATAFORMA/PASSARELA"}
        ElseIf TIPO = "MATERIAIS ESTRUTURA" Then
            Return {"CIVIL 300", "CIVIL 350", "ZAR345-Z275 ZINC", "A36", "A572GR42", "A572GR50", "SAE1020", "A588", "SAE1008", "SAE1010", "SAE1012", "SAE1018", "A500", "A501", "A53", "XADREZ A36", "XADREZ ALUM", "SAC300", "SAC350", "SCH40 SAE1008", "SCH40 SAE1008"}
        ElseIf TIPO = "MATERIAIS TELHAS" Then
            Return {"ACO INOX", "ALUMINIO", "ALUMINIO ENV", "ALUMINIO PP", "GALVANIZADO", "PP ZINC", "PP GALV", "PP GALV PERF", "PP ZINC PERF", "ZAR345-Z275 GALV", "ZINCALUME", "ALUM ESP 1,00"}
        ElseIf TIPO = "MERCADORIAS" Then
            Return {"PERFIS LAMINADOS", "PERFIS SOLDADOS", "PERFIS MISCELANEA", "CANTONEIRA", "CHUMBADOR", "MISCELANEA", "ESCADA"}
        ElseIf TIPO = "MATERIAIS" Then
            Return {"MATERIAIS ESTRUTURA", "MATERIAIS TELHAS"}
        ElseIf TIPO = "TRATAMENTOS" Then
            Return {"TIPO DE TRATAMENTO"}
        ElseIf TIPO = "TIPO DE TRATAMENTO" Then
            Return {"GALVANIZADO", "GALVANIZADO COM JATO", "PINTURA LIQUIDA", "PINTURA LIQUIDA 2 DM", "PINTURA LIQUIDA 3 DM", "PINTURA PO", "PINTURA PO 2 DM", "POS PINTURA LIQUIDA", "POS PINTURA PO", "SEM PINTURA"}
        End If
        Return {""}
    End Function

    Public Shared Sub ALIMENTA(ByVal LISTA As ListBox, ByVal CHAVE As String, ByVal FILTRO As String)
        LISTA.Items.Clear()
        For Each LINHA In RETORNA_PRODUTO(CHAVE)
            If LINHA.Contains(FILTRO.ToUpper) Then
                LISTA.Items.Add(LINHA)
            End If
        Next
    End Sub
End Class
