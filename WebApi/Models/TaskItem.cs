using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Done
    }

    public class TaskItem
    {
        [Key]
        public Guid taskItemId { get; set; }
        [Required, MaxLength(200)]
        public string title { get; set; }
        public string description { get; set; }
        public TaskStatus status { get; set; } = TaskStatus.ToDo;
        public DateTime createdAt { get; set; } = DateTime.Now;

        [Required]
        public Project project { get; set; }
    }
}