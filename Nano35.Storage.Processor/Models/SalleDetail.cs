using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class SalleDetail :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public double Price { get; set; }
        public int Count { get; set; }
        public Guid SalleId { get; set; }
        public Guid StorageItemId { get; set; }
        public Guid FromUnitId { get; set; }
        
        //Forgein keys
        public WarehouseByItemOnStorage FromWarehouse { get; set; }
        public Salle Salle { get; set; }
    }

    public class SalleDetailFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<SalleDetail>()
                .HasKey(u => new {u.Id});  
            
            //Data
            modelBuilder.Entity<SalleDetail>()
                .Property(b => b.Price)
                .IsRequired();
            modelBuilder.Entity<SalleDetail>()
                .Property(b => b.Count)
                .IsRequired();
            
            //Forgein keys
            modelBuilder.Entity<SalleDetail>()
                .HasOne(p => p.FromWarehouse)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.StorageItemId, p.FromUnitId});
            
            modelBuilder.Entity<SalleDetail>()
                .HasOne(p => p.Salle)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.SalleId});
        }
    }
}