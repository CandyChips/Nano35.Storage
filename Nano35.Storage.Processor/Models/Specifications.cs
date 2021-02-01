using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class Specification :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        public Guid InstanceId { get; set; }
        
        //Data
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }
        
        //Forgein keys
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
    }

    public class SpecificationsFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Specification>()
                .HasKey(u => new {u.Id, u.InstanceId});  
            
            //Data
            modelBuilder.Entity<Specification>()
                .Property(b => b.Key)
                .IsRequired();
            modelBuilder.Entity<Specification>()
                .Property(b => b.Value)
                .IsRequired();
            modelBuilder.Entity<Specification>()
                .Property(b => b.IsDeleted)
                .IsRequired();
            
            //Forgein keys
            modelBuilder.Entity<Specification>()
                .HasOne(p => p.Article)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.ArticleId, p.InstanceId});
        }
    }
}