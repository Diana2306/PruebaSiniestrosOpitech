using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

using Siniestros.Application.Persistence;
using Siniestros.Infrastructure.Persistence;
using Siniestros.Application.Common.Behaviors;
using Siniestros.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// ===============================
// Controllers + JSON (Enum como string)
// ===============================
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// ===============================
// Swagger
// ===============================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===============================
// DbContext + Interface (CQRS)
// ===============================
builder.Services.AddDbContext<SiniestrosDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Permite inyectar ISiniestrosDbContext en handlers
builder.Services.AddScoped<ISiniestrosDbContext>(sp => sp.GetRequiredService<SiniestrosDbContext>());

// ===============================
// Repositories
// ===============================
builder.Services.AddScoped<Siniestros.Application.Repositories.ISiniestroRepository, Siniestros.Infrastructure.Repositories.SiniestroRepository>();
builder.Services.AddScoped<Siniestros.Application.Repositories.ICiudadRepository, Siniestros.Infrastructure.Repositories.CiudadRepository>();
builder.Services.AddScoped<Siniestros.Application.Repositories.IDepartamentoRepository, Siniestros.Infrastructure.Repositories.DepartamentoRepository>();
builder.Services.AddScoped<Siniestros.Application.Repositories.ITipoSiniestroRepository, Siniestros.Infrastructure.Repositories.TipoSiniestroRepository>();

// ===============================
// MediatR
// ===============================
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Siniestros.Application.Commands.CrearSiniestroCommand>();
});

// ===============================
// FluentValidation
// ===============================
builder.Services.AddValidatorsFromAssemblyContaining<Siniestros.Application.Commands.CrearSiniestroCommand>();

// Pipeline: valida antes de ejecutar handlers (commands/queries)
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// ✅ REGISTRAR el middleware porque implementa IMiddleware
builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

// ===============================
// Pipeline HTTP
// ===============================
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();