using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTSPrep_MVC.Controllers;
using TTSPrep_MVC.Models;
using TTSPrep_MVC.Repository.IRepository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using TTSPrep_MVC.Models.ViewModels;

namespace TTSPrep_MVC.Tests.Controller;

public class ProjectControllerTests
{
    private IUnitOfWork _unitOfWork;
    private ProjectController _projectController;

    public ProjectControllerTests()
    {
        #region Dependencies
        _unitOfWork = A.Fake<IUnitOfWork>();
        #endregion

        #region SUT
        _projectController = new ProjectController(_unitOfWork);
        #endregion
    }

    [Fact]
    public void ProjectController_Index_ReturnsSuccess()
    {
        #region Arrange
        string projectId = Guid.NewGuid().ToString(); // Parameter for controller method
        var project = A.Fake<Project>();
        A.CallTo(() => _unitOfWork.Projects.GetByIdAsync(projectId)).Returns(project);
        #endregion

        #region Act
        var result = _projectController.Index(projectId);
        #endregion

        #region Assert
        result.Should().BeOfType<Task<IActionResult>>();
        #endregion
    }

    [Fact]
    public void ProjectController_Create_ReturnsSuccess()
    {
        #region Arrange
        var currentUserId = Guid.NewGuid().ToString();
        var project = A.Fake<ProjectCreateVM>();
        #endregion

        #region Act
        var result = _projectController.Create();
        #endregion

        #region Assert
        result.Should().BeOfType<Task<IActionResult>>();
        #endregion
    }

    [Fact]
    public void ProjectController_Edit_ReturnsSuccess()
    {
        #region Arrange
        string projectId = Guid.NewGuid().ToString(); // Parameter for controller method
        var project = A.Fake<Project>();
        A.CallTo(() => _unitOfWork.Projects.GetByIdAsync(projectId)).Returns(project);
        #endregion

        #region Act
        var result = _projectController.Edit(projectId);
        #endregion

        #region Assert
        result.Should().BeOfType<Task<IActionResult>>();
        #endregion
    }

    [Fact]
    public void ProjectController_Delete_ReturnsSuccess()
    {
        #region Arrange
        string projectId = Guid.NewGuid().ToString(); // Parameter for controller method
        var project = A.Fake<Project>();
        A.CallTo(() => _unitOfWork.Projects.GetByIdAsync(projectId)).Returns(project);
        #endregion

        #region Act
        var result = _projectController.Delete(projectId);
        #endregion

        #region Assert
        result.Should().BeOfType<Task<IActionResult>>();
        #endregion
    }
}
