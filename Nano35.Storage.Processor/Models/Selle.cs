using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
        
        public class Configuration : IEntityTypeConfiguration<Selle>
        {
            public void Configure(EntityTypeBuilder<Selle> builder)
            {
                builder.ToTable("Sells");
                builder.HasKey(u => new {u.Id}); 
                builder.Property(b => b.Number)
                       .IsRequired();
                builder.Property(b => b.CashOperationId)
                       .IsRequired();
                builder.Property(b => b.Date)
                       .IsRequired();
            }
        }
    }
}