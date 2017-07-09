Imports System.Data.SqlClient

Namespace TestControlParts
    Public Class DatabaseInteractionApi
        Public Shared Function DoesTemplateExist(tName As String) As Boolean

            Dim queryString As String =
                    "Select * from templates Where name = '" & tName & "';"
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
            Return tName.Equals(tableName)
        End Function

        Public Shared Sub CreateNewTemplate(tName As String, tbls As List(Of String), cls As List(Of String), flds As List(Of String))

            Dim templateName As String = tName
            Dim tables As List(Of String) = tbls
            Dim cols As List(Of String) = cls
            Dim fields As List(Of String) = flds

            Dim templateInsertString As String = "INSERT INTO templates VALUES ( ('" & templateName & "'))"


        End Sub

        Private Function ReadSingleRow(ByVal record As IDataRecord) As String
            Return String.Format("{0}", record(0), record(1))

        End Function
    End Class
End NameSpace