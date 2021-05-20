using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Processor.Models
{
    public class Spec 
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public Guid ArticleId { get; set; }
        public Guid InstanceId { get; set; }
        public Article Article { get; set; }

        public class Configuration : IEntityTypeConfiguration<Spec>
        {
            public void Configure(EntityTypeBuilder<Spec> builder)
            {
                builder.ToTable("Specs");
                builder.HasKey(u => new {u.ArticleId, u.Key});  
                builder.Property(b => b.Value)
                    .IsRequired();
                builder.HasOne(p => p.Article)
                    .WithMany(p => p.Specs)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new {p.ArticleId, p.InstanceId});
            }
        }
    }
}