using CSite.Models;

namespace CSite.DTO.Extension_Methods
{
    public static class Extensions
    {


        #region Car
        public static CarDTO CarToDTO(this Car car)
        {
            if (car == null)
            {
                return null;
            }
            else
            {
                CarDTO carDTO = new CarDTO();
                carDTO.ID = car.ID;
                carDTO.Name = car.Name;
                carDTO.Notes = car.Notes;
                carDTO.Account = car.Account;
                return carDTO;
            }

        }
        public static Car DTOToCar(this CarDTO carDto)
        {
            if (carDto == null)
            {
                return null;
            }
            else
            {
                Car car = new Car();
                car.ID = carDto.ID;
                car.Name = carDto.Name;
                car.Notes = carDto.Notes;
                car.Account = carDto.Account;
                return car;
            }
        }
        #endregion

        #region CarProduct
        public static CarProductDTO CarProductToDTO(this CarProduct carProduct)
        {

            if (carProduct == null)
            {
                return null;
            }
            else
            {
                CarProductDTO carProductDTO = new CarProductDTO();
                carProductDTO.Quantity = carProduct.Quantity;
                carProductDTO.CarID = carProduct.CarID;
                carProductDTO.ProductID = carProduct.ProductID;
                return carProductDTO;
            }
        }
        public static CarProduct DTOToCarProduct(this CarProductDTO carProductDTO)
        {
            if (carProductDTO == null)
            {
                return null;
            }
            else
            {
                CarProduct carProduct = new CarProduct();
                carProduct.Quantity = carProductDTO.Quantity;
                carProduct.CarID = carProductDTO.CarID;
                carProduct.ProductID = carProductDTO.ProductID;
                return carProduct;
            }
        }
        #endregion

        #region Customer
        public static CustomerDTO CustomerToDTO(this Customer customer)
        {
            if (customer == null)
            {
                return null;
            }
            else
            {
                CustomerDTO customerDTO = new CustomerDTO();
                customerDTO.ID = customer.ID;
                customerDTO.Name = customer.Name;
                customerDTO.Phone = customer.Phone;
                customerDTO.Notes = customer.Notes;
                customerDTO.Account = customer.Account;
                return customerDTO;
            }

        }
        public static Customer DTOToCustomer(this CustomerDTO customerDto)
        {
            if (customerDto == null)
            {
                return null;
            }
            else
            {
                Customer customer = new Customer();
                customer.ID = customerDto.ID;
                customer.Name = customerDto.Name;
                customer.Notes = customerDto.Notes;
                customer.Phone = customerDto.Phone;
                customer.Account = customerDto.Account;
                return customer;
            }
        }
        #endregion

        #region ExportProduct
        public static ExportProductDTO ExportProductToDTO(this ExportProduct exportProduct)
        {

            if (exportProduct == null)
            {
                return null;
            }
            else
            {
                ExportProductDTO exportProductDTO = new ExportProductDTO();
                exportProductDTO.ProductID = exportProduct.ProductID;
                exportProductDTO.ProductPrice = exportProduct.Price;
                exportProductDTO.TotalPrice = exportProduct.TotalPrice;
                exportProductDTO.ExportReceiptID = exportProduct.ReceiptID;
                exportProductDTO.Quantity = exportProduct.Quantity;

                return exportProductDTO;
            }
        }
        public static ExportProduct DTOToExportProduct(this ExportProductDTO exportProductDTO)
        {
            if (exportProductDTO == null)
            {
                return null;
            }
            else
            {
                ExportProduct exportProduct = new ExportProduct();
                exportProduct.Quantity = exportProductDTO.Quantity;
                exportProduct.TotalPrice = exportProductDTO.TotalPrice;
                exportProduct.Price = exportProductDTO.ProductPrice;
                exportProduct.ProductID = exportProductDTO.ProductID;
                exportProduct.ReceiptID = exportProductDTO.ExportReceiptID;

                return exportProduct;
            }
        }
        #endregion

