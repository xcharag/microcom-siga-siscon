using BaseLibrary.Responses;

namespace ClientLibrary.Services.Contracts;

public interface IGenericServiceInterfaceString<T>
{
    Task<List<T>?> GetAll(string baseUrl);
    Task<T?> GetById(string id, string baseUrl);
    Task<GeneralResponse?> Create(T item, string baseUrl);
    Task<GeneralResponse> Update(T item, string baseUrl);
    Task<GeneralResponse> Delete(string id, string baseUrl);
}