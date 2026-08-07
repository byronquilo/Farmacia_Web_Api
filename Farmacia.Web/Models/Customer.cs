using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Farmacia.web.Models;

namespace Farmacia.web.Models;
public class Customer
{
    public int Id {get;set;}//PK del cliente
    public string Name {get;set;}=string.Empty;//nombre del cliente
    public string IdentityNumber {get;set;}=string.Empty;//numero de identidad
    public string Address {get;set;}=string.Empty;//direccion
    public string Phone {get;set;}=string.Empty;//telefono
    public string Email {get;set;}=string.Empty;//email
    [ValidateNever]

    //un cliente pude terner muchas ventas 
    public List<Sale> Sales{get;set;}=new();

}