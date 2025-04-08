using System.ComponentModel.DataAnnotations;

namespace Proyecto.DTO
{
    public record class UpdateGameDTO(
        [Required][StringLength(50)] string Name,
        [Required] int GenreId,
        [Required][Range(1, 100)] decimal Price,
        [Required] DateOnly ReleaseDate);

}
