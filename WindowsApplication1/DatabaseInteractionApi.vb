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

        Public Shared Sub CreateNewTemplate(tName As String, tText As String, tbls As List(Of String), cls As List(Of String), flds As List(Of String))

            Dim templateName As String = tName
            Dim templateText As String = tText
            Dim tables As List(Of String) = tbls
            Dim cols As List(Of String) = cls
            Dim fields As List(Of String) = flds

            Dim templateInsertString As String = "INSERT INTO templates (name, template_text) VALUES ('" & templateName & "','" & templateText & "')"

            For Each table As String In tables
                Dim tablesInsertString As String = "INSERT INTO tables (templatenameandfields, template_id) VALUES ('" & table & "',1)"


                Dim colInsertString As String = "INSERT INTO col VALUES ('rubenstable:string', 1)"
                Dim colInsertSTring2 As String = "INSERT INTO col VALUES ('ruthstable:string', 1)"

                cols.Add(colInsertString)
                cols.Add(colInsertSTring2)


            Next

        End Sub

        Private Sub createNewDatabaseTable(connectionString As String, tableName As String, tableColumnNames As List(Of String))

            Using connection As New SqlConnection(connectionString)
                connection.Open()
                ' tableName = table|<tr><td>%column:rubenstale : Stringcolumn: ruthstale : String


                Dim obj As SqlCommand
                Dim strSQL As String
                obj = connection.CreateCommand()
                Dim tbl() As String = tableName.Split("|")
                strSQL = "CREATE TABLE " & tbl(1) & "  (" &
                         "Id int NOT NULL PRIMARY KEY IDENTITY(1,1), "

                For Each colName As String In tableColumnNames
                    Dim colNameSplit() As String = colName.Split(":")
                    Dim currColType = ""
                    If (colNameSplit(2).Equals("string")) Then
                        currColType = "VARCHAR(255)"
                    Else

                    End If
                    strSQL += colNameSplit(1) & " " & currColType & ","
                Next
                If strSQL.LastIndexOf(",") = strSQL.Length - 1 Then
                    strSQL = strSQL.Substring(0, strSQL.Length - 1)
                End If


                '                "LastName  VARCHAR(30), " &
                '                         "FirstName VARCHAR(20), " &
                '                         "Address   VARCHAR(50) " &
                '                
                strSQL += ") "

                obj.CommandText = strSQL
                obj.ExecuteNonQuery()
                connection.Close()


            End Using
        End Sub

        Private Shared Function ReadSingleRow(ByVal record As IDataRecord) As String
            Return String.Format("{0}", record(0), record(1))

        End Function
    End Class
End NameSpace