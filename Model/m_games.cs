public class ModelGames : DB {
    public ModelGames() : base () {
        this.table = "GAMES";
    }
}
public class Games
{
    private int id;
    private string name;
    private string type;
    private string status;
    private int capacity;
    private float price;

//constructor
    public Games(int id, string name, string type, string status, int capacity, float price)
    {
        this.id = id;
        this.name = name;
        this.type = type;
        this.status = status;
        this.capacity = capacity;
        this.price = price;
    }
    public int Id => id;
    public string Name => name;
    public float Price => price;
    public string Type { get => type; set => type = value; }
    public string Status { get => status; set => status = value; }
    public int Capacity { get => capacity; set => capacity = value; }
}