using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class Salle :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public string Number { get; set; }
        public DateTime Date { get; set; }
        
        //Forgein keys
    }

    public class SalleFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Salle>()
                .HasKey(u => new {u.Id});  
            
            //Data
            modelBuilder.Entity<Salle>()
                .Property(b => b.Number)
                .IsRequired();
            modelBuilder.Entity<Salle>()
                .Property(b => b.Date)
                .IsRequired();
            
            //Forgein keys
        }
    }
}