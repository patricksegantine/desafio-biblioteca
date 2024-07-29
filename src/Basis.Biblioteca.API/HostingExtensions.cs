using Asp.Versioning;
using Basis.Biblioteca.Application.Common.AutoMapper;
using Basis.Biblioteca.Application.UseCases.Assunto.Search;
using Basis.Biblioteca.Application.UseCases.Autor.Search;
using Basis.Biblioteca.Application.UseCases.Livro.Create;
using Basis.Biblioteca.Application.UseCases.Livro.Delete;
using Basis.Biblioteca.Application.UseCases.Livro.Get;
using Basis.Biblioteca.Application.UseCases.Livro.Search;
using Basis.Biblioteca.Application.UseCases.Livro.Update;
using Basis.Biblioteca.Domain.Repositories;
using Basis.Biblioteca.Infrastructure.Persistence.SqlServer;
using Basis.Biblioteca.Infrastructure.Persistence.SqlServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;

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
        builder.Services.AddScoped<IGetLivroUsecase, GetLivroUsecase>();
        builder.Services.AddScoped<IUpdateLivroUsecase, UpdateLivroUsecase>();
        builder.Services.AddScoped<IDeleteLivroUsecase, DeleteLivroUsecase>();

        // Registrar os repositórios
        builder.Services.AddScoped<IAutorRepository, AutorRepository>();
        builder.Services.AddScoped<IAssuntoRepository, AssuntoRepository>();
        builder.Services.AddScoped<ILivroRepository, LivroRepository>();

        // AutoMapper
        builder.Services.AddAutoMapper(m => m.AddProfile<CustomProfile>());

        // Adiciona outros serviços
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
        builder.Services.AddSwaggerGen(options =>
        {
            options.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["controller"]}_{e.RelativePath}_{e.HttpMethod}");
            options.DescribeAllParametersInCamelCase();
            options.IncludeXmlComments(XmlCommentsFilePath);
        });

        return builder;
    }

    private static string XmlCommentsFilePath
    {
        get
        {
            var programAssembly = typeof(Program).Assembly;
            var basePath = Path.GetDirectoryName(programAssembly.Location);
            var fileName = $"{programAssembly.GetName().Name}.xml";
            return Path.Combine(basePath!, fileName);
        }
    }
}
