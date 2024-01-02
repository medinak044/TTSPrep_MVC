using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTSPrep_MVC.Data;
using TTSPrep_MVC.Models;
using TTSPrep_MVC.Repository;

namespace TTSPrep_MVC.Tests.Repository;

public class WordRepositoryTests
{
    private Word exampleWord = new Word()
    {
        Id = Guid.NewGuid().ToString(),
        OriginalSpelling = "Erika",
        ModifiedSpelling = "Ericah",
        ProjectId = Guid.NewGuid().ToString()
    };

    private async Task<AppDbContext> GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var databaseContext = new AppDbContext(options);
        databaseContext.Database.EnsureCreated();
        if (await databaseContext.Words.CountAsync() < 0)
        {
            for (int i = 0; i < 10; i++)
            {
                databaseContext.Words.Add(exampleWord);
                await databaseContext.SaveChangesAsync();
            }
        }

        return databaseContext;
    }

    [Fact]
    public async void WordRepository_AddAsync_ReturnsBool()
    {
        #region Arrange
        var word = exampleWord;
        var dbContext = await GetDbContext();
        var wordRepository = new WordRepository(dbContext);
        #endregion

        #region Act
        var result = await wordRepository.AddAsync(word);
        #endregion

        #region Assert
        result.Should().Be(true);
        #endregion
    }

    [Fact]
    public async void WordRepository_GetByIdAsync_ReturnsBool()
    {
        #region Arrange
        var wordId = exampleWord.Id;
        var dbContext = await GetDbContext();
        var wordRepository = new WordRepository(dbContext);
        #endregion

        #region Act
        var result = wordRepository.GetByIdAsync(wordId);
        #endregion

        #region Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<Word>>();
        #endregion
    }
}
