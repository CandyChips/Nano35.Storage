﻿using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class ComingDetail :
        ICastable
    {
        // Primary key
        public Guid ComingId { get; set; }
        public Guid StorageItemId { get; set; }
        
        // Data
        public double Price { get; set; }
        public string ToPlace { get; set; }
        public int Count { get; set; }
        public Guid ToUnitId { get; set; }
        
        // Foreign keys
        public WarehouseByItemOnStorage ToWarehouse { get; set; }
        public Coming Coming { get; set; }
    }

    public class ComingDetailFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            // Primary key
            modelBuilder.Entity<ComingDetail>()
                .HasKey(u => new {u.ComingId, u.StorageItemId});  
            
            // Data
            modelBuilder.Entity<ComingDetail>()
                .Property(b => b.Price)
                .IsRequired();
            modelBuilder.Entity<ComingDetail>()
                .Property(b => b.Count)
                .IsRequired();
            modelBuilder.Entity<ComingDetail>()
                .Property(b => b.ToPlace)
                .IsRequired();
            modelBuilder.Entity<ComingDetail>()
                .Property(b => b.ToUnitId)
                .IsRequired();
            
            // Foreign keys
            modelBuilder.Entity<ComingDetail>()
                .HasOne(p => p.ToWarehouse)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.StorageItemId, p.ToUnitId, p.ToPlace});
            
            modelBuilder.Entity<ComingDetail>()
                .HasOne(p => p.Coming)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.ComingId});
        }
    }
}