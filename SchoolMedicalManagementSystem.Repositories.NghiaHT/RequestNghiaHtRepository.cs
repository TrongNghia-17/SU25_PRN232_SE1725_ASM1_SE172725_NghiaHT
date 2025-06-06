using Microsoft.EntityFrameworkCore;
using SMMS.Repositories.NghiaHT.Basic;
using SMMS.Repositories.NghiaHT.DBContext;
using SMMS.Repositories.NghiaHT.ModelExtensions;
using SMMS.Repositories.NghiaHT.Models;

namespace SMMS.Repositories.NghiaHT;

public class RequestNghiaHtRepository : GenericRepository<RequestNghiaHt>
{
    public RequestNghiaHtRepository()
    {
        _context ??= new DBContext.SU25_PRN232_SE1725_G2_SMMSContext();
    }

    public RequestNghiaHtRepository(SU25_PRN232_SE1725_G2_SMMSContext context)
    {
        _context = context;
    }

    public async Task<List<RequestNghiaHt>> GetAllAsync()
    {
        var item = await _context
            .RequestNghiaHts
            .Include(x => x.MedicationCategoryQuanTn)
            .ToListAsync();

        return item ?? new List<RequestNghiaHt>();
    }

    public async Task<RequestNghiaHt> GetByIdAsync(int id)
    {
        var item = await _context
            .RequestNghiaHts
            .Include(x => x.MedicationCategoryQuanTn)
            .FirstOrDefaultAsync(x => x.RequestNghiaHtid == id);

        return item ?? new RequestNghiaHt();
    }

    public async Task<List<RequestNghiaHt>> 
        SearchAsync(
        string medicationName
        , int quantity
        , string categoryName
        )
    {
        var item = await _context
            .RequestNghiaHts
            .Include(x => x.MedicationCategoryQuanTn)
            .Where(x =>
            (
            x.MedicationName.Contains(medicationName) || string.IsNullOrEmpty(medicationName)
            && x.Quantity == quantity || quantity == null || quantity == 0)
            && (x.MedicationCategoryQuanTn.CategoryName.Contains(categoryName) || string.IsNullOrEmpty(categoryName))
            )
            .ToListAsync();

        return item ?? new List<RequestNghiaHt>();
    }

    public async Task<PaginationResult<List<RequestNghiaHt>>>
        SearchAsync(
        string medicationName
        , int quantity
        , string categoryName, int currentPage, int pageSize)
    {
        var requestNghiaHt = await SearchAsync(medicationName, quantity, categoryName);

        var totalItems = requestNghiaHt.Count;
        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        requestNghiaHt = requestNghiaHt
            .Skip(pageSize * (currentPage - 1))
            .Take(pageSize)
            .ToList();

        var result = new PaginationResult<List<RequestNghiaHt>>
        {
            Items = requestNghiaHt,
            TotalItems = totalItems,
            TotalPages = totalPages,
            CurrentPage = currentPage,
            PageSize = pageSize
        };

        return result;
    }

    public async Task<PaginationResult<List<RequestNghiaHt>>>
        SearchWithPaginAsync(
        SearchRequestNghiaHt searchRequestNghiaHt)
    {
        var requestNghiaHt = await this.SearchAsync(searchRequestNghiaHt.CategoryName, searchRequestNghiaHt.MedicationName, searchRequestNghiaHt.Quantity);

        var totalItems = requestNghiaHt.Count;
        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        requestNghiaHt = requestNghiaHt
            .Skip(pageSize * (currentPage - 1))
            .Take(pageSize)
            .ToList();

        var result = new PaginationResult<List<RequestNghiaHt>>
        {
            Items = requestNghiaHt,
            TotalItems = totalItems,
            TotalPages = totalPages,
            CurrentPage = currentPage,
            PageSize = pageSize
        };

        return result;
    }

    public async Task<PaginationResult<List<RequestNghiaHt>>>
        GetAllWithPaging(int currentPage, int pageSize)
    {
        var items = await GetAllAsync();

        //// Paging
        var totalItems = items.Count();
        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        items = items.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

        var result = new PaginationResult<List<RequestNghiaHt>>
        {
            TotalItems = totalItems,
            TotalPages = totalPages,
            CurrentPage = currentPage,
            PageSize = pageSize,
            Items = items
        };

        return result;
    }    
}
