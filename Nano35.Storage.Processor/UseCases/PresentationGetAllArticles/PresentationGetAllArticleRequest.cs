using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit.Initializers;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.PresentationGetAllArticles
{
    public class PresentationGetAllArticlesRequest :
        UseCaseEndPointNodeBase<IPresentationGetAllArticlesRequestContract, IPresentationGetAllArticlesResultContract>
    {
        private readonly ApplicationContext _context;

        public PresentationGetAllArticlesRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<UseCaseResponse<IPresentationGetAllArticlesResultContract>> Ask
            (IPresentationGetAllArticlesRequestContract input, CancellationToken cancellationToken)
        {
            var result = await _context
                .Articles
                .Where(c => c.InstanceId == input.InstanceId)
                .Select(a => new PresentationArticleViewModel()
                {
                    Brand = a.Brand,
                    Id = a.Id,
                    Info = a.Info,
                    CategoryId = a.CategoryId,
                    Model = a.Model
                })
                .ToListAsync(cancellationToken: cancellationToken);

            return Pass(new PresentationGetAllArticlesResultContract() {Articles = result});
        }
    }   
}