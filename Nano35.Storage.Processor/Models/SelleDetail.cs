using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class SelleDetail 
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public Guid SelleId { get; set; }
        public Guid StorageItemId { get; set; }
        public Guid FromUnitId { get; set; }
        public string FromPlace { get; set; }
        public WarehouseByItemOnStorage FromWarehouse { get; set; }
        public Selle Selle { get; set; }

        public class Configuration : IEntityTypeConfiguration<SelleDetail>
        {
            public void Configure(EntityTypeBuilder<SelleDetail> builder)
            {
                builder.ToTable("SelleDetails");
                builder.HasKey(u => new {u.Id});  
                builder.Property(b => b.Count)
                    .IsRequired();
                builder.Property(b => b.Price)
                    .IsRequired();
                builder.Property(b => b.FromPlace)
                    .IsRequired();
                builder.HasOne(p => p.FromWarehouse)
                    .WithMany(p => p.SelleDetails)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new {p.StorageItemId, p.FromUnitId, p.FromPlace});
            
                builder.HasOne(p => p.Selle)
                    .WithMany(p => p.Details)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new {SalleId = p.SelleId});
            }
        }
    }
}