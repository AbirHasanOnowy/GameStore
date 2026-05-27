using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Dtos
{
    // Required is used for input validation
    public record UpdateGameDto(
        [Required] [MinLength(2), MaxLength(100)] string Name,
        [Required] [Range(1, 50)] int GenreId,
        [Range(0, 100)] decimal Price,
        [Required] DateOnly ReleaseDate
    );
}
