using System.Security.Claims;
using AutoMapper;
using AutoMoq;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Moq;
using Repositories;

namespace Business.Tests
{
    [TestFixture]
    public class ProjectServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IGenericRepository<Project>> _projectRepositoryMock;
        private Mock<IGenericRepository<User>> _userRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private ProjectService _projectService;
        private AutoMoqer _mocker;
        private static string projectId = Guid.NewGuid().ToString();
        private Project _project = new Project
        {
            Id = projectId,
            ProjectName = "Project 1",
            Description = "Description 1",
            UserId = "user1",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
        };
        private ProjectDetailsVm _projectDetailsVm = new ProjectDetailsVm
        {
            ProjectName = "Project 1",
            Description = "Description 1",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
        };

        private ProjectListVm _projectListVm = new ProjectListVm
        {
            ProjectName = "Project 1",
            Description = "Description 1",
            ProjectId = projectId
        };

        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMoqer();
            _unitOfWorkMock = _mocker.GetMock<IUnitOfWork>();
            _projectRepositoryMock = _mocker.GetMock<IGenericRepository<Project>>();
            _userRepositoryMock = _mocker.GetMock<IGenericRepository<User>>();
            _unitOfWorkMock.Setup(m => m.Repository<Project>()).Returns(_projectRepositoryMock.Object);
            _unitOfWorkMock.Setup(m => m.Repository<User>()).Returns(_userRepositoryMock.Object);
            _mapperMock = _mocker.GetMock<IMapper>();
            _httpContextAccessorMock = _mocker.GetMock<IHttpContextAccessor>();
            _projectService = new ProjectService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetAllProjectsAsync_ShouldReturnListOfProjectListVm()
        {
            // Arrange
            var projects = new List<Project> { _project };
            var projectListVms = new List<ProjectListVm> { _projectListVm };
            _projectRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(projects);
            _mapperMock.Setup(m => m.Map<List<ProjectListVm>>(projects)).Returns(projectListVms);

            // Act
            var result = await _projectService.GetAllProjectsAsync();

            // Assert
            Assert.AreEqual(projectListVms, result);
        }

        [Test]
        public async Task GetProjectByIdAsync_ShouldReturnProjectDetailsVm()
        {
            // Arrange
            var projectId = "1";
            var projectEntity = _project;
            var projectDetailsVm = _projectDetailsVm;

            _projectRepositoryMock.Setup(r => r.GetByIdAsync(projectId)).ReturnsAsync(projectEntity);
            _mapperMock.Setup(m => m.Map<ProjectDetailsVm>(projectEntity)).Returns(projectDetailsVm);

            // Act
            var result = await _projectService.GetProjectByIdAsync(projectId);

            // Assert
            Assert.AreEqual(projectDetailsVm, result);
        }

        [Test]
        public async Task CreateProjectAsync_ShouldReturnProjectDetailsVm()
        {
            // Arrange
            var project = _projectDetailsVm;
            var projectEntity = _project;
            var userId = "user1";

            _userRepositoryMock.Setup(u => u.GetAllAsync()).ReturnsAsync(new List<User>
            {
                new User { Id = userId }
            });
            _mapperMock.Setup(m => m.Map<Project>(project)).Returns(projectEntity);
            _mapperMock.Setup(m => m.Map<ProjectDetailsVm>(projectEntity)).Returns(project);

            // Act
            var result = await _projectService.CreateProjectAsync(project);

            // Assert
            Assert.AreEqual(project, result);
        }

        [Test]
        public async Task UpdateProjectAsync_ShouldCallRepositoryUpdateAndCompleteAsync()
        {
            // Arrange
            var project = _projectDetailsVm;
            var projectEntity = _project;

            _mapperMock.Setup(m => m.Map<Project>(project)).Returns(projectEntity);

            // Act
            await _projectService.UpdateProjectAsync(project);

            // Assert
            _projectRepositoryMock.Verify(r => r.Update(projectEntity), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Test]
        public async Task DeleteProjectAsync_ShouldCallRepositoryDeleteAndCompleteAsync()
        {
            // Arrange
            var projectId = "1";
            var project = _project;

            _projectRepositoryMock.Setup(r => r.GetByIdAsync(projectId)).ReturnsAsync(project);

            // Act
            await _projectService.DeleteProjectAsync(projectId);

            // Assert
            _projectRepositoryMock.Verify(r => r.Delete(project), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }
    }
}
