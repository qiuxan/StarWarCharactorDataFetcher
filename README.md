# Star Wars Character Statistics

ASP.NET Core API solution for the Star Wars coding challenge.

The application fetches Star Wars character data from SWAPI, follows pagination across all result pages, filters out characters with non-numeric height or mass values, and returns character statistics.

## Requirements Covered

- Fetch all characters from SWAPI.
- Follow the `next` URL until there are no more pages.
- Ignore characters with unknown or non-numeric height or weight.
- Calculate:
  - Average height in centimeters, rounded to 2 decimals.
  - Average weight in kilograms, rounded to 2 decimals.
  - 95th percentile height in centimeters.
  - 95th percentile weight in kilograms.
- Display the results through a simple API endpoint.

## Tech Stack

- .NET 10
- ASP.NET Core Web API
- `HttpClient` typed client
- Swagger / OpenAPI

## API Source

The application uses the SWAPI people endpoint:

```text
https://swapi.py4e.com/api/people?format=json
```

Each SWAPI response contains a `results` array and a `next` URL. The service keeps requesting `next` until it becomes `null`.

## Project Structure

```text
StarWar/
  Controllers/
    CharactersController.cs
  Dtos/
    StarWarCharacterDataRequest.cs
    StarWarCharacterDataResponse.cs
    Swapi/
      SwapiPeopleResponse.cs
  Service/
    IStarWarCharacterDataFetcher.cs
    StarWarCharacterDataFetcher.cs
  Program.cs
```

## How It Works

1. `CharactersController` calls `IStarWarCharacterDataFetcher`.
2. `StarWarCharacterDataFetcher` uses `HttpClient` to fetch SWAPI data.
3. The service loops through all paginated responses.
4. Characters with invalid height or mass values are ignored.
5. Valid character values are used to calculate averages and 95th percentile values.
6. The controller returns a clear text response.

## Run Locally

From the repository root:

```bash
dotnet restore
dotnet run --project StarWar
```

The app runs on:

```text
http://localhost:5001
https://localhost:7216
```

## Endpoint

```http
GET /Characters
```

Example:

```bash
curl http://localhost:5001/Characters
```

Example output:

```text
Average Height: 174.36 cm
Average Weight: 82.45 kg
95th Percentile Height: 202 cm
95th Percentile Weight:135 kg
```

## Swagger

When running in development mode, Swagger UI is available at:

```text
http://localhost:5001/swagger
https://localhost:7216/swagger
```

## Calculation Notes

Average values are calculated with LINQ `Average()` and rounded to 2 decimal places.

The 95th percentile is calculated by sorting the numeric values and selecting the value at the ceiling of `count * 0.95`, minus one for zero-based indexing.

Characters are included only when both height and mass can be parsed as numbers.
