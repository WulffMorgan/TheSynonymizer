using Application.Interfaces;
using Infrastructure.Repositories;

namespace Infrastructure.UnitTests;

[TestClass]
public class SynonymsRepositoryTest
{
    [TestMethod]
    public void AddSynonyms_AddThenGet_ShouldGetSynonyms()
    {
        // Arrange
        var synonymsRepository = GetNewSynonymsRepository();
        IEnumerable<string> gottenSynonyms;

        // Act
        synonymsRepository.AddSynonyms("Pig", "Warthog", "Pumbaa");
        gottenSynonyms = synonymsRepository.GetSynonymsForWord("Warthog");

        // Assert
        Assert.AreEqual(2, gottenSynonyms.Count());
    }

    [TestMethod]
    public void AddSynonyms_AddNewThenAddNewDuplicate_ShouldNotAddDuplicate()
    {
        // Arrange
        var synonymsRepository = GetNewSynonymsRepository();
        IEnumerable<string> gottenSynonyms;

        // Act
        synonymsRepository.AddSynonyms("Pig", "Warthog", "Pumbaa");
        synonymsRepository.AddSynonyms("Swine", "Pig");
        gottenSynonyms = synonymsRepository.GetSynonymsForWord("Swine");

        // Assert
        Assert.AreEqual(3, gottenSynonyms.Count());
    }

    [TestMethod]
    public void AddSynonyms_AddNewThenAddMore_ShouldAddAll()
    {
        // Arrange
        var synonymsRepository = GetNewSynonymsRepository();
        IEnumerable<string> gottenSynonymsBefore,
            gottenSynonymsAfter;

        // Act
        synonymsRepository.AddSynonyms("ground");
        gottenSynonymsBefore = synonymsRepository.GetSynonymsForWord("ground");
        synonymsRepository.AddSynonyms("ground", "dirt", "earth", "soil");
        gottenSynonymsAfter = synonymsRepository.GetSynonymsForWord("ground");

        // Assert
        Assert.AreEqual(0, gottenSynonymsBefore.Count());
        Assert.AreEqual(3, gottenSynonymsAfter.Count());
    }

    [TestMethod]
    public void AddSynonyms_AddDuplicates_ShouldIgnoreDuplicates()
    {
        // Arrange
        var synonymsRepository = GetNewSynonymsRepository();
        IEnumerable<string> gottenSynonymsBefore,
            gottenSynonymsAfter;

        // Act
        synonymsRepository.AddSynonyms("ground", "dirt", "earth");
        gottenSynonymsBefore = synonymsRepository.GetSynonymsForWord("ground");
        synonymsRepository.AddSynonyms("ground", "dirt", "earth", "soil");
        gottenSynonymsAfter = synonymsRepository.GetSynonymsForWord("ground");

        // Assert
        Assert.AreEqual(2, gottenSynonymsBefore.Count());
        Assert.AreEqual(3, gottenSynonymsAfter.Count());
    }

    [TestMethod]
    public void AddSynonyms_AddConnecting_ShouldConnectSynonyms()
    {
        // Arrange
        var synonymsRepository = GetNewSynonymsRepository();
        IEnumerable<string> gottenSynonyms;

        // Act
        synonymsRepository.AddSynonyms("police", "cop");
        synonymsRepository.AddSynonyms("policeman", "po-po");
        synonymsRepository.AddSynonyms("police", "policeman");
        gottenSynonyms = synonymsRepository.GetSynonymsForWord("police");

        // Assert
        Assert.AreEqual(3, gottenSynonyms.Count());
    }

    [TestMethod]
    public void GetSynonymsForWord_UnknownWord_ShouldReturnEmpty()
    {
        // Arrange
        var synonymsRepository = GetNewSynonymsRepository();
        IEnumerable<string> gottenSynonyms;

        // Act
        gottenSynonyms = synonymsRepository.GetSynonymsForWord("Supercalifragilisticexpialidocious");

        // Assert
        Assert.AreEqual(0, gottenSynonyms.Count());
    }

    [TestMethod]
    public void GetSynonymsForWord_GetTwice_ShouldNotReferenceSameObject()
    {
        // Arrange
        var synonymsRepository = GetNewSynonymsRepository();
        IEnumerable<string> gottenSynonyms1, gottenSynonyms2;

        // Act
        synonymsRepository.AddSynonyms("distant", "far", "remote");
        gottenSynonyms1 = synonymsRepository.GetSynonymsForWord("remote");
        gottenSynonyms2 = synonymsRepository.GetSynonymsForWord("remote");

        // Assert
        Assert.AreNotSame(gottenSynonyms1, gottenSynonyms2);
    }

    [TestMethod]
    public void GetSynonyms_ShouldBeCaseInsensitive()
    {
        // Arrange
        var synonymsRepository = GetNewSynonymsRepository();
        int synonymCountLowercase, synonymCountUppercase;

        // Act
        synonymsRepository.AddSynonyms("big", "large");
        synonymsRepository.AddSynonyms("BIG", "HUGE", "ENORMOUS");
        synonymCountLowercase = synonymsRepository.GetSynonymsForWord("big").Count();
        synonymCountUppercase = synonymsRepository.GetSynonymsForWord("BIG").Count();

        // Assert
        Assert.AreEqual(synonymCountUppercase, synonymCountLowercase);
    }

    private static ISynonymsRepository GetNewSynonymsRepository()
        => new SynonymsRepository();
}
