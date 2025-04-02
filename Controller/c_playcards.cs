using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
public class C_Playcards : Controller
{
    public C_Playcards()
    {
        cardHome = layout.CreatePage(
    "card home",
    (page) =>
    {
        Table? Tabla = page.SearchLabel<Table>("TABLEINFO");
        if (Tabla != null)
        {
            Tabla.childs = new();
            Tabla.SetColumns(6);
            Tabla.InsertChild<Label>("ID");
            Tabla.InsertChild<Label>("ESTADO");
            Tabla.InsertChild<Label>("SALDO");
            Tabla.InsertChild<Label>("PUNTOS");
            Tabla.InsertChild<Label>("FECHA DE INICIO");
            Tabla.InsertChild<Label>("FECHA DE EXPIRACION");

            var Tarjetas = modelPlaycards.Read();
            foreach (var tj in Tarjetas)
            {
                Tabla.InsertChild<Label>(tj["ID"]?.ToString() ?? "");
                Tabla.InsertChild<Label>(tj["STATUS"]?.ToString() ?? "");
                Tabla.InsertChild<Label>(tj["BALANCE"]?.ToString() ?? "");
                Tabla.InsertChild<Label>(tj["POINTS"]?.ToString() ?? "");
                Tabla.InsertChild<Label>(tj["ISSUEDATE"]?.ToString() ?? "");
                Tabla.InsertChild<Label>(tj["EXPDATE"]?.ToString() ?? "");
            }
        }
    }
);
        if (cardHome.SearchLabel<Form>("SEARCHCARD") is Form search)
        {
            search.SetAction((form, data) =>
            {
                var cards = modelPlaycards.Read($"ID = {data["ID"]}");
                if (cards.Count <= 0)
                {
                    form.SetWarning("No existe esta tarjeta con este ID.");
                    return;
                }
                var card = cards[0];
                selectcard = new Playcard(
                    Convert.ToInt32(card["ID"]),
                    card["STATUS"] + "",
                    Convert.ToInt32(card["BALANCE"]),
                    Convert.ToInt32(card["POINTS"]),
                    card["ISSUEDATE"] + "",
                    card["EXPDATE"] + ""
                );
                form.page?.wind.LoadPage(editCard);

            });
        }
{
        purchaseCard = layout.CreatePage("purchase card");
        purchaseCard
            .SearchLabel<Form>("CARDFORM")
            ?.SetAction(
                (form, data) =>
                {
                    modelPlaycards.Create(
                        "STATUS, BALANCE, POINTS, ISSUEDATE, EXPDATE",
                        $"'ACTIVA', {Convert.ToSingle(data["BALANCE"])}, 0, '{DateTime.Now.ToString("yyyy-MM-dd")}', '2050-10-10'"
                    );

<<<<<<< HEAD
                    GeneratePDF(data["ID"].ToString(), data["BALANCE"].ToString());
                    
                });

            string imagePath = "0.jpg";
            string outputPath = $"Targeta{"ID"}.pdf";
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(outputPath, FileMode.Create));
            doc.Open();

            doc.Add(new Paragraph("ID: " + data["ID"]));
            doc.Add(new Paragraph("Estado: ACTIVA"));
            doc.Add(new Paragraph("Saldo: " + data["BALANCE"]));
            doc.Add(new Paragraph("Puntos: 0"));
            doc.Add(new Paragraph("Fecha de inicio: " + DateTime.Now.ToString("yyyy-MM-dd")));
            doc.Add(new Paragraph("Fecha de expiración: 2050-10-10"));

            Image img = Image.GetInstance(imagePath);
            img.SetAbsolutePosition(50, 500);
            img.ScaleToFit(200, 200);
            doc.Add(img);

            doc.Close();
            Console.WriteLine("✅ PDF generado: " + outputPath);

=======
                    //string ruta = "C:UsersandymOneDriveEscritorioTargetas"; // Ruta donde se guardará el PDF

                    // Crear el documento PDF
                    int id = Convert.ToInt32(modelPlaycards.Read()[^1]["ID"]);
                    string outputPath = $"keycard - {id}.pdf";
                    try
                    {
                        /*using (FileStream stream = new FileStream(outputPath, FileMode.Create))
                        {
                            Document doc = new Document(PageSize.A4.Rotate());
                            PdfWriter.GetInstance(doc, stream);
                            doc.Open();
                            Image img = Image.GetInstance("0.jpg");
                            img.ScaleToFit(doc.PageSize.Width, doc.PageSize.Height);
                            img.SetAbsolutePosition(0, 0);
                            doc.Add(img);
                            doc.Close();
                        }*/
                        Image img = Image.GetInstance("1.jpeg");
                        Document doc = new Document(new Rectangle(img.Width, img.Height));
                        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(outputPath, FileMode.Create));
                        doc.AddTitle("KEYCARD");
                        doc.AddCreator("checkpoint");
                        doc.Open();
                        doc.Add(img);
                        img.SetAbsolutePosition(0, 0);
                        img.ScaleToFit(doc.PageSize.Width, doc.PageSize.Height);
                        doc.Add(img);
                        PdfContentByte canvas = writer.DirectContent;
                        Font font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 500, BaseColor.BLUE);
                        Phrase phrase = new Phrase($"{id}");
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_CENTER, phrase, img.Width / 2, img.Height / 2, 0);
                        doc.Close();
                        writer.Close();
                    }
                    catch
                    {

                    }
                    form.page?.wind.BackLoadPage();
