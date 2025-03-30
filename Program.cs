DB.SETDATABASE(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "database.db"));

ModelEmployees modelEmployee = new ModelEmployees();
ModelPlaycards modelPlaycard = new ModelPlaycards();
ModelGames modelGames = new ModelGames();
ModelPrizes modelPrizes = new ModelPrizes();
CheckPointUI layout = new CheckPointUI(new Window());

/*DATOS LOCALES*/
Dictionary<string, Dictionary<string, string[]>> depts = new()
{
    {
        "Administracion",
        new Dictionary<string, string[]>
        {
            {
                "Admin",
                new string[]
                {
                    "admin",
                    //"register employee",
                    //"edit employee",
                    //"delete employee",
                    //"check employee",
                    "card home",
                    "purchase card",
                    "delete card",
                    "recharge card",
                    "check card",
                    "edit card",
                    "prize home",
                    //"claim prize",
                    "add prize amount",
                    "add prize",
                    "edit prize",
                    "delete prize",
                    "game home",
                    "add game",
                    //"check game",
                }
            },
        }
    },
    {
        "Recursos Humanos",
        new Dictionary<string, string[]>
        {
            {
                "HR Manager",
                new string[]
                {
                    "admin",
                    //"register employee",
                    //"edit employee",
                    //"delete employee",
                    //"check employee",
                }
            },
            { "HR Assistant", new string[] { /*"login", "home",*/ "check employee" } },
        }
    },
    {
        "Tarjetas",
        new Dictionary<string, string[]>
        {
            {
                "Card Manager",
                new string[]
                {
                    "card home",
                    "purchase card",
                    "delete card",
                    "recharge card",
                    //"check card",
                    "edit card",
                }
            },
            {
                "Cashier",
                new string[] { "card home", "purchase card", "recharge card", "check card" }
            },
        }
    },
    {
        "Premios",
        new Dictionary<string, string[]>
        {
            {
                "Prize Manager",
                new string[]
                {
                    "prize home",
                    //"claim prize",
                    "add prize amount",
                    "add prize",
                    "edit prize",
                    "delete prize",
                }
            },
            { "Prize Assistant", new string[] { "prize home"/*, "claim prize"*/ } },
        }
    },
    {
        "Juegos",
        new Dictionary<string, string[]>
        {
            { "Game Manager", new string[] { "game home", "add game"/*, "check game"*/ } },
            { "Operator", new string[] { "game home"/*, "check game"*/ } },
        }
    },
};
Employee? Empleado = null;
Employee? selectemp = null;

/**/
Page error404 = new Page(layout.wind, "404");

Page login = error404,
    home = error404;
Page admin = error404,
    registerEmployee = error404,
    editEmployee = error404,
    deleteEmployee = error404,
    checkEmployee = error404;
Page cardHome = error404,
    purchaseCard = error404,
    deleteCard = error404,
    rechargeCard = error404,
    checkCard = error404,
    editCard = error404;
Page prizeHome = error404,
    claimPrize = error404,
    addPrizeAmount = error404,
    addPrize = error404,
    editPrize = error404,
    deletePrize = error404;
Page gameHome = error404,
    addGame = error404,
    checkGame = error404;



Prizes? prize = null;
prizeHome = layout.CreatePage(
    "prize home",
    (page) =>
    {
        var prizes = modelPrizes.Read();
        Selector? sel = page.SearchLabel<Selector>("PRIZES");
        sel?.childs.Clear();
        foreach (var p in prizes)
        {
            sel?.InsertChild<Button>(
                    $"{p["ID"]}    {p["NAME"]} {p["PRICE"]}    {p["AMOUNT"]}",
                    ("link", $"{claimPrize.key}")
                )
                .SetAction(() =>
                {
                    prize = new Prizes(
                        Convert.ToInt32(p["ID"]),
                        p["NAME"]?.ToString() ?? "",
                        Convert.ToSingle(p["PRICE"]),
                        Convert.ToInt32(p["AMOUNT"])
                    );
                });
        }

        Table? info = prizeHome.SearchLabel<Table>("TABLEINFO")?.SetColumns(4);
        if (info != null)
        {
            info.childs.Clear();
            info.InsertChild<Label>("ID");
            info.InsertChild<Label>("NAME");
            info.InsertChild<Label>("PRICE");
            info.InsertChild<Label>("AMOUNT");
            foreach (var prize in prizes)
            {
                info.InsertChild<Label>($"{prize["ID"]}");
                info.InsertChild<Label>($"{prize["NAME"]}");
                info.InsertChild<Label>($"{prize["PRICE"]}");
                info.InsertChild<Label>($"{prize["AMOUNT"]}");
            }
        }
    }
);
addPrizeAmount = layout.CreatePage("add prize amount");
claimPrize = layout.CreatePage(
    "claim prize",
    (page) =>
    {
        page.SetRef("prize", $"{prize?.Name} por {prize?.Price} pts");
    }
);
claimPrize
    .SearchLabel<Form>("CLAIMFORM")
    ?.SetAction(
        (form, data) =>
        {
            int amount = Convert.ToInt32(data["AMOUNT"]);
            int cardID = Convert.ToInt32(data["IDCARD"]);
            var card = modelPlaycard.Read($"ID = {cardID}");
            float total = amount * (prize?.Price ?? 0);
            if (prize?.Amount - amount < 0)
            {
                form.SetWarning("No hay suficientes premios.");
                return;
            }
            if (card.Count > 0)
            {
                if (total <= Convert.ToInt32(card[0]["POINTS"]))
                {
                    modelPlaycard.Update($"POINTS = POINTS - {total}", $"ID = {cardID}");
                    modelPrizes.Update($"AMOUNT = AMOUNT - {amount}", $"ID = {prize?.Id}");
                    form.page?.wind?.BackLoadPage();
                }
                else
                {
                    form.SetWarning("Puntos insuficientes");
                }
            }
            else
            {
                form.SetWarning("No se encontró la tarjeta");
            }
        }
    );
addPrize = layout.CreatePage("add prize");
editPrize = layout.CreatePage("edit prize");
deletePrize = layout.CreatePage("delete prize");



admin.InsertLink(ConsoleKey.F1, registerEmployee);
//layout.wind.Execute();
C_Employees employees = new C_Employees();
C_Games games = new C_Games();
C_Prizes prizes = new C_Prizes();
C_Playcards playcards = new C_Playcards();
Controller.layout.wind.Execute();