using System.Net.Http.Json;
using BaseLibrary.DTOs.Menu;
using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts.SingleTables;

namespace ClientLibrary.Services.Implementations.SingleTables;

public class MenuNavService (GetHttpClient getHttpClient) : IMenuNavService
{
    public const string MenuUrl = "api/Menu";
    
    public async Task<GeneralResponse> CreateMenu(MenusCreation menu)
    {
        var httpClient = getHttpClient.GetPublicHttpClient();
        var result = await httpClient.PostAsJsonAsync($"{MenuUrl}/create", menu);
        if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Ocurrio un error al crear el menu");
        return (await result.Content.ReadFromJsonAsync<GeneralResponse>())!;
    }

    public async Task<GeneralResponse> UpdateMenu(MenusEdit menu)
    {
        var httpClient = getHttpClient.GetPublicHttpClient();
        var result = await httpClient.PutAsJsonAsync($"{MenuUrl}/update", menu);
        if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Ocurrio un error al actualizar el menu");
        return (await result.Content.ReadFromJsonAsync<GeneralResponse>())!;
    }

    public async Task<GeneralResponse> DeleteMenu(int id)
    {
        var httpClient = getHttpClient.GetPublicHttpClient();
        var result = await httpClient.DeleteAsync($"{MenuUrl}/delete/{id}");
        if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Ocurrio un error al eliminar el menu");
        return (await result.Content.ReadFromJsonAsync<GeneralResponse>())!;
    }

    public async Task<GeneralResponse> AddMenuItem(MenusCreation menu)
    {
        var httpClient = getHttpClient.GetPublicHttpClient();
        var result = await httpClient.PostAsJsonAsync($"{MenuUrl}/create/menu-item", menu);
        if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Ocurrio un error al agregar el item al menu");
        return (await result.Content.ReadFromJsonAsync<GeneralResponse>())!;
    }

    public Task<IEnumerable<MenusCreation>?> GetAllMenus()
    {
        var httpClient = getHttpClient.GetPublicHttpClient();
        return httpClient.GetFromJsonAsync<IEnumerable<MenusCreation>>($"{MenuUrl}/get-all");
    }
}