using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class Category :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        //Forgein keys
    }

    public class CategorysFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Category>()
                .HasKey(u => u.Id);  
            
            //Data
            modelBuilder.Entity<Category>()
                .Property(b => b.Name)
                .IsRequired();
            modelBuilder.Entity<Category>()
                .Property(b => b.IsDeleted)
                .IsRequired();
            
            //Forgein keys
        }
    }
}