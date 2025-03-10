public class ModelEmployees : DB {

    public ModelEmployees() : base () {
        this.table = "EMPLOYEES";
        Console.WriteLine(dsn);
        Console.WriteLine(dbPath);
        CreateAdminIfEmpty();
    }
    public Employee? Login(string email,string password){
        var data = Read($"EMAIL = '{email}'AND PASSWORD ='{password}'");
        if (data.Count>0){
            var emp =data[0];
            return new Employee(
                Convert.ToInt32(emp["ID"]),
                emp["FIRSTNAME"]?.ToString() ?? "",
                emp["LASTNAME"]?.ToString() ?? "",
                emp["SEX"]?.ToString() ?? "",
                emp["BIRTHDATE"]?.ToString() ?? "",
                emp["PHONENO"]?.ToString() ?? "",
                emp["EMAIL"]?.ToString() ?? "",
                emp["ADDRESS"]?.ToString() ?? "",
                emp["HIREDATE"]?.ToString() ?? "",
                emp["WORKDEPT"]?.ToString() ?? "",
                emp["JOB"]?.ToString() ?? "",
                Convert.ToSingle(emp["SALARY"] ?? 0)
            );
        }
        return null;
    }
    public void CreateAdminIfEmpty() {
        var employees = Read("1=1");
        if (employees.Count == 0) {
            // Si está vacía, insertar un administrador por defecto
            string columns = "FIRSTNAME, LASTNAME, SEX, BIRTHDATE, PHONENO, EMAIL, PASSWORD, ADDRESS, HIREDATE, WORKDEPT, JOB, SALARY";
            string values = "'admin', 'admin', 'OTRO', '2000-01-01', '0000000000', 'admin@example.com', '123', '', '2025-01-01', 'admin', 'admin', 0";
            Create(columns, values);
        }
    }
    public bool IsEmailRegistered(string email)
{
    var result = Read($"EMAIL = '{email}'");  // Suponiendo que tienes un método Read que lee los datos de la base de datos
    return result.Count > 0;  // Si ya hay algún registro con ese correo, retorna true
}
    public bool RegisterEmployee(string firstname, string lastname, string sex, string birthdate, string phoneno, string email, string password, string address, string hiredate, string workdept, string job, float salary)
{
    // Validación básica de campos (puedes agregar más validaciones según tus necesidades)
    if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
    {
        Console.WriteLine("Error: Todos los campos son obligatorios.");
        return false;  // Si falta algún campo, retornamos false
    }
        // Verificar si el correo electrónico ya está registrado
    if (IsEmailRegistered(email))
    {
        Console.WriteLine("Error: El correo electrónico ya está registrado.");
        return false;  // Si el correo ya está registrado, retornamos false
    }
   // Construir la consulta SQL para insertar el nuevo empleado
    string columns = "FIRSTNAME, LASTNAME, SEX, BIRTHDATE, PHONENO, EMAIL, PASSWORD, ADDRESS, HIREDATE, WORKDEPT, JOB, SALARY";
    string values = $"'{firstname}', '{lastname}', '{sex}', '{birthdate}', '{phoneno}', '{email}', '{password}', '{address}', '{hiredate}', '{workdept}', '{job}', {salary}";

    // Intentamos insertar el nuevo empleado en la base de datosok
    Create(columns, values);  // Llamamos al método Create para insertar el empleado

    return true;
}
    public bool IsEmailRegisteredForCurrentEmployee(int id, string email)
{
    var result = Read($"EMAIL = '{email}' AND ID != {id}");  // Verifica si otro empleado tiene el mismo correo
    return result.Count > 0;  // Si ya hay otro empleado con ese correo, retorna true
}

    public bool EditEmployeeById(int id, string firstname, string lastname, string sex, string birthdate, string phoneno, string email, string address, string hiredate, string workdept, string job, float salary)
{
    // Validación básica de campos
    if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(email))
    {
        Console.WriteLine("Error: Todos los campos son obligatorios.");
        return false;  // Si falta algún campo, retornamos false
    }

    // Verificar si el correo electrónico ya está registrado (pero que no sea el mismo del empleado que estamos editando)
    if (IsEmailRegistered(email) && !IsEmailRegisteredForCurrentEmployee(id, email))
    {
        Console.WriteLine("Error: El correo electrónico ya está registrado.");
        return false;  // Si el correo ya está registrado por otro empleado, retornamos false
    }

    // Construir la consulta SQL para actualizar los datos del empleado
    string setValues = $"FIRSTNAME = '{firstname}', LASTNAME = '{lastname}', SEX = '{sex}', BIRTHDATE = '{birthdate}', PHONENO = '{phoneno}', EMAIL = '{email}', ADDRESS = '{address}', HIREDATE = '{hiredate}', WORKDEPT = '{workdept}', JOB = '{job}', SALARY = {salary}";

    // Intentamos actualizar el empleado en la base de datos
    bool success = UpdateEmployeeById(id, setValues);

    if (success)
    {
        Console.WriteLine("Empleado actualizado exitosamente.");
        return true;  // Si la actualización fue exitosa, retornamos true
    }
    else
    {
        Console.WriteLine("Error: No se pudo actualizar el empleado.");
        return false;  // Si la actualización falló, retornamos false
    }
}
     public bool UpdateEmployeeById(int id, string setValues)
{
    try
    {
        // Construir la consulta SQL para actualizar los datos del empleado
        string query = $"UPDATE EMPLOYEES SET {setValues} WHERE ID = {id}";

        // Ejecutamos la consulta SQL (esto es solo un ejemplo; ajusta según tu implementación)
        ExecuteQuery(query);  // Método que ejecuta la consulta SQL

        return true;  // Si la actualización fue exitosa
    }
    catch (Exception ex)
    {
        // Si ocurre algún error, lo capturamos y retornamos false
        Console.WriteLine($"Error al actualizar el empleado: {ex.Message}");
        return false;
    }
}
    public bool EditEmployeeByIdPassword(int id, string firstname, string lastname, string sex, string birthdate, string phoneno, string email, string password, string address, string hiredate, string workdept, string job, float salary)
{
    // Validación básica de campos
    if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
    {
        Console.WriteLine("Error: Todos los campos son obligatorios.");
        return false;  // Si falta algún campo, retornamos false
    }

    // Verificar si el correo electrónico ya está registrado (pero que no sea el mismo del empleado que estamos editando)
    if (IsEmailRegistered(email) && !IsEmailRegisteredForCurrentEmployee(id, email))
    {
        Console.WriteLine("Error: El correo electrónico ya está registrado.");
        return false;  // Si el correo ya está registrado por otro empleado, retornamos false
    }

    // Construir la consulta SQL para actualizar los datos del empleado
    string setValues = $"FIRSTNAME = '{firstname}', LASTNAME = '{lastname}', SEX = '{sex}', BIRTHDATE = '{birthdate}', PHONENO = '{phoneno}', EMAIL = '{email}', PASSWORD = '{password}', ADDRESS = '{address}', HIREDATE = '{hiredate}', WORKDEPT = '{workdept}', JOB = '{job}', SALARY = {salary}";

    // Intentamos actualizar el empleado en la base de datos
    bool success = UpdateEmployeeById(id, setValues);

    if (success)
    {
        Console.WriteLine("Empleado actualizado exitosamente.");
        return true;  // Si la actualización fue exitosa, retornamos true
    }
    else
    {
        Console.WriteLine("Error: No se pudo actualizar el empleado.");
        return false;  // Si la actualización falló, retornamos false
    }
}
    public bool DeleteEmployeeById(int id)
{
    try
    {
        // Construir la consulta SQL para eliminar el empleado con el ID dado
        string query = $"DELETE FROM EMPLOYEES WHERE ID = {id}";

        // Ejecutamos la consulta SQL para eliminar el empleado
        ExecuteQuery(query);  // Método que ejecuta la consulta SQL

        Console.WriteLine("Empleado eliminado exitosamente.");
        return true;  // Si la eliminación fue exitosa, retornamos true
    }
    catch (Exception ex)
    {
        // Si ocurre algún error, lo capturamos y retornamos false
        Console.WriteLine($"Error al eliminar el empleado: {ex.Message}");
        return false;  // Si la eliminación falló, retornamos false
    }
}
    public List<Employee> GetEmployeesByDepartment(string department)
{
    List<Employee> employees = new List<Employee>();

    // Construir la consulta SQL para obtener los empleados del departamento especificado
    string query = $"SELECT * FROM EMPLOYEES WHERE WORKDEPT = '{department}'";

    // Leer los datos de la base de datos
    var result = Read(query);  // Aquí deberías tener un método Read que ejecute la consulta y obtenga los datos

    foreach (var emp in result)
    {
        // Crear el objeto Employee para cada registro y agregarlo a la lista
        Employee employee = new Employee(
            Convert.ToInt32(emp["ID"]),
            emp["FIRSTNAME"]?.ToString() ?? "",
            emp["LASTNAME"]?.ToString() ?? "",
            emp["SEX"]?.ToString() ?? "",
            emp["BIRTHDATE"]?.ToString() ?? "",
            emp["PHONENO"]?.ToString() ?? "",
            emp["EMAIL"]?.ToString() ?? "",
            emp["ADDRESS"]?.ToString() ?? "",
            emp["HIREDATE"]?.ToString() ?? "",
            emp["WORKDEPT"]?.ToString() ?? "",
            emp["JOB"]?.ToString() ?? "",
            Convert.ToSingle(emp["SALARY"] ?? 0)
        );

        employees.Add(employee);  // Agregar el empleado a la lista
    }

    return employees;  // Retornar la lista de empleados
}
    public Employee? GetEmployeeById(int id)
{
    // Construir la consulta SQL para obtener el empleado con el ID dado
    string query = $"SELECT * FROM EMPLOYEES WHERE ID = {id}";

    // Leer los datos de la base de datos
    var result = Read(query);  // Aquí deberías tener un método Read que ejecute la consulta y obtenga los datos

    if (result.Count > 0)
    {
        var emp = result[0];  // Tomamos el primer (y único) resultado

        // Crear un objeto Employee con los datos obtenidos y retornarlo
        return new Employee(
            Convert.ToInt32(emp["ID"]),
            emp["FIRSTNAME"]?.ToString() ?? "",
            emp["LASTNAME"]?.ToString() ?? "",
            emp["SEX"]?.ToString() ?? "",
            emp["BIRTHDATE"]?.ToString() ?? "",
            emp["PHONENO"]?.ToString() ?? "",
            emp["EMAIL"]?.ToString() ?? "",
            emp["ADDRESS"]?.ToString() ?? "",
            emp["HIREDATE"]?.ToString() ?? "",
            emp["WORKDEPT"]?.ToString() ?? "",
            emp["JOB"]?.ToString() ?? "",
            Convert.ToSingle(emp["SALARY"] ?? 0)
        );
    }

    // Si no se encuentra ningún empleado con el ID dado, retornar null
    return null;
}
    public List<Employee> GetAllEmployees()
{
    List<Employee> employees = new List<Employee>();

    // Construir la consulta SQL para obtener todos los empleados
    string query = "SELECT * FROM EMPLOYEES";

    // Leer los datos de la base de datos
    var result = Read(query);  // Aquí deberías tener un método Read que ejecute la consulta y obtenga los datos

    foreach (var emp in result)
    {
        // Crear el objeto Employee para cada registro y agregarlo a la lista
        Employee employee = new Employee(
            Convert.ToInt32(emp["ID"]),
            emp["FIRSTNAME"]?.ToString() ?? "",
            emp["LASTNAME"]?.ToString() ?? "",
            emp["SEX"]?.ToString() ?? "",
            emp["BIRTHDATE"]?.ToString() ?? "",
            emp["PHONENO"]?.ToString() ?? "",
            emp["EMAIL"]?.ToString() ?? "",
            emp["ADDRESS"]?.ToString() ?? "",
            emp["HIREDATE"]?.ToString() ?? "",
            emp["WORKDEPT"]?.ToString() ?? "",
            emp["JOB"]?.ToString() ?? "",
            Convert.ToSingle(emp["SALARY"] ?? 0)
        );

        employees.Add(employee);  // Agregar el empleado a la lista
    }

    return employees;  // Retornar la lista de empleados
}

}
        
    public class Employee 
{
    private int id;
    private string firstname;
    private string lastname;
    private string sex;
    private string birthdate;
    private string phoneno;
    private string email;
    private string adress;
    private string hiredate;
    private string workdept;
    private string job;
    private float salary;

    // Constructor
    public Employee(int id, string firstname, string lastname, string sex, string birthdate, string phoneno, string email, string adress, string hiredate, string workdept, string job, float salary)
    {
        this.id = id;
        this.firstname = firstname;
        this.lastname = lastname;
        this.sex = sex;
        this.birthdate = birthdate;
        this.phoneno = phoneno;
        this.email = email;
        this.adress = adress;
        this.hiredate = hiredate;
        this.workdept = workdept;
        this.job = job;
        this.salary = salary;
    }
    public int Id => id;
    public string Firstname => firstname;
    public string Lastname => lastname;
    public string Sex => sex;
    public string Birthdate => birthdate;
    public string Phoneno => phoneno;
    public string Email => email;
    public string Adress => adress;
    public string Hiredate => hiredate;
    public string Workdept => workdept;
    public string Job => job;
    public float Salary => salary;

}
