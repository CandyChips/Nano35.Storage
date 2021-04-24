using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Processor.Models
{
    public class Article :
        ICastable
    {
        public Guid Id { get; init; }
        public Guid InstanceId { get; init; }
        public bool IsDeleted { get; init; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Info { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public override string ToString()
        {
            return $"{Category} {Brand} {Model}";
        }

        public ICollection<StorageItem> StorageItems { get; set; }

        public Article()
        {
            StorageItems = new List<StorageItem>();
        }


        public class Configuration : IEntityTypeConfiguration<Article>
        {
            public void Configure(EntityTypeBuilder<Article> builder)
            {
                builder.ToTable("Articles");
                builder.HasKey(u => new {u.Id, u.InstanceId});
                builder.Property(b => b.IsDeleted)
                    .IsRequired();
                builder.Property(b => b.Model)
                    .IsRequired();
                builder.Property(b => b.Brand)
                    .IsRequired();
                builder.Property(b => b.Info)
                    .IsRequired();
                builder.HasOne(p => p.Category)
                    .WithMany(p => p.Articles)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new {p.CategoryId, p.InstanceId});
            }
        }
    }
}