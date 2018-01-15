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
    public class OwnerController : Controller
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        //SHOW ALL OWNERS
        protected OwnerDetailViewModel ConvertOwnerToOwnerDetailViewModel(Owner owner)
        {
            return new OwnerDetailViewModel()
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                CarByOwner = _ownerService.GetAllCarsByOwner(owner.Id)
            };
        }

        [HttpGet("/Owner/Owners")]
        public IActionResult Owners()
        {
            var model = new OwnerListViewModel() { Owners = new List<OwnerDetailViewModel>() };
            var allOwners = _ownerService.GetAllOwners();
            model.Owners.AddRange(allOwners.Select(ConvertOwnerToOwnerDetailViewModel).ToList());
            return View(model);
        }

        //EDIT OWNER
        public OwnerEditViewModel ConvertOwnerToEditViewModel(Owner owner)
        {
            var editView = new OwnerEditViewModel
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName
            };
            return editView;
        }

        [HttpGet("/Owner/{id}")]
        public IActionResult Edit([FromRoute] int id)
        {
            var owner = _ownerService.GetOwnerById(id);
            if (owner == null)
            {
                return NotFound();
            }

            var editView = ConvertOwnerToEditViewModel(owner);

            return View(editView);
        }

        //GO TO CREATE NEW OWNER PAGE
        [HttpGet("/Owner/New")]
        public IActionResult New()
        {
            return View("Edit", new OwnerEditViewModel()
            {
                FirstName = null,
            });
        }

        //SAVE OWNER
        [HttpPost("/Owner/Owners")]
        public IActionResult Save([FromForm] OwnerEditViewModel editView)
        {
            if (ModelState.IsValid)
            {
                var owner = editView.Id == 0 ? new Owner() : _ownerService.GetOwnerById(editView.Id);
                owner.FirstName = editView.FirstName;
                owner.LastName = editView.LastName;
                _ownerService.Save(owner);

                return Redirect("/Owner/Owners");
            }
            return View("Edit", editView);
        }

        //DELETE OWNER
        [HttpPost("/Owner/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _ownerService.Delete(id);
            return Redirect("/Owner/Owners");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
