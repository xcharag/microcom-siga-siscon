using BaseLibrary.DTOs.Parametros.Banco;
using BaseLibrary.Entities;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers;

public class BancoController (IGenericRepositoryInterface<BancoDto> genericRepositoryInterface) 
    : GenericController<BancoDto>(genericRepositoryInterface)
{
    
}