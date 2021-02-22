using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.UpdateArticleBrand
{
    public class UpdateArticleBrandRequest :
        IPipelineNode<
            IUpdateArticleBrandRequestContract, 
            IUpdateArticleBrandResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateArticleBrandRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateArticleBrandSuccessResultContract : 
            IUpdateArticleBrandSuccessResultContract
        {
            
        }
        
        public async Task<IUpdateArticleBrandResultContract> Ask(
            IUpdateArticleBrandRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Articles
                .FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken));
            result.Brand = input.Brand;
            return new UpdateArticleBrandSuccessResultContract();
        }
    }
}