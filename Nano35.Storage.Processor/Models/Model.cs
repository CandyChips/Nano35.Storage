using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class Model :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        
        //Forgein keys
    }

    public class ModelsFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Model>()
                .HasKey(u => new {u.Id});  
            
            //Data
            modelBuilder.Entity<Model>()
                .Property(b => b.Name)
                .IsRequired();
            modelBuilder.Entity<Model>()
                .Property(b => b.IsDeleted)
                .IsRequired();
            
            //Forgein keys
        }
    }
}