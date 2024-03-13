using AutoMapper;
using Entities.Models;
using Entities.ViewModels;

namespace Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectDetailsVm>()
               .ForMember(projectVm => projectVm.StartDate, opt => opt.MapFrom(model => model.StartDate.ToShortDateString()))
               .ForMember(projectVm => projectVm.EndDate, opt => opt.MapFrom(model => model.EndDate.ToString()))
               .ForMember(projectVm => projectVm.ProjectId, opt => opt.MapFrom(model => model.Id));
            CreateMap<ProjectDetailsVm, Project>()
                .ForMember(project => project.StartDate, opt => opt.MapFrom(vm => DateTime.Parse(vm.StartDate)))
                .ForMember(project => project.EndDate, opt => opt.MapFrom(vm => DateTime.Parse(vm.EndDate)))
                .ForMember(project => project.Id, opt => opt.MapFrom(vm => vm.ProjectId ?? Guid.NewGuid().ToString()));
            CreateMap<Project, ProjectListVm>()
                .ForMember(projectVm => projectVm.ProjectId, opt => opt.MapFrom(model => model.Id));
        }
    }
}
