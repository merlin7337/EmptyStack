namespace EmptyStack.Data;

public class Answer
{
    public int id { get; set; }
    public string answer { get; set; }
    public int parentquestionid { get; set; }
    public int ownerid { get; set; }
    public int score { get; set; }
    public DateTime createddate { get; set; } = DateTime.UtcNow;
    public DateTime? lastmodifieddate { get; set; }
}