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

                    string ruta = "documento.pdf"; // Ruta donde se guardará el PDF

                    // Crear el documento PDF
                    Document doc = new Document();

                    try
                    {
                        // Crear el escritor que guardará el PDF en la ruta especificada
                        PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));

                        // Abrir el documento para escribir
                        doc.Open();

                        // Agregar un título


                        string rutaPDF = "CheckPoint.pdf"; // Ruta del PDF

                        try
                        {
                            using (PdfReader lector = new PdfReader(rutaPDF))
                            {
                                string textoCompleto = "Hola Simona la mona jajajaja";
                                for (int i = 1; i <= lector.NumberOfPages; i++)
                                {
                                    textoCompleto += ITextExtractionStrategy.ReferenceEquals(lector, i);
                                }

                                Console.WriteLine("Texto extraído del PDF:\n");
                                Console.WriteLine(textoCompleto);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error al leer el PDF: " + ex.Message);
                        }
                    }
                    catch
                    {

                    }
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

        editCard = layout.CreatePage("edit card");
    }
}