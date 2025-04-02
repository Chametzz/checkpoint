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
        /*        addPrizeAmount = layout.CreatePage("add prize amount");
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
                    );*/
        addPrize = layout.CreatePage("add prize");
        addPrize
            .SearchLabel<Form>("ADDPRIZE")
            ?.SetAction(
                (form, data) =>
                {
                    // Verificación de datos vacíos
                    if (string.IsNullOrWhiteSpace(data["NAME"]?.ToString()) ||
                        string.IsNullOrWhiteSpace(data["PRICE"]?.ToString()) ||
                        string.IsNullOrWhiteSpace(data["AMOUNT"]?.ToString()))
                    {
                        form.SetWarning("Todos los campos son obligatorios.");
                        return;
                    }

                    string name = data["NAME"].ToString()!;
                    float price;
                    int amount;

                    // Validar y convertir precio
                    if (!float.TryParse(data["PRICE"].ToString(), out price))
                    {
                        form.SetWarning("El precio debe ser un número válido.");
                        return;
                    }

                    // Validar y convertir cantidad
                    if (!int.TryParse(data["AMOUNT"].ToString(), out amount))
                    {
                        form.SetWarning("La cantidad debe ser un número entero.");
                        return;
                    }

                    // Verificar si el premio ya existe (opcional)
                    var existingPrize = modelPrizes.Read($"NAME = '{name}'");
                    if (existingPrize.Count > 0)
                    {
                        form.SetWarning("El premio ya existe.");
                        return;
                    }

                    // Insertar el nuevo premio en la base de datos
                    bool success = modelPrizes.Create("NAME, PRICE, AMOUNT", $"'{name}', {price}, {amount}");

                    if (!success)
                    {
                        form.SetWarning("Error al agregar el premio.");
                        return;
                    }

                    // Recargar la página para mostrar el nuevo premio
                    form.page?.wind?.BackLoadPage();
                }
            );
        deletePrize = layout.CreatePage("delete prize");
        deletePrize
            .SearchLabel<Form>("DELETEPRIZE")
            ?.SetAction(
                (form, data) =>
                {
                    int prizeID;

                    // Verificar que el ID no sea vacío y sea un número
                    if (!int.TryParse(data["ID"]?.ToString(), out prizeID))
                    {
                        form.SetWarning("Por favor ingrese un ID válido.");
                        return;
                    }

                    // Verificar si el premio existe
                    var prize = modelPrizes.Read($"ID = {prizeID}");
                    if (prize.Count == 0)
                    {
                        form.SetWarning("No se encontró el premio con ese ID.");
                        return;
                    }

                    // Eliminar el premio
                    bool success = modelPrizes.Delete($"ID = {prizeID}");

                    if (!success)
                    {
                        form.SetWarning("Error al eliminar el premio.");
                        return;
                    }

                    // Recargar la página para mostrar la lista actualizada
                    form.page?.wind?.BackLoadPage();
                }
            );
        addPrizeAmount = layout.CreatePage("add prize amount");
        addPrizeAmount
            .SearchLabel<Form>("AMOUNTFORM")
            ?.SetAction(
                (form, data) =>
                {
                    int prizeID;
                    int amountToAdd;

                    // Verificar que el ID sea un número
                    if (!int.TryParse(data["ID"]?.ToString(), out prizeID))
                    {
                        form.SetWarning("Por favor ingrese un ID válido.");
                        return;
                    }

                    // Verificar que la cantidad sea un número
                    if (!int.TryParse(data["AMOUNT"]?.ToString(), out amountToAdd))
                    {
                        form.SetWarning("Por favor ingrese una cantidad válida.");
                        return;
                    }

                    // Verificar si el premio existe
                    var prize = modelPrizes.Read($"ID = {prizeID}");
                    if (prize.Count == 0)
                    {
                        form.SetWarning("No se encontró el premio con ese ID.");
                        return;
                    }

                    // Actualizar la cantidad del premio
                    bool success = modelPrizes.Update($"AMOUNT = AMOUNT + {amountToAdd}", $"ID = {prizeID}");

                    if (!success)
                    {
                        form.SetWarning("Error al agregar la cantidad al premio.");
                        return;
                    }

                    // Recargar la página para mostrar la lista actualizada
                    form.page?.wind?.BackLoadPage();
                }
            );
        editPrize = layout.CreatePage("edit prize");
    }
}