using StarWar.Dtos;

namespace StarWar.Service;

public interface IStarWarCharacterDataFetcher
{
    Task<StarWarCharacterDataResponse> GetCharacterDataAsync(StarWarCharacterDataRequest request);

}