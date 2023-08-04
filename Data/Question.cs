namespace EmptyStack.Data;

public class Question
{
    public int id { get; set; }
    public string? title { get; set; }
    public string? description { get; set; }
    public int likescount { get; set; }
    public long lastmodifieddate { get; set; }
    public long createddate { get; set; }
    public int ownerid { get; set; }
    public string[]? tags { get; set; }
}