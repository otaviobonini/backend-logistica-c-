using Logistica_API.Controllers;
using Logistica_API.Mappers;
using Logistica_API.Repositories;
using Logistica_API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opcoes =>
    opcoes.UseNpgsql(builder.Configuration.GetConnectionString("Banco")));

builder.Services.AddScoped<VeiculoRepository>();
builder.Services.AddScoped<MotoristaRepository>();
builder.Services.AddScoped<EntregaRepository>();

builder.Services.AddScoped<VeiculoMapper>();
builder.Services.AddScoped<MotoristaMapper>();
builder.Services.AddScoped<EntregaMapper>();

builder.Services.AddScoped<VeiculoService>();
builder.Services.AddScoped<MotoristaService>();
builder.Services.AddScoped<EntregaService>();

builder.Services.AddControllers(opcoes => opcoes.Filters.Add<ApiExceptionFilter>())
    .ConfigureApiBehaviorOptions(opcoes => opcoes.InvalidModelStateResponseFactory = ApiExceptionFilter.TratarValidacao);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opcoes => opcoes.SwaggerDoc("v1", new OpenApiInfo
{
    Title = "Logística API",
    Version = "Etapa 1",
    Description = "API para uma transportadora controlar veículos, motoristas e entregas, com as regras R1, R2 e R3."
}));

var app = builder.Build();

using (var escopo = app.Services.CreateScope())
{
    escopo.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
