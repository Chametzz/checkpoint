public class ModelEmployees : DB {
    public ModelEmployees() : base () {
        this.table = "EMPLOYEES";
    }
    public Employee? Login(string email,string password){
        //SQLiteDataReader data = Read($"EMAIL = '{email}'AND PASSWORD ='{password}'");
        return null;
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