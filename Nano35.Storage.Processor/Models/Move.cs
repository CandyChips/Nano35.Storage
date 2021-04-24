using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class Move :
        ICastable
    {
        public Guid Id { get; set; }
        public Guid InstanceId { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public ICollection<MoveDetail> Details { get; set; }
        public Move() { Details = new List<MoveDetail>(); }
        
        public class Configuration : IEntityTypeConfiguration<Move>
        {
            public void Configure(EntityTypeBuilder<Move> builder)
            {
                builder.ToTable("Moves");
                builder.HasKey(u => new {u.Id}); 
                builder.Property(b => b.Number)
                    .IsRequired();
                builder.Property(b => b.Date)
                    .IsRequired();
            }
        }
    }
}