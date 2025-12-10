using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Project
    {
        [Key]
        public Guid projectId { get; set; }
        [Required, MaxLength(200)]
        public string name { get; set; }
        public string description { get; set; }
        public DateTime createdAt { get; set; }

        [Required]
        public User owner { get; set; }

        public IReadOnlyCollection<TaskItem> tasks { get; set; }
        public IReadOnlyCollection<ActivityLog> activityLogs { get; set; }
    }
}