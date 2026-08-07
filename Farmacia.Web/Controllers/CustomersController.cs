using System.Resources;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Farmacia.web.Models;
using Farmacia.web.Data;


namespace Farmacia.Web.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly PharmacyContext _context;
    public CustomersController(PharmacyContext context)
    {
        _context= context;
    }

    //get api/customer - obtener todos los clientes 
    [HttpGet]
    public async Task<ActionResult<List<Customer>>> GetAll()
    {
        var customers = await _context.Customers.ToListAsync();
        return Ok(customers);
    }

    // Get api/customer/{id}_obterner un cliente por id
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetById(int id)
    {
        var customer = await _context.Customers
            
            .FirstOrDefaultAsync(c => c.Id == id);
            if(customer is null)return NotFound();
            return Ok(customer);
            
    }
    // POST api/customers -crear nuevo cliente
    [HttpPost]
    public async Task<ActionResult<Customer>> Create(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new {id=customer.Id}, customer);
    }

    // PUT api/customers/{id}- Actualizar cliente
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, Customer updatedCustomer)
    {
        var customer = await _context.Customers.FindAsync(id);
        if(customer is null) return NotFound();
        customer.Name = updatedCustomer.Name;
        customer.IdentityNumber = updatedCustomer.IdentityNumber;
        customer.Address = updatedCustomer.Address;
        customer.Phone = updatedCustomer.Phone;
        customer.Email = updatedCustomer.Email;
        await _context.SaveChangesAsync();
        return Ok(customer);
    }
    // DELETE api/customers/id - eliminar un cliente 
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if(customer is null) return NotFound();

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return NoContent();

    }
}