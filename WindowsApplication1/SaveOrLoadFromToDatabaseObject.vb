Public MustInherit Class SaveOrLoadFromToDatabaseObject
    Public MustOverride Sub SaveToDatabase()
    Public MustOverride Sub LoadFromDatabase()
    Public MustOverride Function ConvertToHTMLDocument() As String
End Class