        #region Supplier
        public static SupplierDTO SupplierToDTO(this Supplier supplier)
        {
            if (supplier == null)
            {
                return null;
            }
            else
            {
                SupplierDTO supplierDTO = new SupplierDTO();
                supplierDTO.ID = supplier.ID;
                supplierDTO.Name = supplier.Name;
                supplierDTO.Phone = supplier.Phone;
                supplierDTO.Notes = supplier.Notes;
                supplierDTO.Account = supplier.Account;
                return supplierDTO;
            }

        }
        public static Supplier DTOToSupplier(this SupplierDTO supplierDTO)
        {
            if (supplierDTO == null)
            {
                return null;
            }
            else
            {
                Supplier Supplier = new Supplier();
                Supplier.ID = supplierDTO.ID;
                Supplier.Name = supplierDTO.Name;
                Supplier.Notes = supplierDTO.Notes;
                Supplier.Phone = supplierDTO.Phone;
                Supplier.Account = supplierDTO.Account;
                return Supplier;
            }
        }
        #endregion

        #region Users
        public static UsersDTO UsersToDTO(this Users users)
        {
            if (users == null)
            {
                return null;
            }
            else
            {
                UsersDTO usersDTO = new UsersDTO();
                usersDTO.UserName = users.UserName;
                usersDTO.Password = users.Password;
                usersDTO.Type = users.Type;
                usersDTO.CarID = users.CarID;
                return usersDTO;
            }

        }
        public static Users DTOToUsers(this UsersDTO usersDTO)
        {

            if (usersDTO == null)
            {
                return null;
            }
            else
            {
                Users users = new Users();
                users.UserName = usersDTO.UserName;
                users.Password = usersDTO.Password;
                users.Type = usersDTO.Type;
                users.CarID = usersDTO.CarID;
                return users;
            }
        }
        #endregion

        #region ImportProduct
        //ImportProductToDTO
        public static ImportProductDTO ImportProductToDTO(this ImportProduct importProduct)
        {
            if (importProduct != null)
            {
                return new ImportProductDTO
                {
                    ImportReceiptID = importProduct.ReceiptID,
                    ProductID = importProduct.ProductID,
                    Quantity = importProduct.Quantity,
                    TotalPrice = importProduct.TotalPrice
                };
            }
            else
            {
                return null;
            }
        }

