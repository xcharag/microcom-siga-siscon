using System.Text;
using BaseLibrary.DTOs.Parametros.CentroCosto;
using BaseLibrary.DTOs.Parametros.Proveedor;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using ServerLibrary.Repositories.Contracts.Comprobantes;
using ServerLibrary.Repositories.Contracts.Parametros;
using ServerLibrary.Repositories.Contracts.SingleTables;
using ServerLibrary.Repositories.Implementations;
using ServerLibrary.Repositories.Implementations.Comprobantes;
using ServerLibrary.Repositories.Implementations.Parametros;
using ServerLibrary.Repositories.Implementations.SingleTables;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtSection>(builder.Configuration.GetSection("JwtSection"));
var jwtSection = builder.Configuration.GetSection(nameof(JwtSection)).Get<JwtSection>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")??
                         throw new InvalidOperationException("Sorry, the connection string is not found"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSection!.Issuer,
        ValidAudience = jwtSection!.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.Key!))
    };
});

builder.Services.AddScoped<IUserAccount, UserAccountRepository>();
builder.Services.AddScoped<IMenu, MenuRepository>();
builder.Services.AddScoped<IPlanCuenta, PlanCuentaRepository>();
builder.Services.AddScoped<IBanco, BancoRepository>();
builder.Services.AddScoped<IDocument, DocumentRepository>();
builder.Services.AddScoped<ICorrelativo, CorrelativoRepository>();

builder.Services.AddScoped<IGenericRepositoryInterface<Grupo>, GrupoRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<Nivel>, NivelesRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<ProveedorDto>, ProveedorRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<TipoDoc>, TipoDocRepository>();
builder.Services.AddScoped<IGenericRepositoryInterfaceString<CentroCostoDto>, CentroCostoRepository>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm", 
        policyBuilder => policyBuilder
            .WithOrigins("http://servidor01:5230","http://localhost:5230")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowBlazorWasm");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();