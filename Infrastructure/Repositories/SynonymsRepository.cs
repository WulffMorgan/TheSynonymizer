using Application.Interfaces;

namespace Infrastructure.Repositories;

public class SynonymsRepository : ISynonymsRepository
{
    private readonly Dictionary<string, Guid> _words = new();
    private readonly Dictionary<Guid, HashSet<string>> _synonyms = new();

    public void AddSynonyms(params string[] synonyms)
        => AddSynonyms((IEnumerable<string>)synonyms);
    public void AddSynonyms(IEnumerable<string> synonyms)
    {
        if(synonyms.Count() <= 1)
            return;

        var commonIdentifier = Guid.NewGuid();
        var newSet = new HashSet<string>();
        List<Guid> foundIdentifiers = new();

        foreach(var word_ in synonyms)
        {
            var word = word_.ToLower();
            if(_words.TryGetValue(word, out var foundIdentifier))
            {
                foundIdentifiers.Add(foundIdentifier);
                _words[word] = commonIdentifier;
            }
            else
            {
                _words.Add(word, commonIdentifier);
                _=newSet.Add(word);
            }
        }

        var concatenation = ConcatenateWithFound(newSet, foundIdentifiers);
        _synonyms.Add(commonIdentifier, concatenation);
    }

    public IEnumerable<string> GetSynonymsForWord(string word)
    {
        word = word.ToLower();

        if(!_words.TryGetValue(word, out var commonIdentifier))
            return Enumerable.Empty<string>();

        return _synonyms[commonIdentifier]
            .Where(w => w != word)
            .ToList();
    }

    private HashSet<string> ConcatenateWithFound(HashSet<string> newSet, List<Guid> foundIdentifiers)
    {
        IEnumerable<string> concatenation = newSet;
        foreach(var identifier in foundIdentifiers)
            concatenation = concatenation.Concat(_synonyms[identifier]);
        return concatenation.ToHashSet();
    }
}
