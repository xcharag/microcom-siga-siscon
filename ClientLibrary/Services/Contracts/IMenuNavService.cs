using BaseLibrary.DTOs;
using BaseLibrary.DTOs.Menu;
using BaseLibrary.Responses;

namespace ClientLibrary.Services.Contracts;

public interface IMenuNavService
{
    Task<GeneralResponse> CreateMenu(MenusCreation menu);
    Task<GeneralResponse> UpdateMenu(MenusEdit menu);
    Task<GeneralResponse> DeleteMenu(int id);
    Task<GeneralResponse> AddMenuItem(MenusCreation menu);
    Task<IEnumerable<MenusCreation>?> GetAllMenus();
}