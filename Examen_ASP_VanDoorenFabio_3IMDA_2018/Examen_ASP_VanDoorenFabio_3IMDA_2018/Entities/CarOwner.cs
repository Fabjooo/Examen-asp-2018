using System;
using System.Collections.Generic;

namespace Examen_ASP_VanDoorenFabio_3IMDA_2018.Entities
{
    public class CarOwner
    {
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
