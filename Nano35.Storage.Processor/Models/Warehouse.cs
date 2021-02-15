using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class WarehouseByItemOnStorage :
        ICastable
    {
        // Primary key
        public Guid UnitId { get; set; }
        public Guid InstanceId { get; set; }
        public string Name { get; set; }
        
        //Data
        public int Count { get; set; }
        public bool IsDeleted { get; set; }

        //Foreign keys
        public Guid StorageItemId { get; set; }
        public StorageItem StorageItem { get; set; }
    }

    public class WarehousesFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<WarehouseByItemOnStorage>()
                .HasKey(u => new {u.StorageItemId, u.UnitId, u.Name});  
            
            //Data
            modelBuilder.Entity<WarehouseByItemOnStorage>()
                .Property(b => b.Count)
                .IsRequired();
            
            modelBuilder.Entity<WarehouseByItemOnStorage>()
                .Property(b => b.IsDeleted)
                .IsRequired();

            //Foreign keys
            modelBuilder.Entity<WarehouseByItemOnStorage>()
                .HasOne(p => p.StorageItem)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.StorageItemId, p.InstanceId});
        }
    }
}