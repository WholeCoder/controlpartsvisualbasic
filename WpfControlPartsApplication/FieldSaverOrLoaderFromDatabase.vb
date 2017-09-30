Imports WpfControlPartsApplication.WpfControlPartsApplication

Public Class FieldSaverOrLoaderFromDatabase
    Inherits SaveOrLoadFromToDatabaseObject

    Public Property FieldTypeString As TextBox
    Public Property FieldValue As TextBox

    Public Overrides Sub SaveToDatabase()
        Throw New NotImplementedException
    End Sub

    Public Overrides Sub LoadFromDatabase()
        Throw New NotImplementedException
    End Sub

    Public Overrides Function ConvertToHTMLDocument() As String
        Return FieldValue.Text
    End Function
End Class
