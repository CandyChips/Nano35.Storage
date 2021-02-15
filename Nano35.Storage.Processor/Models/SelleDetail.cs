using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class SelleDetail :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public double Price { get; set; }
        public int Count { get; set; }
        public Guid SelleId { get; set; }
        public Guid StorageItemId { get; set; }
        public Guid FromUnitId { get; set; }
        public string FromPlace { get; set; }
        
        //Foreign keys
        public WarehouseByItemOnStorage FromWarehouse { get; set; }
        public Selle Selle { get; set; }
    }

    public class SelleDetailFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<SelleDetail>()
                .HasKey(u => new {u.Id});  
            
            //Data
            modelBuilder.Entity<SelleDetail>()
                .Property(b => b.Price)
                .IsRequired();
            modelBuilder.Entity<SelleDetail>()
                .Property(b => b.Count)
                .IsRequired();
            modelBuilder.Entity<SelleDetail>()
                .Property(b => b.FromPlace)
                .IsRequired();
            
            //Foreign keys
            modelBuilder.Entity<SelleDetail>()
                .HasOne(p => p.FromWarehouse)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.StorageItemId, p.FromUnitId, p.FromPlace});
            
            modelBuilder.Entity<SelleDetail>()
                .HasOne(p => p.Selle)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {SalleId = p.SelleId});
        }
    }
}