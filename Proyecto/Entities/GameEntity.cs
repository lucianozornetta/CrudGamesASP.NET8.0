namespace Proyecto.Entities
{
    public class GameEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public decimal Price { get; set; } 
        public int GenreId { get; set; }

        public GenreEntity? Genre { get; set; }
        public DateOnly ReleaseDate { get; set; }
    }   
}
