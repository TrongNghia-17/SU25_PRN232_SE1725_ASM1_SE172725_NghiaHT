using Microsoft.AspNetCore.Mvc;
using SMMS.Repositories.NghiaHT.Models;
using SMMS.Services.NghiaHT;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SMMS.APIServices.BE.NghiaHT.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicationCategoryQuanTnsController : ControllerBase
{
    private readonly MedicationCategoryQuanTnService _medicationCategoryQuanTnService;

    public MedicationCategoryQuanTnsController(MedicationCategoryQuanTnService medicationCategoryQuanTnService)
    {
        _medicationCategoryQuanTnService = medicationCategoryQuanTnService;
    }

    // GET: api/<EventTypePhucTmsController>
    [HttpGet]
    public async Task<IEnumerable<MedicationCategoryQuanTn>> Get()
    {
        return await _medicationCategoryQuanTnService.GetAllAsync();
    }

    // GET api/<MedicationCategoryQuanTnsController>/5
    [HttpGet("{id}")]
    public async Task<MedicationCategoryQuanTn> Get(int id)
    {
        return await _medicationCategoryQuanTnService.GetByIdAsync(id);
    }

    // POST api/<MedicationCategoryQuanTnsController>
    [HttpPost]
    public async Task<int> Post(MedicationCategoryQuanTn medicationCategoryQuanTn)
    {
        return await _medicationCategoryQuanTnService.CreateAsync(medicationCategoryQuanTn);
    }

    // PUT api/<MedicationCategoryQuanTnsController>/5
    [HttpPut("{id}")]
    public async Task<int> Put(int id, [FromBody] MedicationCategoryQuanTn medicationCategoryQuanTn)
    {
        medicationCategoryQuanTn.MedicationCategoryQuanTnid = id;
        return await _medicationCategoryQuanTnService.UpdateAsync(medicationCategoryQuanTn);
    }

    // DELETE api/<MedicationCategoryQuanTnsController>/5
    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
        return await _medicationCategoryQuanTnService.DeleteAsync(id);
    }
}
