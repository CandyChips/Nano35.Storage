using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Models
{
    public class Coming :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        // Data
        public Guid InstanceId { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public Guid ClientId { get; set; }
        public Guid CashOperationId { get; set; }
        
        // Foreign keys
    }

    public class ComingFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Coming>()
                .HasKey(u => new {u.Id});  
            
            //Data
            modelBuilder.Entity<Coming>()
                .Property(b => b.Number)
                .IsRequired();
            modelBuilder.Entity<Coming>()
                .Property(b => b.InstanceId)
                .IsRequired();
            modelBuilder.Entity<Coming>()
                .Property(b => b.CashOperationId)
                .IsRequired();
            modelBuilder.Entity<Coming>()
                .Property(b => b.ClientId)
                .IsRequired();
            modelBuilder.Entity<Coming>()
                .Property(b => b.Date)
                .IsRequired();
            
            //Foreign keys
        }
    }

    public class ComingAutoMapperProfile : Profile
    {
        public ComingAutoMapperProfile()
        {
            CreateMap<Coming, IComingViewModel>()
                .ForMember(dest => dest.Id, source => source
                    .MapFrom(source => source.Id))
                .ForMember(dest => dest.Number, source => source
                    .MapFrom(source => source.Number));
        }
    }
}