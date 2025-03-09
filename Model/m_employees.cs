public class ModelEmployees : DB {

    public ModelEmployees() : base () {
        this.table = "EMPLOYEES";
        CreateAdminIfEmpty();
    }
    public Employee? Login(string email,string password){
        var data = Read($"EMAIL = '{email}'AND PASSWORD ='{password}'");
        if (data.Count>0){
            var emp =data[0];
            return new Employee(emp["ID"], emp["FIRSTNAME"], emp["LASTNAME"], emp["SEX"], emp["BIRTHDATE"], emp["PHONENO"],
            emp["EMAIL"], emp["ADRESS"], emp["HIREDATE"], emp["WORKDEPT"], emp["JOB"], emp["SALARY"]);
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