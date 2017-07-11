Public Class TextBoxSaver
    Inherits SaveToDatabaseObject

    Public tBox As TextBox

    Public Sub New(txtB As TextBox)
        tBox = txtB
    End Sub

    Public Overrides Sub SaveToDatabase()
        Throw New NotImplementedException
    End Sub
End Class