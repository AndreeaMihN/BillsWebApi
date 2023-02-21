using Bill.Application.Features.Users.Commands.CreateUser;
using Bill.Application.Features.Users.Queries.GetUsers;
using Bill.Domain.Repositories;
using Bill.Domain.Users;
using Bill.Infrastructure.Configurations;
using Bill.Infrastructure.Contexts;
using Bill.Infrastructure.Domain.Users;
using Bill.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetSection("MongoDB:ConnectionString")?.Value ?? "";
var database = builder.Configuration.GetSection("MongoDB:Database")?.Value ?? "";

//DB Config
builder.Services.Configure<UserConfiguration>(
options =>
{
    options.ConnectionString = connectionString;
    options.Database = database;
    //Add identity
    //var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();
    //builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
    //    options.ConnectionString, options.Database);
}
);

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(connectionString, database);

// Add services to the container.
builder.Services.AddSingleton<IUserContext, UserContext>();
builder.Services.AddScoped<IUserReadOnlyRepository, UserReadOnlyRepository>();
builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
builder.Services.AddScoped<IBillUnitOfWork, BillUnitOfWork>();

// add mediators querries/commands
builder.Services.AddMediatR(typeof(GetUsersQuery));
builder.Services.AddMediatR(typeof(CreateUserCommand));
builder.Services.AddMediatR(typeof(CreateUserHandler));
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
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommand>(); // register validators

builder.Services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

//Seq logging
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSeq();
});

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