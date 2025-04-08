using Microsoft.EntityFrameworkCore;
using Proyecto.DTO;
using Proyecto.Entities;

namespace Proyecto.Mapping
{
    public static class GameMapping
    {

        public static GameEntity ToEntity(this CreateGameDTO gameDTO)
        {
            GameEntity gameEntity = new()
            {
                Name = gameDTO.Name,
                GenreId = gameDTO.GenreId,
                Price = gameDTO.Price,
                ReleaseDate = gameDTO.ReleaseDate
            };

            return gameEntity;
        }

        public static GameSummaryDTO ToSummaryDto(this GameEntity gameEntity)
        {
            return new(
                gameEntity.Id,
                gameEntity.Name,
                gameEntity.Genre!.Name,
                gameEntity.Price,
                gameEntity.ReleaseDate
                );
        }

        public static GameDetailsDTO ToDetailsDto(this GameEntity gameEntity)
        {
            return new(
                gameEntity.Id,
                gameEntity.Name,
                gameEntity.Genre!.Id,
                gameEntity.Price,
                gameEntity.ReleaseDate
                );
        }

        public static GameEntity toEntity(this UpdateGameDTO gameDTO , int id)
        {
            return new GameEntity()
            {
                Name = gameDTO.Name,
                Id = id,
                Price = gameDTO.Price,
                ReleaseDate = gameDTO.ReleaseDate,
                GenreId = gameDTO.GenreId
            };
        }
    }
}
