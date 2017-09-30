Imports WpfControlPartsApplication
Imports WpfControlPartsApplication.WpfControlPartsApplication

Namespace WpfControlPartsApplication
    Public Class GenerateHTMLFromTextBoxOfHTML
        Inherits SaveOrLoadFromToDatabaseObject

        Public Property TBox As TextBox

        Public Overrides Sub SaveToDatabase()
            Throw New NotImplementedException
        End Sub

        Public Overrides Sub LoadFromDatabase()
            Throw New NotImplementedException
        End Sub

        Public Overrides Function ConvertToHTMLDocument() As String
            Return TBox.Text
        End Function
    End Class
End Namespace
