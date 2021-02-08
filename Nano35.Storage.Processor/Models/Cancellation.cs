using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class Cancellation :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public string Number { get; set; }
        public DateTime Date { get; set; }
        
        //Foreign keys
    }

    public class CancellationsFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Cancellation>()
                .HasKey(u => new {u.Id});  
            
            //Data
            modelBuilder.Entity<Cancellation>()
                .Property(b => b.Number)
                .IsRequired();
            modelBuilder.Entity<Cancellation>()
                .Property(b => b.Date)
                .IsRequired();
            
            //Foreign keys
        }
    }
}