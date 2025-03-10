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
Employee Empleado = null;
Page Home = null;
Page Registro = null;

Page Login = layout.CreatePage("login", (page) => {});

Login.SearchLabel<Form>("LOGINFORM").SetAction((form, data) => {

    Empleado = Patata.Login(data["USERNAME"], data["PASSWORD"]);
    if ( Empleado != null) {

        form.page.wind?.ReplacePage(Home);
    }
    else {
        form.SetWarning("Los datos no coinciden.");
    }
});


Home = layout.CreatePage("home", (page) => {
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

    //Logaut
    page.SearchLabel<Button>("LOGOUTBUTTON").action = () => {
        Empleado = null;
        page.wind?.ReplacePage(Login);

    };

});

Registro = layout.CreatePage("register employee", (page) => {
    page.SearchLabel<Form>("REGISTERFORM").SetAction((form, data) => {
        Empleado = Patata.RegisterEmployee(data["FIRTSNAME"]);
    });



    });

    



Dictionary<string, List<int>> diccionario = new Dictionary<string, List<int>>();



layout.wind.Execute();