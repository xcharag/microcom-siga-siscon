using System.Net.Http.Json;
using BaseLibrary.DTOs.PlanCta;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;

namespace ClientLibrary.Services.Implementations;

public class PlanCuentaService(GetHttpClient getHttpClient) : IPlanCuentaService
{
    public async Task<List<PlanCuentaDto>?> GetAll(string baseUrl)
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var results = await httpClient.GetFromJsonAsync<List<PlanCuentaDto>>($"{baseUrl}/all");
        return results;
    }

    public async Task<PlanCuentaDto?> GetById(int id, string baseUrl)
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var result = await httpClient.GetFromJsonAsync<PlanCuentaDto>($"{baseUrl}/single/{id}");
        return result!;
    }

    public async Task<GeneralResponse?> Create(PlanCuentaDto item, string baseUrl)
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var response = await httpClient.PostAsJsonAsync($"{baseUrl}/create", item);
        var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
        return result;
    }

    public async Task<GeneralResponse> Update(PlanCuentaDto item, string baseUrl)
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var response = await httpClient.PutAsJsonAsync($"{baseUrl}/update", item);
        var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
        return result!;
    }

    public async Task<GeneralResponse> Delete(int id, string baseUrl)
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var response = await httpClient.DeleteAsync($"{baseUrl}/delete/{id}");
        var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
        return result!;
    }

    public async Task<PlanCuentasResponse> GenerateCodPlanCuenta(string cuentaPadre, string baseUrl)
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var results = await httpClient.GetFromJsonAsync<PlanCuentasResponse>($"{baseUrl}/generate/{cuentaPadre}");
        return results!;
    }
}