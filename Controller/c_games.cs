public class C_Games : Controller
{
    public C_Games()
    {
        gameHome = layout.CreatePage(
            "game home",
            (page) =>
            {
                var games = modelGames.Read();

                // Selector para elegir juegos
                Selector? sel = page.SearchLabel<Selector>("PRIZES"); // Cambia "PRIZES" si necesitas otro nombre
                sel?.childs.Clear();
                foreach (var g in games)
                {
                    sel?.InsertChild<Button>(
                            $"{g["ID"]}    {g["NAME"]} {g["TYPE"]}    {g["PRICE"]}",
                            ("link", $"{checkGame.key}")
                        )
                        .SetAction(() =>
                        {
                            /*selectedGame = new Game(Convert.ToInt32(g["ID"]), g["NAME"]?.ToString() ?? "", g["TYPE"]?.ToString() ?? "", g["STATUS"]?.ToString() ?? "", Convert.ToInt32(g["CAPACITY"]), Convert.ToSingle(g["PRICE"]));*/
                        });
                }

                // Tabla con la información de los juegos
                Table? info = gameHome.SearchLabel<Table>("TABLEINFO")?.SetColumns(6);
                if (info != null)
                {
                    info.childs.Clear();
                    info.InsertChild<Label>("ID");
                    info.InsertChild<Label>("NAME");
                    info.InsertChild<Label>("TYPE");
                    info.InsertChild<Label>("STATUS");
                    info.InsertChild<Label>("CAPACITY");
                    info.InsertChild<Label>("PRICE");

                    foreach (var game in games)
                    {
                        info.InsertChild<Label>($"{game["ID"]}");
                        info.InsertChild<Label>($"{game["NAME"]}");
                        info.InsertChild<Label>($"{game["TYPE"]}");
                        info.InsertChild<Label>($"{game["STATUS"]}");
                        info.InsertChild<Label>($"{game["CAPACITY"]}");
                        info.InsertChild<Label>($"{game["PRICE"]}");
                    }
                }
            }
        );
        addGame = layout.CreatePage("add game");
        if(addGame.SearchLabel<Form>("ADDGAME") is Form addForm) {
            addForm.SetAction((form, data) => {
                modelGames.Create($"NAME, TYPE, STATUS, CAPACITY, PRICE", $"'{data["NAME"]}', '{data["TYPE"]}', '{data["STATUS"]}', {data["CAPACITY"]}, {data["PRICE"]}");
                form.page?.wind.BackLoadPage();
            });
        }
        checkGame = layout.CreatePage("check game");
    }
}