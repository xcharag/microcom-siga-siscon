using System.Net.Http.Json;
using BaseLibrary.DTOs.Documentos;
using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts.Comprobantes;

namespace ClientLibrary.Services.Implementations.Comprobantes;

public class DocumentoImplementation(GetHttpClient getHttpClient) : IDocumentoService
{
    private string baseUrl = "api/Documento";
    public async Task<List<DocumentoDto>> GetAllDocumentos()
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var results = await httpClient.GetFromJsonAsync<List<DocumentoDto>>($"{baseUrl}/all");
        return results!;
    }

    public async Task<List<DocumentoDto>> GetAllDocumentosDateRange(string start, string end)
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var results = await httpClient.GetFromJsonAsync<List<DocumentoDto>>($"{baseUrl}/all/{start}/{end}");
        return results!;
    }

    public async Task<List<DocumentoDto>> GetAllIngresos()
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var results = await httpClient.GetFromJsonAsync<List<DocumentoDto>>($"{baseUrl}/all/ingresos");
        return results!;
    }

    public async Task<List<DocumentoDto>> GetAllEgresos()
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var results = await httpClient.GetFromJsonAsync<List<DocumentoDto>>($"{baseUrl}/all/egresos");
        return results!;
    }

    public async Task<List<DocumentoDto>> GetAllFondosFijos()
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var results = await httpClient.GetFromJsonAsync<List<DocumentoDto>>($"{baseUrl}/all/fondosfijos");
        return results!;
    }

    public async Task<GeneralResponse> CreateDocumento(AsientoDto item)
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var response = await httpClient.PostAsJsonAsync($"{baseUrl}/create", item);
        var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
        return result!;
    }

    public async Task<GeneralResponse> UpdateDocumento(AsientoDto item)
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var response = await httpClient.PutAsJsonAsync($"{baseUrl}/update", item);
        var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
        return result!;
    }

    public async Task<GeneralResponse> DeleteDocumento(string id)
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var response = await httpClient.DeleteAsync($"{baseUrl}/delete/{id}");
        var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
        return result!;
    }
}