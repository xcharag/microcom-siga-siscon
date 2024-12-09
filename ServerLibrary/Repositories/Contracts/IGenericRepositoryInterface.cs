using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts;

public interface IGenericRepositoryInterface<T>
{
    Task<List<T>> GetAll();
    Task<T?> GetById(int id);
    Task<GeneralResponse> Create(T item);
    Task<GeneralResponse> Update(T item);
    Task<GeneralResponse> Delete(int id);
}