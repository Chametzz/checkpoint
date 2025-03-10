using System.Data.Common;

internal class ModelPrizes : DB {
    public ModelPrizes() : base () {
        this.table = "PRIZES";
    }
}
public class Prizes
{
    private int id;
    private string name;
    private float price;
    private int amount;

    public Prizes(int id, string name, float price, int amount)
    {
        this.id = id;
        this.name = name;
        this.price = price;
        this.amount = amount;
    }
    public int Id => id;
    public string Name => name;
    public float Price => price;
    public int Amount => amount;
    
}