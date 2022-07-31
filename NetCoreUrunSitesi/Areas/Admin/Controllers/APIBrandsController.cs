﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities;

namespace NetCoreUrunSitesi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class APIBrandsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres;

        public APIBrandsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres = "https://localhost:7132/Api/Brands";
        }

        // GET: APIBrandsController
        public async Task<ActionResult> IndexAsync()
        {
            return View(await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdres));
        }

        // GET: APIBrandsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: APIBrandsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: APIBrandsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Brand brand)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    brand.CreateDate = DateTime.Now;
                    var response = await _httpClient.PostAsJsonAsync(_apiAdres, brand);
                    if (!response.IsSuccessStatusCode) return null;
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(brand);
        }

        // GET: APIBrandsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            return View(await _httpClient.GetFromJsonAsync<Brand>($"{_apiAdres}/{id}"));
        }

        // POST: APIBrandsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Brand brand)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PutAsJsonAsync($"{_apiAdres}/{id}", brand);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(brand);
        }

        // GET: APIBrandsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            return View(await _httpClient.GetFromJsonAsync<Brand>($"{_apiAdres}/{id}"));
        }

        // POST: APIBrandsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Brand brand)
        {
            try
            {
                await _httpClient.DeleteAsync($"{_apiAdres}/{id}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}