        //DTOToImportProduct
        public static ImportProduct DTOToImportProduct(this ImportProductDTO importProductDTO)
        {
            if (importProductDTO != null)
            {
                return new ImportProduct
                {
                    ReceiptID = importProductDTO.ImportReceiptID,
                    ProductID = importProductDTO.ProductID,
                    Price = importProductDTO.BuyingPrice,
                    Quantity = importProductDTO.Quantity,
                    TotalPrice = importProductDTO.TotalPrice,

                };
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region ExportReciept
        //ExportRecieptToDTO
        public static ExportRecieptDTO ExportRecieptToDTO(this ExportReciept exportReciept)
        {

            if (exportReciept != null)
            {
                return new ExportRecieptDTO
                {
                    ID = exportReciept.ID,
                    Date = exportReciept.Date.ToString(),
                    Total = exportReciept.Total,
                    Notes = exportReciept.Notes,
                    Paid = exportReciept.Paid,
                    Remaining = exportReciept.Remaining,
                    customerID = exportReciept.CustomerID,
                    UserName = exportReciept.UserName,
                    CarID = exportReciept.CarID,
                };
            }
            else
            {
                return null;
            }
        }

        //DTOToExportReciept
        public static ExportReciept DTOToExportReciept(this ExportRecieptDTO exportRecieptDTO)
        {

            if (exportRecieptDTO != null)
            {
                return new ExportReciept
                {
                    ID = exportRecieptDTO.ID,
                    Date = Convert.ToDateTime(exportRecieptDTO.Date),
                    Total = exportRecieptDTO.Total,
                    Notes = exportRecieptDTO.Notes,
                    Paid = exportRecieptDTO.Paid,
                    Remaining = exportRecieptDTO.Remaining,
                    CustomerID = exportRecieptDTO.customerID,
                    UserName = exportRecieptDTO.UserName,
                    CarID = exportRecieptDTO.CarID,

                };
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region ImportReciept
        //ImportRecieptToDTO
        public static ImportRecieptDTO ImportRecieptToDTO(this ImportReciept importReciept)
        {

            if (importReciept != null)
            {
                return new ImportRecieptDTO
                {
                    ID = importReciept.ID,
                    Date = importReciept.Date.ToString(),
                    Total = importReciept.Total,
                    Notes = importReciept.Notes,
                    Paid = importReciept.Paid,
                    Remaining = importReciept.Remaining,
                    SUPID = importReciept.SupplierID,
                    UserName = importReciept.UserName
                };
            }
            else
            {
                return null;
            }
        }

        //DTOToImportReciept
        public static ImportReciept DTOToImportReciept(this ImportRecieptDTO importRecieptDTO)
        {

            if (importRecieptDTO != null)
            {
                return new ImportReciept
                {
                    ID = importRecieptDTO.ID,
                    Date = Convert.ToDateTime(importRecieptDTO.Date),
                    Total = importRecieptDTO.Total,
                    Notes = importRecieptDTO.Notes,
                    Paid = importRecieptDTO.Paid,
                    Remaining = importRecieptDTO.Remaining,
                    SupplierID = importRecieptDTO.SUPID,
                    UserName = importRecieptDTO.UserName
                };
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Transactions
        //transactionsTODTO
        public static TransactionsDTO TransactionsToDTO(this Transactions transactions)
        {

            if (transactions != null)
            {
                return new TransactionsDTO
                {
                    ID = transactions.ID,
                    AccountID = transactions.AccountID,
                    AccountType = transactions.AccountType,
                    Amount = transactions.Amount,
                    Type = transactions.Type,
                    OperationID = transactions.OperationID,
                    Operation = transactions.Operation,
                    Date = transactions.Date.ToString(),
                    UserName = transactions.UserName,
                    Notes = transactions.Notes,

                };
            }
            else
            {
                return null;
            }
        }

        //DTO to Transactions 
        public static Transactions DTOTOTransactions(this TransactionsDTO transactionsDTO)
        {

            if (transactionsDTO != null)
            {
                return new Transactions
                {
                    ID = transactionsDTO.ID,
                    AccountID = transactionsDTO.AccountID,
                    AccountType = transactionsDTO.AccountType,
                    Amount = transactionsDTO.Amount,
                    Type = transactionsDTO.Type,
                    OperationID = transactionsDTO.OperationID,
                    Operation = transactionsDTO.Operation,
                    Date = Convert.ToDateTime(transactionsDTO.Date),
                    UserName = transactionsDTO.UserName,
                    Notes = transactionsDTO.Notes,

                };
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Product
        //ProductToDto 
        public static ProductDTO ProductToDTO(this Product product)
        {
            if (product != null)
            {
                return new ProductDTO
                {
                    ID = product.ID,
                    Name = product.Name,
                    BuyingPrice = product.BuyingPrice,
                    SellingPrice = product.SellingPrice,
                    Quantity = product.Quantity

                };
            }
            else
            {
                return null;
            }
        }

        //DTOToProduct 
        public static Product DTOToProduct(this ProductDTO productDTO)
        {
            if (productDTO != null)
            {
                return new Product
                {
                    ID = productDTO.ID,
                    Name = productDTO.Name,
                    BuyingPrice = productDTO.BuyingPrice,
                    SellingPrice = productDTO.SellingPrice,
                    Quantity = productDTO.Quantity

                };
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}


