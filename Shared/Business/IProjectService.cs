using Entities.ViewModels;

namespace Business
{
    public interface IProjectService
    {
        Task<List<ProjectListVm>> GetAllProjectsAsync();
        Task<ProjectDetailsVm?> GetProjectByIdAsync(string projectId);
        Task<ProjectDetailsVm> CreateProjectAsync(ProjectDetailsVm project);
        Task UpdateProjectAsync(ProjectDetailsVm project);
        Task DeleteProjectAsync(string projectId);
    }
}