Imports System.Reflection
Imports System.Reflection.Emit

Class MainWindow

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Dim dGrid As New DataGrid
        dGrid.AutoGenerateColumns = False
        '        dGrid.Width = 400

        '        dGrid.Columns.Add(
        '            New DataGridTextColumn())
        '
        '        dGrid.Items.Add("John")
        '        dGrid.Items.Add("Jill")
        Dim properties As Dictionary(Of String, Type) = New Dictionary(Of String, Type)
        properties.Add("Name", GetType(String))
        properties.Add("Age", GetType(String))
        Dim myClazz As Type = CreateClass("MyClazz", properties)

        Dim args() As Object = {}

        Dim o As Object = Activator.CreateInstance(myClazz, args)
        o.Name = "Ruben"
        o.Age = "38"

        Dim o1 As Object = Activator.CreateInstance(myClazz, args)
        o1.Name = "Ruth"
        o1.Age = "35"




        Dim col1 As DataGridTextColumn =
                New DataGridTextColumn()
        col1.Width = 200
        col1.Binding = New Binding("Name")
        col1.Header = "Name"

        Dim col2 As DataGridTextColumn =
                New DataGridTextColumn()
        col2.Width = 200
        col2.Binding = New Binding("Age")
        col2.Header = "Age"

        dGrid.Columns.Add(col1)
        dGrid.Columns.Add(col2)

        Dim dta1 = New With {.Name = "John", .Age = "25"}
        Dim dta2 = New With {.Name = "Jill", .Age = "29"}

        Dim dataList = {o, o1}.ToList()

        dGrid.ItemsSource = dataList

        '        dGrid.Items.Add(
        '            dta1)
        '        dGrid.Items.Add(
        '            dta2)


        '        Dim dataGridColumn As DataGridTextColumn = dGrid.Columns(0)
        '        dataGridColumn.Binding = New Binding(".")
        '
        cnvs.Children.Add(dGrid)
        Canvas.SetTop(dGrid, 100.0)
        Canvas.SetLeft(dGrid, 100.0)


    End Sub

    Public Shared Function CreateClass(ByVal className As String, ByVal properties As Dictionary(Of String, Type)) As Type

        Dim myDomain As AppDomain = AppDomain.CurrentDomain
        Dim myAsmName As New AssemblyName("MyAssembly")
        Dim myAssembly As AssemblyBuilder = myDomain.DefineDynamicAssembly(myAsmName, AssemblyBuilderAccess.Run)

        Dim myModule As ModuleBuilder = myAssembly.DefineDynamicModule("MyModule")

        Dim myType As TypeBuilder = myModule.DefineType(className, TypeAttributes.Public)

        myType.DefineDefaultConstructor(MethodAttributes.Public)

        For Each o In properties

            Dim prop As PropertyBuilder = myType.DefineProperty(o.Key, PropertyAttributes.HasDefault, o.Value, Nothing)

            Dim field As FieldBuilder = myType.DefineField("_" + o.Key, o.Value, FieldAttributes.[Private])

            Dim getter As MethodBuilder = myType.DefineMethod("get_" + o.Key, MethodAttributes.[Public] Or MethodAttributes.SpecialName Or MethodAttributes.HideBySig, o.Value, Type.EmptyTypes)
            Dim getterIL As ILGenerator = getter.GetILGenerator()
            getterIL.Emit(OpCodes.Ldarg_0)
            getterIL.Emit(OpCodes.Ldfld, field)
            getterIL.Emit(OpCodes.Ret)

            Dim setter As MethodBuilder = myType.DefineMethod("set_" + o.Key, MethodAttributes.[Public] Or MethodAttributes.SpecialName Or MethodAttributes.HideBySig, Nothing, New Type() {o.Value})
            Dim setterIL As ILGenerator = setter.GetILGenerator()
            setterIL.Emit(OpCodes.Ldarg_0)
            setterIL.Emit(OpCodes.Ldarg_1)
            setterIL.Emit(OpCodes.Stfld, field)
            setterIL.Emit(OpCodes.Ret)

            prop.SetGetMethod(getter)
            prop.SetSetMethod(setter)

        Next

        Return myType.CreateType()

    End Function
End Class
