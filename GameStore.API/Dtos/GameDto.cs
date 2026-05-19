namespace GameStore.API.Dtos
{
    public record GameDto(
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
