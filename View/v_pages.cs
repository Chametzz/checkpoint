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
                return wind.InsertPage(new CheckEmpPage(wind, "check employee", action));

            case "card home":
                return wind.InsertPage(new CardHomePage(wind, "card home", action));
            case "purchase card":
                return wind.InsertPage(new PurchaseCardPage(wind, "purchase card", action));
            case "delete card":
                return wind.InsertPage(new DeleteCardPage(wind, "delete card", action));
            case "recharge card":
                return wind.InsertPage(new RechargeCardPage(wind, "recharge card", action));
            case "check card":
                return wind.InsertPage(new CheckCardPage(wind, "check card", action));
            case "edit card":
                return wind.InsertPage(new EditCardPage(wind, "edit card", action));

            case "prize home":
                return wind.InsertPage(new PrizeHomePage(wind, "prize home", action));
            case "claim prize":
                return wind.InsertPage(new ClaimPrizePage(wind, "claim prize", action));
            case "add prize amount":
                return wind.InsertPage(new AddPrizeAmountPage(wind, "add prize amount", action));
            case "add prize":
                return wind.InsertPage(new AddPrizePage(wind, "add prize", action));
            case "edit prize":
                return wind.InsertPage(new EditPrizePage(wind, "edit prize", action));
            case "delete prize":
                return wind.InsertPage(new DeletePrizePage(wind, "delete prize", action));

            case "game home":
                return wind.InsertPage(new GameHomePage(wind, "game home", action));
            case "add game":
                return wind.InsertPage(new AddGamePage(wind, "add game", action));
            case "check game":
                return wind.InsertPage(new CheckGamePage(wind, "check game", action));
            default:
                return wind.InsertPage("ERROR 404", action);
        }
    }
}

