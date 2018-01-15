using System.Collections.Generic;
using Examen_ASP_VanDoorenFabio_3IMDA_2018.Entities;

namespace Examen_ASP_VanDoorenFabio_3IMDA_2018.Services
{
    public interface ICarService
    {
        List<Car> GetAllCars();
        Car GetCarById(int id);
        List<CarType> GetAllTypes();
        CarType GetTypeById(int id);
        List<Owner> GetAllOwners();
        Owner GetOwnerById(int id);
        void Save(Car car);
        void Delete(int id);
    }
}