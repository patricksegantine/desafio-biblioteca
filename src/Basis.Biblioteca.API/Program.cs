using Basis.Biblioteca.API.Middleares;
using Microsoft.DependencyInjection.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args)
    .ConfigureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();
await app.RunAsync();
