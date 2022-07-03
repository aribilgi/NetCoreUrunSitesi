using BL;
using Entities;
using Microsoft.AspNetCore.Mvc;
using NetCoreUrunSitesi.Models;
using System.Diagnostics;

namespace NetCoreUrunSitesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Slider> _sliderRepository;
        private readonly IRepository<Product> _productRepository;

        public HomeController(IRepository<Slider> sliderRepository, IRepository<Product> productRepository)
        {
            _sliderRepository = sliderRepository;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> IndexAsync()
        {
            //var sliders = await _sliderRepository.GetAllAsync();

            var model = new HomePageViewModel()
            {
                Sliders = _sliderRepository.GetAll(),
                Products = _productRepository.GetAll()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}