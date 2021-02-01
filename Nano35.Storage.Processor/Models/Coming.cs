using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class Coming :
        StorageItemEvent,
        ICastable
    {
        // Primary key
        
        //Data
        
        //Forgein keys
        public Guid ToUnitId { get; set; }
        public Guid StorageItemId { get; set; }
        public Warehouse ToWarehouse { get; set; }
    }

    public class ComingsFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            
            //Data

            //Forgein keys
            modelBuilder.Entity<Coming>()
                .HasOne(p => p.ToWarehouse)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.StorageItemId, p.ToUnitId});
        }
    }
}