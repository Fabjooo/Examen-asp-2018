using System;
using System.Collections.Generic;

namespace Examen_ASP_VanDoorenFabio_3IMDA_2018.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<CarOwner> Car { get; set; }
    }
}
