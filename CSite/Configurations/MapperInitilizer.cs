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
            CreateMap<CarsDTO, Cars>().ReverseMap();

            //CarProductController
            CreateMap<CarProductsDTO, CarProducts>().ReverseMap();

            //ExpensesController
            CreateMap<Expenses, Expenses>().ReverseMap();

            //CustomerController
            CreateMap<CustomersDTO, Customers>().ReverseMap();

            //ExportProductController
            CreateMap<ExportProductsDTO, ExportProducts>().ReverseMap();

            //ExportRecieptController
            CreateMap<ExportReciepts, ExportRecieptsDTO>().ReverseMap();

            //ImportProductController
            CreateMap<ImportProducts, ImportProductsDTO>().ReverseMap();

            //ImportRecieptController
            CreateMap<ImportReciepts, ImportRecieptsDTO>().ReverseMap();

            //ProductController
            CreateMap<Products, ProductsDTO>().ReverseMap();

            //SupplierController
            CreateMap<Suppliers, SuppliersDTO>().ReverseMap();

            //TransactionsController
            CreateMap<Transactions, TransactionsDTO>().ReverseMap();

            //UsersController
            CreateMap<Users, UsersDTO>().ReverseMap();
        }
    }
}
