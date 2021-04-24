using System;
using System.Linq;
using System.Threading.Tasks;
using Nano35.Storage.Processor.Models;

namespace Nano35.Storage.Processor.Services
{
    public static class DataInitializer
    {
        public static async Task InitializeRolesAsync(
            ApplicationContext modelBuilder)
        {
            if(!modelBuilder.StorageItemConditions.Any())
            {

                await modelBuilder.StorageItemConditions.AddAsync(new StorageItemCondition()
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "Новый"
                });

                await modelBuilder.StorageItemConditions.AddAsync(new StorageItemCondition()
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "С разбора"
                });

                await modelBuilder.StorageItemConditions.AddAsync(new StorageItemCondition()
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "Витринный образец"
                });
                
                await modelBuilder.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}