using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class PlaceOnStorage :
        ICastable
    {
        // Primary key
        public Guid UnitId { get; set; }
        
        //Data
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class PlaceOnStoragesFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<PlaceOnStorage>()
                .HasKey(u => new {u.UnitId});  
            
            //Data
            modelBuilder.Entity<PlaceOnStorage>()
                .Property(b => b.Name)
                .IsRequired();
            
            modelBuilder.Entity<PlaceOnStorage>()
                .Property(b => b.IsDeleted)
                .IsRequired();

            //Forgein keys
        }
    }

}