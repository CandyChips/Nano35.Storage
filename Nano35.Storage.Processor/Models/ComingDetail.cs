using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Processor.Models
{
       public class ComingDetail :
              ICastable
       {
              public Guid ComingId { get; set; }
              public Guid StorageItemId { get; set; }
              public double Price { get; set; }
              public string ToPlace { get; set; }
              public int Count { get; set; }
              public Guid ToUnitId { get; set; }
              public WarehouseByItemOnStorage ToWarehouse { get; set; }
              public Coming Coming { get; set; }


              public class Configuration : IEntityTypeConfiguration<ComingDetail>
              {
                     public void Configure(EntityTypeBuilder<ComingDetail> builder)
                     {
                            builder.ToTable("ComingDetails");
                            builder.HasKey(u => new {u.ComingId, u.StorageItemId});
                            builder.Property(b => b.Price)
                                   .IsRequired();
                            builder.Property(b => b.Count)
                                   .IsRequired();
                            builder.Property(b => b.ToPlace)
                                   .IsRequired();
                            builder.Property(b => b.ToUnitId)
                                   .IsRequired();
                            builder.HasOne(p => p.ToWarehouse)
                                   .WithMany(p => p.ComingDetails)
                                   .OnDelete(DeleteBehavior.NoAction)
                                   .HasForeignKey(p => new {p.StorageItemId, p.ToUnitId, p.ToPlace});
                            builder.HasOne(p => p.Coming)
                                   .WithMany(p => p.Details)
                                   .OnDelete(DeleteBehavior.NoAction)
                                   .HasForeignKey(p => new {p.ComingId});
                     }
              }
       }
}