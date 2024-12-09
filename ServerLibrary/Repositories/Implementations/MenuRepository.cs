using BaseLibrary.DTOs;
using BaseLibrary.DTOs.Menu;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations;

public class MenuRepository(IOptions<JwtSection> config, AppDbContext appDbContext) : IMenu
{
    public async Task<GeneralResponse> CreateMenu(MenusCreation? menu)
    {
        if (menu is null) return new GeneralResponse(false, "El modelo esta vacio");

        var checkMenu = await appDbContext.Menus.FirstOrDefaultAsync(x => x.Name!.Equals(menu.MenuName));
        if (checkMenu is not null) return new GeneralResponse(false, "El menu ya existe");

        var existingMenuItems = await appDbContext.MenuItems
            .Where(x => menu.MenuItems!.Select(mi => mi.Name).Contains(x.Name!))
            .ToListAsync();

        var newMenuItems = menu.MenuItems!
            .Where(mi => !existingMenuItems.Select(x => x.Name).Contains(mi.Name))
            .Select(mi => new MenuItems() { Name = mi.Name, Block = mi.Block })
            .ToList();

        // Add new MenuItems to the database
        appDbContext.MenuItems.AddRange(newMenuItems);
        await appDbContext.SaveChangesAsync();

        var newMenu = new Menu()
        {
            Name = menu.MenuName,
            MenuItemsList = existingMenuItems.Concat(newMenuItems).ToList()
        };

        await AddToDatabase(newMenu);

        // Set the MenuId for new MenuItems and update the database
        foreach (var newItem in newMenuItems)
        {
            newItem.MenuId = newMenu.Id;
        }

        appDbContext.MenuItems.UpdateRange(newMenuItems);
        await appDbContext.SaveChangesAsync();

        return new GeneralResponse(true, "Menu y Menu Items creados con exito");
    }

    public async Task<GeneralResponse> UpdateMenu(MenusEdit? menu)
    {
        if (menu is null) return new GeneralResponse(false, "El modelo esta vacio");
        
        var checkMenu = await appDbContext.Menus.FirstOrDefaultAsync(x => x.Id.Equals(menu.MenuId));
        if (checkMenu is null) return new GeneralResponse(false, "El menu no existe");
        
        if (menu.MenuName != null)
        {
            checkMenu.Name = menu.MenuName;
            await appDbContext.SaveChangesAsync();
        }
        
        if (menu.DeleteMenuItems is not null)
        {
            var deleteMenuItems = await appDbContext.MenuItems
                .Where(x => menu.DeleteMenuItems.Contains(x.Id) && x.MenuId == checkMenu.Id)
                .ToListAsync();

            appDbContext.MenuItems.RemoveRange(deleteMenuItems);
            await appDbContext.SaveChangesAsync();
        }
        
        if (menu.AddMenuItems is not null)
        {
            var existingMenuItems = await appDbContext.MenuItems
                .Where(x => menu.AddMenuItems.Select(mi => mi.Name).Contains(x.Name!) && x.MenuId == checkMenu.Id)
                .ToListAsync();

            var newMenuItems = menu.AddMenuItems
                .Where(mi => !existingMenuItems.Select(x => x.Name).Contains(mi.Name))
                .Select(mi => new MenuItems() { Name = mi.Name, Block = mi.Block, MenuId = checkMenu.Id })
                .ToList();

            if (newMenuItems.Any())
            {
                appDbContext.MenuItems.AddRange(newMenuItems);
                await appDbContext.SaveChangesAsync();
            }
            else
            {
                return new GeneralResponse(false, "Los Menu Items ya existen");
            }
        }
        
        return new GeneralResponse(true, "Menu Items actualizados con exito");
    }

    public async Task<GeneralResponse> DeleteMenu(int id)
    {
        if (id <= 0) return new GeneralResponse(false, "El id esta vacio");
        
        var checkMenu = await appDbContext.Menus
            .Include(m => m.MenuItemsList)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
        if (checkMenu is null) return new GeneralResponse(false, "El menu no existe");
        
        appDbContext.MenuItems.RemoveRange(checkMenu.MenuItemsList!);
        appDbContext.Menus.Remove(checkMenu);
        await appDbContext.SaveChangesAsync();
        
        return new GeneralResponse(true, "Menu y Menu Items eliminados con exito");
    }

    public async Task<GeneralResponse> AddMenuItem(MenusCreation? menu)
    {
        if (menu is null) return new GeneralResponse(false, "El modelo esta vacio");

        var checkMenu = await appDbContext.Menus.FirstOrDefaultAsync(x => x.Name!.Equals(menu.MenuName));
        if (checkMenu is null) return new GeneralResponse(false, "El menu no existe");

        var existingMenuItems = await appDbContext.MenuItems
            .Where(x => menu.MenuItems!.Select(mi => mi.Name).Contains(x.Name!) && x.MenuId == checkMenu.Id)
            .ToListAsync();

        var newMenuItems = menu.MenuItems!
            .Where(mi => !existingMenuItems.Select(x => x.Name).Contains(mi.Name))
            .Select(mi => new MenuItems() { Name = mi.Name, Block = mi.Block, MenuId = checkMenu.Id })
            .ToList();

        if (newMenuItems.Any())
        {
            appDbContext.MenuItems.AddRange(newMenuItems);
            await appDbContext.SaveChangesAsync();
        }
        else
        {
            return new GeneralResponse(false, "Los Menu Items ya existen");
        }

        return new GeneralResponse(true, "Menu Items agregados con exito");
    }

    public async Task<IEnumerable<MenusCreation>> GetAllMenus()
    {
        var menus = await appDbContext.Menus
            .Include(m => m.MenuItemsList)
            .ToListAsync();

        var result = menus.Select(m => new MenusCreation
        {
            MenuName = m.Name,
            MenuItems = m.MenuItemsList!.Select(mi => new MenuItemsDto
            {
                Name = mi.Name,
                Block = mi.Block
            }).ToList()
        });

        return result;
    }

    private async Task<T> AddToDatabase<T>(T model)
    {
        var result = appDbContext.Add(model!);
        await appDbContext.SaveChangesAsync();
        return (T)result.Entity;
    }
}