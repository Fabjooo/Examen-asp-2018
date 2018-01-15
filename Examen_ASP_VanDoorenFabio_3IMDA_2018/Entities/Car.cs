using System;
using System.Collections.Generic;

namespace Examen_ASP_VanDoorenFabio_3IMDA_2018.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public virtual CarType Cartype { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public virtual List<CarOwner> Owner { get; set; }
        public DateTime DatePurchased { get; set; }
    }
}
