using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Storage.Processor.Models;

namespace Nano35.Storage.Processor.Services
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleType> ArticleTypes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Cancelation> Cancelations {get;set;}
        public DbSet<Category> Categorys { get; set; }
        public DbSet<CategoryGroup> CategoryGroups { get; set; }
        public DbSet<Coming> Comings { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<PlaceOnStorage> PlaceOnStorages { get; set; }
        public DbSet<Salle> Salles { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<StorageItem> StorageItems { get; set; }
        public DbSet<StorageItemCondition> StorageItemConditions { get; set; }
        public DbSet<StorageItemEvent> StorageItemEvents { get; set; }
        public DbSet<StorageItemEventDetail> StorageItemEventDetails { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
            Update();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ArticlesFluentContext().Configure(modelBuilder);
            new ArticleTypesFluentContext().Configure(modelBuilder);
            new BrandsFluentContext().Configure(modelBuilder);
            new CancelationsFluentContext().Configure(modelBuilder);
            new CategorysFluentContext().Configure(modelBuilder);
            new CategoryGroupsFluentContext().Configure(modelBuilder);
            new ComingsFluentContext().Configure(modelBuilder);
            new ModelsFluentContext().Configure(modelBuilder);
            new MovesFluentContext().Configure(modelBuilder);
            new PlaceOnStoragesFluentContext().Configure(modelBuilder);
            new SallesFluentContext().Configure(modelBuilder);
            new SpecificationsFluentContext().Configure(modelBuilder);
            new StorageItemsFluentContext().Configure(modelBuilder);
            new StorageItemConditionsFluentContext().Configure(modelBuilder);
            new StorageItemEventsFluentContext().Configure(modelBuilder);
            new StorageItemEventDetailsFluentContext().Configure(modelBuilder);
            new WarehousesFluentContext().Configure(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        public void Update()
        {
            Articles.Load();
            ArticleTypes.Load();
            Brands.Load();
            Cancelations.Load();
            Categorys.Load();
            CategoryGroups.Load();
            Comings.Load();
            Models.Load();
            Moves.Load();
            PlaceOnStorages.Load();
            Salles.Load();
            Specifications.Load();
            StorageItems.Load();
            StorageItemConditions.Load();
            StorageItemEvents.Load();
            StorageItemEventDetails.Load();
            Warehouses.Load();
        }
    }
    public class DataInitializer
    {
        public static async Task InitializeRolesAsync(
            ApplicationContext modelBuilder)
        {
            if(!modelBuilder.ArticleTypes.Any())
            {
                modelBuilder.ArticleTypes.Add(new Models.ArticleType(){
                    Name = "Устройства",
                    Id = Guid.NewGuid(),
                    IsDeleted = false
                });
                modelBuilder.ArticleTypes.Add(new Models.ArticleType(){
                    Name = "Аксессуары",
                    Id = Guid.NewGuid(),
                    IsDeleted = false
                });
                modelBuilder.ArticleTypes.Add(new Models.ArticleType(){
                    Name = "Запчасти и комплектующие",
                    Id = Guid.NewGuid(),
                    IsDeleted = false
                });

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
