using Entities.Enums;

namespace Entities.Models
{
    public class Project: BaseModel
    {
        public required string ProjectName { get; set; }
        public required string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectStatus Status { get; set; }
        public int Priority { get; set; }
        public decimal Budget { get; set; }
        public string? Comments { get; set; }
        public string UserId { get; set; }
        public User Owner { get; set; }
    }
}
