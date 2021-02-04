using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class CancelationDetail :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public int Count { get; set; }
        public Guid CancelationId { get; set; }
        public Guid StorageItemId { get; set; }
        public Guid FromUnitId { get; set; }
        
        //Forgein values
        public WarehouseByItemOnStorage FromWarehouse { get; set; }
        public Cancelation Cancelation { get; set; }
    }

    public class CancelationDetailFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<CancelationDetail>()
                .HasKey(u => new {u.Id});  
            
            //Data
            modelBuilder.Entity<CancelationDetail>()
                .Property(b => b.Count)
                .IsRequired();
            
            //Forgein keys
            modelBuilder.Entity<CancelationDetail>()
                .HasOne(p => p.FromWarehouse)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.StorageItemId, p.FromUnitId});
            
            modelBuilder.Entity<CancelationDetail>()
                .HasOne(p => p.Cancelation)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.CancelationId});
        }
    }
}