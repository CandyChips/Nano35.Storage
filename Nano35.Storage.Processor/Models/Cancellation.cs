using System;
using System.Collections.Generic;
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
        public Guid InstanceId { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        
        //Foreign keys
        
        public ICollection<CancelationDetail> Details { get; set; }

        public Cancellation()
        {
            Details = new List<CancelationDetail>();
        }
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