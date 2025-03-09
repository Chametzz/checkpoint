using SQLitePCL;
using System;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Collections.Specialized.BitVector32;

DB.SETDATABASE(Path.Combine(Directory.GetCurrentDirectory(), "database.db"));

ModelEmployees Patata = new ModelEmployees();
CheckPointUI layout = new CheckPointUI(new Window());
Employee Empleado = null;


Page Login = layout.CreatePage("login", (page) => {});

Login.SearchLabel<Form>("LOGINFORM").SetAction((form, data) => {

    Employee emp = Patata.Login(data["USERNAME"], data["PASSWORD"]);

    form.SetWarning("FUNCIONA");
});


Page Home = layout.CreatePage("home", (page) => {
    page.SetRef("welcome", $"Bienvenido {Empleado.Firstname}");
    page.SetRef("id", $"ID:{Empleado.Id}");
    page.SetRef("first_name", $"Nombre:{Empleado.Firstname}");
    page.SetRef("last_name", $"Apellido:{Empleado.Lastname}");
    page.SetRef("sex", $"Sexo:{Empleado.Sex}");
    page.SetRef("birthdate", $"Fecha de Nacimiento:{Empleado.Birthdate}");
    page.SetRef("phone_no", $"Telefono:{Empleado.Phoneno}");
    page.SetRef("email", $"Correo Electronico:{Empleado.Email}");
    page.SetRef("adress", $"Dirección:{Empleado.Adress}");
    page.SetRef("hiredate", $"Fecha de Contratacion:{Empleado.Hiredate}");
    page.SetRef("workdept", $"Departamento:{Empleado.Workdept}");
    page.SetRef("job", $"Trabajo:{Empleado.Job}");
    page.SetRef("salary", $"Salario:{Empleado.Salary}");
});





layout.wind.Execute();