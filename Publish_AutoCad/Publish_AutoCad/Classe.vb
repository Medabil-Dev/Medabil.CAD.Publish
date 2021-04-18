Public Class Prancha
    Public Dwgname As String = ""
    Public Layout As String = ""
    Public Title As String = ""
End Class
Public Class Pacote
    Public Pranchas As New List(Of Prancha)
End Class
Public Class Job
    Public Pacotes As New List(Of Pacote)
End Class

