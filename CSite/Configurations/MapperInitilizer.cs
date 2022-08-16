using AutoMapper;
using CSite.DTO;
using CSite.Models;

namespace CSite.Configurations
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            //CarController
            CreateMap<List<CarDTO>, List<Car>>().ReverseMap();
            CreateMap<CarDTO, Car>().ReverseMap();

            //CarProductController
            CreateMap<CarProductDTO, CarProduct>().ReverseMap();

            //CustomerController
            CreateMap<CustomerDTO, Customer>().ReverseMap();

            //ExportProductController
            CreateMap<ExportProductDTO, ExportProduct>().ReverseMap();


        }
    }
}
