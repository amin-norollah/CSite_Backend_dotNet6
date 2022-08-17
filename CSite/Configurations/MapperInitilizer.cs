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

            //ExpensesController
            CreateMap<Expenses, Expenses>().ReverseMap();

            //CustomerController
            CreateMap<CustomerDTO, Customer>().ReverseMap();

            //ExportProductController
            CreateMap<ExportProductDTO, ExportProduct>().ReverseMap();

            //ExportRecieptController
            CreateMap<ExportReciept, ExportRecieptDTO>().ReverseMap();

            //ImportProductController
            CreateMap<ImportProduct, ImportProductDTO>().ReverseMap();

            //ImportRecieptController
            CreateMap<ImportReciept, ImportRecieptDTO>().ReverseMap();

            //ProductController
            CreateMap<Product, ProductDTO>().ReverseMap();

            //SupplierController
            CreateMap<Supplier, SupplierDTO>().ReverseMap();

            //TransactionsController
            CreateMap<Transactions, TransactionsDTO>().ReverseMap();

            //UsersController
            CreateMap<Users, UsersDTO>().ReverseMap();
        }
    }
}
