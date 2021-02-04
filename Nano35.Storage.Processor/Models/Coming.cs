using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class Coming :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public string Number { get; set; }
        public DateTime Date { get; set; }
        
        //Forgein keys
    }

    public class ComingFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Coming>()
                .HasKey(u => new {u.Id});  
            
            //Data
            modelBuilder.Entity<Coming>()
                .Property(b => b.Number)
                .IsRequired();
            modelBuilder.Entity<Coming>()
                .Property(b => b.Date)
                .IsRequired();
            
            //Forgein keys
        }
    }
}