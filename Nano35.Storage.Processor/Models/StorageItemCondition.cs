using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class StorageItemCondition :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        //Forgein keys
    }

    public class StorageItemConditionsFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<StorageItemCondition>()
                .HasKey(u => u.Id);  
            
            //Data
            modelBuilder.Entity<StorageItemCondition>()
                .Property(b => b.Name)
                .IsRequired();
            modelBuilder.Entity<StorageItemCondition>()
                .Property(b => b.IsDeleted)
                .IsRequired();
            
            //Forgein keys
        }
    }
}