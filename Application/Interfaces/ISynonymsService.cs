using Domain.Models;

namespace Application.Interfaces;

public interface ISynonymsService
{
    void AddSynonyms(params string[] synonyms);
    void AddSynonyms(ISet<string> synonyms);
    WordWithSynonyms GetSynonymsForWord(string word);
}
