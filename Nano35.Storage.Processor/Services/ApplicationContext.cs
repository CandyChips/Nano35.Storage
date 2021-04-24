using Microsoft.EntityFrameworkCore;
using Nano35.Storage.Processor.Models;

namespace Nano35.Storage.Processor.Services
{
    public class ApplicationContext : DbContext
    {
        
        public DbSet<Cancellation> Cancellations {get;set;}
        public DbSet<CancelationDetail> CancellationsDetails {get;set;}
        
        public DbSet<Coming> Comings { get; set; }
        public DbSet<ComingDetail> ComingDetails { get; set; }
        
        public DbSet<Move> Moves { get; set; }
        public DbSet<MoveDetail> MoveDetails { get; set; }
        
        public DbSet<Selle> Sells { get; set; }
        public DbSet<SelleDetail> SelleDetails { get; set; }
        
        public DbSet<Spec> Specs { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
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
            modelBuilder.ApplyConfiguration(new Article.Configuration());
            modelBuilder.ApplyConfiguration(new Spec.Configuration());
            modelBuilder.ApplyConfiguration(new StorageItemCondition.Configuration());
            modelBuilder.ApplyConfiguration(new StorageItem.Configuration());
            modelBuilder.ApplyConfiguration(new WarehouseByItemOnStorage.Configuration());
            modelBuilder.ApplyConfiguration(new Category.Configuration());
            modelBuilder.ApplyConfiguration(new Coming.Configuration());
            modelBuilder.ApplyConfiguration(new ComingDetail.Configuration());
            modelBuilder.ApplyConfiguration(new Selle.Configuration());
            modelBuilder.ApplyConfiguration(new SelleDetail.Configuration());
            modelBuilder.ApplyConfiguration(new Move.Configuration());
            modelBuilder.ApplyConfiguration(new MoveDetail.Configuration());
            modelBuilder.ApplyConfiguration(new Cancellation.Configuration());
            modelBuilder.ApplyConfiguration(new CancelationDetail.Configuration());
            base.OnModelCreating(modelBuilder);
        }

        private void Update()
        {
            Comings.Load();
            ComingDetails.Load();
            
            Moves.Load();
            MoveDetails.Load();
            
            CancellationsDetails.Load();
            Cancellations.Load();
            
            Sells.Load();
            SelleDetails.Load();
            
            Articles.Load();
            Categories.Load();
            Specs.Load();
            StorageItems.Load();
            StorageItemConditions.Load();
            Warehouses.Load();
        }
    }
}
