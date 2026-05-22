using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Dtos
{
    // Required is used for input validation
    public record UpdateGameDto(
        [Required] string Name,
        [Required] string Genre,
        [Required] decimal Price,
        [Required] DateOnly ReleaseDate
    );
}
