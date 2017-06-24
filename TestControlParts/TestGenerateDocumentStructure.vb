Imports System.CodeDom.Compiler
Imports WindowsApplication1
Imports NUnit.Framework

Public Class TestGenerateDocumentStructure
    <Test()>
    Public Sub SeeIfTableArgumentsCanBeParsedOutIntoObjects()
        Dim template As String = "<!DOCTYPE html><html><head><title>Test Template</title></head><body><table>|table:rubenstable|<tr><td colspan=""3"">Test 3 Header</td></tr>*<tr><td>col</td><td>col 2</td><td>col3</td></tr>*<tr><td>%column:rubenstale%</td><td>%column:ruthstale%</td><td></td></tr>|</table></body></html>"
        Dim templateHash As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(template, "|", "*")
        Dim documentStructureList As List(Of String) = templateHash.Item("documentstructure")

        Assert.True(3 = documentStructureList.Count)
        Console.WriteLine("<!DOCTYPE html><html><head><title>Test Template</title></head><body><table>".Equals(documentStructureList(0)))
        Console.WriteLine("table:rubenstable".Equals(documentStructureList(1)))
        Console.WriteLine("</table></body></html>".Equals(documentStructureList(2)))
    End Sub
End Class
