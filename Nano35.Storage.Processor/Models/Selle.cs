using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class Selle :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public Guid ClientId { get; set; }
        public Guid CashOperationId { get; set; }
        
        //Foreign keys
    }

    public class SelleFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Selle>()
                .HasKey(u => new {u.Id});  
            
            //Data
            modelBuilder.Entity<Selle>()
                .Property(b => b.Number)
                .IsRequired();
            modelBuilder.Entity<Selle>()
                .Property(b => b.CashOperationId)
                .IsRequired();
            modelBuilder.Entity<Selle>()
                .Property(b => b.Date)
                .IsRequired();
            
            //Foreign keys
        }
    }
}