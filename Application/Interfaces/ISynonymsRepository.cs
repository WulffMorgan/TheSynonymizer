namespace Application.Interfaces;

public interface ISynonymsRepository
{
    void AddSynonyms(params string[] synonyms);
    void AddSynonyms(ISet<string> synonyms);
    IEnumerable<string> GetSynonymsForWord(string word);
}
