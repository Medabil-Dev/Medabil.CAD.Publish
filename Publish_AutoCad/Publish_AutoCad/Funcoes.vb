Imports System.Windows.Forms

Public Class Funcoes
    Public Shared Function compara(ByVal arquivo1 As String, ByVal arquivo2 As String) As Boolean
        Dim data1 As Date = IO.File.GetLastWriteTime(arquivo1)
        Dim data2 As Date = IO.File.GetLastWriteTime(arquivo2)
        If data1 > data2 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Sub apagar_linhas(ByVal datagrid As DataGridView)
        If datagrid.SelectedRows.Count > 0 Then
            If MsgBox("Deseja mesmo Apagar o(s) " & datagrid.SelectedRows.Count & " item(s) selecionados?" & vbNewLine & "Nao será possível desfazer.", vbYesNo + vbQuestion, "Apagar") = vbYes Then
                For Each row As DataGridViewRow In datagrid.SelectedRows
                    datagrid.Rows.Remove(row)
                Next

            End If
        End If


    End Sub
End Class