>>>>>>> 2f40de11304faf0725bf8df6a68a73542662d88d
                }
            );

        deleteCard = layout.CreatePage("delete card");
        deleteCard
            .SearchLabel<Form>("DELETECARD")
            ?.SetAction(
                (form, data) =>
                {
                    if (modelPlaycards.Read($"ID = {data["ID"]}").Count <= 0)
                    {
                        form.SetWarning("No existe el ID");
                        return;
                    }
                    bool recep = modelPlaycards.Delete($"ID = {data["ID"]}");
                    if (recep)
                    {
                        form.page?.wind.BackLoadPage();
                    }
                    else
                    {
                        form.SetWarning("Ocurrió un error.");
                    }
                }
            );

        rechargeCard = layout.CreatePage("recharge card");
        rechargeCard
        .SearchLabel<Form>("RECHARGECARD")
        ?.SetAction(
            (form, data) =>
            {
                var tarjeta = modelPlaycards.Read($"ID = {data["ID"]}");
                if (tarjeta.Count == 0)
                {
                    form.SetWarning("No existe una tarjeta con este ID.");
                    return;
                }

                float saldoActual = Convert.ToSingle(tarjeta[0]["BALANCE"]);
                float recarga = Convert.ToSingle(data["BALANCE"]);

                if (recarga <= 0)
                {
                    form.SetWarning("El monto a recargar debe ser mayor a 0.");
                    return;
                }

                float nuevoSaldo = saldoActual + recarga;

                bool actualizado = modelPlaycards.Update(
                    $"BALANCE = {nuevoSaldo}",
                    $"ID = {data["ID"]}"
                );

                if (actualizado)
                {

                    form.page?.wind.BackLoadPage();
                }
                else
                {
                    form.SetWarning("Error al actualizar el saldo.");
                }
            }
        );


        checkCard = layout.CreatePage("check card");

        editCard = layout.CreatePage("edit card", (page) =>
        {
            page.SearchLabel<Input>("STATUS")?.SetProperty("value", selectcard?.Status ?? "");
            page.SearchLabel<Input>("BALANCE")?.SetProperty("value", selectcard?.Balance + "" ?? "");
            page.SearchLabel<Input>("POINTS")?.SetProperty("value", selectcard?.Points + "" ?? "");
        });
        Form? editForm = editCard.SearchLabel<Form>("EDITCARD");
        if (editForm != null)
        {
            editForm.SetAction((form, data) =>
            {
                Console.WriteLine(selectcard);
                if (selectcard != null)
                {
                    modelPlaycards.Update($"STATUS = '{data["STATUS"]}', BALANCE = {data["BALANCE"]}, POINTS = {data["POINTS"]}", $"ID = {selectcard.Id}");
                }
                form.page?.wind.BackLoadPage();
            });
        }
    }
}