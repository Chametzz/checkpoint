
public class GameHomePage : Page
{
    public GameHomePage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad){
        SetTitle("JUEGOS");
        InsertLabel<Selector>("Selecciona un juego", ("name", "PRIZES"));
        InsertLabel<Table>("", ("name", "TABLEINFO"));
    }
}

public class AddGamePage : Page
{
    public AddGamePage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad){
        SetTitle("AGREGAR JUEGO");
        Form newGame = InsertLabel<Form>("Ingrese los detalles del nuevo juego:", ("name", "ADDGAME"));
        newGame.InsertChild<Input>("Nombre:", ("type", "text"), ("name", "NAME"), ("required", "true"));
        newGame.InsertChild<Input>("Tipo:", ("type", "text"), ("name", "TYPE"), ("required", "true"));
        newGame.InsertChild<Input>("Estado:", ("type", "text"), ("name", "STATUS"), ("required", "true"));
        newGame.InsertChild<Input>("Capacidad:", ("type", "number"), ("name", "CAPACITY"), ("required", "true"));
        newGame.InsertChild<Input>("Precio:", ("type", "number"), ("name", "PRICE"), ("required", "true"));
        newGame.InsertChild<Input>("Agregar", ("type", "submit"));
    }
}

public class CheckGamePage : Page
{
    public CheckGamePage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        SetTitle("CONSULTAR JUEGO");
        InsertLabel<Label>("ID:", ("ref", "id"));
        InsertLabel<Label>("Nombre:", ("ref", "name"));
        InsertLabel<Label>("Tipo:", ("ref", "type"));
        InsertLabel<Label>("Estado:", ("ref", "status"));
        InsertLabel<Label>("Capacidad:", ("ref", "capacity"));
        InsertLabel<Label>("Precio:", ("ref", "price"));
    }
}

public class EditGamePage : Page
{
    public EditGamePage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad){
        SetTitle("EDITAR JUEGO");
        Form editGame = InsertLabel<Form>("Seleccione un juego para editar:", ("name", "EDITGAME"));
        editGame.InsertChild<Input>("ID del juego:", ("type", "number"), ("name", "ID"), ("required", "true"));
        editGame.InsertChild<Input>("Nombre:", ("type", "text"), ("name", "NAME"), ("required", "true"));
        editGame.InsertChild<Input>("Tipo:", ("type", "text"), ("name", "TYPE"), ("required", "true"));
        editGame.InsertChild<Input>("Estado:", ("type", "text"), ("name", "STATUS"));
        editGame.InsertChild<Input>("Capacidad:", ("type", "number"), ("name", "CAPACITY"), ("required", "true"));
        editGame.InsertChild<Input>("Precio:", ("type", "number"), ("name", "PRICE"), ("required", "true"));
        editGame.InsertChild<Input>("Actualizar", ("type", "submit"));
    }
}

public class DeleteGamePage : Page
{
    public DeleteGamePage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad){
        SetTitle("ELIMINAR JUEGO");
        Form deleteGame = InsertLabel<Form>("Ingrese el ID del juego que desea eliminar:", ("name", "DELETEGAME"));
        deleteGame.InsertChild<Input>("ID del juego:", ("type", "number"), ("name", "ID"), ("required", "true"));
        deleteGame.InsertChild<Input>("Eliminar", ("type", "submit"));
    }
}

public class SimulationGamePage : Page
{
    public SimulationGamePage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad)
    {
        SetTitle("SIMULACIÓN");
        Form gameform = InsertLabel<Form>("Ingrese el ID de la tarjeta:", ("name", "GAMEFORM"));
        gameform.InsertChild<Input>("ID del juego:", ("type", "number"), ("name", "ID"), ("required", "true"));
        gameform.InsertChild<Input>("Jugar", ("type", "submit"));
    }
}