using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateArticle
{
    public class CreateArticleRequest :
        EndPointNodeBase<
            ICreateArticleRequestContract,
            ICreateArticleResultContract>
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
        
        public override async Task<ICreateArticleResultContract> Ask(
            ICreateArticleRequestContract input,
            CancellationToken cancellationToken)
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
            
            await _context.AddAsync(article, cancellationToken);

            if (input.Specs != null)
            {
                var specs = input.Specs.Select(a => new Spec()
                {
                    ArticleId = input.NewId,
                    InstanceId = input.InstanceId,
                    Key = a.Key,
                    Value = a.Value
                });
                
                await _context.Specs.AddRangeAsync(specs, cancellationToken);
            }

            return new CreateArticleSuccessResultContract();
        }
    }
}