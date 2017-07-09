Imports System.CodeDom.Compiler
Imports System.Data.SqlClient
Imports WindowsApplication1
Imports NUnit.Framework

Public Class TestDatabase
    <Test()>
    Public Sub testTemplateExists()
        '        Dim str As String = "Server = localhost" & "\SQLEXPRESS;Database=ControlParts;" &
        '                                      "User ID=sa;Password=ssGood&Plenty;"
        '        ReadOrderData(str)
        Dim queryString As String =
                "Select * from templates Where name = 'arr12';"
        Dim connectionString As String = "Server = localhost" & "\SQLEXPRESS;Database=ControlPartsTest;" & "User ID=sa;Password=ssGood&Plenty;"
        Dim tableName As String = ""
        Using connection As New SqlConnection(connectionString)
            Dim command As New SqlCommand(queryString, connection)
            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader()

            ' Call Read before accessing data.
            While reader.Read()
                tableName = ReadSingleRow(CType(reader, IDataRecord))
                Console.WriteLine(tableName)
            End While
            reader.Close()

            connection.Close()
        End Using
        Assert.True(tableName.Equals("arr12"))
    End Sub

    Private Function ReadSingleRow(ByVal record As IDataRecord) As String
        Return String.Format("{0}", record(0), record(1))

    End Function

End Class