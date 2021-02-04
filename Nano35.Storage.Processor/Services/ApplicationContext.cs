using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Storage.Processor.Models;

namespace Nano35.Storage.Processor.Services
{
    public class ApplicationContext : DbContext
    {
        
        public DbSet<Cancelation> Cancelations {get;set;}
        public DbSet<CancelationDetail> CancelationDetails {get;set;}
        
        public DbSet<Coming> Comings { get; set; }
        public DbSet<ComingDetail> ComingDetails { get; set; }
        
        public DbSet<Move> Moves { get; set; }
        public DbSet<MoveDetail> MoveDetails { get; set; }
        
        public DbSet<Salle> Salles { get; set; }
        public DbSet<SalleDetail> SalleDetails { get; set; }
        
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<StorageItem> StorageItems { get; set; }
        public DbSet<StorageItemCondition> StorageItemConditions { get; set; }
        public DbSet<WarehouseByItemOnStorage> Warehouses { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
            Update();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ArticlesFluentContext().Configure(modelBuilder);
            new SpecificationsFluentContext().Configure(modelBuilder);
            new StorageItemsFluentContext().Configure(modelBuilder);
            new StorageItemConditionsFluentContext().Configure(modelBuilder);
            new StorageItemEventsFluentContext().Configure(modelBuilder);
            new StorageItemEventDetailsFluentContext().Configure(modelBuilder);
            new WarehousesFluentContext().Configure(modelBuilder);

            new CancelationFluentContext().Configure(modelBuilder);
            new CancelationDetailFluentContext().Configure(modelBuilder);
            
            new ComingFluentContext().Configure(modelBuilder);
            new ComingDetailFluentContext().Configure(modelBuilder);
            
            new MoveFluentContext().Configure(modelBuilder);
            new MoveDetailFluentContext().Configure(modelBuilder);
            
            new SalleFluentContext().Configure(modelBuilder);
            new SalleDetailFluentContext().Configure(modelBuilder);
            
            base.OnModelCreating(modelBuilder);
        }
        public void Update()
        {
            Comings.Load();
            ComingDetails.Load();
            
            Moves.Load();
            MoveDetails.Load();
            
            Cancelations.Load();
            CancelationDetails.Load();
            
            Salles.Load();
            SalleDetails.Load();
            
            Articles.Load();
            Categorys.Load();
            Specifications.Load();
            StorageItems.Load();
            StorageItemConditions.Load();
            Warehouses.Load();
        }
    }
    public class DataInitializer
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
