using Entities.Enums;

namespace Entities.ViewModels
{
    public class ProjectDetailsVm
    {
        public string? ProjectId { get; set; }
        public required string ProjectName { get; set; }
        public required string Description { get; set; }
        public required string StartDate { get; set; }
        public string? EndDate { get; set; }
        public ProjectStatus Status { get; set; }
        public int Priority { get; set; }
        public decimal Budget { get; set; }
        public string? Comments { get; set; }
    }
}
