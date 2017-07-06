
Option Explicit On
Option Strict On

Imports System.Data
Imports System.Data.SqlClient

Module Module1

    Sub Main()
        Dim str As String = "Server = localhost" & "\SQLEXPRESS;Database=ControlParts;" &
                                      "User ID=sa;Password=ssGood&Plenty;"
        ReadOrderData(str)

    End Sub

    Private Sub ReadOrderData(ByVal connectionString As String)
        Dim queryString As String =
            "Select * From sys.tables Where name = 'authors' AND type = 'U';"
        Dim createTableString = "CREATE TABLE " & "ControlParts" & ". People (" &
                                         "Id int NOT NULL PRIMARY KEY, " &
                                         "LastName  VARCHAR(30), " &
                                         "FirstName VARCHAR(20), " &
                                         "Address   VARCHAR(50) " &
                                         ") "

        Using connection As New SqlConnection(connectionString)
            Dim command As New SqlCommand(queryString, connection)
            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader()

            ' Call Read before accessing data.
            While reader.Read()
                ReadSingleRow(CType(reader, IDataRecord))
            End While

            Dim obj As SqlCommand
            Dim strSQL As String
            obj = connection.CreateCommand()
            strSQL = "CREATE TABLE " & "ControlParts" & "  (" &
                     "Id int NOT NULL PRIMARY KEY, " &
                     "LastName  VARCHAR(30), " &
                     "FirstName VARCHAR(20), " &
                     "Address   VARCHAR(50) " &
                     ") "
            ' Execute
            ' Call Close when done reading.
            reader.Close()

            obj.CommandText = strSQL
            obj.ExecuteNonQuery()
            connection.Close()

            Console.WriteLine("Pres enter to continue")
            Console.ReadLine()
        End Using
    End Sub

    Private Sub ReadSingleRow(ByVal record As IDataRecord)
        Console.WriteLine(String.Format("{0}", record(0), record(1)))

    End Sub

End Module