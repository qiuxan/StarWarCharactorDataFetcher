using StarWar.Dtos;
using StarWar.Dtos.Swapi;
using StarWar.Service;

namespace StarWar.Controllers.Service;

public class StarWarCharacterDataFetcher:IStarWarCharacterDataFetcher
{
    private readonly HttpClient _httpClient;
   
    public StarWarCharacterDataFetcher(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<StarWarCharacterDataResponse> GetCharacterDataAsync(StarWarCharacterDataRequest request)
    {

        List<SwapiPerson>  characterList = [];
        var nextUrl = request.Url;

        while (nextUrl != null)
        {
            var response = await _httpClient.GetFromJsonAsync<SwapiPeopleResponse>(nextUrl);

            var characterListToAdd = response?.Results
                .Where(character =>
                    int.TryParse(character.Height, out _) &&
                    int.TryParse(character.Mass, out _))
                .ToList();

            if (characterListToAdd != null)
            {
                characterList.AddRange(characterListToAdd);
            }

            nextUrl = response?.Next;
        }

        
        var averageHeight = 
            RoundedToTwoDecimals(
                characterList
                .Average(character => double.Parse(character.Height)));
        var averageWeight = RoundedToTwoDecimals(characterList
            .Average(character => double.Parse(character.Mass)));
        
        return new StarWarCharacterDataResponse
        {
            AverageHeight = averageHeight,
            AverageWeight = averageWeight,
            Percentile95Height = RoundedToTwoDecimals(averageHeight * 0.95),
            Percentile95Weight = RoundedToTwoDecimals(averageWeight * 0.95),
        };
    }

    private static double RoundedToTwoDecimals(double input)
    {
        return Math.Round(input,2);
    }
}