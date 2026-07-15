using Microsoft.AspNetCore.Mvc;
using StarWar.Controllers.Dtos;
using StarWar.Controllers.Service;

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
    public IActionResult Get()
    {
        string url = "https://swapi.py4e.com/api/people?format=json";
        
        var response = _dataFetcher.GetCharacterData(new StarWarCharacterDataRequest { Url = url } );

        var averageHeight = response.AverageHeight;
        var averageWeight = response.AverageWeight;
        
        var percentile95Height = averageHeight * 0.95;
        var percentile95Weight = averageWeight * 0.95;


        return Ok($"Average Height: {averageHeight}" + " cm \n"+
                  $"Average Weight:  {averageWeight}" +" kg \n"+
                  $"95th Percentile Height: {percentile95Height}" + " cm \n"+
                  $"95th Percentile Weight:{percentile95Weight}" + " kg \n"
                  );
    }
}