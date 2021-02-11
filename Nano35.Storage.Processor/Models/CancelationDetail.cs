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
        public string FromPlace { get; set; }
        
        //Forgein values
        public WarehouseByItemOnStorage FromWarehouse { get; set; }
        public Cancellation Cancellation { get; set; }
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
            modelBuilder.Entity<CancelationDetail>()
                .Property(b => b.FromPlace)
                .IsRequired();
            
            //Forgein keys
            modelBuilder.Entity<CancelationDetail>()
                .HasOne(p => p.FromWarehouse)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.StorageItemId, p.FromUnitId, p.FromPlace});
            
            modelBuilder.Entity<CancelationDetail>()
                .HasOne(p => p.Cancellation)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.CancelationId});
        }
    }
}