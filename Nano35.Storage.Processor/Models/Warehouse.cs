using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Processor.Models
{
    public class WarehouseByItemOnStorage :
        ICastable
    {
        // Primary key
        public Guid UnitId { get; set; }
        public Guid InstanceId { get; set; }
        public string Name { get; set; }
        
        //Data
        public int Count { get; set; }
        public bool IsDeleted { get; set; }

        //Foreign keys
        public Guid StorageItemId { get; set; }
        public StorageItem StorageItem { get; set; }
        
        
        public ICollection<ComingDetail> ComingDetails { get; set; }
        public ICollection<SelleDetail> SelleDetails { get; set; }
        public ICollection<CancelationDetail> CancelationDetails { get; set; }
        public ICollection<MoveDetail> MoveFromDetails { get; set; }
        public ICollection<MoveDetail> MoveToDetails { get; set; }

        public WarehouseByItemOnStorage()
        {
            ComingDetails = new List<ComingDetail>();
            SelleDetails = new List<SelleDetail>();
            CancelationDetails = new List<CancelationDetail>();
            MoveFromDetails = new List<MoveDetail>();
            MoveToDetails = new List<MoveDetail>();
        }
        
    }

    public class WarehousesFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<WarehouseByItemOnStorage>()
                .HasKey(u => new {u.StorageItemId, u.UnitId, u.Name});  
            
            //Data
            modelBuilder.Entity<WarehouseByItemOnStorage>()
                .Property(b => b.Count)
                .IsRequired();
            
            modelBuilder.Entity<WarehouseByItemOnStorage>()
                .Property(b => b.IsDeleted)
                .IsRequired();

            //Foreign keys
            modelBuilder.Entity<WarehouseByItemOnStorage>()
                .HasOne(p => p.StorageItem)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.StorageItemId, p.InstanceId});
        }
    }

    public class WarehouseByItemOnStorageAutoMapperProfile : Profile
    {
        public WarehouseByItemOnStorageAutoMapperProfile()
        {
            CreateMap<WarehouseByItemOnStorage, PlaceOnStorage>()
                .ForMember(dest => dest.Name, source => source
                    .MapFrom(source => source.Name))
                .ForMember(dest => dest.Count, source => source
                    .MapFrom(source => source.Count))
                .ForMember(dest => dest.UnitId, source => source
                    .MapFrom(source => source.UnitId))
                .ForMember(dest => dest.StorageItemId, source => source
                    .MapFrom(source => source.StorageItemId));
        }
    }
}