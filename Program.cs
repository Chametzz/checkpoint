using SQLitePCL;
using System;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

DB.SETDATABASE(Path.Combine(Directory.GetCurrentDirectory(), "database.db"));

ModelEmployees Patata = new ModelEmployees();
CheckPointUI layout = new CheckPointUI(new Window());

/*DATOS LOCALES*/
Dictionary<string, Dictionary<string, string[]>> depts = new()
{
    { "Administracion", new Dictionary<string, string[]>
        {
            { "Admin", new string[] { "login", "home", "admin", "register employee", "edit employee", "delete employee", "check employee",
                                     "card home", "purchase card", "delete card", "recharge card", "check card", "edit card",
                                     "prize home", "claim prize", "add prize amount", "add prize", "edit prize", "delete prize",
                                     "game home", "add game", "check game" } }
        }
    },
    { "Recursos Humanos", new Dictionary<string, string[]>
        {
            { "HR Manager", new string[] { "login", "home", "register employee", "edit employee", "delete employee", "check employee" } },
            { "HR Assistant", new string[] { "login", "home", "check employee" } }
        }
    },
    { "Tarjetas", new Dictionary<string, string[]>
        {
            { "Card Manager", new string[] { "login", "home", "card home", "purchase card", "delete card", "recharge card", "check card", "edit card" } },
            { "Cashier", new string[] { "login", "home", "card home", "purchase card", "recharge card", "check card" } }
        }
    },
    { "Premios", new Dictionary<string, string[]>
        {
            { "Prize Manager", new string[] { "login", "home", "prize home", "claim prize", "add prize amount", "add prize", "edit prize", "delete prize" } },
            { "Prize Assistant", new string[] { "login", "home", "prize home", "claim prize" } }
        }
    },
    { "Juegos", new Dictionary<string, string[]>
        {
            { "Game Manager", new string[] { "login", "home", "game home", "add game", "check game" } },
            { "Operator", new string[] { "login", "home", "game home", "check game" } }
        }
    }
};
Employee? Empleado = null;
Employee? selectemp = null;
/**/
Page error404 = new Page(layout.wind, "404");

Page login = error404, home = error404;
Page admin = error404, registerEmployee = error404, editEmployee = error404, deleteEmployee = error404, checkEmployee = error404;
Page cardHome = error404, purchaseCard = error404, deleteCard = error404, rechargeCard = error404, checkCard = error404, editCard = error404;
Page prizeHome = error404, claimPrize = error404, addPrizeAmount = error404, addPrize = error404, editPrize = error404, deletePrize = error404;
Page gameHome = error404, addGame = error404, checkGame = error404;

login = layout.CreatePage("login", (page) => { });
login.SearchLabel<Form>("LOGINFORM")?.SetAction((form, data) =>
{

    Empleado = Patata.Login(data["USERNAME"], data["PASSWORD"]);
    if (Empleado != null)
    {
        form.page?.wind?.ReplacePage(home);
    }
    else
    {
        form.SetWarning("Los datos no coinciden.");
    }
});

home = layout.CreatePage("home", (page) =>
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
    page.SearchLabel<Button>("LOGOUTBUTTON")?.SetAction(() =>
    {
        Empleado = null;
        page.wind?.ReplacePage(login);

    });

});

