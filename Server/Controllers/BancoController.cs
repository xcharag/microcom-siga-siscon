using BaseLibrary.Entities;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers;

public class BancoController (IGenericRepositoryInterface<Banco> genericRepositoryInterface) 
    : GenericController<Banco>(genericRepositoryInterface)
{
    
}