using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class CancelationDetail :
        ICastable
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
        public Guid CancellationsId { get; set; }
        public Guid StorageItemId { get; set; }
        public Guid FromUnitId { get; set; }
        public string FromPlace { get; set; }
        public WarehouseByItemOnStorage FromWarehouse { get; set; }
        public Cancellation Cancellation { get; set; }
    }

    public class CancellationDetailFluentContext
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
            
            //Foreign keys
            modelBuilder.Entity<CancelationDetail>()
                .HasOne(p => p.FromWarehouse)
                .WithMany(p => p.CancelationDetails)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.StorageItemId, p.FromUnitId, p.FromPlace});
            
            modelBuilder.Entity<CancelationDetail>()
                .HasOne(p => p.Cancellation)
                .WithMany(p => p.Details)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {CancelationId = p.CancellationsId});
        }
    }
}