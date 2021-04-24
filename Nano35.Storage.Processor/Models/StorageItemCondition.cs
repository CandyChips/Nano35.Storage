using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Processor.Models
{
    public class StorageItemCondition :
        ICastable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }


        public class Configuration : IEntityTypeConfiguration<StorageItemCondition>
        {
            public void Configure(EntityTypeBuilder<StorageItemCondition> builder)
            {
                builder.ToTable("StorageItemConditions");
                builder.HasKey(u => new {u.Id});
                builder.Property(b => b.IsDeleted)
                    .IsRequired();
                builder.Property(b => b.Name)
                    .IsRequired();
            }
        }
    }
}