using StarWar.Controllers.Dtos;

namespace StarWar.Controllers.Service;

public interface IStarWarCharacterDataFetcher
{
    StarWarCharacterDataResponse  GetCharacterData(StarWarCharacterDataRequest request);
}