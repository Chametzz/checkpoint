public class C_Employees : Controller
{
    public C_Employees()
    {
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
                page.SetRef("money", $"Ganancia: ${money}");
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
        admin.InsertLink(ConsoleKey.F1, registerEmployee);
    }
}