using System;
using System.Collections.Generic;
using Examen_ASP_VanDoorenFabio_3IMDA_2018.Entities;

namespace Examen_ASP_VanDoorenFabio_3IMDA_2018.Models
{
    public class BrandDetailViewModel
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public List<Car> CarsByBrand { get; set; }
    }
}