using System.Collections.Generic;
using Examen_ASP_VanDoorenFabio_3IMDA_2018.Entities;

namespace Examen_ASP_VanDoorenFabio_3IMDA_2018.Services
{
    public interface IOwnerService
    {
        List<Owner> GetAllOwners();
        Owner GetOwnerById(int id);
        List<Car> GetAllCarsByOwner(int ownerId);
        void Save(Owner owner);
        void Delete(int id);
    }
}