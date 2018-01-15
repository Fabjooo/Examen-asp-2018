using System;
using System.Collections.Generic;
using Examen_ASP_VanDoorenFabio_3IMDA_2018.Entities;

namespace Examen_ASP_VanDoorenFabio_3IMDA_2018.Models
{
    public class CarDetailViewModel
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public DateTime DatePurchased { get; set; }
        public string LicensePlate { get; set; }
        public string Owner { get; set; }
        public string CarType { get; set; }
    }
}