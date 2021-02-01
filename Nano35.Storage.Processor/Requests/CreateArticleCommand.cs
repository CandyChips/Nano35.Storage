using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests
{
    public class CreateArticleCommand :
        ICreateArticleRequestContract, 
        ICommandRequest<ICreateArticleResultContract>
    {
        public Guid NewId { get; set; }
        public Guid InstanceId { get; set; }
        public Guid ArticleTypeId { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string CategoryGroup { get; set; }

        private class CreateArticleSuccessResultContract : 
            ICreateArticleSuccessResultContract
        {
            
        }

        private class CreateArticleErrorResultContract :
            ICreateArticleErrorResultContract
        {
            public string Message { get; set; }
        }

        public class CreateArticleHandler : 
            IRequestHandler<CreateArticleCommand, ICreateArticleResultContract>
        {
            private readonly ApplicationContext _context;
            private readonly IBus _bus;
            
            public CreateArticleHandler(
                ApplicationContext context, 
                IBus bus)
            {
                _context = context;
                _bus = bus;
            }
        
            public async Task<ICreateArticleResultContract> Handle(
                CreateArticleCommand message, 
                CancellationToken cancellationToken)
            {
                try
                {
                    var model = _context.Models.Where(c => c.IsDeleted == false).Select(a => a.Name).Contains(message.Model) ? 
                        _context.Models.FirstOrDefault(a => a.Name == message.Model) : 
                        new Model() { IsDeleted = false, Name = message.Model};
                    
                    var brand = _context.Brands.Where(c => c.IsDeleted == false).Select(a => a.Name).Contains(message.Brand) ? 
                        _context.Brands.FirstOrDefault(a => a.Name == message.Brand) : 
                        new Brand() { IsDeleted = false, Name = message.Brand};
                    
                    var category = _context.Categorys.Where(c => c.IsDeleted == false).Select(a => a.Name).Contains(message.Category) ? 
                        _context.Categorys.FirstOrDefault(a => a.Name == message.Category) : 
                        new Category() { IsDeleted = false, Name = message.Category};
                    
                    var categoryGroup = _context.CategoryGroups.Where(c => c.IsDeleted == false).Select(a => a.Name).Contains(message.CategoryGroup) ? 
                        _context.CategoryGroups.FirstOrDefault(a => a.Name == message.CategoryGroup) : 
                        new CategoryGroup() { IsDeleted = false, Name = message.CategoryGroup};
                    
                    var client = new Article(){
                        Id = message.NewId,
                        InstanceId = message.InstanceId,
                        IsDeleted = false,
                        Model = model,
                        Brand = brand,
                        ArticleTypeId = message.ArticleTypeId,
                        Category = category,
                        CategoryGroup = categoryGroup, 
                    };
                    await this._context.AddAsync(client);
                    _context.SaveChanges();
                    return new CreateArticleSuccessResultContract();
                }
                catch (Exception e)
                {
                    return new CreateArticleErrorResultContract();
                }
            }
        }
    }
}