using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class StorageItemEventDetail :
        ICastable
    {
        //Data
        public int Count { get; set; }
        public Guid InstanceId { get; set; }
        public bool IsDeleted { get; set; }
        
        //Forgein keys
        public Guid StorageItemEventId { get; set; }
        public StorageItemEvent StorageItemEvent { get; set; }
        
        public Guid StorageItemId { get; set; }
        public StorageItem StorageItem { get; set; }
    }

    public class StorageItemEventDetailsFluentContext
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<StorageItemEventDetail>()
                .HasKey(u => new {u.StorageItemEventId, u.StorageItemId});  
            
            //Data
            modelBuilder.Entity<StorageItemEventDetail>()
                .Property(b => b.Count)
                .IsRequired();
            
            modelBuilder.Entity<StorageItemEventDetail>()
                .Property(b => b.IsDeleted)
                .IsRequired();

            //Forgein keys
            modelBuilder.Entity<StorageItemEventDetail>()
                .HasOne(p => p.StorageItem)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.StorageItemId, p.InstanceId});
            
            modelBuilder.Entity<StorageItemEventDetail>()
                .HasOne(p => p.StorageItemEvent)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => p.StorageItemEventId);
        }
    }
}