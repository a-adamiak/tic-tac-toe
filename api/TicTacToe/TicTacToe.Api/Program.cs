using System.Text.Json.Serialization;
using TicTacToe.Api.DI;
using TicTacToe.Api.Middleware;
using TicTacToe.Application.DI;
using TicTacToe.Infrastructure.DI;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddVersioning();
builder.Services.AddVersionedSwagger();
builder.Services.AddCors();
var app = builder.Build();

app.UseExceptionMiddleware();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseHttpsRedirection();
app.UseVersionedSwagger();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



app.Run();
