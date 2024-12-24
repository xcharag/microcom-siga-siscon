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
using ClientLibrary.Services.Implementations;
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
builder.Services.AddScoped<IMenuNavService, MenuNavService>();
builder.Services.AddScoped<IPlanCuentaService, PlanCuentaService>();
builder.Services.AddScoped<IGrupoService, GrupoService>();
builder.Services.AddScoped<IBancoService, BancoService>();
builder.Services.AddScoped<IGenericServiceInterface<Nivel>, GenericServiceImplementation<Nivel>>();
builder.Services.AddScoped<IGenericServiceInterfaceString<CentroCostoDto>, GenericServiceImplementationString<CentroCostoDto>>();
builder.Services.AddScoped<IGenericServiceInterface<ProveedorDto>, GenericServiceImplementation<ProveedorDto>>();


builder.Services.AddScoped<ToastService>();
builder.Services.AddScoped<ITooltipService, TooltipService>();
builder.Services.AddFluentUIComponents();

await builder.Build().RunAsync();