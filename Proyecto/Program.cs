using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Proyecto.Data;
using Proyecto.DTO;
using Proyecto.Endpoints;
using System.Diagnostics.Eventing.Reader;



var builder = WebApplication.CreateBuilder(args);
var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GameStoreContext>(options =>
    options.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString)), ServiceLifetime.Scoped);
// service life time scoped no es necesario ya que es el default. Significa que por cada request se utiliza la misma instancia de gamestorecontext, se genera una instancia para cada request.

var app = builder.Build();
app.MapGamesEndpoints();
app.MigrateDB();
app.Run();
