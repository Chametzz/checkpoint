using System;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using SQLitePCL;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                    "register employee",
                    "edit employee",
                    "delete employee",
                    "check employee",
                    "card home",
                    "purchase card",
                    "delete card",
                    "recharge card",
                    "check card",
                    "edit card",
                    "prize home",
                    "claim prize",
                    "add prize amount",
                    "add prize",
                    "edit prize",
                    "delete prize",
                    "game home",
                    "add game",
                    "check game",
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
                    "register employee",
                    "edit employee",
                    "delete employee",
                    "check employee",
                }
            },
            { "HR Assistant", new string[] { "login", "home", "check employee" } },
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
                    "check card",
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
                    "claim prize",
                    "add prize amount",
                    "add prize",
                    "edit prize",
                    "delete prize",
                }
            },
            { "Prize Assistant", new string[] { "prize home", "claim prize" } },
        }
    },
    {
        "Juegos",
        new Dictionary<string, string[]>
        {
            { "Game Manager", new string[] { "game home", "add game", "check game" } },
            { "Operator", new string[] { "game home", "check game" } },
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

login = layout.CreatePage("login", (page) => { });
login
    .SearchLabel<Form>("LOGINFORM")
    ?.SetAction(
        (form, data) =>
        {
            Empleado = modelEmployee.Login(data["USERNAME"], data["PASSWORD"]);
            if (Empleado != null)
            {
                form.page?.wind?.ReplacePage(home);
            }
            else
            {
                form.SetWarning("Los datos no coinciden.");
            }
        }
    );

home = layout.CreatePage(
    "home",
    (page) =>
    {
        Selector? screens = page.SearchLabel<Selector>("SCREENS");
        screens?.childs.Clear();
        if (screens != null && Empleado != null)
        {
            foreach (var item in depts[Empleado.Workdept][Empleado.Job])
            {
                if (item != null)
                {
                    screens.InsertChild<Label>(item, ("link", item));
                }
            }
        }
        if (Empleado != null)
        {
            page.SetRef("welcome", $"¡Bienvenido {Empleado.Firstname} {Empleado.Lastname}!");
            page.SetRef("id", $"ID: {Empleado.Id}");
            page.SetRef("first_name", $"Nombre: {Empleado.Firstname}");
            page.SetRef("last_name", $"Apellido: {Empleado.Lastname}");
            page.SetRef("sex", $"Sexo: {Empleado.Sex}");
            page.SetRef("birthdate", $"Fecha de Nacimiento: {Empleado.Birthdate}");
            page.SetRef("phone_no", $"Telefono: {Empleado.Phoneno}");
            page.SetRef("email", $"Correo Electronico: {Empleado.Email}");
            page.SetRef("adress", $"Direcci�n: {Empleado.Adress}");
            page.SetRef("hiredate", $"Fecha de Contratacion: {Empleado.Hiredate}");
            page.SetRef("workdept", $"Departamento: {Empleado.Workdept}");
            page.SetRef("job", $"Trabajo: {Empleado.Job}");
            page.SetRef("job", $"Trabajo: {Empleado.Job}");
            page.SetRef("salary", $"Salario: {Empleado.Salary}");
        }
        else
        {
            page.SetRef("welcome", "");
            page.SetRef("id", "");
            page.SetRef("first_name", "");
            page.SetRef("last_name", "");
            page.SetRef("sex", "");
            page.SetRef("birthdate", "");
            page.SetRef("phone_no", "");
            page.SetRef("email", "");
            page.SetRef("adress", "");
            page.SetRef("hiredate", "");
            page.SetRef("workdept", "");
            page.SetRef("job", "");
            page.SetRef("salary", "");
        }

        //Logaut
        page.SearchLabel<Button>("LOGOUTBUTTON")
            ?.SetAction(() =>
            {
                Empleado = null;
                page.wind?.ReplacePage(login);
            });
    }
);

admin = layout.CreatePage(
    "admin",
    (page) =>
    {
        Table? info = admin.SearchLabel<Table>("TABLEINFO");
        Selector? workdep = admin.SearchLabel<Selector>("WORKDEP");
        Selector? job = admin.SearchLabel<Selector>("JOB");
        Selector? emp = admin.SearchLabel<Selector>("EMPLOYEE");
        workdep?.childs.Clear();
        UpdateTable(workdep?.GetProperty("value") ?? "", job?.GetProperty("value") ?? "");
        if (workdep != null && job != null)
        {
            foreach (var item in depts)
            {
                workdep
                    .InsertChild<Button>(item.Key, ("value", item.Key))
                    .SetAction(() =>
                    {
                        job?.SetProperty("value", "");
                        job?.childs.Clear();
                        foreach (var item in depts[workdep.GetProperty("value")])
                        {
                            job?.InsertChild<Button>(item.Key, ("value", item.Key))
                                .SetAction(
                                    () =>
                                        UpdateTable(
                                            workdep.GetProperty("value"),
                                            job.GetProperty("value")
                                        )
                                );
                        }
                        UpdateTable(workdep.GetProperty("value"), job?.GetProperty("value") ?? "");
                    });
            }
        }
        /*job?.childs.Clear();
        if (workdep != null && job != null && depts.ContainsKey(workdep.GetProperty("value")))
        {
            foreach (var item in depts[workdep.GetProperty("value")])
            {
                job.InsertChild<Button>(item.Key, ("value", item.Key)).SetAction(() => UpdateTable(workdep.GetProperty("value"), job.GetProperty("value")));
            }
            UpdateTable(workdep.GetProperty("value"), job.GetProperty("value"));
        }*/
        emp?.childs.Clear();
        if (emp != null && workdep != null && job != null)
        {
            var emps = modelEmployee.Read();
            foreach (var employee in emps)
            {
                if (
                    (
                        workdep.GetProperty("value") == ""
                        || workdep.GetProperty("value") == $"{employee["WORKDEPT"]}"
                    )
                    && (
                        job.GetProperty("value") == ""
                        || job.GetProperty("value") == $"{employee["JOB"]}"
                    )
                )
                {
                    emp.InsertChild<Button>(
                            $"{employee["ID"]} - {employee["FIRSTNAME"]} {employee["LASTNAME"]}",
                            ("link", checkEmployee.key)
                        )
                        .SetAction(() =>
                        {
                            selectemp = new Employee(
                                Convert.ToInt32(employee["ID"]),
                                employee["FIRSTNAME"]?.ToString() ?? "",
                                employee["LASTNAME"]?.ToString() ?? "",
                                employee["SEX"]?.ToString() ?? "",
                                DateTime
                                    .Parse(employee["BIRTHDATE"]?.ToString() ?? "")
                                    .ToString("yyyy-MM-dd HH:mm:ss"),
                                employee["PHONENO"]?.ToString() ?? "",
                                employee["EMAIL"]?.ToString() ?? "",
                                employee["ADDRESS"]?.ToString() ?? "",
                                DateTime
                                    .Parse(employee["HIREDATE"]?.ToString() ?? "")
                                    .ToString("yyyy-MM-dd HH:mm:ss"),
                                employee["WORKDEPT"]?.ToString() ?? "",
                                employee["JOB"]?.ToString() ?? "",
                                Convert.ToSingle(employee["SALARY"] ?? 0)
                            );
                        });
                }
            }
        }

        void UpdateTable(string workdeptvalue, string jobvalue)
        {
            if (info != null)
            {
                info.SetColumns(12);
                info.childs.Clear();
                info.InsertChild<Label>("ID");
                info.InsertChild<Label>("FIRSTNAME");
                info.InsertChild<Label>("LASTNAME");
                info.InsertChild<Label>("SEX");
                info.InsertChild<Label>("BIRTHDATE");
                info.InsertChild<Label>("PHONENO");
                info.InsertChild<Label>("EMAIL");
                info.InsertChild<Label>("ADDRESS");
                info.InsertChild<Label>("HIREDATE");
                info.InsertChild<Label>("WORKDEPT");
                info.InsertChild<Label>("JOB");
                info.InsertChild<Label>("SALARY");
                var emps = modelEmployee.Read();
                foreach (var emp in emps)
                {
                    if (
                        workdeptvalue == ""
                        || workdeptvalue == $"{emp["WORKDEPT"]}" && jobvalue == ""
                        || jobvalue == $"{emp["JOB"]}"
                    )
                    {
                        info.InsertChild<Label>($"{emp["ID"]}"); // ID
                        info.InsertChild<Label>($"{emp["FIRSTNAME"]}"); // FIRSTNAME
                        info.InsertChild<Label>($"{emp["LASTNAME"]}"); // LASTNAME
                        info.InsertChild<Label>($"{emp["SEX"]}"); // SEX
                        info.InsertChild<Label>($"{emp["BIRTHDATE"]}"); // BIRTHDATE
                        info.InsertChild<Label>($"{emp["PHONENO"]}"); // PHONENO
                        info.InsertChild<Label>($"{emp["EMAIL"]}"); // EMAIL
                        info.InsertChild<Label>($"{emp["ADDRESS"]}"); // ADDRESS
                        info.InsertChild<Label>($"{emp["HIREDATE"]}"); // HIREDATE
                        info.InsertChild<Label>($"{emp["WORKDEPT"]}"); // WORKDEPT
                        info.InsertChild<Label>($"{emp["JOB"]}"); // JOB
                        info.InsertChild<Label>($"{emp["SALARY"]}"); // SALARY
                    }
                }
            }
        }
    }
);

checkEmployee = layout.CreatePage(
    "check employee",
    (page) =>
    {
        if (selectemp != null)
        {
            page.SetRef("welcome", $"¡Bienvenido {selectemp.Firstname} {selectemp.Lastname}!");
            page.SetRef("id", $"ID: {selectemp.Id}");
            page.SetRef("first_name", $"Nombre: {selectemp.Firstname}");
            page.SetRef("last_name", $"Apellido: {selectemp.Lastname}");
            page.SetRef("sex", $"Sexo: {selectemp.Sex}");
            page.SetRef("birthdate", $"Fecha de Nacimiento: {selectemp.Birthdate}");
            page.SetRef("phone_no", $"Telefono: {selectemp.Phoneno}");
            page.SetRef("email", $"Correo Electronico: {selectemp.Email}");
            page.SetRef("address", $"Direcci�n: {selectemp.Adress}");
            page.SetRef("hiredate", $"Fecha de Contratacion: {selectemp.Hiredate}");
            page.SetRef("workdept", $"Departamento: {selectemp.Workdept}");
            page.SetRef("job", $"Trabajo: {selectemp.Job}");
            page.SetRef("job", $"Trabajo: {selectemp.Job}");
            page.SetRef("salary", $"Salario: {selectemp.Salary}");
        }
        else
        {
            page.SetRef("welcome", "");
            page.SetRef("id", "");
            page.SetRef("first_name", "");
            page.SetRef("last_name", "");
            page.SetRef("sex", "");
            page.SetRef("birthdate", "");
            page.SetRef("phone_no", "");
            page.SetRef("email", "");
            page.SetRef("address", "");
            page.SetRef("hiredate", "");
            page.SetRef("workdept", "");
            page.SetRef("job", "");
            page.SetRef("salary", "");
        }
    }
);
checkEmployee
    .SearchLabel<Button>("EDITBUTTON")
    ?.SetAction(() =>
    {
        checkEmployee.wind.LoadPage(editEmployee);
    });
checkEmployee
    .SearchLabel<Button>("DELETEBUTTON")
    ?.SetAction(() =>
    {
        checkEmployee.wind.LoadPage(deleteEmployee);
    });

registerEmployee = layout.CreatePage(
    "register employee",
    (page) =>
    {
        Input? workdep = page.SearchLabel<Input>("WORKDEPT");
        Input? job = page.SearchLabel<Input>("JOB");
        workdep?.childs.Clear();
        job?.SetOnChange(
            (label, Page) =>
            {
                label.childs.Clear();
                if (
                    workdep != null
                    && job != null
                    && depts.ContainsKey(workdep.GetProperty("value"))
                )
                {
                    foreach (var item in depts[workdep.GetProperty("value")])
                    {
                        job.InsertChild<Label>(item.Key, ("value", item.Key));
                    }
                }
            }
        );
        if (workdep != null && job != null)
        {
            foreach (var item in depts)
            {
                workdep.InsertChild<Label>(item.Key, ("value", item.Key));
            }
        }
    }
);
registerEmployee
    .SearchLabel<Form>("REGISTERFORM")
    ?.SetAction(
        (form, data) =>
        {
            if (data["PASSWORD"] != data["VERIFYPASS"])
            {
                form.SetWarning("Las contraseñas no coinciden.");
                return;
            }
            bool recep = modelEmployee.RegisterEmployee(
                data["FIRSTNAME"]?.ToString() ?? "",
                data["LASTNAME"]?.ToString() ?? "",
                data["SEX"]?.ToString() ?? "",
                data["BIRTHDATE"]?.ToString() ?? "",
                data["PHONENO"]?.ToString() ?? "",
                data["EMAIL"]?.ToString() ?? "",
                data["PASSWORD"]?.ToString() ?? "",
                data["ADDRESS"]?.ToString() ?? "",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                data["WORKDEPT"]?.ToString() ?? "",
                data["JOB"]?.ToString() ?? "",
                Convert.ToSingle(data["SALARY"])
            );
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

editEmployee = layout.CreatePage(
    "edit employee",
    (page) =>
    {
        page.SearchLabel<Input>("FIRSTNAME")?.SetProperty("value", $"{selectemp?.Firstname}");
        page.SearchLabel<Input>("LASTNAME")?.SetProperty("value", $"{selectemp?.Lastname}");
        page.SearchLabel<Input>("SEX")?.SetProperty("value", $"{selectemp?.Sex}");
        page.SearchLabel<Input>("BIRTHDATE")?.SetProperty("value", $"{selectemp?.Birthdate}");
        page.SearchLabel<Input>("PHONENO")?.SetProperty("value", $"{selectemp?.Phoneno}");
        page.SearchLabel<Input>("EMAIL")?.SetProperty("value", $"{selectemp?.Email}");
        page.SearchLabel<Input>("ADDRESS")?.SetProperty("value", $"{selectemp?.Adress}");
        page.SearchLabel<Input>("WORKDEPT")?.SetProperty("value", $"{selectemp?.Workdept}");
        page.SearchLabel<Input>("JOB")?.SetProperty("value", $"{selectemp?.Job}");
        page.SearchLabel<Input>("SALARY")?.SetProperty("value", $"{selectemp?.Salary}");
        page.SearchLabel<Input>("PASSWORD")?.SetProperty("value", "");
    }
);
editEmployee
    .SearchLabel<Form>("EDITFORM")
    ?.SetAction(
        (form, data) =>
        {
            if (selectemp != null)
            {
                if (data["PASSWORD"] == "")
                {
                    if (
                        modelEmployee.EditEmployeeById(
                            selectemp.Id,
                            data["FIRSTNAME"],
                            data["LASTNAME"],
                            data["SEX"],
                            data["BIRTHDATE"],
                            data["PHONENO"],
                            data["EMAIL"],
                            data["ADDRESS"],
                            selectemp.Hiredate,
                            data["WORKDEPT"],
                            data["JOB"],
                            Convert.ToSingle(data["SALARY"])
                        )
                    )
                    {
                        selectemp = modelEmployee.GetEmployeeById(selectemp.Id);
                        form.page?.wind?.BackLoadPage();
                    }
                    else
                    {
                        form.SetWarning("Ocurrió un error.");
                    }
                }
                else
                {
                    if (
                        modelEmployee.EditEmployeeByIdPassword(
                            selectemp.Id,
                            data["FIRSTNAME"],
                            data["LASTNAME"],
                            data["SEX"],
                            data["BIRTHDATE"],
                            data["PHONENO"],
                            data["EMAIL"],
                            data["PASSWORD"],
                            data["ADDRESS"],
                            selectemp.Hiredate,
                            data["WORKDEPT"],
                            data["JOB"],
                            Convert.ToSingle(data["SALARY"])
                        )
                    )
                    {
                        selectemp = modelEmployee.GetEmployeeById(selectemp.Id);
                        form.page?.wind?.BackLoadPage();
                    }
                    else
                    {
                        form.SetWarning("Ocurrió un error.");
                    }
                }
            }
        }
    );

deleteEmployee = layout.CreatePage("delete employee");
deleteEmployee
    .SearchLabel<Form>("DELETEFORM")
    ?.SetAction(
        (form, data) =>
        {
            bool recep = modelEmployee.DeleteEmployeeById(Convert.ToInt32(data["ID"]));
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

/*registerEmployee = layout.CreatePage("register employee", (page) =>
{
    page.SearchLabel<Form>("REGISTERFORM").SetAction((form, data) =>
    {
        Empleado = modelEmployee.RegisterEmployee(data["FIRTSNAME"]);
    });



});*/
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

            var Tarjetas = modelPlaycard.Read();
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
            modelPlaycard.Create(
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
        }
        /*
                string rutaImagen = "720120.jpg";
                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(rutaImagen);
                if (File.Exists(rutaImagen)) // Verifica si la imagen existe antes de agregarla
                {
                    imagen.ScaleToFit(400f, 1000f); // Ajustar tamaño de la imagen
                    /*imagen.SetAbsolutePosition(0f, 0f);
                    doc.Add(imagen);
                    }
                else
                {
                    Console.WriteLine("Imagen no encontrada, se generará el PDF sin imagen.");
                }
                // Cerrar el documento
                doc.Close();
                purchaseCard.InsertLabel<Label>(
                    "PDF creado con éxito en: " + Path.GetFullPath(ruta)
                );
                layout.wind.RefreshPage();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
            
            form.page?.wind?.BackLoadPage();
        }
        */
    );

deleteCard = layout.CreatePage("delete card");
deleteCard
    .SearchLabel<Form>("DELETECARD")
    ?.SetAction(
        (form, data) =>
        {
            if (modelPlaycard.Read($"ID = {data["ID"]}").Count <= 0)
            {
                form.SetWarning("No existe el ID");
                return;
            }
            bool recep = modelPlaycard.Delete($"ID = {data["ID"]}");
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

checkCard = layout.CreatePage("check card");

editCard = layout.CreatePage("edit card");

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
                .SetAction(() => {
                    /*selectedGame = new Game(Convert.ToInt32(g["ID"]), g["NAME"]?.ToString() ?? "", g["TYPE"]?.ToString() ?? "", g["STATUS"]?.ToString() ?? "", Convert.ToInt32(g["CAPACITY"]), Convert.ToSingle(g["PRICE"]));*/
                });
        }

        // Tabla con la información de los juegos
        Table? info = gameHome.SearchLabel<Table>("TABLEINFO")?.SetColumns(5);
        if (info != null)
        {
            info.childs.Clear();
            info.InsertChild<Label>("ID");
            info.InsertChild<Label>("NAME");
            info.InsertChild<Label>("TYPE");
            info.InsertChild<Label>("CAPACITY");
            info.InsertChild<Label>("PRICE");

            foreach (var game in games)
            {
                info.InsertChild<Label>($"{game["ID"]}");
                info.InsertChild<Label>($"{game["NAME"]}");
                info.InsertChild<Label>($"{game["TYPE"]}");
                info.InsertChild<Label>($"{game["CAPACITY"]}");
                info.InsertChild<Label>($"{game["PRICE"]}");
            }
        }
    }
);
addGame = layout.CreatePage("add game");
checkGame = layout.CreatePage("check game");

admin.InsertLink(ConsoleKey.F1, registerEmployee);
admin.InsertLink(ConsoleKey.F2, editEmployee);
admin.InsertLink(ConsoleKey.F3, deleteEmployee);
layout.wind.Execute();
