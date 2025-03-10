internal class ModelPlaycards : DB {
    public ModelPlaycards() : base () {
        this.table = "PLAYCARDS";
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