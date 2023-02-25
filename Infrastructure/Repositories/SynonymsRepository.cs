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
            .Select(w => w.Trim().ToLower())
            .ToHashSet();

        if (((ISet<string>)synonyms).Count <= 1)
        {
            return;
        }

        var newIdentifier = Guid.NewGuid();
        HashSet<string> newSet = new();
        HashSet<Guid> foundIdentifiers = new();

        foreach (var word in synonyms)
        {
            if (_words.TryGetValue(word, out var foundIdentifier))
            {
                _=foundIdentifiers.Add(foundIdentifier);
            }
            else
            {
                _words.Add(word, newIdentifier);
                _=newSet.Add(word);
            }
        }

        _=ConcatenateWithFound(newSet, foundIdentifiers, newIdentifier);
        _synonyms.Add(newIdentifier, newSet);
    }

    public IEnumerable<string> GetSynonymsForWord(string word)
    {
        word = word.Trim().ToLower();

        return _words.TryGetValue(word, out var commonIdentifier)
            ? _synonyms[commonIdentifier]
                .Where(w => w != word)
                .ToList()
            : Enumerable.Empty<string>();
    }

    private ISet<string> ConcatenateWithFound(ISet<string> newSet, ISet<Guid> foundIdentifiers, Guid newIdentifier)
    {
        foreach (var identifier in foundIdentifiers)
        {
            var synonyms = _synonyms[identifier];
            foreach (var synonym in synonyms)
            {
                _=newSet.Add(synonym);
                _words[synonym] = newIdentifier;
            }

            _=_synonyms.Remove(identifier);
        }
        return newSet;
    }
}
