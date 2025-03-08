
public class AdminPage : Page {
    public AdminPage(Window wind, string title, Action<Page>? toLoad = null) : base(wind, title, toLoad){
        InsertLabel<Input>("Seleccione un departamento", ("type", "select"), ("name", "WORKDEP"));
    }
}