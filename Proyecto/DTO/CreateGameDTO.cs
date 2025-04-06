using System.ComponentModel.DataAnnotations;

namespace Proyecto.DTO
{
    public record class CreateGameDTO(
        [Required][StringLength(50)] string Name,
        [Required][StringLength(20)]string Genre,
        [Required][Range(1,100)]decimal Price,
        [Required]DateOnly ReleaseDate
        );

}
