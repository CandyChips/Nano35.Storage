using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class MoveDetail :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public int Count { get; set; }
        public Guid StorageItemId { get; set; }
        public Guid MoveId { get; set; }
        public Guid ToUnitId { get; set; }
        public string ToPlace { get; set; }
        public Guid FromUnitId { get; set; }
        public string FromPlace { get; set; }
        
        //Foreign keys
        public WarehouseByItemOnStorage ToWarehouse { get; set; }
        public WarehouseByItemOnStorage FromWarehouse { get; set; }
        public Move Move { get; set; }
    }

    public class MoveDetailFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<MoveDetail>()
                .HasKey(u => new {u.Id});  
            
            //Data
            modelBuilder.Entity<MoveDetail>()
                .Property(b => b.Count)
                .IsRequired();

            modelBuilder.Entity<MoveDetail>()
                .Property(b => b.ToPlace)
                .IsRequired();

            modelBuilder.Entity<MoveDetail>()
                .Property(b => b.FromPlace)
                .IsRequired();

            //Foreign keys
            modelBuilder.Entity<MoveDetail>()
                .HasOne(p => p.ToWarehouse)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.StorageItemId, p.ToUnitId, p.ToPlace});

            modelBuilder.Entity<MoveDetail>()
                .HasOne(p => p.FromWarehouse)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.StorageItemId, p.FromUnitId, p.FromPlace});

            modelBuilder.Entity<MoveDetail>()
                .HasOne(p => p.Move)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.MoveId});
        }
    }
}