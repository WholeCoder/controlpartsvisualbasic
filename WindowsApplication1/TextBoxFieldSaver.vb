﻿Public Class TextBoxFieldSaver
    Inherits SaveToDatabaseObject

    Public tBox As TextBox
    Public textBoxTypeString As String

    Public Sub New(tBoxTypeString As String, txtB As TextBox)
        tBox = txtB
        textBoxTypeString = tBoxTypeString
    End Sub

    Public Overrides Sub SaveToDatabase()
        Throw New NotImplementedException
    End Sub
End Class