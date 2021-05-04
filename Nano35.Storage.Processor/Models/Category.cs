using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Processor.Models
{
    public class Category 
    {
        public Guid Id { get; set; }
        public Guid InstanceId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public ICollection<Article> Articles { get; set; }

        public Category() { Articles = new List<Article>(); }

        public override string ToString() => ParentCategory != null ? $"{ParentCategory} {Name}" : Name;
        
        public class Configuration : IEntityTypeConfiguration<Category>
        {
            public void Configure(EntityTypeBuilder<Category> builder)
            {
                builder.ToTable("Categories");
                builder.HasKey(u => new {u.Id, u.InstanceId}); 
                builder.Property(b => b.InstanceId)
                    .IsRequired();
                builder.Property(b => b.IsDeleted)
                    .IsRequired();
                builder.Property(b => b.Name)
                    .IsRequired();
                builder.HasOne(p => p.ParentCategory)
                    .WithMany()
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new {p.ParentCategoryId, p.InstanceId});
            }
        }
    }
}