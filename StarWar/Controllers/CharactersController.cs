using Microsoft.AspNetCore.Mvc;
using StarWar.Dtos;
using StarWar.Service;

namespace StarWar.Controllers;

[ApiController]
[Route("[controller]")]
public class CharactersController : Controller
{
    private readonly IStarWarCharacterDataFetcher _dataFetcher;
    public CharactersController(IStarWarCharacterDataFetcher dataFetcher)
    {
        _dataFetcher = dataFetcher;
    }

    [HttpGet(Name = "GetCharacters")]
    public async Task<IActionResult> Get()
    {
        string url = "https://swapi.py4e.com/api/people?format=json";
        
        var response = await _dataFetcher.GetCharacterDataAsync(new StarWarCharacterDataRequest { Url = url } );

        return Ok($"Average Height: {response.AverageHeight}" + " cm \n"+
                  $"Average Weight:  {response.AverageWeight}" +" kg \n"+
                  $"95th Percentile Height: {response.Percentile95Height}" + " cm \n"+
                  $"95th Percentile Weight:{response.Percentile95Weight}" + " kg \n"
                  );
    }
}