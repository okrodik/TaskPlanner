using System.ComponentModel.DataAnnotations; 
 
namespace TaskPlanner.Models 
{ 
    public class TaskItem 
    { 
        public int Id { get; set; } 
        public string Title { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty; 
        public DateTime CreatedDate { get; set; } 
        public bool IsCompleted { get; set; } 
        public DateTime? CompletedDate { get; set; } 
    } 
} 
