using BaseLibrary.DTOs.Parametros.Proveedor;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers;

public class ProveedorController(IGenericRepositoryInterface<ProveedorDto> genericRepositoryInterface)
    : GenericController<ProveedorDto>(genericRepositoryInterface)
{
    
}