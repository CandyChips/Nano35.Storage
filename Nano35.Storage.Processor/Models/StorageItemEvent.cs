using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class StorageItemEvent :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public string Number { get; set; }
        public Guid Creator { get; set; }
        
        //Forgein keys
    }

    public class StorageItemEventsFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<StorageItemEvent>()
                .HasKey(u => new {u.Id});  
            
            //Data
            modelBuilder.Entity<StorageItemEvent>()
                .Property(b => b.CreationDate)
                .HasColumnType("datetime")
                .IsRequired();
            
            modelBuilder.Entity<StorageItemEvent>()
                .Property(b => b.IsDeleted)
                .IsRequired();
            
            modelBuilder.Entity<StorageItemEvent>()
                .Property(b => b.Number)
                .IsRequired();
            
            modelBuilder.Entity<StorageItemEvent>()
                .Property(b => b.Creator)
                .IsRequired();

            //Forgein keys
        }
    }
}