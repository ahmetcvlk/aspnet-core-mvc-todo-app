using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class TodoItem
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage = "The title field is required.")]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }  // Açıklama
        public bool IsDone { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Oluşturulma tarihi
    }
}