admin = layout.CreatePage("admin", (page) =>
{
    Table? info = admin.SearchLabel<Table>("TABLEINFO");
    Selector? workdep = admin.SearchLabel<Selector>("WORKDEP");
    Selector? job = admin.SearchLabel<Selector>("JOB");
    Selector? emp = admin.SearchLabel<Selector>("EMPLOYEE");
    workdep?.childs.Clear();
    if (workdep != null && job != null)
    {
        foreach (var item in depts)
        {
            workdep.InsertChild<Button>(item.Key, ("value", item.Key)).SetAction(() => UpdateTable(workdep.GetProperty("value"), job.GetProperty("value")));
        }
    }
    job?.childs.Clear();
    if (workdep != null && job != null && depts.ContainsKey(workdep.GetProperty("value")))
    {
        foreach (var item in depts[workdep.GetProperty("value")])
        {
            job.InsertChild<Button>(item.Key, ("value", item.Key)).SetAction(() => UpdateTable(workdep.GetProperty("value"), job.GetProperty("value")));
        }
        UpdateTable(workdep.GetProperty("value"), job.GetProperty("value"));
    }
    emp?.childs.Clear();
    if (emp != null && workdep != null && job != null)
    {
        var emps = Patata.Read();
        foreach (var employee in emps)
        {
            if ((workdep.GetProperty("value") == "" || workdep.GetProperty("value") == $"{employee["WORKDEPT"]}") && (job.GetProperty("value") == "" || job.GetProperty("value") == $"{employee["JOB"]}"))
            {
                emp.InsertChild<Button>($"{employee["ID"]} - {employee["FIRSTNAME"]} {employee["LASTNAME"]}", ("link", checkEmployee.key)).SetAction(() =>
                {
                    selectemp = new Employee(
                        Convert.ToInt32(employee["ID"]),
                        employee["FIRSTNAME"]?.ToString() ?? "",
                        employee["LASTNAME"]?.ToString() ?? "",
                        employee["SEX"]?.ToString() ?? "",
                        employee["BIRTHDATE"]?.ToString() ?? "",
                        employee["PHONENO"]?.ToString() ?? "",
                        employee["EMAIL"]?.ToString() ?? "",
                        employee["ADDRESS"]?.ToString() ?? "",
                        employee["HIREDATE"]?.ToString() ?? "",
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
            var emps = Patata.Read();
            foreach (var emp in emps)
            {
                if (workdeptvalue == "" || workdeptvalue == $"{emp["WORKDEPT"]}" && jobvalue == "" || jobvalue == $"{emp["JOB"]}")
                {
                    info.InsertChild<Label>($"{emp["ID"]}");  // ID
                    info.InsertChild<Label>($"{emp["FIRSTNAME"]}");  // FIRSTNAME
                    info.InsertChild<Label>($"{emp["LASTNAME"]}");  // LASTNAME
                    info.InsertChild<Label>($"{emp["SEX"]}");  // SEX
                    info.InsertChild<Label>($"{emp["BIRTHDATE"]}");  // BIRTHDATE
                    info.InsertChild<Label>($"{emp["PHONENO"]}");  // PHONENO
                    info.InsertChild<Label>($"{emp["EMAIL"]}");  // EMAIL
                    info.InsertChild<Label>($"{emp["ADDRESS"]}");  // ADDRESS
                    info.InsertChild<Label>($"{emp["HIREDATE"]}");  // HIREDATE
                    info.InsertChild<Label>($"{emp["WORKDEPT"]}");  // WORKDEPT
                    info.InsertChild<Label>($"{emp["JOB"]}");  // JOB
                    info.InsertChild<Label>($"{emp["SALARY"]}");  // SALARY
                }
            }
        }
    }
});

checkEmployee = layout.CreatePage("check employee", (page) =>
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
        page.SetRef("adress", $"Direcci�n: {selectemp.Adress}");
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
        page.SetRef("adress", "");
        page.SetRef("hiredate", "");
        page.SetRef("workdept", "");
        page.SetRef("job", "");
        page.SetRef("salary", "");
    }
});

registerEmployee = layout.CreatePage("register employee");
registerEmployee.SearchLabel<Form>("REGISTERFORM")?.SetAction((form, data) => {
    bool recep = Patata.RegisterEmployee(
                data["FIRSTNAME"]?.ToString() ?? "",
                data["LASTNAME"]?.ToString() ?? "",
                data["SEX"]?.ToString() ?? "",
                data["BIRTHDATE"]?.ToString() ?? "",
                data["PHONENO"]?.ToString() ?? "",
                data["EMAIL"]?.ToString() ?? "",
                data["PASSWORD"]?.ToString() ?? "",
                data["ADDRESS"]?.ToString() ?? "",
                data["HIREDATE"]?.ToString() ?? "",
                data["WORKDEPT"]?.ToString() ?? "",
                data["JOB"]?.ToString() ?? "",
                Convert.ToSingle(data["SALARY"])
            );
    if(recep) {
        form.page?.wind.BackLoadPage();
    } else {
        form.SetWarning("Ocurrió un error.");
    }
});

editEmployee = layout.CreatePage("edit employee");
deleteEmployee = layout.CreatePage("delete employee");

/*registerEmployee = layout.CreatePage("register employee", (page) =>
{
    page.SearchLabel<Form>("REGISTERFORM").SetAction((form, data) =>
    {
        Empleado = Patata.RegisterEmployee(data["FIRTSNAME"]);
    });



});*/
admin.InsertLink(ConsoleKey.F1, registerEmployee);
admin.InsertLink(ConsoleKey.F2, editEmployee);
admin.InsertLink(ConsoleKey.F3, deleteEmployee);
layout.wind.Execute();