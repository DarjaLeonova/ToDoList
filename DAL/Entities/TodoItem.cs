namespace DAL.Entities;

public class TodoItem : BaseEntity
{
    public string Title { get; set; }
    
    public bool IsCompleted { get; set; }
}