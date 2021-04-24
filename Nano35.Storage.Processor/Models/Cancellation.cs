using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class Cancellation :
        ICastable
    {
        public Guid Id { get; set; }
        public Guid InstanceId { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public ICollection<CancelationDetail> Details { get; set; }

        public Cancellation() { Details = new List<CancelationDetail>(); }
        
        public class Configuration : IEntityTypeConfiguration<Cancellation>
        {
            public void Configure(EntityTypeBuilder<Cancellation> builder)
            {
                builder.ToTable("Cancellations");
                builder.HasKey(u => new {u.Id}); 
                builder.Property(b => b.InstanceId)
                    .IsRequired();
                builder.Property(b => b.Number)
                    .IsRequired();
                builder.Property(b => b.Date)
                    .IsRequired();
                builder.Property(b => b.Comment)
                    .IsRequired();
            }
        }
    }
}