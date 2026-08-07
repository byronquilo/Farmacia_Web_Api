using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace Farmacia.web.Models;
public class Sale
{
    public int Id {get;set;}
    public DateTime SaleDate {get;set;}=DateTime.UtcNow;
    public int Quantity {get;set;}
    public decimal TotalPrice { get; set; } // Precio total = Quantity * Medication.Price
    public int  CustomerId {get;set;}
    public int  MedicationId {get;set;}
    [ValidateNever]

    public Customer Customer { get; set; } = null!; // 1 Venta pertenece a 1 Cliente
    [ValidateNever]
    public Medication Medication { get; set; } = null!; // 1 Venta es de 1 Medicamento

}