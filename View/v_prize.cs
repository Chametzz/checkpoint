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
        claimform.InsertChild<Input>("ID de la tarjeta:", ("type", "number"), ("name", "ID"));
        claimform.InsertChild<Input>("Reclamar", ("type", "submir"));
    }
}

public class AddPrizeAmountPage : Page
{
    public AddPrizeAmountPage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        SetTitle("");
    }
}

public class AddPrizePage : Page
{
    public AddPrizePage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        
    }
}

public class EditPrizePage : Page {
    public EditPrizePage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {
        
    }
}

public class DeletePrizePage : Page
{
    public DeletePrizePage(Window wind, string key, Action<Page>? toLoad = null) : base(wind, key, toLoad) {

    }
}