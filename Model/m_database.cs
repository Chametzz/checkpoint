using System.Data.SQLite;
internal class DB{
    private static string dbPath = "";
    private static string dsn = "";
    protected SQLiteConnection connection;
    protected string table = "";
    public DB() {        
        connection = new SQLiteConnection(dsn);
    }

    public static void SETDATABASE(string path) {//PONERLO ARRIBA DEL TODO
        dbPath = path;
        dsn = $"Data Source={dbPath};Version=3";
        if (!File.Exists(dbPath)) {
            SQLiteConnection.CreateFile(dbPath);
        }
        CreateTables();
    }

    private static void CreateTables() {
        using (var connection = new SQLiteConnection(dsn)) {
            connection.Open();

            // Crear las tablas
            string[] createTableCommands = new string[] {
                @"
                    CREATE TABLE IF NOT EXISTS EMPLOYEES (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT, 
                        FIRSTNAME TEXT NOT NULL, 
                        LASTNAME TEXT NOT NULL, 
                        SEX TEXT NOT NULL, 
                        BIRTHDATE DATE NOT NULL, 
                        PHONENO TEXT, 
                        EMAIL TEXT NOT NULL,
                        PASSWORD TEXT NOT NULL,
                        ADDRESS TEXT,
                        HIREDATE DATE NOT NULL, 
                        WORKDEPT TEXT, 
                        JOB TEXT, 
                        SALARY DECIMAL(9, 2)
                    );
                ",
                @"
                    CREATE TABLE IF NOT EXISTS PLAYCARDS (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT, 
                        STATUS TEXT NOT NULL, 
                        BALANCE DECIMAL(9, 2), 
                        POINTS INT, 
                        ISSUEDATE DATE NOT NULL, 
                        EXPDATE DATE
                    );
                ",
                @"
                    CREATE TABLE IF NOT EXISTS PRIZES (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT, 
                        NAME TEXT NOT NULL, 
                        PRICE DECIMAL(9, 2), 
                        AMOUNT INT
                    );
                ",
                @"
                    CREATE TABLE IF NOT EXISTS GAMES (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT, 
                        NAME TEXT NOT NULL, 
                        TYPE TEXT NOT NULL, 
                        STATUS TEXT, 
                        CAPACITY INT, 
                        PRICE DECIMAL(9, 2)
                    );
                "
            };

            foreach (var commandText in createTableCommands) {
                using (var command = new SQLiteCommand(commandText, connection)) {
                    command.ExecuteNonQuery();
                }
            }
        }
    }

    public void Create(string columns, string values) {
        string query = $"INSERT INTO {table} ({columns}) VALUES ({values})";
        ExecuteQuery(query);
    }

    public SQLiteDataReader Read(string condition = "1=1") {
        string query = $"SELECT * FROM {table} WHERE {condition}";
        connection.Open();
        var command = new SQLiteCommand(query, connection);
        return command.ExecuteReader();
    }

    public void Update(string setColumns, string condition) {
        string query = $"UPDATE {table} SET {setColumns} WHERE {condition}";
        ExecuteQuery(query);
    }

    public void Delete(string condition) {
        string query = $"DELETE FROM {table} WHERE {condition}";
        ExecuteQuery(query);
    }

    private void ExecuteQuery(string query) {
        try {
            connection.Open();
            using (var command = new SQLiteCommand(query, connection)) {
                command.ExecuteNonQuery();
            }
        } catch (Exception ex) {
            Console.WriteLine($"Error executing query: {ex.Message}");
        } finally {
            connection.Close();
        }
    }
}

internal class ModelEmployees : DB {
    public ModelEmployees() : base () {
        this.table = "EMPLOYEES";
    }
}

internal class ModelGames : DB {
    public ModelGames() : base () {
        this.table = "GAMES";
    }
}

internal class ModelPlaycards : DB {
    public ModelPlaycards() : base () {
        this.table = "PLAYCARDS";
    }
}

internal class ModelPrizes : DB {
    public ModelPrizes() : base () {
        this.table = "PRIZES";
    }
}
internal class Empleado 
{
   public  string Nombre {get; set; }
   public string Apellido {get; set; }
   public int Edad {get; set;}
   public string cargo { get; set;}
   public decimal salario {get; set;}
   

}