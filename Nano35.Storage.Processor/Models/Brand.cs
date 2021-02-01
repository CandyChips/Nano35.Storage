using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class Brand :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        //Forgein keys
    }

    public class BrandsFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Brand>()
                .HasKey(u => u.Id);  
            
            //Data
            modelBuilder.Entity<Brand>()
                .Property(b => b.Name)
                .IsRequired();
            modelBuilder.Entity<Brand>()
                .Property(b => b.IsDeleted)
                .IsRequired();
            
            //Forgein keys
        }
    }
}