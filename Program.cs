using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinimalApiGestióndeAlquilerdeVehículos;
using MinimalApiGestióndeAlquilerdeVehículos.Endpoints;
using MinimalApiGestióndeAlquilerdeVehículos.Entidades;
//using MinimalApiGestióndeAlquilerdeVehículos.Migrations;
using MinimalApiGestióndeAlquilerdeVehículos.Repositorios;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);
var origenesPermitidos = builder.Configuration.GetValue<string>("originesPermitidos")!;
//Inicio de area de los servicios

builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer("name=DefaultConnection"));

builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(configuracion =>
    {
        configuracion.WithOrigins(origenesPermitidos).AllowAnyHeader().AllowAnyMethod();
    });
    opciones.AddPolicy("libre", configuracion =>
    {
        configuracion.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
 });

builder.Services.AddOutputCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepositorioClientes, RepositorioClientes>();
builder.Services.AddScoped<IRepositorioVehiculos, RepositorioVehiculos>();
builder.Services.AddScoped<IRepositorioAlquiler, RepositorioAlquiler>();

builder.Services.AddAutoMapper(typeof(Program));






//Fin del area de los Servicios
var app = builder.Build();

//Inicio de area de los middleware

//if (builder.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();

//}

app.UseSwagger();
app.UseSwaggerUI();



app.UseCors();
app.UseOutputCache();


//inicio endpoint
app.MapGet("/", () => "Hello World!");
//var endpointClientes = app.MapGroup("/clientes").MapClientes();
app.MapGroup("/clientes").MapClientes();
app.MapGroup("/vehiculos").MapVehiculos();
//fin endpoint
//Fin de area de los middleware
app.Run();



