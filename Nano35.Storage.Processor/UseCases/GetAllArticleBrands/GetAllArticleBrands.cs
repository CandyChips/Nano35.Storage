using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllArticleBrands
{
    public class GetAllArticlesBrands :
        UseCaseEndPointNodeBase<IGetAllArticlesBrandsRequestContract, IGetAllArticlesBrandsResultContract>
    {
        private readonly ApplicationContext _context;
        public GetAllArticlesBrands(ApplicationContext context)
        {
            _context = context;
        }

        public override async Task<UseCaseResponse<IGetAllArticlesBrandsResultContract>> Ask(
            IGetAllArticlesBrandsRequestContract input, CancellationToken cancellationToken) =>
            new(new GetAllArticlesBrandsResultContract()
            {
                Data =
                    await _context
                        .Articles
                        .Where(c => c.CategoryId == input.CategoryId && c.CategoryId == input.CategoryId && c.IsDeleted == false)
                        .Select(a => a.Brand)
                        .Distinct()
                        .ToListAsync(cancellationToken: cancellationToken)
            });
    }   
}