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

namespace Examen_ASP_VanDoorenFabio_3IMDA_2018.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarService _carService;

        public HomeController(ICarService carService)
        {
            _carService = carService;
        }

        //LIST  OF ALL CARS
        protected CarDetailViewModel ConvertCarToCarDetailViewModel(Car car)
        {
            return new CarDetailViewModel()
            {
                Id = car.Id,
                Color = car.Color,
                DatePurchased = car.DatePurchased,
                LicensePlate = car.LicensePlate,
                Owner = string.Join(";", car.Owner.Select(x => x.Owner.FirstName + " " + x.Owner.LastName)),
                CarType = car.Cartype?.Model + " (" + car.Cartype?.Brand + ")",
            };
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            var model = new CarListViewModel() { Cars = new List<CarDetailViewModel>() };
            var allCars = _carService.GetAllCars();
            model.Cars.AddRange(allCars.Select(ConvertCarToCarDetailViewModel).ToList());
            return View(model);
        }

        //EDIT CAR
        public CarEditViewModel ConvertCarToEditViewModel(Car car)
        {
            var editView = new CarEditViewModel
            {
                Id = car.Id,
                LicensePlate = car.LicensePlate,
                DatePurchased = car.DatePurchased,
                Color = car.Color,
                CarType = car.Cartype?.Model,
                CarTypeId = car.Cartype?.Id,
                Owner = string.Join(";", car.Owner.Select(x => x.Owner.FirstName + " " + x.Owner.LastName)),
                OwnerId = car.Owner?.Select(x => x.OwnerId).FirstOrDefault(),
            };
            return editView;
        }

        [HttpGet("/{id}")]
        public IActionResult Edit([FromRoute] int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null)
            {
                return NotFound();
            }

            var editView = ConvertCarToEditViewModel(car);
            editView.CarTypes = _carService.GetAllTypes().Select(x => new SelectListItem
            {
                Text = x.Model + " (" + x.Brand + ")",
                Value = x.Id.ToString(),
            }
            ).ToList();
            editView.Owners = _carService.GetAllOwners().Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.Id.ToString(),
            }
            ).ToList();
            return View(editView);
        }


        //GO TO CREATE NEW CAR PAGE
        [HttpGet("/Home/New")]
        public IActionResult New()
        {
            return View("Edit", new CarEditViewModel()
            {
                LicensePlate = null,
                CarTypes = _carService.GetAllTypes().Select(x => new SelectListItem
                {
                    Text = x.Model + " (" + x.Brand + ")",
                    Value = x.Id.ToString(),
                }).ToList(),
                Owners = _carService.GetAllOwners().Select(x => new SelectListItem
                {
                    Text = x.FirstName + " " + x.LastName,
                    Value = x.Id.ToString(),
                }).ToList(),
            });
        }

        //SAVE NEW CAR
        [HttpPost("/")]
        public IActionResult Save([FromForm] CarEditViewModel editView)
        {

            if (ModelState.IsValid)
            {
                var car = editView.Id == 0 ? new Car() : _carService.GetCarById(editView.Id);
                car.LicensePlate = editView.LicensePlate;
                car.Cartype = editView.CarTypeId.HasValue ? _carService.GetTypeById(editView.CarTypeId.Value) : null;
                car.DatePurchased = editView.DatePurchased;
                car.Color = editView.Color;

                List<CarOwner> OwnersList = new List<CarOwner>();
                OwnersList.Add(new CarOwner()
                {
                    OwnerId = _carService.GetOwnerById(editView.OwnerId.Value).Id
                });
                car.Owner = OwnersList;

                _carService.Save(car);
                return Redirect("/");
            }
            return View("Edit", editView);
        }

        //DELETE CAR
        [HttpPost("/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _carService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        //GO TO BRANDS
        [HttpGet("/Brands")]
        public IActionResult Brands()
        {
            ViewData["Message"] = "Brands";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
