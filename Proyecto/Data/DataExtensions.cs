using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Proyecto.Data
{
    public static class DataExtensions
    {
        public static async Task MigrateDB(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}
