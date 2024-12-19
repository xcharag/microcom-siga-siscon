using BaseLibrary.DTOs.Parametros.CentroCosto;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers;

public class CentroCostoController(IGenericRepositoryInterfaceString<CentroCostoDto> genericRepositoryInterface)
    : GenericControllerString<CentroCostoDto>(genericRepositoryInterface)
{
    
}