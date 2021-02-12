using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Processor.Models
{
    public class Article :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        public Guid InstanceId { get; set; }
        
        // Data
        public bool IsDeleted { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Info { get; set; }
        public Guid CategoryId { get; set; }
        
        // Foreign keys
        public Category Category { get; set; }
    }

    public class ArticlesFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            // Primary key
            modelBuilder.Entity<Article>()
                .HasKey(u => new {u.Id, u.InstanceId});  
            
            // Data
            modelBuilder.Entity<Article>()
                .Property(b => b.IsDeleted)
                .IsRequired();
            modelBuilder.Entity<Article>()
                .Property(b => b.Model)
                .IsRequired();
            modelBuilder.Entity<Article>()
                .Property(b => b.Brand)
                .IsRequired();
            modelBuilder.Entity<Article>()
                .Property(b => b.Info)
                .IsRequired();
            
            // Foreign keys
            modelBuilder.Entity<Article>()
                .HasOne(p => p.Category)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.CategoryId, p.InstanceId});
        }
    }

    public class ArticleAutoMapperProfile : Profile
    {
        public ArticleAutoMapperProfile()
        {
            CreateMap<Article, IArticleViewModel>()
                .ForMember(dest => dest.Id, source => source
                    .MapFrom(source => source.Id))
                .ForMember(dest => dest.Category, source => source
                    .MapFrom(source => source.Category.Name))
                .ForMember(dest => dest.Brand, source => source
                    .MapFrom(source => source.Brand))
                .ForMember(dest => dest.Model, source => source
                    .MapFrom(source => source.Model));
        }
    }
}