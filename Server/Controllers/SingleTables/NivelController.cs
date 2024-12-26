using BaseLibrary.Entities;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers.SingleTables;

public class NivelController (IGenericRepositoryInterface<Nivel> genericRepositoryInterface)
    : GenericController<Nivel>(genericRepositoryInterface)
{
    
}