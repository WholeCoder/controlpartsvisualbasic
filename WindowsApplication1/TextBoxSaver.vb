﻿Public Class TextBoxSaver
    Inherits SaveOrLoadFromToDatabaseObject

    Public tBox As TextBox

    Public Sub New(txtB As TextBox)
        tBox = txtB
    End Sub

    Public Overrides Sub SaveToDatabase()
        Throw New NotImplementedException
    End Sub

    Public Overrides Sub LoadFromDatabase()
        Throw New NotImplementedException
    End Sub

    Public Overrides Function ConvertToHTMLDocument() As String
        Throw New NotImplementedException
    End Function
End Class