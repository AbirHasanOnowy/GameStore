using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Dtos
{
    public record CreateGameDto(
        [Required] [MinLength(2), MaxLength(100)] string Name,
        [Required] [Range(1, 50)] int GenreId,
        [Range(0, 100)] decimal Price,
        [Required] DateOnly ReleaseDate
    );
}
