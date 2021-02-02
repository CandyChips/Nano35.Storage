using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Processor.Models
{
    public class StorageItemCondition :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        //Forgein keys
    }

    public class StorageItemConditionsFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<StorageItemCondition>()
                .HasKey(u => u.Id);  
            
            //Data
            modelBuilder.Entity<StorageItemCondition>()
                .Property(b => b.Name)
                .IsRequired();
            modelBuilder.Entity<StorageItemCondition>()
                .Property(b => b.IsDeleted)
                .IsRequired();
            
            //Forgein keys
        }
    }

    public class StorageItemConditionAutoMapperProfile : Profile
    {
        public StorageItemConditionAutoMapperProfile()
        {
            CreateMap<StorageItemCondition, IStorageItemConditionViewModel>()
                .ForMember(dest => dest.Id, source => source
                    .MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, source => source
                    .MapFrom(source => source.Name));
        }
    }
}