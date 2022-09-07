using AutoMapper;
using CSite.Data.DTO;
using CSite.Data.Entities;

namespace CSite.Configurations
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<Cars, CarsDTO>().ReverseMap();
            CreateMap<Products, ProductsDTO>().ReverseMap();
            CreateMap<AspNetRoles, RolesDTO>().ReverseMap();
            CreateMap<Receipts, ReceiptsDTO>().ReverseMap();
            CreateMap<AspNetUsers, UsersDTO>().ReverseMap();
        }
    }
}
