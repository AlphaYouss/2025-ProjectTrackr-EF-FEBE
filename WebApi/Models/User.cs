using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class User
    {
        [Key]
        public Guid userId { get; set; } = Guid.NewGuid();
        [Required, MaxLength(20)]
        public string username { get; set; }
        [Required, MaxLength(200)]
        public string email { get; set; }
        [Required]
        public string passwordHash { get; set; }
        public DateTime createdAt { get; set; }

        public IReadOnlyCollection<Project> projects { get; set; }
    }
}