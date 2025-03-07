public class CheckPointUI {
    public Window wind;
    public CheckPointUI(Window wind) {
        this.wind = wind;
    }
    
    public Page CreatePage(string name, Action<Page>? action = null){
        action ??= (page) => { };
        Page mold;
        switch (name) {
            case "login":
                mold = wind.InsertPage("CHECKPOINT", action);
                Form login = mold.InsertLabel<Form>("Ingrese sus datos a continuación:", ("id", "loginform"));
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
            default:
                mold = wind.InsertPage("ERROR 404", action);
                return mold;
        }
    }
}