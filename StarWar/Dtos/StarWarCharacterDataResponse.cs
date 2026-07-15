namespace StarWar.Dtos;

public record StarWarCharacterDataResponse
{
    public double AverageHeight { get; set; }
    public double AverageWeight { get; set; }
    
    public double Percentile95Height { get; set; }
    
    public double Percentile95Weight { get; set; }
    
}