public class PrizeHomePage : Page
{
    public PrizeHomePage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        SetTitle("¡PREMIOS!");
        InsertLabel<Selector>("Selecciona el premio por adquirir", ("name", "PRIZES"));
        InsertLabel<Table>("", ("name", "TABLEINFO"));
    }
}

public class ClaimPrizePage : Page
{
    public ClaimPrizePage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        SetTitle("RECLAMAR PREMIO");
        InsertLabel<Label>("Adquirir");
        InsertLabel<Label>("", ("ref", "prize"));
        Form claimform = InsertLabel<Form>("", ("name", "CLAIMFORM"));
        claimform.InsertChild<Input>("Cantidad:", ("type", "number"), ("name", "AMOUNT"));
        claimform.InsertChild<Input>("ID de la tarjeta:", ("type", "number"), ("name", "IDCARD"));
        claimform.InsertChild<Input>("Reclamar", ("type", "submit"));
    }
}

public class AddPrizeAmountPage : Page
{
    public AddPrizeAmountPage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        SetTitle("AGREGAR CANTIDAD DE PREMIOS");
        Form amountform = InsertLabel<Form>("", ("name", "AMOUNTFORM"));
        amountform.InsertChild<Input>("ID:", ("type", "number"), ("name", "ID"));
        amountform.InsertChild<Input>("CANTIDAD:", ("type", "number"), ("name", "AMOUNT"));
        amountform.InsertChild<Input>("Agregar", ("type", "submit"));
    }
}

public class AddPrizePage : Page
{
    public AddPrizePage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        SetTitle("AGREGAR PREMIO");
        Form newPrize = InsertLabel<Form>("Introduzca los detalles del nuevo premio:", ("name", "ADDPRIZE"));
        newPrize.InsertChild<Input>("Nombre del premio:", ("type", "text"), ("name", "NAME"), ("required", "true"));
        newPrize.InsertChild<Input>("Precio:", ("type", "number"), ("name", "PRICE"), ("required", "true"));
        newPrize.InsertChild<Input>("Cantidad:", ("type", "number"), ("name", "AMOUNT"), ("required", "true"));
        newPrize.InsertChild<Input>("Agregar", ("type", "submit"));
    }
}

public class EditPrizePage : Page {
    public EditPrizePage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        SetTitle("EDITAR PREMIO");
        Form editPrize = InsertLabel<Form>("Actualice los detalles del premio:", ("name", "EDITPRIZE"));
        editPrize.InsertChild<Input>("Nombre del premio:", ("type", "text"), ("name", "NAME"), ("required", "true"));
        editPrize.InsertChild<Input>("Precio:", ("type", "number"), ("name", "PRICE"), ("required", "true"));
        editPrize.InsertChild<Input>("Cantidad:", ("type", "number"), ("name", "AMOUNT"), ("required", "true"));
        editPrize.InsertChild<Input>("Actualizar", ("type", "submit"));
    }
}

public class DeletePrizePage : Page
{
    public DeletePrizePage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        SetTitle("ELIMINAR PREMIO");
        Form deletePrize = InsertLabel<Form>("Ingrese el ID del premio que desea eliminar:", ("name", "DELETEPRIZE"));
        deletePrize.InsertChild<Input>("ID del premio:", ("type", "number"), ("name", "ID"), ("required", "true"));
        deletePrize.InsertChild<Input>("Eliminar", ("type", "submit"));
    }
}