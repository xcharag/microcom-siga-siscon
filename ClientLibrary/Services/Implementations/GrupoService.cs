using System.Net.Http.Json;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;

namespace ClientLibrary.Services.Implementations;

public class GrupoService(GetHttpClient getHttpClient) : IGrupoService
{
    public async Task<List<Grupo>?> GetAll(string baseUrl)
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var results = await httpClient.GetFromJsonAsync<List<Grupo>>($"{baseUrl}/all");
        return results;
    }

    public async Task<Grupo?> GetById(int id, string baseUrl)
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var result = await httpClient.GetFromJsonAsync<Grupo>($"{baseUrl}/single/{id}");
        return result!;
    }

    public async Task<GeneralResponse?> Create(Grupo item, string baseUrl)
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var response = await httpClient.PostAsJsonAsync($"{baseUrl}/create", item);
        var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
        return result;
    }

    public async Task<GeneralResponse> Update(Grupo item, string baseUrl)
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
}