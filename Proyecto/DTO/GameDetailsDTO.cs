namespace Proyecto.DTO
{
    public record class GameDetailsDTO(
    
        int Id,
        string Name,
        int Genre,
        decimal Price,
        DateOnly ReleaseDate
        );
    
}
