public class CardHomePage : Page
{
    public CardHomePage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        SetTitle("PLAYCARDS");
        InsertLabel<Table>("", ("name", "TABLEINFO"));
    }
}
public class PurchaseCardPage : Page
{
    public PurchaseCardPage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        SetTitle("ADQUIRIR TARJETA");
        Form newcard = InsertLabel<Form>("Introduzca el monto inicial para su nueva tarjeta:", ("name", "CARDFORM"));
        newcard.InsertChild<Input>("Saldo inicial:", ("type", "number"), ("name", "BALANCE"), ("required", "true"));
        newcard.InsertChild<Input>("Comprar", ("type", "submit"));
    }
}

public class DeleteCardPage : Page
{
    public DeleteCardPage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        SetTitle("ELIMINAR TARJETA");
        Form newcard = InsertLabel<Form>("Ingrese el ID de la tarjeta que desea eliminar:", ("name", "DELETECARD"));
        newcard.InsertChild<Input>("ID:", ("type", "number"), ("name", "ID"), ("required", "true"));
        newcard.InsertChild<Input>("Eliminar", ("type", "submit"));
    }
}

public class RechargeCardPage : Page
{
    public RechargeCardPage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        SetTitle("RECARGAR TARJETA");
        Form newcard = InsertLabel<Form>("Introduzca el monto para recargar su tarjeta:", ("name", "RECHARGECARD"));
        newcard.InsertChild<Input>("Saldo por agregar:", ("type", "number"), ("name", "BALANCE"), ("required", "true"));
        newcard.InsertChild<Input>("Comprar", ("type", "submit"));
    }
}

public class CheckCardPage : Page
{
    public CheckCardPage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        SetTitle("CONSULTAR TARJETA");
        InsertLabel<Label>("ID:", ("ref", "id"));
        InsertLabel<Label>("Estado:", ("ref", "status"));
        InsertLabel<Label>("Saldo:", ("ref", "balance"));
        InsertLabel<Label>("Puntos:", ("ref", "points"));
        InsertLabel<Label>("Fecha de emisión:", ("ref", "issuedate"));
        InsertLabel<Label>("Fecha de expiración:", ("ref", "expdate"));
    }
}

public class EditCardPage : Page
{
    public EditCardPage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        Form register = InsertLabel<Form>("Actualiza los campos de la tarjeta:", ("name", "EDITCARD"));
        register.InsertChild<Input>("Estado:", ("type", "text"), ("name", "STATUS"), ("required", "true"));
        register.InsertChild<Input>("Balance:", ("type", "number"), ("name", "BALANCE"), ("required", "true"));
        register.InsertChild<Input>("Puntos:", ("type", "number"), ("name", "POINTS"), ("required", "true"));
        register.InsertChild<Input>("Editar", ("type", "submit"));
    }
}