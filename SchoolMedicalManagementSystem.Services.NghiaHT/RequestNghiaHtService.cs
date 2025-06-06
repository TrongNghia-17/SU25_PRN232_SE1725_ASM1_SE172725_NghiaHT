using SMMS.Repositories.NghiaHT.ModelExtensions;
using SMMS.Repositories.NghiaHT.Models;
using SMMS.Repositories.NghiaHT;

namespace SMMS.Services.NghiaHT;

//public interface IRequestNghiaHtService
//{

//}

public class RequestNghiaHtService : IRequestNghiaHtService
{
    private readonly RequestNghiaHtRepository _requestNghiaHtRepository;

    public RequestNghiaHtService()
    {
        _requestNghiaHtRepository = new RequestNghiaHtRepository();
    }

    public async Task<int> CreateAsync(RequestNghiaHt requestNghiaHt)
    {
        return await _requestNghiaHtRepository.CreateAsync(requestNghiaHt);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _requestNghiaHtRepository.GetByIdAsync(id);
        return await _requestNghiaHtRepository.RemoveAsync(item);
    }

    public async Task<List<RequestNghiaHt>> GetAllAsync()
    {
        return await _requestNghiaHtRepository.GetAllAsync();
    }

    public async Task<RequestNghiaHt> GetByIdAsync(int id)
    {
        return await _requestNghiaHtRepository.GetByIdAsync(id);
    }

    public async Task<List<RequestNghiaHt>> SearchAllAsync(string medicationName, int quantity, string categoryName)
    {
        return await _requestNghiaHtRepository.SearchAsync(medicationName, quantity, categoryName);
    }

    public async Task<int> UpdateAsync(RequestNghiaHt requestNghiaHt)
    {
        return await _requestNghiaHtRepository.UpdateAsync(requestNghiaHt);
    }

    public Task<PaginationResult<List<RequestNghiaHt>>> SearchWithPagingAsync(string medicationName, int quantity, string categoryName, int currentPage, int pageSize)
    {
        return _requestNghiaHtRepository.SearchAsync(medicationName, quantity, categoryName, currentPage, pageSize);
    }

    public Task<PaginationResult<List<RequestNghiaHt>>> GetAllWithPaging(int currentPage, int pageSize)
    {
        return _requestNghiaHtRepository.GetAllWithPaging(currentPage, pageSize);
    }

    public async Task<PaginationResult<List<RequestNghiaHt>>> SearchWithPagingAsync(SearchRequestNghiaHt searchRequest)
    {
        var list = await _requestNghiaHtRepository.SearchWithPaginAsync(searchRequest);
        return list ?? new PaginationResult<List<RequestNghiaHt>>();
    }
}
