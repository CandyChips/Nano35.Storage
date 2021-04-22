﻿using System;
using System.Collections.Generic;
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
        public Guid Id { get; init; }
        public Guid InstanceId { get; init; }
        
        // Data
        public bool IsDeleted { get; init; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Info { get; set; }
        public Guid CategoryId { get; set; }
        
        // Foreign keys
        public Category Category { get; set; }

        public override string ToString()
        {
            return $"{Category} {Brand} {Model}";
        }
        
        public ICollection<StorageItem> StorageItems { get; set; }

        public Article()
        {
            StorageItems = new List<StorageItem>();
        }
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
                .WithMany(p => p.Articles)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.CategoryId, p.InstanceId});
        }
    }

    public class ArticleAutoMapperProfile :
        Profile
    {
        public ArticleAutoMapperProfile()
        {
            CreateMap<Article, ArticleViewModel>()
                .ForMember(dest => dest.Id, source => source
                    .MapFrom(source => source.Id))
                .ForMember(dest => dest.Category, source => source
                    .MapFrom(source => source.Category.ToString()))
                .ForMember(dest => dest.Brand, source => source
                    .MapFrom(source => source.Brand))
                .ForMember(dest => dest.Model, source => source
                    .MapFrom(source => source.Model));
        }
    }
}