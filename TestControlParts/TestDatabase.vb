Imports System.CodeDom.Compiler
Imports System.Data.SqlClient
Imports WindowsApplication1
Imports WindowsApplication1.TestControlParts
Imports NUnit.Framework
Imports TestControlParts.TestControlParts

Public Class TestDatabase

    <Test()>
    Public Sub testTemplateExists()
        Dim doesarr12exist As Boolean = DatabaseInteractionApi.DoesTemplateExist("arr12")

        Assert.True(doesarr12exist)
    End Sub

    <Test()>
    Public Sub testCreateNewTemplate()
        Dim templateName As String = "a456"
        Dim tables As List(Of String) = New List(Of String)
        tables.Add("table|<tr><td>%column:rubenstale:string%column:ruthstale:string%")
        Dim cols As List(Of String) = New List(Of String)
        cols.Add("column:rubenstale:string")
        cols.Add("column:ruthstale:string")
        Dim fields As List(Of String) = New List(Of String)

        DatabaseInteractionApi.CreateNewTemplate(templateName, tables, cols, fields)
    End Sub
End Class