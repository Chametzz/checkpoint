
public class LoginPage : Page
{
    public LoginPage(Window wind, string title, Action<Page>? toLoad = null) : base(wind, title, toLoad) {
        Form login = InsertLabel<Form>("Ingrese sus datos a continuación:", ("name", "LOGINFORM"));
        login.InsertChild<Input>("Usuario:", ("type", "text"), ("name", "USERNAME"), ("required", "true"));
        login.InsertChild<Input>("Contraseña:", ("type", "password"), ("name", "PASSWORD"), ("required", "true"));
        login.InsertChild<Input>("Iniciar sesión", ("type", "submit"));
    }
}