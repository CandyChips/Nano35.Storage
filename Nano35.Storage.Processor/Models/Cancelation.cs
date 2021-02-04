using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class Cancelation :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public string Number { get; set; }
        public DateTime Date { get; set; }
        
        //Forgein keys
    }

    public class CancelationFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Cancelation>()
                .HasKey(u => new {u.Id});  
            
            //Data
            modelBuilder.Entity<Cancelation>()
                .Property(b => b.Number)
                .IsRequired();
            modelBuilder.Entity<Cancelation>()
                .Property(b => b.Date)
                .IsRequired();
            
            //Forgein keys
        }
    }
}