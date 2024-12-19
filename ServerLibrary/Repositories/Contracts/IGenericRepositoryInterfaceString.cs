using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts;

public interface IGenericRepositoryInterfaceString<T>
{
    Task<List<T>> GetAll();
    Task<T?> GetById(string id);
    Task<GeneralResponse> Create(T item);
    Task<GeneralResponse> Update(T item);
    Task<GeneralResponse> Delete(string id);
}