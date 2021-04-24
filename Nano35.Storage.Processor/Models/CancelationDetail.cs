using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class CancelationDetail :
        ICastable
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
        public Guid CancellationsId { get; set; }
        public Guid StorageItemId { get; set; }
        public Guid FromUnitId { get; set; }
        public string FromPlace { get; set; }
        public WarehouseByItemOnStorage FromWarehouse { get; set; }
        public Cancellation Cancellation { get; set; }

        public class Configuration : IEntityTypeConfiguration<CancelationDetail>
        {
            public void Configure(EntityTypeBuilder<CancelationDetail> builder)
            {
                builder.ToTable("CancellationsDetails");
                builder.HasKey(u => new {u.Id});  
                builder.Property(b => b.Count)
                    .IsRequired();
                builder.Property(b => b.CancellationsId)
                    .IsRequired();
                builder.Property(b => b.StorageItemId)
                    .IsRequired();
                builder.Property(b => b.FromUnitId)
                    .IsRequired();
                builder.Property(b => b.FromPlace)
                    .IsRequired();
                builder.HasOne(p => p.FromWarehouse)
                    .WithMany(p => p.CancellationsDetails)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new {p.StorageItemId, p.FromUnitId, p.FromPlace});
                builder.HasOne(p => p.Cancellation)
                    .WithMany(p => p.Details)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new {p.CancellationsId});
            }
        }
    }
}