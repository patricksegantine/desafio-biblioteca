using Basis.Biblioteca.Application.UseCases.Assunto.Search;
using Basis.Biblioteca.Application.UseCases.Autor.Search;
using Basis.Biblioteca.Application.UseCases.Livro.Create;
using Basis.Biblioteca.Application.UseCases.Livro.Search;
using Basis.Biblioteca.Domain.Repositories;
using Basis.Biblioteca.Infrastructure.Persistence.SqlServer;
using Basis.Biblioteca.Infrastructure.Persistence.SqlServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Microsoft.DependencyInjection.Extensions;

public static class HostingExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        // Configura o Serilog
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Host.UseSerilog();

        // Adiciona os serviços do EF Core com base na configuração
        var databaseProvider = builder.Configuration.GetValue<string>("DatabaseProvider");

        if (databaseProvider == "SqlServer")
        {
            builder.Services.AddDbContext<BibliotecaContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        }
        else if (databaseProvider == "InMemory")
        {
            builder.Services.AddDbContext<BibliotecaContext>(options =>
                options.UseInMemoryDatabase("InMemoryDb"));
        }

        // Registrar os casos de uso
        builder.Services.AddScoped<ISearchAssuntoUsecase, SearchAssuntoUsecase>();
        builder.Services.AddScoped<ISearchAutorUsecase, SearchAutorUsecase>();
        builder.Services.AddScoped<ICreateLivroUsecase, CreateLivroUsecase>();
        builder.Services.AddScoped<ISearchLivroUsecase, SearchLivroUsecase>();

        // Registrar os repositórios
        builder.Services.AddScoped<IAutorRepository, AutorRepository>();
        builder.Services.AddScoped<IAssuntoRepository, AssuntoRepository>();
        builder.Services.AddScoped<ILivroRepository, LivroRepository>();

        // Adiciona outros serviços
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }
}
