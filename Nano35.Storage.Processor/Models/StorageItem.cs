using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Processor.Models
{
    public class StorageItem :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        public Guid InstanceId { get; set; }
        
        //Data
        public string Comment { get; set; }
        public string HiddenComment { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public bool IsDeleted { get; set; }
        
        //Foreign keys
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
        
        public Guid ConditionId { get; set; }
        public StorageItemCondition Condition { get; set; }

        public override string ToString()
        {
            return $"{Article}, {Condition.Name}";
        }
    }

    public class StorageItemsFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<StorageItem>()
                .HasKey(u => new {u.Id, u.InstanceId});  
            
            //Data
            modelBuilder.Entity<StorageItem>()
                .Property(b => b.IsDeleted)
                .IsRequired();
            
            modelBuilder.Entity<StorageItem>()
                .Property(b => b.Comment)    
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
            
            modelBuilder.Entity<StorageItem>()
                .Property(b => b.HiddenComment)    
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
            
            modelBuilder.Entity<StorageItem>()
                .Property(b => b.RetailPrice)    
                .HasColumnType("money")
                .IsRequired();
            
            modelBuilder.Entity<StorageItem>()
                .Property(b => b.PurchasePrice)    
                .HasColumnType("money")
                .IsRequired();
            
            //Foreign keys
            modelBuilder.Entity<StorageItem>()
                .HasOne(p => p.Article)
                .WithMany(p => p.StorageItems)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new { p.ArticleId, p.InstanceId });
            
            modelBuilder.Entity<StorageItem>()
                .HasOne(p => p.Condition)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.ConditionId});
        }
    }

    public class StorageItemAutoMapperProfile : Profile
    {
        public StorageItemAutoMapperProfile()
        {
            CreateMap<StorageItem, IStorageItemViewModel>()
                .ForMember(dest => dest.Id, source => source
                    .MapFrom(source => source.Id))
                .ForMember(dest => dest.Condition, source => source
                    .MapFrom(source => source.Condition.Name))
                .ForMember(dest => dest.Comment, source => source
                    .MapFrom(source => source.Comment))
                .ForMember(dest => dest.HiddenComment, source => source
                    .MapFrom(source => source.HiddenComment))
                .ForMember(dest => dest.RetailPrice, source => source
                    .MapFrom(source => source.RetailPrice))
                .ForMember(dest => dest.PurchasePrice, source => source
                    .MapFrom(source => source.PurchasePrice))
                .ForMember(dest => dest.Article, source => source
                .MapFrom(source => source.Article.ToString()));
        }
    }
}