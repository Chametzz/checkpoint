public class C_Prizes : Controller
{
    public C_Prizes()
    {
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
                    var card = modelPlaycards.Read($"ID = {cardID}");
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
                            modelPlaycards.Update($"POINTS = POINTS - {total}", $"ID = {cardID}");
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
    }
}