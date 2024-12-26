using System.Net.Http.Json;
using BaseLibrary.DTOs.Parametros.Banco;
using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts.Parametros;

namespace ClientLibrary.Services.Implementations.Parametros;

public class BancoService(GetHttpClient getHttpClient) : IBancoService
{
    public async Task<List<BancoDto>?> GetAll(string baseUrl)
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var results = await httpClient.GetFromJsonAsync<List<BancoDto>>($"{baseUrl}/all");
        return results;
    }

    public async Task<BancoDto?> GetById(int id, string baseUrl)
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var result = await httpClient.GetFromJsonAsync<BancoDto>($"{baseUrl}/single/{id}");
        return result!;
    }

    public async Task<GeneralResponse?> Create(BancoDto item, string baseUrl)
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var response = await httpClient.PostAsJsonAsync($"{baseUrl}/create", item);
        var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
        return result;
    }

    public async Task<GeneralResponse> Update(BancoDto item, string baseUrl)
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

    public async Task<GeneralResponse> GenerateCodBanco(string codPlanCuenta, string baseUrl)
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var results = await httpClient.GetFromJsonAsync<GeneralResponse>($"{baseUrl}/generate/{codPlanCuenta}");
        return results!;
    }
}