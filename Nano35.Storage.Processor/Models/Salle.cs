using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class Salle :
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

    public class SallesFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            
            //Data

            //Forgein keys
            modelBuilder.Entity<Salle>()
                .HasOne(p => p.FromWarehouse)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.StorageItemId, p.FromUnitId});
        }
    }
}