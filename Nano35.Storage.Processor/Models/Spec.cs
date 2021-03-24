using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Processor.Models
{
    public class Spec :
        ICastable
    {
        // Primary key
        
        //Data
        public string Key { get; set; }
        public string Value { get; set; }
        public Guid ArticleId { get; set; }
        public Guid InstanceId { get; set; }
        
        //Foreign keys
        public Article Article { get; set; }
    }

    public class ArticleSpecFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Spec>()
                .HasKey(u => new {u.ArticleId, u.Key});  
            
            //Data
            modelBuilder.Entity<Spec>()
                .Property(b => b.Value)
                .IsRequired();
            
            //Foreign keys
            modelBuilder.Entity<Spec>()
                .HasOne(p => p.Article)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.ArticleId, p.InstanceId});
        }
    }

    public class ArticleSpecAutoMapperProfile : Profile
    {
        public ArticleSpecAutoMapperProfile()
        {
            CreateMap<Spec, SpecViewModel>()
                .ForMember(dest => dest.Key, source => source
                    .MapFrom(source => source.Key))
                .ForMember(dest => dest.Value, source => source
                    .MapFrom(source => source.Value));
        }
    }
}