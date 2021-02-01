using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class Cancelation :
        StorageItemEvent,
        ICastable
    {
        // Primary key
        
        //Data
        
        //Forgein keys
        public Guid FromUnitId { get; set; }
        public Guid StorageItemId { get; set; }
        public Warehouse FromWarehouse { get; set; }
    }

    public class CancelationsFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            
            //Data

            //Forgein keys
            modelBuilder.Entity<Cancelation>()
                .HasOne(p => p.FromWarehouse)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.StorageItemId, p.FromUnitId});
        }
    }
}