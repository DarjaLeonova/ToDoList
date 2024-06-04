using System.ComponentModel.DataAnnotations;

namespace BLL.Models;

public class TodoItemModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }
    
    public bool IsCompleted { get; set; }
}