public class ModelGames : DB {
    public ModelGames() : base () {
        this.table = "GAMES";
    }
}
public class Games
{
    private int id;
    private string name;
    private string description;
    private float price;

//constructor
    public Games(int id, string name, string description, float price)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.price = price;
    }
    public int Id => id;
    public string Name => name;
    public string Description => description;
    public float Price => price;


}