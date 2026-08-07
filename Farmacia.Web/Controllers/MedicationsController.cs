using System.Resources;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Farmacia.web.Models;
using Farmacia.web.Data;


namespace Farmacia.Web.Controllers;
[ApiController]
[Route("api/[controller]")]
public class MedicationsController : ControllerBase
{
    private readonly PharmacyContext _context;
    public MedicationsController(PharmacyContext context)
    {
        _context= context;
    }

    //get api/medication - obtener todos los medicamentos 
    [HttpGet]
    public async Task<ActionResult<List<Medication>>> GetAll()
    {
        var medications = await _context.Medications.ToListAsync();
        return Ok(medications);
    }

    // Get api/medication/{id}_obterner un medicamento por id
    [HttpGet("{id}")]
    public async Task<ActionResult<Medication>> GetById(int id)
    {
        var medication = await _context.Medications
            .FirstOrDefaultAsync(m => m.Id == id);
            if(medication is null)return NotFound();
            return Ok(medication);
            
    }
    // POST api/medications -crear nuevo medicamento
    [HttpPost]
    public async Task<ActionResult<Medication>> Create(Medication medication)
    {
        
        _context.Medications.Add(medication);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new {id=medication.Id}, medication);

        
    }

    // PUT api/medications/{id}- Actualizar medicamento
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, Medication updatedMedication)
    {
        var medication = await _context.Medications.FindAsync(id);
        if(medication is null) return NotFound();
        medication.Name = updatedMedication.Name;
        medication.Price = updatedMedication.Price;
        medication.Stock = updatedMedication.Stock;
        medication.ExpirationDate = updatedMedication.ExpirationDate;
        await _context.SaveChangesAsync();
        return Ok(medication);
    }
    // DELETE api/medications/id - eliminar un medicamento 
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var medication = await _context.Medications.FindAsync(id);
        if(medication is null) return NotFound();

        _context.Medications.Remove(medication);
        await _context.SaveChangesAsync();
        return NoContent();

    }
}