using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.GetAllArticleModels
{
    public class GetAllArticlesModelsRequest :
        IPipelineNode<
            IGetAllArticlesModelsRequestContract,
            IGetAllArticlesModelsResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllArticlesModelsRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllArticlesModelsSuccessResultContract : 
            IGetAllArticlesModelsSuccessResultContract
        {
            public IEnumerable<string> Data { get; set; }
        }

        private class GetAllArticlesModelsErrorResultContract : 
            IGetAllArticlesModelsErrorResultContract
        {
            public string Message { get; set; }
        }
        
        public async Task<IGetAllArticlesModelsResultContract> Ask
            (IGetAllArticlesModelsRequestContract input, CancellationToken cancellationToken)
        {
            var result = await _context
                .Articles
                .Where(c => c.InstanceId == input.InstanceId && c.CategoryId == input.CategoryId)
                .Select(a => a.Brand)
                .Distinct()
                .ToListAsync(cancellationToken: cancellationToken);

            return new GetAllArticlesModelsSuccessResultContract() {Data = result};
        }
    }   
}