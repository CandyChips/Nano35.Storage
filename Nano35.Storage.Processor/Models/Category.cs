using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Processor.Models
{
    public class Category :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        public Guid InstanceId { get; set; }
        
        //Data
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? ParentCategoryId { get; set; }
        
        //Foreign keys
        public Category? ParentCategory { get; set; }

        public override string ToString()
        {
            return ParentCategory != null ? $"{ParentCategory} {Name}" : Name;
        }
    }

    public class CategoriesFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Category>()
                .HasKey(u => new {u.Id, u.InstanceId});  
            
            //Data
            modelBuilder.Entity<Category>()
                .Property(b => b.IsDeleted)
                .IsRequired();
            
            modelBuilder.Entity<Category>()
                .Property(b => b.Name)
                .IsRequired();
            
            //Foreign keys
            modelBuilder.Entity<Category>()
                .HasOne(p => p.ParentCategory)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.ParentCategoryId, p.InstanceId});
        }
    }

    public class ArticlesCategoryAutoMapperProfile : Profile
    {
        public ArticlesCategoryAutoMapperProfile()
        {
            CreateMap<Category, ICategoryViewModel>()
                .ForMember(dest => dest.Id, source => source
                    .MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, source => source
                    .MapFrom(source => source.Name))
                .ForMember(dest => dest.ParentCategoryId, source => source
                    .MapFrom(source => source.ParentCategoryId));
        }
    }
}