public class Controller
{
    protected static ModelEmployees modelEmployee = new ModelEmployees();
    protected static ModelPlaycards modelPlaycards = new ModelPlaycards();
    protected static ModelGames modelGames = new ModelGames();
    protected static ModelPrizes modelPrizes = new ModelPrizes();
    public static CheckPointUI layout = new CheckPointUI(new Window());
    protected static Page error404 = new Page(layout.wind, "404");
    protected static Page login = error404,
        home = error404;
    protected static Page admin = error404,
        registerEmployee = error404,
        editEmployee = error404,
        deleteEmployee = error404,
        checkEmployee = error404;
    protected static Page cardHome = error404,
        purchaseCard = error404,
        deleteCard = error404,
        rechargeCard = error404,
        checkCard = error404,
        editCard = error404;
    protected static Page prizeHome = error404,
        claimPrize = error404,
        addPrizeAmount = error404,
        addPrize = error404,
        editPrize = error404,
        deletePrize = error404;
    protected static Page gameHome = error404,
        addGame = error404,
        checkGame = error404;
    protected static Dictionary<string, Dictionary<string, string[]>> depts = new()
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
    protected static Employee? Empleado = null;
    protected static Employee? selectemp = null;
    protected static Prizes? prize = null;
    protected static Prizes? selectprize = null;
    protected static Playcard? selectcard = null;
    public Controller()
    {
        
    }
}