using Bill.Application.Features.Clients.Queries.GetClients;
using Bill.Domain.Clients;
using Bill.Domain.Repositories;
using Bill.Infrastructure.Configurations;
using Bill.Infrastructure.Contexts;
using Bill.Infrastructure.Domain.Clients;
using Bill.Infrastructure.Repositories;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddSwaggerGen();

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