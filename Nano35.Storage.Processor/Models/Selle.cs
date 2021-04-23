using System;
using System.Collections.Generic;
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
        public Guid InstanceId { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public Guid CashOperationId { get; set; }
        
        //Foreign keys

        public ICollection<SelleDetail> Details { get; set; }

        public Selle()
        {
            Details = new List<SelleDetail>();
        }
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