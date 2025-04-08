using System.ComponentModel.DataAnnotations;

namespace Proyecto.DTO
{
    public record class CreateGameDTO(
        [Required][StringLength(50)] string Name,
        [Required]int GenreId,
        [Required][Range(1,100)]decimal Price,
        [Required]DateOnly ReleaseDate
        );

}
