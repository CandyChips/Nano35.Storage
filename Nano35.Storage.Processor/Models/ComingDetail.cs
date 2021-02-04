using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class ComingDetail :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public double Price { get; set; }
        public int Count { get; set; }
        public Guid ComingId { get; set; }
        public Guid StorageItemId { get; set; }
        public Guid ToUnitId { get; set; }
        
        //Forgein keys
        public WarehouseByItemOnStorage ToWarehouse { get; set; }
        public Coming Coming { get; set; }
    }

    public class ComingDetailFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<ComingDetail>()
                .HasKey(u => new {u.Id});  
            
            //Data
            modelBuilder.Entity<ComingDetail>()
                .Property(b => b.Price)
                .IsRequired();
            modelBuilder.Entity<ComingDetail>()
                .Property(b => b.Count)
                .IsRequired();
            
            //Forgein keys
            modelBuilder.Entity<ComingDetail>()
                .HasOne(p => p.ToWarehouse)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.StorageItemId, p.ToUnitId});
            
            modelBuilder.Entity<ComingDetail>()
                .HasOne(p => p.Coming)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.ComingId});
        }
    }
}