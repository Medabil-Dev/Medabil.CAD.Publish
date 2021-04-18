Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.PlottingServices
Imports Autodesk.AutoCAD.EditorInput
Imports System.Windows.Forms


Public Class _Core

    Public Shared Sub Modificar_bloco(ByVal nome As ObjectId, ByVal tag As String, ByVal valor As String)
        ' MsgBox("Entrei no seta")
        Using myTrans As Transaction = nome.Database.TransactionManager.StartTransaction
            Try
                Dim BlocRef As BlockReference = nome.GetObject(OpenMode.ForRead)
                Dim BlocRefAttrCollection As AttributeCollection = BlocRef.AttributeCollection

                If BlocRefAttrCollection.Count > 0 Then
                    ' MsgBox("ENCONTREI TAGS")
                    For Each BlocRefAttrID As ObjectId In BlocRefAttrCollection
                        Try
                            Dim BlocRefAttr As AttributeReference = BlocRefAttrID.GetObject(OpenMode.ForWrite)
                            'MsgBox(BlocRefAttr.Tag)
                            If BlocRefAttr.Tag.Equals(tag, StringComparison.OrdinalIgnoreCase) = True Then
                                'MsgBox(BlocRefAttr.Tag)
                                BlocRefAttr.TextString = valor
                                Exit For
                            End If
                        Catch ex As Exception
                            ' MsgBox(ex.Message)
                        End Try

                    Next
                End If
            Catch ex As Exception
                'MsgBox(ex.Message)
            Finally
                myTrans.Commit()
            End Try
        End Using
    End Sub
    <CommandMethod("tst2")> _
    Public Shared Sub seta_3d()
        Dim acDoc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument

        Dim ss = InputBox("Digite o comando", "", "")
        If ss.Length > 0 Then
            acDoc.SendStringToExecute(ss & " ", True, False, False)
        End If

    End Sub




    Public Shared Sub Seta_Atributos2(ByVal bloco As String(), ByVal tag As String, ByVal valor As String)

        Dim doc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
        Dim db As Database = doc.Database
        Dim ed As Editor = doc.Editor
        Dim attref As AttributeReference = Nothing
        Dim bref As BlockReference = Nothing


        'Dim blkname As String = bloco ''   <--- change block name
        Dim attTag As String = String.Empty

        Try

            Dim tr As Transaction = db.TransactionManager.StartTransaction()
            Using tr

                Dim selres As PromptSelectionResult = Nothing

                Dim tvs As New List(Of TypedValue)
                tvs.Add(New TypedValue(0, "insert"))
                tvs.Add(New TypedValue(66, 1))

                For Each subbloco In bloco
                    'tvs.Add(New TypedValue(2, "`U*," + subbloco))
                Next



                Dim filt As SelectionFilter = New SelectionFilter(tvs.ToArray)

                selres = ed.GetSelection(filt)

                If selres.Status <> PromptStatus.OK Then
                    Return
                End If
                Dim selset As SelectionSet = selres.Value
                Dim bname As String = String.Empty
                For Each selobj As SelectedObject In selset
                    Dim sent As Entity = DirectCast(tr.GetObject(selobj.ObjectId, OpenMode.ForRead), Entity)
                    If TypeOf sent Is BlockReference Then
                        Dim obr As BlockReference = DirectCast(sent, BlockReference)

                        Dim dynId As ObjectId = sent.ObjectId
                        If dynId.IsNull Or dynId.IsErased Then
                            Continue For
                        End If

                        For Each subbloco In bloco
                            If subbloco.ToLower = obr.Name.ToLower Then
                                Using doclock As DocumentLock = doc.LockDocument()
                                    Modificar_bloco(sent.ObjectId, tag, valor)
                                End Using
                            End If
                        Next
                    End If

                Next
                tr.Commit()
            End Using
        Catch ex As Autodesk.AutoCAD.Runtime.Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    <CommandMethod("tst1")> _
    Public Shared Sub Mercadorias2()
        If Funcoes.compara(Cfg.net_dir & "\Publish_AutoCad.dll", Cfg.bin_dir & "\Publish_AutoCad.dll") = True Then
            MsgBox("Há uma nova versão do aplicativo. " & vbNewLine & "Feche o AutoCAD, abra-o novamente execute o comando 'INSTALA_PLOT_PDF2'")
            Exit Sub
        End If


        Dim s As New Merc2

        s.Show()
    End Sub
    <CommandMethod("PLOT_PDF2_L")> _
    Public Shared Sub Mostra_Menu()
        Dim T As Date = #01/01/2017 1:00:00 PM#
        If Math.Round((T - Date.Now).TotalDays) < 1 Then
            MsgBox("Este aplicativo está desatualizado." & vbNewLine & "Como atualizar:" & vbNewLine & "Feche todas as janelas do CAD, abra um CAD novo, digite Ferramentas e clique em instalar para atualizar." & vbNewLine & "Caso não dê certo, vá em R:\Lisps\Plot_Pdf e execute o arquivo Atualizar.bat, com todas as janelas do CAD fechadas. Para maiores dúvidas, entre em contato com o desenvolvedor." & vbNewLine & "Daniel Maciel", vbExclamation, "Desatualizado.")

            'Application.Exit()
            Exit Sub
        End If
        'If Funcoes.compara(Cfg.net_dir & "\Publish_AutoCad.dll", Cfg.bin_dir & "\Publish_AutoCad.dll") = True Then
        '    MsgBox("Há uma nova versão do aplicativo. " & vbNewLine & "Feche o AutoCAD, abra-o novamente execute o comando 'INSTALA_PLOT_PDF2'")
        '    Exit Sub
        'End If


        Dim s As New Menu

        s.Show()
    End Sub

    Public Shared Function ler_layout(ByVal arquivo As String) As List(Of String)
        Dim retorno As New List(Of String)
        Try
            Dim acDoc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
            Dim acCurDb As Database = acDoc.Database
            Dim acExDb As Database = New Database(False, True)
            acExDb.ReadDwgFile(arquivo, FileOpenMode.OpenForReadAndAllShare, True, "")
            Using acTransEx As Transaction = acExDb.TransactionManager.StartTransaction()
                Dim layoutsEx As DBDictionary = _
                                acTransEx.GetObject(acExDb.LayoutDictionaryId, OpenMode.ForRead)
                For Each item As DBDictionaryEntry In layoutsEx
                    retorno.Add(item.Key.ToString)
                Next
                acTransEx.Abort()
            End Using
            acExDb.Dispose()
        Catch ex As Exception
            retorno.Add("Model")
            Return retorno
        End Try
        Return retorno
    End Function

End Class
