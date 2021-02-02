using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Processor.Models
{
    public class ArticleType :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        
        //Forgein keys
    }

    public class ArticleTypesFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<ArticleType>()
                .HasKey(u => new {u.Id});  
            
            //Data
            modelBuilder.Entity<ArticleType>()
                .Property(b => b.Name)
                .IsRequired();
            modelBuilder.Entity<ArticleType>()
                .Property(b => b.IsDeleted)
                .IsRequired();
            
            //Forgein keys
        }
    }

    public class ArticleTypesAutoMapperProfile : Profile
    {
        public ArticleTypesAutoMapperProfile()
        {
            CreateMap<ArticleType, IArticleTypeViewModel>()
                .ForMember(dest => dest.Id, source => source
                    .MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, source => source
                    .MapFrom(source => source.Name));
        }
    }
}