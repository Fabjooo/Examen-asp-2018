using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Examen_ASP_VanDoorenFabio_3IMDA_2018.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Examen_ASP_VanDoorenFabio_3IMDA_2018.Models
{
    public class CarEditViewModel
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Color { get; set; }
        public DateTime DatePurchased { get; set; }
        public string Owner { get; set; }
        public int? OwnerId { get; set; }
        public List<SelectListItem> Owners { get; set; }
        public string CarType { get; set; }
        public int? CarTypeId { get; set; }
        public List<SelectListItem> CarTypes { get; set; }
    }
}