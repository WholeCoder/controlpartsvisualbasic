Public Class TableSaver
    Inherits SaveToDatabaseObject

    Public tBoxs As List(Of List(Of TextBox))
    Public textBoxTypeStrings As List(Of List(Of String))
    Public tableFormatString As String
    Public table_id As Integer

    Public Sub New(tblFormatString As String, t_id As Integer)
        tBoxs = New List(Of List(Of TextBox))
        textBoxTypeStrings = New List(Of List(Of String))
        tableFormatString = tblFormatString
        table_id = t_id
    End Sub

    Public Sub Add(tb, tbTypeString)
        tBoxs.Add(tb)
        textBoxTypeStrings.Add(tbTypeString)
    End Sub

    Public Overrides Sub SaveToDatabase()
        Throw New NotImplementedException
    End Sub
End Class