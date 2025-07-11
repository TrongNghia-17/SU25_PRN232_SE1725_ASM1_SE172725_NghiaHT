﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SMMS.Repositories.NghiaHT.ModelExtensions;
using SMMS.Repositories.NghiaHT.Models;
using static System.Net.WebRequestMethods;

namespace SMMS.MVCWebApp.NghiaHT.Controllers;

public class RequestNghiaHtsController : Controller
{
    private string APIEndPoint = "https://localhost:7128/api/";

    public async Task<IActionResult> Index(string medicationName, int? quantity, string categoryName, int currentPage = 1, int pageSize = 2)
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
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

            try
            {
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
                            ViewBag.TotalItems = result.TotalItems; // Đảm bảo TotalItems không null
                            return View(result.Items ?? new List<RequestNghiaHt>());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần
                Console.WriteLine($"Error calling API: {ex.Message}");
            }
        }

        // Gán giá trị mặc định nếu API thất bại
        ViewBag.MedicationName = medicationName;
        ViewBag.Quantity = quantity;
        ViewBag.CategoryName = categoryName;
        ViewBag.CurrentPage = currentPage;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalItems = 0; // Giá trị mặc định khi không có dữ liệu
        return View(new List<RequestNghiaHt>());
    }

    public async Task<IActionResult> Create()
    {
        var medicationCategoryQuanTn = await this.GetMedicationCategoryQuanTn();
        ViewData["MedicationCategoryQuanTnid"] = new SelectList(medicationCategoryQuanTn, "MedicationCategoryQuanTnid", "CategoryName");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RequestNghiaHt requestNghiaHt)
    {
        if (ModelState.IsValid)
        {
            using (var httpClient = new HttpClient())
            {
                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                using (var response = await httpClient.PostAsJsonAsync(APIEndPoint + "RequestNghiaHts", requestNghiaHt))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<int>(content);

                        if (result > 0)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
        var medicationCategoryQuanTn = await this.GetMedicationCategoryQuanTn();
        ViewData["MedicationCategoryQuanTnid"] = new SelectList(medicationCategoryQuanTn, "MedicationCategoryQuanTnid", "CategoryName");
        return View(requestNghiaHt);
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

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var requestNghiaHt = new RequestNghiaHt();

        using (var httpClient = new HttpClient())
        {

            var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);


            using (var response = await httpClient.GetAsync(APIEndPoint + $"RequestNghiaHts/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    requestNghiaHt = JsonConvert.DeserializeObject<RequestNghiaHt>(content);
                }
            }
        }

        if (requestNghiaHt == null)
        {
            return NotFound();
        }

        return View(requestNghiaHt);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var requestNghiaHt = new RequestNghiaHt();

        using (var httpClient = new HttpClient())
        {

            var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);


            using (var response = await httpClient.GetAsync(APIEndPoint + $"RequestNghiaHts/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    requestNghiaHt = JsonConvert.DeserializeObject<RequestNghiaHt>(content);
                }
            }
        }
        if (requestNghiaHt == null)
        {
            return NotFound();
        }

        return View(requestNghiaHt);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        using (var httpClient = new HttpClient())
        {
            var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

            using (var response = await httpClient.DeleteAsync(APIEndPoint + $"RequestNghiaHts/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<bool>(content);

                    if (result)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var requestNghiaHt = new RequestNghiaHt();
        using (var httpClient = new HttpClient())
        {

            var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);


            using (var response = await httpClient.GetAsync(APIEndPoint + $"RequestNghiaHts/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    requestNghiaHt = JsonConvert.DeserializeObject<RequestNghiaHt>(content);
                }
            }
        }

        if (requestNghiaHt == null)
        {
            return NotFound();
        }
        var medicationCategoryQuanTn = await this.GetMedicationCategoryQuanTn();
        ViewData["MedicationCategoryQuanTnid"] = new SelectList(medicationCategoryQuanTn, "MedicationCategoryQuanTnid", "CategoryName");
        return View(requestNghiaHt);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(RequestNghiaHt requestNghiaHt)
    {

        if (ModelState.IsValid)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                    using (var response = await httpClient.PutAsJsonAsync(APIEndPoint + "RequestNghiaHts", requestNghiaHt))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<int>(content);

                            if (result > 0)
                            {
                                //return RedirectToAction(nameof(Index));
                                return RedirectToAction(nameof(Details), new { id = requestNghiaHt.RequestNghiaHtid });
                            }
                        }
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        var medicationCategoryQuanTn = await this.GetMedicationCategoryQuanTn();
        ViewData["MedicationCategoryQuanTnid"] = new SelectList(medicationCategoryQuanTn, "MedicationCategoryQuanTnid", "CategoryName");
        return View(requestNghiaHt);
    }

    public async Task<IActionResult> MedicationCategoryQuanTnList()
    {
        return View();
    }
}
