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

Page Login = layout.CreatePage("login", (page) => {});

Login.SearchLabel<Form>("LOGINFORM").SetAction((form, data) => {

    form.SetWarning("FUNCIONA");

});
Employee emp = Patata.Login(data["USERNAME"], data["PASSWORD"]);


layout.wind.Execute();
Window wind = new Window();