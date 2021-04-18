Imports System.Windows.Forms
Imports Autodesk.AutoCAD.PlottingServices
Imports System.IO
Imports System.Text

Public Class Menu

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If Directory.Exists(TextBox1.Text) Then

          


            Dim contador = 1
            Dim packs As New List(Of List(Of Prancha))
            Dim pac As New Pacote
            'monta os pacotes baseados no limite de pranchas
            For Each linha As DataGridViewRow In DataGridView1.Rows
                'Dim pac As New Pacote
                Dim novo As New Prancha
                novo.Dwgname = linha.Cells(3).Value
                novo.Layout = linha.Cells(1).Value
                novo.Title = linha.Cells(0).Value

                pac.Pranchas.Add(novo)

                If contador > Cfg.limite Then
                    Dim ss As New List(Of Prancha)
                    For Each pr As Prancha In pac.Pranchas
                        ss.Add(pr)
                    Next
                    'ss.Pranchas.AddRange(pac.Pranchas)
                    packs.Add(ss)
                    pac.Pranchas.Clear()
                    contador = 0
                End If
                contador = contador + 1
            Next

            If pac.Pranchas.Count > 0 Then
                packs.Add(pac.Pranchas)
            End If


            'packs.Pacotes.Add(pac)


          







            'executa cada pacote
            Dim cont As Integer = 1
            Dim ssd As New Load
            ssd.Show()
            For Each volume As List(Of Prancha) In packs
                Dim ARQUIVO_DSD As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Publish" & cont & ".dsd"
                If File.Exists(ARQUIVO_DSD) Then
                    File.Delete(ARQUIVO_DSD)
                End If






                Using dsdDwgFiles As New DsdEntryCollection

                    Dim total As Integer = DataGridView1.Rows.Count
                    Dim atual As Integer = 0

                    'For Each linha As DataGridViewRow In DataGridView1.Rows
                    '    atual = atual + 1
                    '    ss.status.Text = "Por favor aguarde... Organizando dados..."
                    '    ss.Update()
                    '    ' Add first drawing file
                    '    Using dsdDwgFile1 As New DsdEntry

                    '        ' Set the file name and layout
                    '        dsdDwgFile1.DwgName = linha.Cells(3).Value
                    '        dsdDwgFile1.Layout = linha.Cells(1).Value
                    '        ' dsdDwgFile1.AutoDelete = "ENG1_A3|C:\PDF\Selo.dwg"
                    '        dsdDwgFile1.Title = linha.Cells(0).Value

                    '        ' Set the page setup override
                    '        dsdDwgFile1.Nps = ""
                    '        dsdDwgFile1.NpsSourceDwg = ""
                    '        dsdDwgFiles.Add(dsdDwgFile1)
                    '    End Using
                    'Next



                    For Each linha As Prancha In volume
                        atual = atual + 1
                        ssd.status.Text = "Por favor aguarde... Organizando dados..."
                        ssd.Update()
                        ' Add first drawing file
                        Using dsdDwgFile1 As New DsdEntry

                            ' Set the file name and layout
                            dsdDwgFile1.DwgName = linha.Dwgname
                            dsdDwgFile1.Layout = linha.Layout
                            ' dsdDwgFile1.AutoDelete = "ENG1_A3|C:\PDF\Selo.dwg"
                            dsdDwgFile1.Title = linha.Title

                            ' Set the page setup override
                            dsdDwgFile1.Nps = ""
                            dsdDwgFile1.NpsSourceDwg = ""
                            dsdDwgFiles.Add(dsdDwgFile1)
                        End Using
                    Next




                    ' Set the properties for the DSD file and then write it out
                    Using dsdFileData As New DsdData

                        ' Set the target information for publishing
                        dsdFileData.DestinationName = Environment.GetFolderPath( _
                            Environment.SpecialFolder.MyDocuments) & "\MyPublish2.pdf"




                        dsdFileData.ProjectPath = TextBox1.Text


                        ' Set the drawings that should be added to the publication
                        dsdFileData.SetDsdEntryCollection(dsdDwgFiles)

                        ' Set the general publishing properties
                        dsdFileData.LogFilePath = "C:\plot_pdf\Log.txt"



                        dsdFileData.SheetType = SheetType.SinglePdf

                        If (folha.Text.Contains("PNG")) Then
                            dsdFileData.SheetType = SheetType.OriginalDevice
                        End If
                        ' Create the DSD file
                        dsdFileData.WriteDsd(ARQUIVO_DSD)


                        'gambiarra para tratar o DSD
                        tratar_dsd(ARQUIVO_DSD, folha.Text)



                        Try
                            ' Publish the specified drawing files in the DSD file, and
                            ' honor the behavior of the BACKGROUNDPLOT system variable
                            Using dsdDataFile As New DsdData

                                dsdDataFile.ReadDsd(ARQUIVO_DSD)

                                ' Get the DWG to PDF.pc3 and use it as a 
                                ' device override for all the layouts
                                Dim acPlCfg As PlotConfig =
                                    PlotConfigManager.SetCurrentConfig("DWG to PDF.PC3")

                                If (folha.Text.Contains("PNG")) Then
                                    acPlCfg = PlotConfigManager.SetCurrentConfig("PublishToWeb PNG.PC3")

                                End If

                                ssd.status.Text = "Por favor aguarde... Gerando pacote " & cont & " de " & packs.Count
                               
                                Autodesk.AutoCAD.ApplicationServices.Application.Publisher.PublishExecute(dsdDataFile, acPlCfg)
                            End Using

                        Catch es As Autodesk.AutoCAD.Runtime.Exception
                            MsgBox(es.Message)
                        End Try
                    End Using

                End Using


                cont = cont + 1
                ssd.Update()




            Next
            ssd.Close()




            Me.Close()
        Else
            MsgBox("Diretório de destino inválido. Selecione um diretório", MsgBoxStyle.Exclamation, "Erro!")
        End If

    End Sub
    Public Shared Sub tratar_dsd(ByVal ARQUIVO_DSD As String, ByVal folha As String)
        'gambiarra para setar o pagesetup
        Dim setup_model As String = "ENG1_$$|R:\Lisps\bin\Selo.dwg".Replace("$$", folha)
        Dim setup_layout As String = "ENG1-$$|R:\Lisps\bin\Selo.dwg".Replace("$$", folha)
        Dim setup As String = ""
        Dim dsd As String() = IO.File.ReadAllLines(ARQUIVO_DSD, Encoding.GetEncoding(1252))
        For i = 0 To dsd.Count - 1
            'DEFINE O LAYOUT
            If dsd(i).StartsWith("Layout=") Then
                Dim ssd As String() = dsd(i).Split("=")
                If ssd(1).ToUpper.Contains("MODEL") Then
                    setup = setup_model
                Else
                    setup = setup_layout
                End If
            End If
            'SETA O LAYOUT
            If dsd(i).StartsWith("Setup=") Then
                dsd(i) = dsd(i) & setup
            End If

        Next
        File.Delete(ARQUIVO_DSD)
        IO.File.WriteAllLines(ARQUIVO_DSD, dsd, Encoding.GetEncoding(1252))
        'fim gambiarra
    End Sub


    Public Shared Sub grava_layout(ByVal linha As DataGridViewRow, ByVal coluna As Integer, ByVal arq As Integer, ByVal data As DataGridView)
        Dim colunas As DataGridViewComboBoxCell = linha.Cells(coluna)
        colunas.Items.Clear()

        For Each it In _Core.ler_layout(linha.Cells(arq).Value)
            'MsgBox(it)
            Try
                colunas.Items.Add(it)
            Catch ex As Exception

            End Try

        Next
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        For Each linha As DataGridViewRow In DataGridView1.Rows
            Try
                grava_layout(linha, 1, 3, DataGridView1)
            Catch ex As Exception

            End Try

        Next
        MsgBox("Layouts Importados!")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub SobreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SobreToolStripMenuItem.Click
        MsgBox("Powered By Daniel Lins Maciel" & vbNewLine & Application.ProductName & Explorer.Versao)
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs)
        If DataGridView1.SelectedRows.Count > 0 Then
            If MsgBox("Deseja mesmo Apagar o(s) " & DataGridView1.SelectedRows.Count & " item(s) selecionados?" & vbNewLine & "Nao será possível desfazer.", vbYesNo + vbQuestion, "Apagar") = vbYes Then
                For Each row As DataGridViewRow In DataGridView1.SelectedRows
                    DataGridView1.Rows.Remove(row)
                Next
            End If
        End If
    End Sub

    Private Sub Menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        folha.SelectedIndex = 0
        Me.Text = "Plot PDF " & Explorer.Versao
    End Sub

    Private Sub DataGridView1_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridView1.RowsAdded

    End Sub

    Private Sub DeletarLinhasSelecionadasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeletarLinhasSelecionadasToolStripMenuItem.Click
        Funcoes.apagar_linhas(DataGridView1)
        status.Text = DataGridView1.Rows.Count & "pranchas"
    End Sub

    Private Sub AdicionarPranchasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdicionarPranchasToolStripMenuItem.Click
        Dim ex As New Explorer
        Dim contador As Integer = 0

        Dim l As New Load

        If ex.ShowDialog = Windows.Forms.DialogResult.OK Then

            l.Show()
            l.progresso.Maximum = ex.ListView1.CheckedItems.Count
            For Each prancha As ListViewItem In ex.ListView1.CheckedItems
                'l.ProgressBar1.Value = contador
                Dim arq As New System.IO.DirectoryInfo(prancha.Name)

                Dim arquivo As String = arq.Name.Replace(arq.Extension, "").ToUpper
                l.progresso.Value = contador
                l.status.Text = "Importando arquivo " & contador + 1 & " de " & ex.ListView1.CheckedItems.Count & vbNewLine _
                    & arquivo
                l.Update()
                DataGridView1.Rows.Add({arq.Name.Replace(arq.Extension, "").ToUpper, "", arq.Extension, prancha.Name})
                If DataGridView1.Rows.Count > 0 Then
                    grava_layout(DataGridView1.Rows(contador), 1, 3, DataGridView1)
                End If

                Try
                    Dim ss As DataGridViewComboBoxCell = DataGridView1.Rows(contador).Cells(1)
                    'MsgBox(ss.Items(0))
                    ss.Value = ss.Items(0)
                    If ex.Layout.Checked = True Then

                        ss.Value = ss.Items(0)
                    Else
                        ss.Value = "Model"
                    End If
                Catch ex1 As Exception
                    'MsgBox(ex1.Message)
                End Try
                contador = contador + 1
                '  Update()
            Next

            status.Text = DataGridView1.Rows.Count & "pranchas"
        End If
        l.Close()
        If DataGridView1.Rows.Count > 0 Then
            Dim ARQ As String = Path.GetDirectoryName(DataGridView1.Rows(0).Cells(3).Value)
            If Directory.Exists(ARQ & "\PDF\") Then
                TextBox1.Text = ARQ & "\PDF\"
            Else
                Directory.CreateDirectory(ARQ & "\PDF")

                TextBox1.Text = ARQ & "\PDF\"

            End If


        End If
        Me.Update()
    End Sub

    Private Sub DefinirDestinoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DefinirDestinoToolStripMenuItem.Click
        Dim s As New FolderBrowserDialog
        s.Description = "Selecione a Pasta"



        If s.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = s.SelectedPath
        End If
    End Sub

    Private Sub FerramentasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FerramentasToolStripMenuItem.Click

    End Sub
End Class