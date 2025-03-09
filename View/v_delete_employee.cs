
public class DelEmpPage : Page
{
    public DelEmpPage(Window wind, string title, Action<Page>? toLoad = null) : base(wind, title, toLoad) {
        Form delform = InsertLabel<Form>("¡ATENCIÓN, LOS DATOS NO PODRÁN RECUPERARSE!", ("name", "DELETEFORM"));
        delform.InsertChild<Input>("Introduzca el id del empleado que desea eliminar:", ("name", "ID"), ("type", "number"), ("required", "true"));
        delform.InsertChild<Input>("Eliminar", ("type", "submit"));
    }
}