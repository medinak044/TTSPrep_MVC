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

namespace TTSPrep_MVC.Tests.Controller;

public class WordControllerTests
{
    private IUnitOfWork _unitOfWork;
    private WordController _wordController;

    public WordControllerTests()
    {
        #region Dependencies
        _unitOfWork = A.Fake<IUnitOfWork>();
        #endregion

        #region SUT
        _wordController = new WordController(_unitOfWork);
        #endregion
    }

    [Fact]
    public void WordController_Index_ReturnsSuccess()
    {
        #region Arrange
        string projectId = Guid.NewGuid().ToString(); // Parameter for controller method
        var project = A.Fake<Project>();
        A.CallTo(() => _unitOfWork.Projects.GetByIdAsync(projectId)).Returns(project);
        #endregion

        #region Act
        var result = _wordController.Index(projectId);
        #endregion

        #region Assert
        result.Should().BeOfType<Task<IActionResult>>();
        #endregion
    }

    [Fact]
    public void WordController_Create_ReturnsSuccess()
    {
        #region Arrange
        string projectId = Guid.NewGuid().ToString(); // Parameter for controller method
        var word = A.Fake<Word>();
        A.CallTo(() => _unitOfWork.Words.GetByIdAsync(projectId)).Returns(word);
        #endregion

        #region Act
        var result = _wordController.Create(projectId);
        #endregion

        #region Assert
        result.Should().BeOfType<Task<IActionResult>>();
        #endregion
    }

    [Fact]
    public void WordController_Edit_ReturnsSuccess()
    {
        #region Arrange
        string wordId = Guid.NewGuid().ToString(); // Parameter for controller method
        var word = A.Fake<Word>();
        A.CallTo(() => _unitOfWork.Words.GetByIdAsync(wordId)).Returns(word);
        #endregion

        #region Act
        var result = _wordController.Edit(wordId);
        #endregion

        #region Assert
        result.Should().BeOfType<Task<IActionResult>>();
        #endregion
    }

    [Fact]
    public void WordController_Delete_ReturnsSuccess()
    {
        #region Arrange
        string wordId = Guid.NewGuid().ToString(); // Parameter for controller method
        var word = A.Fake<Word>();
        A.CallTo(() => _unitOfWork.Words.GetByIdAsync(wordId)).Returns(word);
        #endregion

        #region Act
        var result = _wordController.Delete(wordId);
        #endregion

        #region Assert
        result.Should().BeOfType<Task<IActionResult>>();
        #endregion
    }
}
