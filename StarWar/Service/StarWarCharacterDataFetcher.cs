using StarWar.Dtos;
using StarWar.Dtos.Swapi;

namespace StarWar.Service;

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

        if (characterList.Count == 0)
        {
            return new StarWarCharacterDataResponse();
        }
        
        var averageHeight = 
            RoundedToTwoDecimals(
                characterList
                .Average(character => double.Parse(character.Height)));
        var averageWeight = RoundedToTwoDecimals(characterList
            .Average(character => double.Parse(character.Mass)));

        var percentile95Height = CalculatePercentile(
            characterList,
            p => double.Parse(p.Height),
            0.95
        );
        
        var percentile95Weight = CalculatePercentile(
            characterList,
            p => double.Parse(p.Mass),
            0.95
        );
        
        
        
        return new StarWarCharacterDataResponse
        {
            AverageHeight = averageHeight,
            AverageWeight = averageWeight,
            Percentile95Height = percentile95Height,
            Percentile95Weight = percentile95Weight,
        };
    }

    private static double RoundedToTwoDecimals(double input)
    {
        return Math.Round(input,2);
    }
    
    private static double CalculatePercentile(List<SwapiPerson>  persons, Func<SwapiPerson, double> selector  ,double percentile)
    {
        var values = persons
            .Select(selector)
            .OrderBy(p=>p)
            .ToList();
        
        if (values.Count == 0)
        {
            return 0;
        }
        
        int index = (int)Math.Ceiling(values.Count * percentile) - 1;

        return values[index];

    }
    
}
