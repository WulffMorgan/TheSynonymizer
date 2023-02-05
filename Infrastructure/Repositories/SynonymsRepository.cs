using Application.Interfaces;

namespace Infrastructure.Repositories;

public class SynonymsRepository : ISynonymsRepository
{
    private readonly Dictionary<string, Guid> _words = new();
    private readonly Dictionary<Guid, ISet<string>> _synonyms = new();

    public void AddSynonyms(params string[] synonyms)
        => AddSynonyms((IEnumerable<string>)synonyms);
    public void AddSynonyms(IEnumerable<string> synonyms)
    {
        synonyms = synonyms
            .Select(w => w.ToLower())
            .ToHashSet();

        if(((ISet<string>)synonyms).Count <= 1)
            return;

        var commonIdentifier = Guid.NewGuid();
        HashSet<string> newSet = new();
        HashSet<Guid> foundIdentifiers = new();

        foreach(var word in synonyms)
        {
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

        _=ConcatenateWithFound(newSet, foundIdentifiers);
        _synonyms.Add(commonIdentifier, newSet);
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

    private ISet<string> ConcatenateWithFound(ISet<string> newSet, ISet<Guid> foundIdentifiers)
    {
        foreach(var identifier in foundIdentifiers)
        {
            var synonyms = _synonyms[identifier];
            foreach(var synonym in synonyms)
                _=newSet.Add(synonym);
            _=_synonyms.Remove(identifier);
        }
        return newSet;
    }
}
