using Application.Interfaces;
using Application.Services;
using Domain.Models;
using Moq;

namespace Application.UnitTests;

[TestClass]
public class SynonymsServiceTest
{
    private readonly Mock<ISynonymsRepository> _repoMock = new();
    private readonly ISynonymsService _synonymsService;

    public SynonymsServiceTest()
    {
        _synonymsService = new SynonymsService(_repoMock.Object);
    }

    [TestMethod]
    public void AddSynonyms_WithArrayParameter_ShouldCallRepoMethod()
    {
        // Arrange
        var parameter = It.IsAny<string[]>();

        // Act
        _synonymsService.AddSynonyms(parameter);

        // Assert
        _repoMock.Verify(repo => repo.AddSynonyms(parameter), Times.Once());
    }

    [TestMethod]
    public void AddSynonyms_WithIEnumerableParameter_ShouldCallRepoMethod()
    {
        // Arrange
        var parameter = It.IsAny<IEnumerable<string>>();

        // Act
        _synonymsService.AddSynonyms(parameter);

        // Assert
        _repoMock.Verify(repo => repo.AddSynonyms(parameter), Times.Once());
    }

    [TestMethod]
    public void GetSynonymsForWord_ShouldWrapInRecordCorrectly()
    {
        // Arrange
        var word = "Gollum";
        var synonyms = new[] { "smeagol", "precious" };
        _=_repoMock.Setup(repo => repo.GetSynonymsForWord(word.ToLower())).Returns(synonyms);
        WordWithSynonyms result;

        // Act
        result = _synonymsService.GetSynonymsForWord(word);

        // Assert
        _repoMock.Verify(repo => repo.GetSynonymsForWord(word.ToLower()), Times.Once());
        Assert.AreEqual(word.ToLower(), result.Word);
        Assert.AreEqual(synonyms.Length, result.Synonyms.Count());
        for(var i = 1; i < synonyms.Length; i++)
        {
            Assert.IsTrue(result.Synonyms.Contains(synonyms[i]));
        }
    }
}
