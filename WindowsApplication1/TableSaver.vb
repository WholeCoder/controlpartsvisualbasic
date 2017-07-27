Public Class TableSaver
    Inherits SaveToDatabaseObject

    Public tBoxs As List(Of List(Of TextBox))
    Public textBoxTypeStrings As List(Of String)
    Public tableFormatString As String
    Public table_id As Integer

    Public Sub New()
        tBoxs = New List(Of List(Of TextBox))
        textBoxTypeStrings = New List(Of String)
        '        tableFormatString = tblFormatString
        '        table_id = t_id
    End Sub

    Public Sub AddTableFormatString(tftString As String)
        textBoxTypeStrings.Add(tftString)
    End Sub

    Public Sub Add(tb As List(Of TextBox))
        tBoxs.Add(tb)
    End Sub

    Public Overrides Sub SaveToDatabase()
        MessageBox.Show(tableFormatString & "      " & table_id, "The Lorax",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk)
    End Sub

    Public Overrides Sub LoadFromDatabase()
        Throw New NotImplementedException
    End Sub
End Class