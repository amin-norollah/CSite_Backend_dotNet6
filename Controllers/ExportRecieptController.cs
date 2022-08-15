using CSite.DTO;
using CSite.DTO.Extension_Methods;
using CSite.Models;
using CSite.Structures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportRecieptController : ControllerBase
    {
        private readonly CompanyContext _context;

        public ExportRecieptController(CompanyContext context)
        {
            _context = context;
        }

        // GET: api/ExportReciept
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExportRecieptDTO>>> GetExportReciepts()
        {

            return await _context.ExportReciepts.Select(w => w.ExportRecieptToDTO()).ToListAsync();
        }

        // GET: api/ExportReciept/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExportRecieptDTO>> GetExportReciept(int id)
        {
            var exportReciept = await _context.ExportReciepts.FindAsync(id);
            List<ExportProduct> exportproducts = _context.ExportProducts.Where(w => w.ReceiptID == id).ToList();

            if (exportReciept == null)
            {
                return NotFound();
            }
            ExportRecieptDTO exportRecieptDTO = exportReciept.ExportRecieptToDTO();
            exportRecieptDTO.Products = exportproducts.Select(w => w.ExportProductToDTO()).ToArray();
            return exportRecieptDTO;
        }
        // PUT: api/ExportReciept/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExportReciept(int id, ExportRecieptDTO exportRecieptDTO)
        {
            ExportReciept exportReciept = exportRecieptDTO.DTOToExportReciept();
            if (id != exportReciept.ID)
            {
                return BadRequest();
            }
            _context.Entry(exportReciept).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExportRecieptExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        // POST: api/ExportReciept
        [HttpPost]
        public async Task<ActionResult<ExportReciept>> PostExportReciept(ExportRecieptDTO exportRecieptDTO)
        {
            ExportReciept exportReciept = exportRecieptDTO.DTOToExportReciept();
            Transactions tr = new Transactions();

            if (exportReciept.CarID == null)
            {
                Customer Cust = _context.Customers.Find(exportReciept.CustomerID);
                tr = new Transactions()
                {
                    AccountID = exportReciept.CustomerID,
                    AccountType = (int)AccountType.Customer,
                    Amount = exportReciept.Remaining,
                    Type = (int)TransType.Get,
                    Date = exportReciept.Date,
                    OperationID = exportReciept.ID,
                    Operation = (int)Operation.ExportReciept,
                    UserName = exportReciept.UserName,
                };
                Cust.Account += exportReciept.Remaining;
                _context.Entry(Cust).State = EntityState.Modified;
                _context.Transactions.Add(tr);
            }
            else
            {

                Car car = _context.Cars.Find(exportReciept.CarID);
                tr = new Transactions()
                {
                    AccountID = exportReciept.CarID,
                    AccountType = (int)AccountType.Car,
                    Amount = exportReciept.Remaining,
                    Type = (int)TransType.Get,
                    Date = exportReciept.Date,
                    OperationID = exportReciept.ID,
                    Operation = (int)Operation.ExportReciept,
                    UserName = exportReciept.UserName,
                };
                car.Account += exportReciept.Remaining;
                _context.Entry(car).State = EntityState.Modified;
                _context.Transactions.Add(tr);
            }


            _context.ExportReciepts.Add(exportReciept);
            await _context.SaveChangesAsync();
            foreach (var item in exportRecieptDTO.Products)
            {

                item.ExportReceiptID = exportReciept.ID;
                _context.ExportProducts.Add(item.DTOToExportProduct());

                Product product = _context.Products.Find(item.ProductID);
                product.Quantity -= item.Quantity;
                _context.Entry(product).State = EntityState.Modified;

                if (exportReciept.CarID != null)
                {
                    if (_context.CarProducts.FirstOrDefault(w => w.ProductID == item.ProductID) == null)
                    {
                        CarProduct car = new CarProduct()
                        {
                            CarID = exportReciept.CarID,
                            ProductID = item.ProductID,
                            Quantity = item.Quantity,
                        };
                        _context.CarProducts.Add(car);
                    }
                    else
                    {
                        CarProduct car = _context.CarProducts.FirstOrDefault(w => w.ProductID == item.ProductID);
                        car.Quantity += item.Quantity;
                        _context.Entry(car).State = EntityState.Modified;


                    }
                }
            }
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetExportReciept", new { id = exportReciept.ID }, exportReciept.ExportRecieptToDTO());
        }
        // DELETE: api/ExportReciept/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExportReciept(int id)
        {
            var exportReciept = await _context.ExportReciepts.FindAsync(id);
            if (exportReciept == null)
            {
                return NotFound();
            }

            _context.ExportReciepts.Remove(exportReciept);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExportRecieptExists(int id)
        {
            return _context.ExportReciepts.Any(e => e.ID == id);
        }
    }
}
