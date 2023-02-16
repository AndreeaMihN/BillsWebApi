using Bill.Application.Features.Clients.Commands.CreateClient;
using Bill.Application.Features.Clients.Queries.GetClients;
using Bill.Domain.Clients;
using Bill.Domain.Repositories;
using Bill.Infrastructure.Configurations;
using Bill.Infrastructure.Contexts;
using Bill.Infrastructure.Domain.Clients;
using Bill.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    //DB Config
    builder.Services.Configure<ClientConfiguration>(
        options =>
        {
            options.ConnectionString = builder.Configuration.GetSection("MongoDB:ConnectionString")?.Value ?? "";
            options.Database = builder.Configuration.GetSection("MongoDB:Database")?.Value ?? "";
        }
    );

    // Add services to the container.
    builder.Services.AddSingleton<IClientContext, ClientContext>();
    builder.Services.AddScoped<IClientReadOnlyRepository, ClientReadOnlyRepository>();
    builder.Services.AddScoped<IClientCommandRepository, ClientCommandRepository>();
    builder.Services.AddScoped<IBillUnitOfWork, BillUnitOfWork>();

    // add mediators querries/commands
    builder.Services.AddMediatR(typeof(GetClientsQuery));
    builder.Services.AddMediatR(typeof(CreateClientCommand));
    builder.Services.AddMediatR(typeof(CreateClientHandler));
    builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

    //Auto Mapper
    builder.Services.AddAutoMapper(cfg =>
    {
        cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
    });

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllOrigins", builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Bills Management API",
            Description = "Bills Management API"
        });
        options.ExampleFilters();
        options.EnableAnnotations();
        options.IgnoreObsoleteActions();
        options.CustomSchemaIds(x => x.FullName);
    });

    builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

    // validators
    builder.Services.AddValidatorsFromAssemblyContaining<CreateClientCommand>(); // register validators

    builder.Services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors("AllOrigins");

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}