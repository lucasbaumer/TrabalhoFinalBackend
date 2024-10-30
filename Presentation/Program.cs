using Microsoft.EntityFrameworkCore;
using projetoFinal.Application.Interfaces;
using projetoFinal.Application.Services;
using projetoFinal.Core.Interfaces;
using projetoFinal.Infrastructure.Data;
using projetoFinal.Infrastructure.Repositories;
using projetoFinal.Presentation.Mapping;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

builder.Services.AddAutoMapper(typeof(ClientMapper), typeof(SaleMapper));

// Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção de dependência dos serviços e repositórios
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ISaleRepository, SalesRepository>();
builder.Services.AddScoped<ISaleService, SaleService>();


var app = builder.Build();

// Configure o pipeline de requisição HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Uso de CORS
app.UseCors("AllowAllOrigins");
app.MapControllers();

app.Run();