using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SynonymsController : ControllerBase
{
    private readonly ISynonymsService _synonymsService;
    private readonly ILogger<SynonymsController> _logger;

    public SynonymsController(ISynonymsService synonymsService, ILogger<SynonymsController> logger)
    {
        _synonymsService=synonymsService;
        _logger=logger;
    }

    // GET api/<SynonymsController>/word
    [HttpGet("{word}")]
    public ActionResult<WordWithSynonyms> Get(string word)
    {
        var wordWithSynonyms = _synonymsService.GetSynonymsForWord(word);
        return wordWithSynonyms is not null
            ? Ok(wordWithSynonyms)
            : NotFound();
    }

    // POST api/<SynonymsController>
    [HttpPost]
    public ActionResult<WordWithSynonyms> Post([FromBody] ISet<string> synonyms)
    {
        if(!synonyms.Any())
        {
            return BadRequest();
        }

        _synonymsService.AddSynonyms(synonyms);

        var word = synonyms.First();
        var wordWithSynonyms = _synonymsService.GetSynonymsForWord(word);
        return CreatedAtAction(nameof(Get), new { word }, wordWithSynonyms);
    }
}
