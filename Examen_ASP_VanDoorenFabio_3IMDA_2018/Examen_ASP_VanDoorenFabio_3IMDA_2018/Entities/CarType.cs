using System;
using System.Collections.Generic;

namespace Examen_ASP_VanDoorenFabio_3IMDA_2018.Entities
{
    public class CarType
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public virtual IEnumerable<Car> Cars { get; set; }
    }
}
