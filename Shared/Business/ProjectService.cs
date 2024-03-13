using AutoMapper;
using Repositories;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Business
{
    public class ProjectService(IUnitOfWork unitOfWork, IMapper mapper) : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IGenericRepository<Project> _projectRepository = unitOfWork.Repository<Project>();
        private readonly IGenericRepository<User> _userRepository= unitOfWork.Repository<User>();
        private readonly IMapper _mapper = mapper;

        public async Task<List<ProjectListVm>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return _mapper.Map<List<ProjectListVm>>(projects);
        }

        public async Task<ProjectDetailsVm?> GetProjectByIdAsync(string projectId)
        {
            var projectEntity = await _projectRepository.GetByIdAsync(projectId);
            return _mapper.Map<ProjectDetailsVm>(projectEntity);
        }

        public async Task<ProjectDetailsVm> CreateProjectAsync(ProjectDetailsVm project)
        {
            var projectEntity = _mapper.Map<Project>(project);
            projectEntity.UserId = (await _userRepository.GetAllAsync()).First().Id;
            await _projectRepository.AddAsync(projectEntity);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ProjectDetailsVm>(projectEntity);
        }

        public async Task UpdateProjectAsync(ProjectDetailsVm project)
        {
            var projectEntity = _mapper.Map<Project>(project);
            _projectRepository.Update(projectEntity);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteProjectAsync(string projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project != null)
            {
                _projectRepository.Delete(project);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
