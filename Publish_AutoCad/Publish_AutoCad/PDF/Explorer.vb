Imports System.Windows.Forms
Imports System.IO

Public Class Explorer
    Public Shared Versao As String = " v2.7"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim s As New FolderBrowserDialog
        s.Description = "Selecione a Pasta"

        If (Directory.Exists(Cfg.SELECIONADO)) Then

            s.SelectedPath = Cfg.SELECIONADO
        End If

        Dim pasta As String = Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("DWGPREFIX")
        If Directory.Exists(Cfg.SELECIONADO) Then
            s.SelectedPath = Cfg.SELECIONADO
            's.RootFolder = pasta
        End If



        If s.ShowDialog = Windows.Forms.DialogResult.OK Then
            'If Directory.Exists(Cfg.SELECIONADO) Then
            '    s.SelectedPath = Cfg.SELECIONADO
            '    's.RootFolder = pasta
            'End If
            ListView1.Items.Clear()
            Dim pastas = SearchOption.TopDirectoryOnly
            Dim result As Integer = MessageBox.Show("Procurar itens em sub-pastas?", "Confirme", MessageBoxButtons.YesNo)
            If result = DialogResult.No Then

            ElseIf result = DialogResult.Yes Then
                pastas = SearchOption.AllDirectories
            End If


            Dim arquivos = System.IO.Directory.GetFiles(s.SelectedPath, "*", pastas)
            Cfg.SELECIONADO = s.SelectedPath
            For Each linha In arquivos
                If linha.ToUpper.Contains(".DWG") Or linha.ToUpper.Contains(".DXF") Then
                    Listagem.add(linha, ListView1, ImageList1, s.SelectedPath)
                End If
            Next
            status.Text = ListView1.Items.Count & " itens"
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "Limpar Seleção" Then
            For Each item As ListViewItem In ListView1.CheckedItems
                item.Checked = False
            Next
            Button3.Text = "Selecionar Tudo"
        Else
            For Each item As ListViewItem In ListView1.Items
                item.Checked = True
            Next
            Button3.Text = "Limpar Seleção"
        End If
        
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private m_SortingColumn As ColumnHeader
   
    Private Sub Explorer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Explorer " & Explorer.Versao
    End Sub

    Private Sub ListView1_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles ListView1.ColumnClick
        ' Get the new sorting column. 
        Dim new_sorting_column As ColumnHeader = ListView1.Columns(e.Column)
        ' Figure out the new sorting order. 
        Dim sort_order As System.Windows.Forms.SortOrder
        If m_SortingColumn Is Nothing Then
            ' New column. Sort ascending. 
            sort_order = SortOrder.Ascending
        Else ' See if this is the same column. 
            If new_sorting_column.Equals(m_SortingColumn) Then
                ' Same column. Switch the sort order. 
                If m_SortingColumn.Text.StartsWith("> ") Then
                    sort_order = SortOrder.Descending
                Else
                    sort_order = SortOrder.Ascending
                End If
            Else
                ' New column. Sort ascending. 
                sort_order = SortOrder.Ascending
            End If
            ' Remove the old sort indicator. 
            m_SortingColumn.Text = m_SortingColumn.Text.Substring(2)
        End If
        ' Display the new sort order. 
        m_SortingColumn = new_sorting_column
        If sort_order = SortOrder.Ascending Then
            m_SortingColumn.Text = "> " & m_SortingColumn.Text
        Else
            m_SortingColumn.Text = "< " & m_SortingColumn.Text
        End If
        ' Create a comparer. 
        ListView1.ListViewItemSorter = New clsListviewSorter(e.Column, sort_order)
        ' Sort. 
        ListView1.Sort()
    End Sub

   

End Class