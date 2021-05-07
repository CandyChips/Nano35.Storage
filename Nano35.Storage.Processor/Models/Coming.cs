using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Processor.Models
{
    public class Coming 
    {
        public Guid Id { get; set; }
        public Guid InstanceId { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public Guid ClientId { get; set; }
        public ICollection<ComingDetail> Details { get; set; }
        
        public Coming()
        {
            Details = new List<ComingDetail>();
        }
        
        public class Configuration : IEntityTypeConfiguration<Coming>
        {
            public void Configure(EntityTypeBuilder<Coming> builder)
            {
                builder.ToTable("Comings");
                builder.HasKey(u => new {u.Id}); 
                builder.Property(b => b.Number)
                       .IsRequired();
                builder.Property(b => b.InstanceId)
                       .IsRequired();
                builder.Property(b => b.ClientId)
                       .IsRequired();
                builder.Property(b => b.Date)
                       .IsRequired();
            }
        }
    }
}