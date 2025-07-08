using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SMMS.Repositories.NghiaHT.ModelExtensions;
using SMMS.Repositories.NghiaHT.Models;
using static System.Net.WebRequestMethods;

namespace SMMS.MVCWebApp.NghiaHT.Controllers;

public class RequestNghiaHtsController : Controller
{
    private string APIEndPoint = "https://localhost:7128/api/";

    public async Task<IActionResult> Index(string medicationName, int quantity, string categoryName, int currentPage = 1, int pageSize = 3)
    {
        var searchRequest = new SearchRequestNghiaHt
        {
            MedicationName = medicationName,
            Quantity = quantity,
            CategoryName = categoryName,
            CurrentPage = currentPage,
            PageSize = pageSize
        };
        using (var httpClient = new HttpClient())
        {


            var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
            //tokenString = HttpContext.Request.Cookies["TokenString"];
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);



            using (var response = await httpClient.PostAsJsonAsync(APIEndPoint + "RequestNghiaHts/Search", searchRequest))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<PaginationResult<List<RequestNghiaHt>>>(content);

                    if (result != null)
                    {
                        ViewBag.MedicationName = medicationName;
                        ViewBag.Quantity = quantity;
                        ViewBag.CategoryName = categoryName;
                        ViewBag.CurrentPage = currentPage;
                        ViewBag.PageSize = pageSize;
                        ViewBag.TotalItems = result.TotalItems;
                        return View(result.Items);
                    }
                }
            }
        }

        return View(new List<RequestNghiaHt>());
    }

    public async Task<IActionResult> Create()
    {
        var medicationCategoryQuanTn = await this.GetMedicationCategoryQuanTn();
        ViewData["MedicationCategoryQuanTnid"] = new SelectList(medicationCategoryQuanTn, "MedicationCategoryQuanTnid", "CategoryName");
        return View();
    }

    public async Task<List<MedicationCategoryQuanTn>> GetMedicationCategoryQuanTn()
    {
        var medicationCategoryQuanTn = new List<MedicationCategoryQuanTn>();

        using (var httpClient = new HttpClient())
        {

            var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);


            using (var response = await httpClient.GetAsync(APIEndPoint + "MedicationCategoryQuanTns"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    medicationCategoryQuanTn = JsonConvert.DeserializeObject<List<MedicationCategoryQuanTn>>(content);
                }
            }
        }

        return medicationCategoryQuanTn;
    }
}
