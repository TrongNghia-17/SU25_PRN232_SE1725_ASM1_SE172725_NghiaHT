using SMMS.Repositories.NghiaHT.ModelExtensions;
using SMMS.Repositories.NghiaHT.Models;

namespace SMMS.Services.NghiaHT;

public interface IRequestNghiaHtService
{
    Task<List<RequestNghiaHt>> GetAllAsync();
    Task<RequestNghiaHt> GetByIdAsync(int id);
    Task<List<RequestNghiaHt>> 
        SearchAllAsync
        (string medicationName
        , int quantity
        , string categoryName);
    Task<PaginationResult<List<RequestNghiaHt>>>
        SearchWithPagingAsync
        (
        string medicationName
        , int quantity
        , string categoryName,
        int currentPage,
        int pageSize
        );
    Task<PaginationResult<List<RequestNghiaHt>>> SearchWithPagingAsync(SearchRequestNghiaHt searchRequest);

    Task<PaginationResult<List<RequestNghiaHt>>>
        GetAllWithPaging
        (
        int currentPage,
        int pageSize
        );
    Task<int> CreateAsync(RequestNghiaHt requestNghiaHt);
    Task<int> UpdateAsync(RequestNghiaHt requestNghiaHt);
    Task<bool> DeleteAsync(int id);
    
    
}
