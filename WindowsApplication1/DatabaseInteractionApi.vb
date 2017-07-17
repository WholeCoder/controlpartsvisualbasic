Imports System.Data.SqlClient


Namespace TestControlParts
    Public Class DatabaseInteractionApi
        Public Shared Function DoesTemplateExist(tName As String) As Boolean

            Dim queryString As String =
                    "Select * from templates Where name = '" & tName & "';"
            Dim connectionString As String = "Server = localhost" & "\SQLEXPRESS;Database=ControlParts;" & "User ID=sa;Password=ssGood&Plenty;"
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

        Public Shared Sub CreateNewTemplate(field_separtor As String, table_separator As String, table_column_separator As String, tName As String, tText As String, tbls As List(Of String), cls As Hashtable, flds As List(Of String))

            Dim templateName As String = tName
            Dim templateText As String = tText
            Dim tables As List(Of String) = tbls
            Dim colsHash As Hashtable = cls
            Dim fields As List(Of String) = flds
            Dim template_id As Integer = InsertTemplateAndReturnTemplateId(field_separtor, table_separator, table_column_separator, templateName, templateText)

            For Each table As String In tables
                Dim tRay() As String = table.Split(table_column_separator)
                Dim tableName As String = tRay(1)

                Dim colsForThisTable As List(Of String) = colsHash(table)
                Dim table_id = InsertTableIntoDatabaseAndReturnTableId(table, template_id)

                For Each col As String In colsForThisTable
                    Dim datatype As String = col.Split(":")(2)
                    Dim colName As String = col.Split(":")(1)

                    InsertTableColumnsIntoDatabase(colName, table_id, datatype)
                Next


            Next

        End Sub

        Public Shared Sub InsertTableColumnsIntoDatabase(col As String, table_id As Integer, datatype As String)

            Dim colInsertString As String = "INSERT INTO col (columnnamedatatype, table_id, datatype )VALUES ('" & col & "', " & table_id & ",'" & datatype & "')"
            Dim connectionString As String = "Server = localhost" & "\SQLEXPRESS;Database=ControlParts;" & "User ID=sa;Password=ssGood&Plenty;"

            Using connection As New SqlConnection(connectionString)
                connection.Open()


                Dim obj As SqlCommand
                obj = connection.CreateCommand()


                obj.CommandText = colInsertString
                obj.ExecuteNonQuery()
                connection.Close()

            End Using

        End Sub

        Public Shared Function InsertTableIntoDatabaseAndReturnTableId(table As String, templateId As String) As Integer
            Dim tablesInsertString As String = "INSERT INTO tables (templatenameandfields, template_id) VALUES ('" & table & "'," & templateId & ")"
            Dim connectionString As String = "Server = localhost" & "\SQLEXPRESS;Database=ControlParts;" & "User ID=sa;Password=ssGood&Plenty;"

            Using connection As New SqlConnection(connectionString)
                connection.Open()


                Dim obj As SqlCommand
                obj = connection.CreateCommand()


                obj.CommandText = tablesInsertString
                obj.ExecuteNonQuery()
                connection.Close()

            End Using

            Dim teble_id As Integer = -1

            Dim queryTemplateString As String =
                    "Select id From tables Where template_id = " & templateId & " AND templatenameandfields = '" & table & "';"
            Using connection As New SqlConnection(connectionString)
                Dim command As New SqlCommand(queryTemplateString, connection)
                connection.Open()

                Dim reader As SqlDataReader = command.ExecuteReader()

                ' Call Read before accessing data.

                While reader.Read()
                    teble_id = reader(0)
                End While

                connection.Close()


            End Using
            Return teble_id

        End Function

        Public Shared Function InsertTemplateAndReturnTemplateId(field_separtor As String, table_separator As String, table_column_separator As String, templateName As String, templateText As String) As Integer

            Dim connectionString As String = "Server = localhost" & "\SQLEXPRESS;Database=ControlParts;" & "User ID=sa;Password=ssGood&Plenty;"
            Dim templateInsertString As String = "INSERT INTO templates (field_separator,table_separator,table_column_separator,name, template_text) VALUES (" &
                " @fSeparator, @tSeparator, @tcSeparator, @tName, @tText)"

            Dim fParam As SqlParameter =
                    New SqlParameter("@fSeparator", SqlDbType.VarChar, 100)

            Dim tbParam As SqlParameter =
                    New SqlParameter("@tSeparator", SqlDbType.VarChar, 100.0R)

            Dim tbcParam As SqlParameter =
                    New SqlParameter("@tcSeparator", SqlDbType.VarChar, 100)
            Dim tNameParam As SqlParameter =
                    New SqlParameter("@tName", SqlDbType.VarChar, 100)
            Dim tTextParam As SqlParameter =
                    New SqlParameter("@tText", SqlDbType.VarChar, 50000)


            fParam.Value = field_separtor
            tbParam.Value = table_separator
            tbcParam.Value = table_column_separator
            tNameParam.Value = templateName
            tTextParam.Value = templateText

            ' field_separtor & "','" & table_separator & "','" & table_column_separator & "', '" & templateName & "','" & templateText & "')"            Dim connectionString As String = "Server = localhost" & "\SQLEXPRESS;Database=ControlParts;" & "User ID=sa;Password=ssGood&Plenty;"

            Using connection As New SqlConnection(connectionString)
                connection.Open()


                Dim obj As SqlCommand
                obj = connection.CreateCommand()

                obj.Parameters.Add(fParam)
                obj.Parameters.Add(tbParam)
                obj.Parameters.Add(tbcParam)
                obj.Parameters.Add(tNameParam)
                obj.Parameters.Add(tTextParam)

                obj.CommandText = templateInsertString

                obj.Prepare()
                obj.ExecuteNonQuery()
                connection.Close()

            End Using


            Dim queryTemplateString As String =
                    "Select id From templates Where name = '" & templateName & "';"
            Dim template_id As Integer = -1
            Using connection As New SqlConnection(connectionString)
                Dim command As New SqlCommand(queryTemplateString, connection)
                connection.Open()

                Dim reader As SqlDataReader = command.ExecuteReader()

                ' Call Read before accessing data.

                While reader.Read()
                    template_id = reader(0)
                End While

                connection.Close()


            End Using
            Return template_id
        End Function

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

        Public Shared Function ReturnTemplateIdIfTemplateExists(ByRef tableName As String) As Integer

            Dim queryString As String =
                    "Select id from templates Where name = '" & tableName & "';"
            Dim connectionString As String = "Server = localhost" & "\SQLEXPRESS;Database=ControlParts;" & "User ID=sa;Password=ssGood&Plenty;"

            Using connection As New SqlConnection(connectionString)
                Dim command As New SqlCommand(queryString, connection)
                connection.Open()

                Dim reader As SqlDataReader = command.ExecuteReader()

                Dim template_id As Integer = -1
                ' Call Read before accessing data.
                While reader.Read()
                    '                tableName = ReadSingleRow(CType(reader, IDataRecord))
                    template_id = reader.GetInt32(reader.GetOrdinal("id"))
                    Console.WriteLine(reader(0))
                End While
                reader.Close()

                connection.Close()

                Return template_id
            End Using
        End Function

    End Class
End Namespace