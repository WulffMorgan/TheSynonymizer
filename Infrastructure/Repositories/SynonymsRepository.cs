using Application.Interfaces;

namespace Infrastructure.Repositories;

public class SynonymsRepository : ISynonymsRepository
{
    private readonly Dictionary<string, Guid> _words = new();
    private readonly Dictionary<Guid, ISet<string>> _synonyms = new();

    public void AddSynonyms(params string[] synonyms)
        => AddSynonyms(synonyms.ToHashSet());
    public void AddSynonyms(ISet<string> synonyms)
    {
        if (synonyms.Count <= 1)
        {
            return;
        }

        var newIdentifier = Guid.NewGuid();
        HashSet<string> newSet = new();
        HashSet<Guid> foundIdentifiers = new();

        foreach (var word in synonyms)
        {
            var trimmedLowercaseWord = word.Trim().ToLower();
            if (_words.TryGetValue(trimmedLowercaseWord, out var foundIdentifier))
            {
                _=foundIdentifiers.Add(foundIdentifier);
            }
            else
            {
                _words.Add(trimmedLowercaseWord, newIdentifier);
                _=newSet.Add(trimmedLowercaseWord);
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
