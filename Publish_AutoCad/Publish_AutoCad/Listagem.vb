Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing

Public Class Listagem
    Public Shared Sub add(ByVal f As String, ByVal menu As ListView, ByVal lista As ImageList, ByVal pasta As String)
        Dim diretorio = Path.GetDirectoryName(f)
        Dim arq As New System.IO.DirectoryInfo(f)

        Dim EXT As String() = {"DWG", "DXF"}


        For Each X In EXT
            Dim iconForFile As Icon = SystemIcons.WinLogo
            If Not (lista.Images.ContainsKey(arq.Extension)) Then


                Try
                    iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(arq.FullName)
                Catch ex As Exception

                End Try

                lista.Images.Add(arq.Extension, iconForFile)

            End If


            If UCase(arq.Extension).Contains(X) Then
                Dim item As ListViewItem = menu.Items.Add(arq.Name)
                item.ImageIndex = 0
                item.Name = f
                item.ImageKey = arq.Extension
                item.SubItems.Add(pasta)
                '  achei = achei + 1

                Exit For
            End If
        Next
    End Sub
End Class