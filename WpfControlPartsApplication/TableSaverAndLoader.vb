
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Windows.Controls

Namespace WpfControlPartsApplication


    Public Class TableSaverAndLoader
        Inherits SaveOrLoadFromToDatabaseObject

        Public tBoxs As List(Of List(Of System.Windows.Controls.TextBox))
        Public textBoxTypeStrings As List(Of String)
        Public tableFormatString As String
        Public table_id As Integer

        Public dGrid As DataGrid

        Public Sub New()
            tBoxs = New List(Of List(Of Controls.TextBox))
            textBoxTypeStrings = New List(Of String)
            '        tableFormatString = tblFormatString
            '        table_id = t_id
        End Sub

        Public Sub AddTableFormatString(tftString As String)
            textBoxTypeStrings.Add(tftString)
        End Sub

        Public Sub Add(tb As List(Of Controls.TextBox))
            tBoxs.Add(tb)
        End Sub

        Public Overrides Sub SaveToDatabase()
            Dim connectionString As String = "Server = localhost" & "\SQLEXPRESS;Database=ControlParts;" & "User ID=sa;Password=ssGood&Plenty;"

            Dim str As String = CreateInsertIntoTable(tBoxs.Item(0))

            For Each listOfTextBoxes As List(Of Controls.TextBox) In tBoxs

                Using connection As New SqlConnection(connectionString)
                    connection.Open()


                    Dim obj As SqlCommand
                    obj = connection.CreateCommand()

                    Dim count As Integer = 0
                    For Each ent As String In textBoxTypeStrings

                        Dim fParam As SqlParameter
                        If textBoxTypeStrings(count).Split(":")(2).Equals("string") Then
                            fParam = New SqlParameter("@" & textBoxTypeStrings(count).Split(":")(1), SqlDbType.VarChar, 100)
                        ElseIf textBoxTypeStrings(count).Split(":")(2).Equals("string") Then
                            fParam = New SqlParameter("@" & textBoxTypeStrings(count).Split(":")(1), SqlDbType.VarChar, 100)
                        Else
                            fParam = New SqlParameter("@" & textBoxTypeStrings(count).Split(":")(1), SqlDbType.DateTime, 100)
                        End If
                        fParam.Value = listOfTextBoxes.Item(count).Text
                        obj.Parameters.Add(fParam)
                        count = count + 1
                    Next

                    obj.CommandText = str

                    obj.Prepare()
                    obj.ExecuteNonQuery()
                    connection.Close()

                End Using
            Next

        End Sub

        Private Function CreateInsertIntoTable(listOfTextBoxes As List(Of Controls.TextBox)) As String

            Dim str As String = "INSERT INTO " & DatabaseInteractionApi.ReturnTableNameFromId(table_id) & " "
            str &= "("
            Dim count As Integer = 0
            For Each ent As String In textBoxTypeStrings
                str &= textBoxTypeStrings(count).Split(":")(1) & ","
                count = count + 1
            Next

            If str.LastIndexOf(",") = str.Length - 1 Then
                str = str.Substring(0, str.Length - 1)
            End If
            str &= ") VALUES ("
            count = 0

            For Each ent As String In textBoxTypeStrings

                If textBoxTypeStrings(count).Split(":")(2).Equals("string") Then
                    str &= "@" & textBoxTypeStrings(count).Split(":")(1) & ","
                ElseIf textBoxTypeStrings(count).Split(":")(2).Equals("datetime") Then
                    str &= "@" & textBoxTypeStrings(count).Split(":")(1) & ","
                End If

                count = count + 1
            Next
            If str.LastIndexOf(",") = str.Length - 1 Then
                str = str.Substring(0, str.Length - 1)
            End If
            str &= ");"
            '        MessageBox.Show(str, "The Lorax",
            '                        MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk)
            Return str
        End Function

        Public Property myClazz As Type

        Public Overrides Sub LoadFromDatabase()
            Dim queryString As String = GetQueryString()
            Dim connectionString As String = "Server = localhost" & "\SQLEXPRESS;Database=ControlParts;" & "User ID=sa;Password=ssGood&Plenty;"

            Dim dataList = {}.ToList()

            Using connection As New SqlConnection(connectionString)
                Dim command As New SqlCommand(queryString, connection)
                connection.Open()

                Dim reader As SqlDataReader = command.ExecuteReader()

                ' Call Read before accessing data.
                Dim c As Integer = 0
                While reader.Read()

                    Dim cc As Integer = 0


                    '                    Dim listOfTextBoxes = tBoxs.Item(c)



                    Dim args() As Object = {}

                    Dim o As Object = Activator.CreateInstance(myClazz, args)

                    For Each ent As String In textBoxTypeStrings

                        Dim prop As PropertyInfo = myClazz.GetProperty(ent.Split(":")(1))
                        prop.SetValue(o, reader(cc), Nothing)

                        '                        listOfTextBoxes(cc).Text = reader(cc)
                        cc += 1
                    Next
                    dataList.Add(o)
                    c += 1
                End While

                connection.Close()


            End Using
            dGrid.ItemsSource = dataList
            '        MessageBox.Show(queryString, "The Lorax",
            '                        MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk)
        End Sub

        Private Property ListOfColumnNames As List(Of String) = New List(Of String)

        Private Function GetQueryString() As String

            Dim queryString As String = "SELECT "

            For Each ent As String In textBoxTypeStrings
                queryString &= ent.Split(":")(1) + ","
                ListOfColumnNames.Add(ent.Split(":")(1))
            Next

            queryString = queryString.Substring(0, queryString.Count - 1)

            queryString &= " FROM " & DatabaseInteractionApi.ReturnTableNameFromId(table_id)
            Return queryString
        End Function

        Public Overrides Function ConvertToHTMLDocument() As String

            Dim strng As String = ""

            Dim lst = dGrid.ItemsSource
            For Each item As Object In lst
                For Each ent As String In textBoxTypeStrings
                    Dim prop As PropertyInfo = myClazz.GetProperty(ent.Split(":")(1))
                    Dim objectsField = prop.GetValue(item, Nothing)

                    strng &= objectsField & ","
                Next
                strng &= "\n"
            Next

            Return strng
        End Function
    End Class

End Namespace
