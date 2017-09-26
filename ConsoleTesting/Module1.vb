Imports System
Imports System.Reflection
Imports System.Reflection.Emit

Module Module1

    Public Sub Main()
        Dim numbers = New Integer() {1, 2, 4, 8}
        For i As Integer = 0 To numbers.Count() - 1
            Console.WriteLine(numbers(i))
        Next
        Console.ReadKey()
    End Sub
End Module

' This code produces the following output:
'
'o1.Number: 42
'o1.Number: 127
'o1.MyMethod(22): 2794
'o2.Number: 5280