public class CheckPointUI { //Crea un objeto de esto en CheckPointUI, te pedira un objeto de tipo wind, crea uno nuevo, después accede a las vistas usando CreatePage("nombre", () => {})
    public Window wind; //Al final accede a su wind y ejecuta el wind.Execute()
    public CheckPointUI(Window wind) {
        this.wind = wind;
    }
    
    public Page CreatePage(string name, Action<Page>? action = null){
        action ??= (page) => { };
        Page mold;
        switch (name) {
            case "login":
                mold = wind.InsertPage("CHECKPOINT", action);
                Form login = mold.InsertLabel<Form>("Ingrese sus datos a continuación:", ("name", "LOGINFORM"));
                login.InsertChild<Input>("Usuario:", ("type", "text"), ("name", "USERNAME"), ("required", "true"));
                login.InsertChild<Input>("Contraseña:", ("type", "password"), ("name", "PASSWORD"), ("required", "true"));
                login.InsertChild<Input>("Iniciar sesión", ("type", "submit"));
                return mold;
            case "home":
                mold = wind.InsertPage("EMPLEADO", action);
                mold.InsertLabel<Label>("¡Bienvenido!", ("ref", "welcome"));
                mold.InsertLabel<Label>("ID:", ("ref", "id"));
                mold.InsertLabel<Label>("Nombre:", ("ref", "first_name"));
                mold.InsertLabel<Label>("Apellido:", ("ref", "last_name"));
                mold.InsertLabel<Label>("Sexo:", ("ref", "sex"));
                mold.InsertLabel<Label>("Fecha de nacimiento:", ("ref", "birthdate"));
                mold.InsertLabel<Label>("Teléfono:", ("ref", "phone_no"));
                mold.InsertLabel<Label>("Correo electrónico:", ("ref", "email"));
                mold.InsertLabel<Label>("Dirección:", ("ref", "address"));
                mold.InsertLabel<Label>("Fecha de contratación:", ("ref", "hiredate"));
                mold.InsertLabel<Label>("Departamento:", ("ref", "workdept"));
                mold.InsertLabel<Label>("Trabajo:", ("ref", "job"));
                mold.InsertLabel<Label>("Salario:", ("ref", "salary"));
                mold.InsertLabel<Button>("Cerrar sesión", ("id", "logoutbutton"));
                return mold;
            case "admin":
                mold = wind.InsertPage("ADMINISTRACIÓN", action);
                return mold;
            case "register employee":
                mold = wind.InsertPage("REGISTRAR EMPLEADO", action);
                Form register = mold.InsertLabel<Form>("Rellena los campos del empleado:", ("name", "REGISTERFORM"));
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
                register.InsertChild<Input>("Registrar", ("type", "submit"));
                return mold;
            default:
                mold = wind.InsertPage("ERROR 404", action);
                return mold;
        }
    }
}