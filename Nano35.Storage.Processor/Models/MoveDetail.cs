using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class MoveDetail 
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public int Count { get; set; }
        public Guid StorageItemId { get; set; }
        public Guid MoveId { get; set; }
        public Guid ToUnitId { get; set; }
        public string ToPlace { get; set; }
        public Guid FromUnitId { get; set; }
        public string FromPlace { get; set; }
        
        //Foreign keys
        public WarehouseByItemOnStorage ToWarehouse { get; set; }
        public WarehouseByItemOnStorage FromWarehouse { get; set; }
        public Move Move { get; set; }

        public class Configuration : IEntityTypeConfiguration<MoveDetail>
        {
            public void Configure(EntityTypeBuilder<MoveDetail> builder)
            {
                builder.ToTable("MoveDetails");
                builder.HasKey(u => new {u.Id});  
                builder.Property(b => b.Count)
                    .IsRequired();
                builder.Property(b => b.ToPlace)
                    .IsRequired();
                builder.Property(b => b.FromPlace)
                    .IsRequired();
                builder.Property(b => b.ToUnitId)
                    .IsRequired();
                builder.HasOne(p => p.ToWarehouse)
                    .WithMany(p => p.MoveToDetails)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new {p.StorageItemId, p.ToUnitId, p.ToPlace});
                builder.HasOne(p => p.FromWarehouse)
                    .WithMany(p => p.MoveFromDetails)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new {p.StorageItemId, p.FromUnitId, p.FromPlace});
                builder.HasOne(p => p.Move)
                    .WithMany()
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new {p.MoveId});
            }
        }
    }
}