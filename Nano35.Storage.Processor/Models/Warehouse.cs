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
    public class WarehouseByItemOnStorage 
        
    {
        public Guid UnitId { get; set; }
        public Guid InstanceId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public bool IsDeleted { get; set; }
        public Guid StorageItemId { get; set; }
        public StorageItem StorageItem { get; set; }
        public ICollection<ComingDetail> ComingDetails { get; set; }
        public ICollection<SelleDetail> SelleDetails { get; set; }
        public ICollection<CancelationDetail> CancellationsDetails { get; set; }
        public ICollection<MoveDetail> MoveFromDetails { get; set; }
        public ICollection<MoveDetail> MoveToDetails { get; set; }

        public WarehouseByItemOnStorage()
        {
            ComingDetails = new List<ComingDetail>();
            SelleDetails = new List<SelleDetail>();
            CancellationsDetails = new List<CancelationDetail>();
            MoveFromDetails = new List<MoveDetail>();
            MoveToDetails = new List<MoveDetail>();
        }

        public override string ToString()
        {
            return $@"{Name}";
        }
        
        public class Configuration : IEntityTypeConfiguration<WarehouseByItemOnStorage>
        {
            public void Configure(EntityTypeBuilder<WarehouseByItemOnStorage> builder)
            {
                builder.ToTable("Warehouses");
                builder.HasKey(u => new {u.StorageItemId, u.UnitId, u.Name});  
                builder.Property(b => b.Count)
                    .IsRequired();
                builder.Property(b => b.IsDeleted)
                    .IsRequired();
                builder.HasOne(p => p.StorageItem)
                    .WithMany()
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new {p.StorageItemId, p.InstanceId});
            }
        }
    }
}