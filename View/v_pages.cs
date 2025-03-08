public class CheckPointUI
{ //Crea un objeto de esto en CheckPointUI, te pedira un objeto de tipo wind, crea uno nuevo, después accede a las vistas usando CreatePage("nombre", () => {})
    public Window wind; //Al final accede a su wind y ejecuta el wind.Execute()
    public CheckPointUI(Window wind)
    {
        this.wind = wind;
    }

    public Page CreatePage(string name, Action<Page>? action = null)
    {
        action ??= (page) => { };
        Page mold;
        switch (name)
        {
            case "login":
                return wind.InsertPage(new LoginPage(wind, "CHECKPOINT", action));
            case "home":
                return wind.InsertPage(new HomePage(wind, "EMPLEADO", action));
            case "admin":
                return wind.InsertPage(new AdminPage(wind, "ADMINISTRACIÓN", action));
            case "register employee":
                return wind.InsertPage(new AdminPage(wind, "REGISTRAR EMPLEADO", action));
            case "edit employee":
                mold = wind.InsertPage("EDITAR EMPLEADO", action);
                return mold;
            default:
                mold = wind.InsertPage("ERROR 404", action);
                return mold;
        }
    }
}

