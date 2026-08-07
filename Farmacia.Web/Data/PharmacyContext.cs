using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore;
using Farmacia.web.Models;
namespace Farmacia.web.Data;
public class PharmacyContext : DbContext
{
    public PharmacyContext(DbContextOptions<PharmacyContext>options): base(options)
    {
    }
    public DbSet<Customer> Customers {get;set;}
    public DbSet<Medication> Medications {get;set;}
    public DbSet<Sale> Sales {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       


        // Configurar relación en cascada Si borras Cliente, borra sus Ventas
        modelBuilder.Entity<Sale>()
           .HasOne(s=> s.Customer)
           .WithMany(c=> c.Sales )
           .HasForeignKey(s => s.CustomerId)
           .OnDelete(DeleteBehavior.Cascade);

           // No borrar medicamento si tiene ventas
        modelBuilder.Entity<Sale>()
           .HasOne(s => s.Medication)
           .WithMany(m=> m.Sales)
           .HasForeignKey(s => s.MedicationId)
           .OnDelete(DeleteBehavior.Restrict); 
    }

}