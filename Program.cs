using FluentValidation.AspNetCore;
using Inventario.Application.Componentes.Validators;
using Inventario.Application.Equipos.Validators;
using Inventario.Application.Mantenimientos.Validators;
using Inventario.Application.Movimientos.Validators;
using Inventario.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssemblyContaining<CreateEquipoRequestValidator>();
        config.RegisterValidatorsFromAssemblyContaining<CreateComponenteRequestValidator>();
        config.RegisterValidatorsFromAssemblyContaining<CreateMantenimientoRequestValidator>();
        config.RegisterValidatorsFromAssemblyContaining<CreateMovimientoRequestValidator>();
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
