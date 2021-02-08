using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.CreateArticle
{
    public class CreateArticleRequest :
        IPipelineNode<ICreateArticleRequestContract, ICreateArticleResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateArticleRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreateArticleSuccessResultContract : 
            ICreateArticleSuccessResultContract
        {
            
        }
        
        public async Task<ICreateArticleResultContract> Ask(
            ICreateArticleRequestContract input)
        {
            var article = new Article(){
                Id = input.NewId,
                InstanceId = input.InstanceId,
                IsDeleted = false,
                Model = input.Model,
                Brand = input.Brand,
                Info = input.Info,
                CategoryId = input.CategoryId,
            };

            var specs = input.Specs.Select(a => new Spec()
            {
                ArticleId = input.NewId,
                Key = a.Key,
                Value = a.Value
            });
                    
            await _context.AddAsync(article);
            await _context.Specs.AddRangeAsync(specs);
                    
            return new CreateArticleSuccessResultContract();
        }
    }
}