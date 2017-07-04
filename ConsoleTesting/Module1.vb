Imports System.Data.SqlClient
Imports System.Runtime.Remoting.Channels
Imports System.Text.RegularExpressions
Imports WindowsApplication1
Imports System.Data

Module Module1
    Sub Main()

        Dim objConnection As SqlConnection = New SqlConnection("Server=localhost" & "\SQLEXPRESS;Database=ControlParts;" &
                                                               "User ID=sa;Password=ssGood&Plenty;")
        ' Open the database connection.
        objConnection.Open() ' .. Use the connection' Close the database

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.Connection = objConnection
        objCommand.CommandText = "INSERT INTO authors " & "(au_lname, au_fname, contract) " &
                                                    "VALUES('Barnes', 'David', 1)"
        objCommand.ExecuteNonQuery()

        objConnection.Close()

        Console.WriteLine("Press any key to continue.")
        Console.ReadLine()
    End Sub
End Module
