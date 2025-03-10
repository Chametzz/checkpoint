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
        switch (name)
        {
            case "login":
                return wind.InsertPage(new LoginPage(wind, "login", action));
            case "home":
                return wind.InsertPage(new HomePage(wind, "home", action));
            case "admin":
                return wind.InsertPage(new AdminPage(wind, "admin", action));
            case "register employee":
                return wind.InsertPage(new RegEmpPage(wind, "register employee", action));
            case "edit employee":
                return wind.InsertPage(new EditEmpPage(wind, "edit employee", action));
            case "delete employee":
                return wind.InsertPage(new DelEmpPage(wind, "delete employee", action));
            case "check employee":
                return wind.InsertPage(new DelEmpPage(wind, "check employee", action));
            default:
                return wind.InsertPage("ERROR 404", action);
        }
    }
}

