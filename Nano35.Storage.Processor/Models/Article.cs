using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        
        //Data
        public bool IsDeleted { get; set; }
        
        //Forgein keys
        public Guid ArticleTypeId { get; set; }
        public ArticleType ArticleType { get; set; }
        
        public Guid CategoryGroupId { get; set; }
        public CategoryGroup CategoryGroup { get; set; }
        
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
        
        public Guid ModelId { get; set; }
        public Model Model { get; set; }
    }

    public class ArticlesFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Article>()
                .HasKey(u => new {u.Id, u.InstanceId});  
            
            //Data
            modelBuilder.Entity<Article>()
                .Property(b => b.IsDeleted)
                .IsRequired();
            
            //Forgein keys
            modelBuilder.Entity<Article>()
                .HasOne(p => p.ArticleType)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.ArticleTypeId});
            
            modelBuilder.Entity<Article>()
                .HasOne(p => p.CategoryGroup)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.CategoryGroupId});
            
            modelBuilder.Entity<Article>()
                .HasOne(p => p.Category)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.CategoryId});
            
            modelBuilder.Entity<Article>()
                .HasOne(p => p.Brand)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.BrandId} );
            
            modelBuilder.Entity<Article>()
                .HasOne(p => p.Model)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.ModelId});
        }
    }

    public class ArticlesAutoMapperProfile : Profile
    {
        public ArticlesAutoMapperProfile()
        {
            CreateMap<Article, IArticleViewModel>()
                .ForMember(dest => dest.Id, source => source
                    .MapFrom(source => source.Id))
                .ForMember(dest => dest.Brand, source => source
                    .MapFrom(source => source.Brand.Name))
                .ForMember(dest => dest.Model, source => source
                    .MapFrom(source => source.Model.Name))
                .ForMember(dest => dest.Category, source => source
                    .MapFrom(source => source.Category.Name))
                .ForMember(dest => dest.CategoryGroup, source => source
                    .MapFrom(source => source.CategoryGroup.Name));
        }
    }

    public class ArticleAutoMapperProfile : Profile
    {
        public ArticleAutoMapperProfile()
        {
            CreateMap<Article, IArticleViewModel>()
                .ForMember(dest => dest.Id, source => source
                    .MapFrom(source => source.Id))
                .ForMember(dest => dest.ArticleType, source => source
                    .MapFrom(source => source.ArticleType.Name))
                .ForMember(dest => dest.CategoryGroup, source => source
                    .MapFrom(source => source.CategoryGroup.Name))
                .ForMember(dest => dest.Category, source => source
                    .MapFrom(source => source.Category.Name))
                .ForMember(dest => dest.Brand, source => source
                    .MapFrom(source => source.Brand.Name))
                .ForMember(dest => dest.Model, source => source
                    .MapFrom(source => source.Model.Name));
        }
    }
}