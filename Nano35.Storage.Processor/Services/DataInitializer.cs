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

                modelBuilder.StorageItemConditions.Add(new StorageItemCondition()
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "Новый"
                });

                modelBuilder.StorageItemConditions.Add(new StorageItemCondition()
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "С разбора"
                });

                modelBuilder.StorageItemConditions.Add(new StorageItemCondition()
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "Витринный образец"
                });
                
                await modelBuilder
                    .SaveChangesAsync()
                    .ConfigureAwait(false);
            }
        }
    }
}