namespace GameStore.API.Dtos
{
    public record GameSummaryDto(
        // Guid Id,
        // string Title,
        // string Genre,
        // decimal Price,
        // DateTime ReleaseDate

        int Id,
        string Name,
        string Genre,
        decimal Price,
        DateOnly ReleaseDate
    );
}
