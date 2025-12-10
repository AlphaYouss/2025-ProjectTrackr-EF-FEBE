using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class ActivityLog
    {
        [Key]
        public Guid activityLogId { get; set; }
        [Required]
        public string action { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime createdAt { get; set; }

        [Required]
        public User user { get; set; }

        [Required]
        public Project project { get; set; }

        public TaskItem? task { get; set; }
    }
}