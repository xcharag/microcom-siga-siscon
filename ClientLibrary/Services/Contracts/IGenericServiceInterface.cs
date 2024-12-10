using BaseLibrary.Responses;

namespace ClientLibrary.Services.Contracts;

public interface IGenericServiceInterface<T>
{
    Task<List<T>?> GetAll(string baseUrl);
    Task<T?> GetById(int id, string baseUrl);
    Task<GeneralResponse?> Create(T item, string baseUrl);
    Task<GeneralResponse> Update(T item, string baseUrl);
    Task<GeneralResponse> Delete(int id, string baseUrl);
}