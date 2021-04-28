using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllArticles
{
    public class GetAllArticlesRequest :
        EndPointNodeBase<IGetAllArticlesRequestContract, IGetAllArticlesResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllArticlesRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<IGetAllArticlesResultContract> Ask(IGetAllArticlesRequestContract input, CancellationToken cancellationToken) =>
            new GetAllArticlesSuccessResultContract()
            {
                Data = 
                    await _context
                        .Articles
                        .Where(c => c.InstanceId == input.InstanceId)
                        .Select(a => 
                            new ArticleViewModel()
                            {Brand = a.Brand,
                                Model = a.Model,
                                Category = a.Category.Name,
                                CategoryId = a.CategoryId,
                                Id = a.Id,
                                Info = a.Info})
                        .ToListAsync(cancellationToken: cancellationToken)
            };
    }   
}