

public class RegEmpPage : Page
{
    public RegEmpPage(Window wind, string title, Action<Page>? toLoad = null) : base(wind, title, toLoad) {
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