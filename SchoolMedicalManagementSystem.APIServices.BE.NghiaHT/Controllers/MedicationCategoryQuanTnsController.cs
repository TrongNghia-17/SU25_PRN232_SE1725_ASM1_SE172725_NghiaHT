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

    //// GET api/<EventTypePhucTmsController>/5
    //[HttpGet("{id}")]
    //public string Get(int id)
    //{
    //	return "value";
    //}

    //// POST api/<EventTypePhucTmsController>
    //[HttpPost]
    //public void Post([FromBody] string value)
    //{
    //}

    //// PUT api/<EventTypePhucTmsController>/5
    //[HttpPut("{id}")]
    //public void Put(int id, [FromBody] string value)
    //{
    //}

    //// DELETE api/<EventTypePhucTmsController>/5
    //[HttpDelete("{id}")]
    //public void Delete(int id)
    //{
    //}
}
