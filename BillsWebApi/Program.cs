using Bill.Application.Features.Users.Commands.CreateUser;
using Bill.Application.Features.Users.Queries.GetUsers;
using Bill.Domain.Repositories;
using Bill.Domain.Services;
using Bill.Domain.Users;
using Bill.Infrastructure.Configurations;
using Bill.Infrastructure.Contexts;
using Bill.Infrastructure.Domain.Users;
using Bill.Infrastructure.Repositories;
using Bill.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

//DB Config
builder.Services.Configure<UserConfiguration>(
    options =>
    {
        options.ConnectionString = configuration["MongoDB:ConnectionString"];
        options.Database = configuration["MongoDB:Database"];
    }
);

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(configuration["MongoDB:ConnectionString"], configuration["MongoDB:Database"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

// Add services to the container.
builder.Services.AddSingleton<IUserContext, UserContext>();
builder.Services.AddScoped<IUserReadOnlyRepository, UserReadOnlyRepository>();
builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
builder.Services.AddScoped<IBillUnitOfWork, BillUnitOfWork>();
builder.Services.AddScoped<ITokenService, TokenService>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();