using System.Resources;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Farmacia.web.Models;
using Farmacia.web.Data;
namespace Farmacia.Web.Controllers;
[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly PharmacyContext _context;
    public SalesController(PharmacyContext context)
    {
        _context= context;
    }

    //get api/Sale - obtener todos las ventas 
    [HttpGet]
    public async Task<ActionResult<List<Sale>>> GetAll()
    {
        var sales = await _context.Sales
        .Include(s => s.Customer)
        .Include(s => s.Medication)
        .ToListAsync();
        return Ok(sales);    


        
    }
    // Get api/Sales/{id}_obterner una venta por id
    [HttpGet("{id}")]
    public async Task<ActionResult<Sale>> GetById(int id)
    {
        var sale = await _context.Sales
            .Include(s => s.Customer)
            .Include(s => s.Medication)
            .FirstOrDefaultAsync(m => m.Id == id);
            if(sale is null)return NotFound();
            return Ok(sale);
            
    }
    // POST api/Sales -crear nuevo 
    [HttpPost]
    public async Task<ActionResult<Sale>> Create(Sale sale)
    {
        //validamos que cliente exista
        var customerExists = await _context.Customers.FindAsync(sale.CustomerId);
        if(customerExists==null)
        {
            return BadRequest($"No existe un Cliente con Id {sale.CustomerId}");
        }
        //validamos que medicamento exista 
        var medicationExists = await _context.Medications.FindAsync(sale.MedicationId);
        if(medicationExists==null)
        {
            return BadRequest($"No existe un Medicamento con Id {sale.MedicationId}");
        }
        sale.TotalPrice = sale.Quantity * medicationExists.Price;
        medicationExists.Stock-=sale.Quantity;
        if (sale.SaleDate == default)
        {
            sale.SaleDate= DateTime.UtcNow;
        }
        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new {id=sale.Id}, sale);    
    }

    // PUT api/sales/{id}- Actualizar venta
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, Sale updatedSale)
    {
        var sale = await _context.Sales.FindAsync(id);
        if(sale is null) return NotFound();
        sale.Quantity= updatedSale.Quantity;
        await _context.SaveChangesAsync();
        return Ok(sale);
    }

   
    // DELETE api/sales/id - eliminar una venta
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var sale = await _context.Sales.FindAsync(id);
        if(sale is null) return NotFound();

        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync();
        return NoContent();

    }
}
