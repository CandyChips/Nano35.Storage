using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Processor.Models
{
    public class StorageItem :
        ICastable
    {
        public Guid Id { get; set; }
        public Guid InstanceId { get; set; }
        public string Comment { get; set; }
        public string HiddenComment { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public bool IsDeleted { get; set; }
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
        public Guid ConditionId { get; set; }
        public StorageItemCondition Condition { get; set; }
        
        public override string ToString()
        {
            return $"{Article}, {Condition.Name}";
        }
        
        public class Configuration : IEntityTypeConfiguration<StorageItem>
        {
            public void Configure(EntityTypeBuilder<StorageItem> builder)
            {
                builder.ToTable("StorageItems");
                builder.HasKey(u => new {u.Id, u.InstanceId});  
                builder.Property(b => b.IsDeleted)
                    .IsRequired();
                builder.Property(b => b.Comment)    
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
                builder.Property(b => b.HiddenComment)    
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
                builder.Property(b => b.RetailPrice)    
                    .HasColumnType("money")
                    .IsRequired();
                builder.Property(b => b.PurchasePrice)    
                    .HasColumnType("money")
                    .IsRequired();
                builder.HasOne(p => p.Article)
                    .WithMany(p => p.StorageItems)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new { p.ArticleId, p.InstanceId });
                builder.HasOne(p => p.Condition)
                    .WithMany()
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new {p.ConditionId});
            }
        }
    }
}