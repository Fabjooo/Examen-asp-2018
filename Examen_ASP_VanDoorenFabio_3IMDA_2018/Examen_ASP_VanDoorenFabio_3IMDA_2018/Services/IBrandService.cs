using System.Collections.Generic;
using Examen_ASP_VanDoorenFabio_3IMDA_2018.Entities;

namespace Examen_ASP_VanDoorenFabio_3IMDA_2018.Services
{
    public interface IBrandService
    {
        List<CarType> GetAllCarTypes();
        CarType GetCarTypeById(int id);
        List<Car> GetAllCarsByBrand(string cartype);
        void Save(CarType cartype);
        void Delete(int id);
    }
}