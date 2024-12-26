using BaseLibrary.Entities;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers.SingleTables;

public class TipoDocController(IGenericRepositoryInterface<TipoDoc> genericRepositoryInterface)
    : GenericController<TipoDoc>(genericRepositoryInterface)
{
    
}