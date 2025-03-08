public class HomePage : Page
{
    public HomePage(Window wind, string title, Action<Page>? toLoad = null) : base(wind, title, toLoad) {
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