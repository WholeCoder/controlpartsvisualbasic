Public Class TableRowSaver
    Inherits SaveToDatabaseObject

    Public tBox As List(Of TextBox)
    Public textBoxTypeString As List(Of String)
    Public tableFormatString As String
    Public table_id As Integer

    Public Sub New(tblFormatString As String, t_id As Integer)
        tBox = New List(Of TextBox)
        textBoxTypeString = New List(Of String)
        tableFormatString = tblFormatString
        table_id = t_id
    End Sub

    Public Sub Add(tb, tbTypeString)
        tBox.Add(tb)
        textBoxTypeString.Add(tbTypeString)
    End Sub

    Public Overrides Sub SaveToDatabase()
        Throw New NotImplementedException
    End Sub
End Class