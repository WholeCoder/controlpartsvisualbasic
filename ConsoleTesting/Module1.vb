﻿Imports System.Text.RegularExpressions
Imports WindowsApplication1

Module Module1
     Sub Main()
        Dim template As String = "<!DOCTYPE html><html><head><title>Test Template</title></head><body><table>|table:rubenstable|<tr><td colspan=""3"">Test 3 Header</td></tr>*<tr><td>col</td><td>col 2</td><td>col3</td></tr>*<tr><td>%column:rubenstale%</td><td>%column:ruthstale%</td><td></td></tr>|</table></body></html>"
        Dim templateHash As Hashtable = TemplateParserUtilitiy.ParseHashTableOfElements(template, "|", "*")
        Dim documentStructureList As List(Of String) = templateHash.Item("documentstructure")

        Console.WriteLine("Count:  " & documentStructureList.Count)
        Console.WriteLine("Part 1 of Document:  " + documentStructureList(0))
        Console.WriteLine("Part 2 of Document:  " + documentStructureList(1))
        Console.WriteLine("Part 3 of Document:  " + documentStructureList(2))
        'TypeOf refForm Is System.Windows.Forms.Form)
        '        Console.WriteLine((TypeOf (New List(Of String)) Is List(Of String)).ToString())
        Console.ReadKey()
    End Sub
End Module
