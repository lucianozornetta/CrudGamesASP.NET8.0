using Proyecto.DTO;
using Proyecto.Endpoints;
using System.Diagnostics.Eventing.Reader;



var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
