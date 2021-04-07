using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllArticleBrands
{
    public class GetAllArticlesBrandsRequest :
        EndPointNodeBase<
            IGetAllArticlesBrandsRequestContract,
            IGetAllArticlesBrandsResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllArticlesBrandsRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllArticlesBrandsSuccessResultContract : 
            IGetAllArticlesBrandsSuccessResultContract
        {
            public List<string> Data { get; set; }
        }
        
        public override async Task<IGetAllArticlesBrandsResultContract> Ask(
            IGetAllArticlesBrandsRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context
                .Articles
                .Where(c => c.CategoryId == input.CategoryId && c.CategoryId == input.CategoryId)
                .Select(a => a.Brand)
                .Distinct()
                .ToListAsync(cancellationToken: cancellationToken);

            return new GetAllArticlesBrandsSuccessResultContract() {Data = result};
        }
    }   
}