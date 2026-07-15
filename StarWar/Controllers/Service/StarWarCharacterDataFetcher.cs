using StarWar.Controllers.Dtos;

namespace StarWar.Controllers.Service;

public class StarWarCharacterDataFetcher:IStarWarCharacterDataFetcher
{
    public StarWarCharacterDataResponse GetCharacterData(StarWarCharacterDataRequest request)
    {
        Console.WriteLine(request.Url);
        return new StarWarCharacterDataResponse
        {
            AverageHeight = 100,
            AverageWeight = 55,
        };
    }
}