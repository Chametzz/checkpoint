public class ModelPlaycards : DB {
    public ModelPlaycards() : base () {
        this.table = "PLAYCARDS";
    }
        public bool DeletePlaycardById(int id)
{
    try
    {
        // Construir la consulta SQL para eliminar el empleado con el ID dado
        string query = $"DELETE FROM {table} WHERE ID = {id}";

        // Ejecutamos la consulta SQL para eliminar el empleado
        ExecuteQuery(query);  // Método que ejecuta la consulta SQL

        Console.WriteLine("Tarjeta eliminada exitosamente.");
        return true;  // Si la eliminación fue exitosa, retornamos true
    }
    catch (Exception ex)
    {
        // Si ocurre algún error, lo capturamos y retornamos false
        Console.WriteLine($"Error al eliminar el tarjeta: {ex.Message}");
        return false;  // Si la eliminación falló, retornamos false
    }
}
}
public class Playcard
{
    private int id;
    private string status;
    private int balance;
    private int points;
    private string issuedate;
    private string expdate;

    //constructor
    public Playcard (int id, string status, int balance, int points, string issuedate, string expdate)
    {
        this.id = id;
        this.status = status;
        this.balance = balance;
        this.points = points;
        this.issuedate = issuedate;
        this.expdate = expdate;

    }
    public int Id => id;
    public string Status => status;
    public int Balance => balance;
    public int Points => points;
    public string Issuedate => issuedate;
    public string Expdate => expdate;

}