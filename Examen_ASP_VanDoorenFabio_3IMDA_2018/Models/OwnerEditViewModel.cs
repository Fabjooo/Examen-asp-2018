using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Examen_ASP_VanDoorenFabio_3IMDA_2018.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Examen_ASP_VanDoorenFabio_3IMDA_2018.Models
{
    public class OwnerEditViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}