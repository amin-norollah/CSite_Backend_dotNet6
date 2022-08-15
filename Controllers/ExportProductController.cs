using CSite.DTO;
using CSite.DTO.Extension_Methods;
using CSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportProductController : ControllerBase
    {
        private readonly CompanyContext _context;

        public ExportProductController(CompanyContext context)
        {
            _context = context;
        }

        // GET: api/ExportProduct
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExportProductDTO>>> GetExportProducts()
        {
            return await _context.ExportProducts.Select(w => w.ExportProductToDTO()).ToListAsync();
        }

        // GET: api/ExportProduct/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExportProductDTO>> GetExportProduct(int id)
        {
            var exportProduct = await _context.ExportProducts.FindAsync(id);


            if (exportProduct == null)
            {
                return NotFound();
            }

            return exportProduct.ExportProductToDTO();
        }

        // PUT: api/ExportProduct/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPut("{id}/{ReceiptID}")]
        public async Task<IActionResult> PutExportProduct(int id, int ReceiptID, ExportProductDTO exportProductDTO)
        {
            ExportProduct exportProduct = exportProductDTO.DTOToExportProduct();
            if (id != exportProduct.ProductID && ReceiptID != exportProduct.ReceiptID)
            {
                return BadRequest();
            }

            _context.Entry(exportProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!ExportProductExists(id, ReceiptID))
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

        // POST: api/ExportProduct
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExportProduct>> PostExportProduct(ExportProductDTO[] exportProductDTO)
        {
            foreach (var item in exportProductDTO)
            {
                ExportProduct exportProduct = item.DTOToExportProduct();

                _context.ExportProducts.Add(exportProduct);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {

                    if (ExportProductExists(exportProduct.ProductID, exportProduct.ReceiptID))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

            }

            return NoContent();
        }

        // DELETE: api/ExportProduct/5

        [HttpDelete("{id}/{ReceiptID}")]
        public async Task<IActionResult> DeleteExportProduct(int id, int ReceiptID)
        {
            var exportProduct = await _context.ExportProducts.FirstOrDefaultAsync(w => w.ProductID == id && w.ReceiptID == ReceiptID);
            if (exportProduct == null)
            {
                return NotFound();
            }

            _context.ExportProducts.Remove(exportProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool ExportProductExists(int id, int ReceiptID)
        {
            return _context.ExportProducts.Any(e => e.ReceiptID == ReceiptID && e.ProductID == id);
        }
    }
}
