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
    public class ImportRecieptController : ControllerBase
    {
        private readonly CSiteDbContext _context;

        public ImportRecieptController(CSiteDbContext context)
        {
            _context = context;
        }

        // GET: api/ImportReciept
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImportRecieptDTO>>> GetImportReciepts()
        {
            return await _context.ImportReciepts.Select(A => A.ImportRecieptToDTO()).ToListAsync();

        }

        // GET: api/ImportReciept/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImportRecieptDTO>> GetImportReciept(int id)
        {
            var imporReciept = await _context.ImportReciepts.FindAsync(id);
            List<ImportProduct> importproducts = _context.ImportProducts.Where(w => w.ReceiptID == id).ToList();

            if (imporReciept == null)
            {
                return NotFound();
            }
            ImportRecieptDTO importRecieptDTO = imporReciept.ImportRecieptToDTO();
            importRecieptDTO.importProducts = importproducts.Select(A => A.ImportProductToDTO()).ToArray();
            return importRecieptDTO;
        }

        // PUT: api/ImportReciept/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImportReciept(int id, ImportRecieptDTO importRecieptDTO)
        {
            ImportReciept importReciept = importRecieptDTO.DTOToImportReciept();

            if (id != importReciept.ID)
            {
                return BadRequest();
            }

            _context.Entry(importReciept).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImportRecieptExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }

            return NoContent();
        }

        // POST: api/ImportReciept
        [HttpPost]
        public async Task<ActionResult<ImportReciept>> PostImportReciept(ImportRecieptDTO importRecieptDTO)
        {
            ImportReciept importReciept = importRecieptDTO.DTOToImportReciept();
            Transactions tr = new Transactions()
            {
                AccountID = importReciept.SupplierID,
                AccountType = (int)AccountType.Supplier,
                Amount = importReciept.Remaining,
                Type = (int)TransType.Paid,
                Date = importReciept.Date,
                OperationID = importReciept.ID,
                Operation = (int)Operation.ImportReciept,
                UserName = importReciept.UserName,

            };
            Supplier sup = _context.Suppliers.Find(importReciept.SupplierID);
            sup.Account += importReciept.Remaining;
            _context.Entry(sup).State = EntityState.Modified;
            _context.Transactions.Add(tr);
            _context.ImportReciepts.Add(importReciept);
            await _context.SaveChangesAsync();
            foreach (var item in importRecieptDTO.importProducts)
            {

                item.ImportReceiptID = importReciept.ID;
                _context.ImportProducts.Add(item.DTOToImportProduct());

                Product product = _context.Products.Find(item.ProductID);
                product.Quantity += item.Quantity;
                _context.Entry(product).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImportReciept", new { id = importReciept.ID }, importReciept.ImportRecieptToDTO());
        }

        // DELETE: api/ImportReciept/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImportReciept(int id)
        {
            var importReciept = await _context.ImportReciepts.FindAsync(id);
            if (importReciept == null)
            {
                return NotFound();
            }

            _context.ImportReciepts.Remove(importReciept);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImportRecieptExists(int id)
        {
            return _context.ImportReciepts.Any(e => e.ID == id);
        }
    }
}
