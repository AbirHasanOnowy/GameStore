using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Dtos
{
    public record CreateGameDto(
        [Required] [MinLength(2), MaxLength(100)] string Name,
        [Required] [MinLength(2), MaxLength(30)] string Genre,
        [Range(0, 100)] decimal Price,
        [Required] DateOnly ReleaseDate
    );
}
