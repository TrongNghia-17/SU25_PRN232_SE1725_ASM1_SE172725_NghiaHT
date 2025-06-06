using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMMS.Repositories.NghiaHT.ModelExtensions;
using SMMS.Repositories.NghiaHT.Models;
using SMMS.Services.NghiaHT;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SMMS.APIServices.BE.NghiaHT.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class RequestNghiaHtsController : ControllerBase
{
    private readonly IRequestNghiaHtService _requestNghiaHtServiceService;

    public RequestNghiaHtsController(IRequestNghiaHtService requestNghiaHtServiceService)
    {
        _requestNghiaHtServiceService = requestNghiaHtServiceService;
    }

    // GET: api/<RequestNghiaHtsController>
    [HttpGet]
    [Authorize(Roles = "1, 2")]
    public async Task<IEnumerable<RequestNghiaHt>> Get()
    {
        return await _requestNghiaHtServiceService.GetAllAsync();
    }

    // GET api/<RequestNghiaHtsController>/5
    [HttpGet("{id}")]
    [Authorize(Roles = "1, 2")]
    public async Task<RequestNghiaHt> Get(int id)
    {
        return await _requestNghiaHtServiceService.GetByIdAsync(id);
    }

    // POST api/<RequestNghiaHtsController>
    [HttpPost]
    [Authorize(Roles = "1, 2")]
    public async Task<int> Post(RequestNghiaHt requestNghiaHt)
    {
        return await _requestNghiaHtServiceService.CreateAsync(requestNghiaHt);
    }

    // PUT api/<RequestNghiaHtsController>/5
    [HttpPut("{id}")]
    [Authorize(Roles = "1, 2")]
    public async Task<int> Put(RequestNghiaHt requestNghiaHt)
    {
        return await _requestNghiaHtServiceService.UpdateAsync(requestNghiaHt);
    }

    // DELETE api/<RequestNghiaHtsController>/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "1")]
    public async Task<bool> Delete(int id)
    {
        return await _requestNghiaHtServiceService.DeleteAsync(id);
    }

    [HttpGet("{medicationName}/{quantity}/{categoryName}")]
    [Authorize(Roles = "1, 2")]
    public async Task<List<RequestNghiaHt>>
        Get
        (
        string medicationName,
        int quantity,
        string categoryName
        )
    {
        return await _requestNghiaHtServiceService.SearchAllAsync(medicationName, quantity, categoryName);
    }

    [HttpGet("{pageSize}/{categoryName}")]
    [Authorize(Roles = "1, 2")]
    public async Task<PaginationResult<List<RequestNghiaHt>>> Get(int currentPage, int pageSize)
    {
        return await _requestNghiaHtServiceService.GetAllWithPaging(currentPage, pageSize);
    }

    [HttpGet("{medicationName}/{quantity}/{categoryName}/{currentPage}/{pageSize}")]
    [Authorize(Roles = "1, 2")]
    public async Task<PaginationResult<List<RequestNghiaHt>>> Get(string medicationName, int quantity, string categoryName, int currentPage, int pageSize)
    {
        return await _requestNghiaHtServiceService.SearchWithPagingAsync(medicationName, quantity, categoryName, currentPage, pageSize);

    }
}

