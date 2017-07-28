Imports WindowsApplication1.TestControlParts

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
        Dim str As String = "INSERT INTO " & DatabaseInteractionApi.ReturnTableNameFromId(table_id) & " "
        str &= "("
        Dim count As Integer = 0
        For Each ent As String In textBoxTypeStrings
            str &= textBoxTypeStrings(count).Split(":")(1) & ","
            count = count + 1
        Next

        If str.LastIndexOf(",") = str.Length - 1 Then
            str = str.Substring(0, str.Length - 1)
        End If
        str &= ") VALUES ("
        count = 0
        Dim listOfTextBoxes As List(Of TextBox) = tBoxs.Item(0)
        For Each ent As String In textBoxTypeStrings

            If textBoxTypeStrings(count).Split(":")(2).Equals("string") Then
                str &= "'" & listOfTextBoxes.Item(count).Text & "',"
            ElseIf textBoxTypeStrings(count).Split(":")(2).Equals("datetime") Then
                str &= "'" & listOfTextBoxes.Item(count).Text & "',"
            End If

            count = count + 1
        Next
        If str.LastIndexOf(",") = str.Length - 1 Then
            str = str.Substring(0, str.Length - 1)
        End If
        str &= ");"
        MessageBox.Show(str, "The Lorax",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk)
    End Sub

    Public Overrides Sub LoadFromDatabase()
        Throw New NotImplementedException
    End Sub
End Class