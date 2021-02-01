using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class Warehouse :
        ICastable
    {
        public Guid InstanceId { get; set; }
        //Data
        public int Count { get; set; }
        public bool IsDeleted { get; set; }

        //Forgein keys
        public Guid StorageItemId { get; set; }
        public StorageItem StorageItem { get; set; }
        
        public Guid UnitId { get; set; }
        public PlaceOnStorage PlaceOnStorage { get; set; }
    }

    public class WarehousesFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Warehouse>()
                .HasKey(u => new {u.StorageItemId, u.UnitId});  
            
            //Data
            modelBuilder.Entity<Warehouse>()
                .Property(b => b.Count)
                .IsRequired();
            
            modelBuilder.Entity<Warehouse>()
                .Property(b => b.IsDeleted)
                .IsRequired();

            //Forgein keys
            modelBuilder.Entity<Warehouse>()
                .HasOne(p => p.StorageItem)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.StorageItemId, p.InstanceId});
            
            modelBuilder.Entity<Warehouse>()
                .HasOne(p => p.PlaceOnStorage)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.UnitId});
        }
    }
}