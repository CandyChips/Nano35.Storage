using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.GetAllArticleBrands
{
    public class GetAllArticlesBrandsRequest :
        IPipelineNode<
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
            public IEnumerable<string> Data { get; set; }
        }

        private class GetAllArticlesBrandsErrorResultContract : 
            IGetAllArticlesBrandsErrorResultContract
        {
            public string Message { get; set; }
        }
        
        public async Task<IGetAllArticlesBrandsResultContract> Ask
            (IGetAllArticlesBrandsRequestContract input, CancellationToken cancellationToken)
        {
            var result = await _context
                .Articles
                .Where(c => c.InstanceId == input.InstanceId && c.CategoryId == input.CategoryId)
                .Select(a => a.Brand)
                .Distinct()
                .ToListAsync(cancellationToken: cancellationToken);

            return new GetAllArticlesBrandsSuccessResultContract() {Data = result};
        }
    }   
}