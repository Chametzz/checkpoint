public class LoginPage : Page
{
    public LoginPage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        Form login = InsertLabel<Form>("Ingrese sus datos a continuación:", ("name", "LOGINFORM"));
        login.InsertChild<Input>("Usuario:", ("type", "text"), ("name", "USERNAME"), ("required", "true"));
        login.InsertChild<Input>("Contraseña:", ("type", "password"), ("name", "PASSWORD"), ("required", "true"));
        login.InsertChild<Input>("Iniciar sesión", ("type", "submit"));
    }
}

public class HomePage : Page
{
    public HomePage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        SetTitle("INICIO");
        InsertLabel<Label>("¡Bienvenido!", ("ref", "welcome"));
        InsertLabel<Label>("ID:", ("ref", "id"));
        InsertLabel<Label>("Nombre:", ("ref", "first_name"));
        InsertLabel<Label>("Apellido:", ("ref", "last_name"));
        InsertLabel<Label>("Sexo:", ("ref", "sex"));
        InsertLabel<Label>("Fecha de nacimiento:", ("ref", "birthdate"));
        InsertLabel<Label>("Teléfono:", ("ref", "phone_no"));
        InsertLabel<Label>("Correo electrónico:", ("ref", "email"));
        InsertLabel<Label>("Dirección:", ("ref", "address"));
        InsertLabel<Label>("Fecha de contratación:", ("ref", "hiredate"));
        InsertLabel<Label>("Departamento:", ("ref", "workdept"));
        InsertLabel<Label>("Trabajo:", ("ref", "job"));
        InsertLabel<Label>("Salario:", ("ref", "salary"));
        InsertLabel<Button>("Cerrar sesión", ("name", "LOGOUTBUTTON"));
    }
}

public class AdminPage : Page {
    public AdminPage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        SetTitle("ADMINISTRACIÓN");
        InsertLabel<Input>("Seleccione un departamento", ("type", "select"), ("name", "WORKDEP"));
    }
}

public class RegEmpPage : Page
{
    public RegEmpPage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        SetTitle("REGISTRAR EMPLEADO");
        Form register = InsertLabel<Form>("Registre los campos del aspirante:", ("name", "REGISTERFORM"));
        register.InsertChild<Input>("Nombre(s):", ("type", "text"), ("name", "FIRSTNAME"), ("required", "true"));
        register.InsertChild<Input>("Apellido(s):", ("type", "text"), ("name", "LASTNAME"), ("required", "true"));
        Input sex = register.InsertChild<Input>("Sexo:", ("type", "select"), ("name", "SEX"), ("required", "true"));
        sex.InsertChild<Label>("HOMBRE", ("value", "HOMBRE"));
        sex.InsertChild<Label>("MUJER", ("value", "MUJER"));
        sex.InsertChild<Label>("OTRO", ("value", "OTRO"));
        register.InsertChild<Input>("Fecha de nacimiento:", ("type", "text"), ("name", "BIRTHDATE"), ("required", "true"));
        register.InsertChild<Input>("Número de teléfono:", ("type", "number"), ("name", "PHONENO"), ("required", "true"));
        register.InsertChild<Input>("Correo electrónico:", ("type", "text"), ("name", "EMAIL"), ("required", "true"));
        register.InsertChild<Input>("Dirección:", ("type", "text"), ("name", "ADDRESS"), ("required", "true"));
        register.InsertChild<Input>("Departamento:", ("type", "select"), ("name", "WORKDEPT"), ("required", "true"));
        register.InsertChild<Input>("Puesto:", ("type", "select"), ("name", "JOB"), ("required", "true"));
        register.InsertChild<Input>("Contraseña:", ("type", "select"), ("name", "PASSWORD"), ("required", "true"));
        register.InsertChild<Input>("Contraseña:", ("type", "select"), ("name", "VERIFYPASS"), ("required", "true"));
        register.InsertChild<Input>("Registrar", ("type", "submit"));
    }
}

public class EditEmpPage : Page
{
    public EditEmpPage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        SetTitle("EDITAR EMPLEADO");
        Form register = InsertLabel<Form>("Registre los campos del aspirante:", ("name", "EDITFORM"));
        register.InsertChild<Input>("Nombre(s):", ("type", "text"), ("name", "FIRSTNAME"), ("required", "true"));
        register.InsertChild<Input>("Apellido(s):", ("type", "text"), ("name", "LASTNAME"), ("required", "true"));
        Input sex = register.InsertChild<Input>("Sexo:", ("type", "select"), ("name", "SEX"), ("required", "true"));
        sex.InsertChild<Label>("HOMBRE", ("value", "HOMBRE"));
        sex.InsertChild<Label>("MUJER", ("value", "MUJER"));
        sex.InsertChild<Label>("OTRO", ("value", "OTRO"));
        register.InsertChild<Input>("Fecha de nacimiento:", ("type", "text"), ("name", "BIRTHDATE"), ("required", "true"));
        register.InsertChild<Input>("Número de teléfono:", ("type", "number"), ("name", "PHONENO"), ("required", "true"));
        register.InsertChild<Input>("Correo electrónico:", ("type", "text"), ("name", "EMAIL"), ("required", "true"));
        register.InsertChild<Input>("Dirección:", ("type", "text"), ("name", "ADDRESS"), ("required", "true"));
        register.InsertChild<Input>("Departamento:", ("type", "select"), ("name", "WORKDEPT"), ("required", "true"));
        register.InsertChild<Input>("Puesto:", ("type", "select"), ("name", "JOB"), ("required", "true"));
        register.InsertChild<Input>("Nueva Contraseña (opcional):", ("type", "select"), ("name", "PASSWORD"), ("required", "true"));
        register.InsertChild<Input>("Editar", ("type", "submit"));
    }
}

public class DelEmpPage : Page
{
    public DelEmpPage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        SetTitle("ELIMINAR EMPLEADO");
        Form delform = InsertLabel<Form>("¡ATENCIÓN, LOS DATOS NO PODRÁN RECUPERARSE!", ("name", "DELETEFORM"));
        delform.InsertChild<Input>("Introduzca el id del empleado que desea eliminar:", ("name", "ID"), ("type", "number"), ("required", "true"));
        delform.InsertChild<Input>("Eliminar", ("type", "submit"));
    }
}

public class CheckEmployee : DB {

}