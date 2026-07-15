namespace StarWar.Controllers.Dtos;

public record StarWarCharacterDataRequest
{
    public string Url { get; set; } = String.Empty;
}