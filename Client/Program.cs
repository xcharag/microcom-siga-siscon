using BaseLibrary.DTOs.Parametros.Banco;
using BaseLibrary.DTOs.Parametros.CentroCosto;
using BaseLibrary.DTOs.Parametros.Proveedor;
using BaseLibrary.Entities;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using ClientLibrary.Services.Contracts.Comprobantes;
using ClientLibrary.Services.Contracts.Parametros;
using ClientLibrary.Services.Contracts.SingleTables;
using ClientLibrary.Services.Implementations;
using ClientLibrary.Services.Implementations.Comprobantes;
using ClientLibrary.Services.Implementations.Parametros;
using ClientLibrary.Services.Implementations.SingleTables;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Components.Tooltip;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CustomHttpHandler>();
builder.Services.AddHttpClient("SystemApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5232");
}).AddHttpMessageHandler<CustomHttpHandler>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5232") });

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<GetHttpClient>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<IMenuNavService, MenuNavImplementation>();
builder.Services.AddScoped<IPlanCuentaService, PlanCuentaImplementation>();
builder.Services.AddScoped<IGrupoService, GrupoImplementation>();
builder.Services.AddScoped<IBancoService, BancoImplementation>();
builder.Services.AddScoped<IDocumentoService, DocumentoImplementation>();

builder.Services.AddScoped<IGenericServiceInterface<Nivel>, GenericServiceImplementation<Nivel>>();
builder.Services.AddScoped<IGenericServiceInterface<ProveedorDto>, GenericServiceImplementation<ProveedorDto>>();
builder.Services.AddScoped<IGenericServiceInterface<TipoDoc>, GenericServiceImplementation<TipoDoc>>();
builder.Services.AddScoped<IGenericServiceInterfaceString<CentroCostoDto>, GenericServiceImplementationString<CentroCostoDto>>();



builder.Services.AddScoped<ToastService>();
builder.Services.AddScoped<ITooltipService, TooltipService>();
builder.Services.AddFluentUIComponents();

await builder.Build().RunAsync();