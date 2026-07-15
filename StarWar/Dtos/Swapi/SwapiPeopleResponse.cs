namespace StarWar.Dtos.Swapi;


public class SwapiPeopleResponse
{
    public int Count { get; set; }
    public string? Next { get; set; }
    public string? Previous { get; set; }
    public List<SwapiPerson> Results { get; set; } = [];
}

public class SwapiPerson
{
    public string Height { get; set; } = string.Empty;
    public string Mass { get; set; } = string.Empty;
}