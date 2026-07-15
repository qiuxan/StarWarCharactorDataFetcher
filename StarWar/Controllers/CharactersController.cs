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

        var averageHeight = response.AverageHeight;
        var averageWeight = response.AverageWeight;
        
        var percentile95Height = response.Percentile95Height;
        var percentile95Weight = response.Percentile95Weight;


        return Ok($"Average Height: {averageHeight}" + " cm \n"+
                  $"Average Weight:  {averageWeight}" +" kg \n"+
                  $"95th Percentile Height: {percentile95Height}" + " cm \n"+
                  $"95th Percentile Weight:{percentile95Weight}" + " kg \n"
                  );
    }
}