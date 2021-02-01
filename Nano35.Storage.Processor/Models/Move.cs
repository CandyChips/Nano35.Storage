using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class Move :
        StorageItemEvent,
        ICastable
    {
        // Primary key
        
        //Data
        
        //Forgein keys
        public Guid ToUnitId { get; set; }
        public Guid FromUnitId { get; set; }
        public Guid StorageItemId { get; set; }
        public Warehouse FromWarehouse { get; set; }
        public Warehouse ToWarehouse { get; set; }
    }

    public class MovesFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            
            //Data

            //Forgein keys
            modelBuilder.Entity<Move>()
                .HasOne(p => p.FromWarehouse)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.StorageItemId, p.FromUnitId});
            
            modelBuilder.Entity<Move>()
                .HasOne(p => p.ToWarehouse)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.StorageItemId, p.ToUnitId});
        }
    }
}