using Entities.Enums;

namespace Entities.ViewModels
{
    public class ProjectListVm
    {
        public required string ProjectId { get; set; }
        public required string ProjectName { get; set; }
        public required string Description { get; set; }
        public ProjectStatus Status { get; set; }
    }
}
