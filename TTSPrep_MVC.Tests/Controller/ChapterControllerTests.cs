using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTSPrep_MVC.Controllers;
using TTSPrep_MVC.Models;
using TTSPrep_MVC.Repository.IRepository;

namespace TTSPrep_MVC.Tests.Controller;

public class ChapterControllerTests
{
    private IUnitOfWork _unitOfWork;
    private ChapterController _chapterController;

    public ChapterControllerTests()
    {
        #region Dependencies
        _unitOfWork = A.Fake<IUnitOfWork>();
        #endregion

        #region SUT
        _chapterController = new ChapterController(_unitOfWork);
        #endregion
    }

    [Fact]
    public void ChapterController_Index_ReturnsSuccess()
    {
        #region Arrange
       
        #endregion

        #region Act
        var result = _chapterController.Index();
        #endregion

        #region Assert
        result.Should().BeOfType<Task<IActionResult>>();
        #endregion
    }

    [Fact]
    public void ChapterController_Create_ReturnsSuccess()
    {
        #region Arrange
        string projectId = Guid.NewGuid().ToString(); // Parameter for controller method
        // Note: Cannot run extension methods because they are static
        //var chapters = A.Fake<List<Chapter>>();
        //A.CallTo(() => _unitOfWork.Chapters.GetSome(c => c.ProjectId == projectId).ToList()).Returns(chapters);
        #endregion

        #region Act
        var result = _chapterController.Create(projectId);
        #endregion

        #region Assert
        result.Should().BeOfType<Task<IActionResult>>();
        #endregion
    }

    [Fact]
    public void ChapterController_Edit_ReturnsSuccess()
    {
        #region Arrange
        string chapterId = Guid.NewGuid().ToString(); // Parameter for controller method
        var chapter = A.Fake<Chapter>();
        A.CallTo(() => _unitOfWork.Chapters.GetByIdAsync(chapterId)).Returns(chapter);
        #endregion

        #region Act
        var result = _chapterController.Edit(chapterId);
        #endregion

        #region Assert
        result.Should().BeOfType<Task<IActionResult>>();
        #endregion
    }

    [Fact]
    public void ChapterController_Delete_ReturnsSuccess()
    {
        #region Arrange
        string chapterId = Guid.NewGuid().ToString(); // Parameter for controller method
        var chapter = A.Fake<Chapter>();
        A.CallTo(() => _unitOfWork.Chapters.GetByIdAsync(chapterId)).Returns(chapter);
        #endregion

        #region Act
        var result = _chapterController.Delete(chapterId);
        #endregion

        #region Assert
        result.Should().BeOfType<Task<IActionResult>>();
        #endregion
    }
}
