Public Class TextBoxFieldSaver
    Inherits SaveOrLoadFromToDatabaseObject

    Public tBox As TextBox
    Public textBoxTypeString As String

    Public Sub New(tBoxTypeString As String, txtB As TextBox)
        tBox = txtB
        textBoxTypeString = tBoxTypeString
    End Sub

    Public Overrides Sub SaveToDatabase()
        Throw New NotImplementedException
    End Sub

    Public Overrides Sub LoadFromDatabase()
        Throw New NotImplementedException
    End Sub

    Public Overrides Function ConvertToHTMLDocument() As String
        Throw New NotImplementedException()
    End Function
End Class