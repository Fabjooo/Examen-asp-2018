using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Examen_ASP_VanDoorenFabio_3IMDA_2018.Models;
using Examen_ASP_VanDoorenFabio_3IMDA_2018.Services;
using Examen_ASP_VanDoorenFabio_3IMDA_2018.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Examen_ASP_VanDoorenFabio_3IMDA_2018.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        //ALL BRANDS
        protected BrandDetailViewModel ConvertBrandToBrandDetailViewModel(CarType cartype)
        {
            return new BrandDetailViewModel()
            {
                Id = cartype.Id,
                Brand = cartype.Brand,
                Model = cartype.Model,
                CarsByBrand = _brandService.GetAllCarsByBrand(cartype.Model)
            };
        }

        [HttpGet("/Brand/Brands")]
        public IActionResult Brands()
        {
            var model = new BrandListViewModel() { Brands = new List<BrandDetailViewModel>() };
            var allBrands = _brandService.GetAllCarTypes();
            model.Brands.AddRange(allBrands.Select(ConvertBrandToBrandDetailViewModel).ToList());
            return View(model);
        }

        //EDIT BRAND
        public BrandEditViewModel ConvertBrandToEditViewModel(CarType cartype)
        {
            var editView = new BrandEditViewModel
            {
                Id = cartype.Id,
                Brand = cartype.Brand,
                Model = cartype.Model
            };
            return editView;
        }

        [HttpGet("/Brand/{id}")]
        public IActionResult Edit([FromRoute] int id)
        {
            var brand = _brandService.GetCarTypeById(id);
            if (brand == null)
            {
                return NotFound();
            }

            var editView = ConvertBrandToEditViewModel(brand);

            return View(editView);
        }

        //GO TO CREATE NEW BRAND PAGE
        [HttpGet("/Brand/New")]
        public IActionResult New()
        {
            return View("Edit", new BrandEditViewModel()
            {
                Brand = null,
            });
        }


        //SAVE BRAND
        [HttpPost("/Brand/Brands")]
        public IActionResult Save([FromForm] BrandEditViewModel editView)
        {
            if (ModelState.IsValid)
            {
                var cartype = editView.Id == 0 ? new CarType() : _brandService.GetCarTypeById(editView.Id);
                cartype.Model = editView.Model;
                cartype.Brand = editView.Brand;
                _brandService.Save(cartype);

                return Redirect("/Brand/Brands");
            }
            return View("Edit", editView);
        }

        //DELETE BRAND
        [HttpPost("/Brand/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _brandService.Delete(id);
            return Redirect("/Brand/Brands");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
