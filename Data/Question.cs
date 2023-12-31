﻿namespace EmptyStack.Data;

public class Question
{
    public int id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public int ownerid { get; set; }
    public int score { get; set; }
    public DateTime createddate { get; set; } = DateTime.UtcNow;
    public DateTime? lastmodifieddate { get; set; }
    public string[]? tags { get; set; }
}