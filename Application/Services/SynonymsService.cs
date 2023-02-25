using Application.Interfaces;
using Domain.Models;

namespace Application.Services;

public class SynonymsService : ISynonymsService
{
    private readonly ISynonymsRepository _synonymsRepository;

    public SynonymsService(ISynonymsRepository synonymsRepository)
    {
        _synonymsRepository=synonymsRepository;
    }

    public void AddSynonyms(params string[] synonyms)
        => _synonymsRepository.AddSynonyms(synonyms);
    public void AddSynonyms(ISet<string> synonyms)
        => _synonymsRepository.AddSynonyms(synonyms);

    public WordWithSynonyms GetSynonymsForWord(string word)
    {
        word = word.Trim().ToLower();
        var synonyms = _synonymsRepository.GetSynonymsForWord(word);
        return new WordWithSynonyms(word, synonyms);
    }
}
