namespace Domain.Models;

public record WordWithSynonyms(string Word, IEnumerable<string> Synonyms);
