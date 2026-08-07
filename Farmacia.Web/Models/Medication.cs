using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Farmacia.web.Models;

namespace Farmacia.web.Models;
public class Medication
{
    public int Id {get;set;}//PK del medicamento
    public string Name {get;set;}=string.Empty;//nombre medicina
    public decimal Price { get; set; } // Precio 
    public int Stock { get; set; } // Cantidad en inventario
    public DateTime ExpirationDate { get; set; } // Fecha de caducidad
    [ValidateNever]

    //un medicamento estas en muchas ventas 
    public List<Sale> Sales{get;set;}=new();

}