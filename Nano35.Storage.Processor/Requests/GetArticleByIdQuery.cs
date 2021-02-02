using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests
{
    public class GetArticleByIdQuery :
        IGetArticleByIdRequestContract, 
        ICommandRequest<IGetArticleByIdResultContract>
    {
        public Guid Id { get; set; }

        private class GetArticleByIdSuccessResultContract : 
            IGetArticleByIdSuccessResultContract
        {
            public IArticleViewModel Data { get; set; }
        }

        private class GetArticleByIdErrorResultContract :
            IGetArticleByIdErrorResultContract
        {
            public string Message { get; set; }
        }

        public class GetAllArticlesHandler : 
            IRequestHandler<GetArticleByIdQuery, IGetArticleByIdResultContract>
        {
            private readonly ApplicationContext _context;
            
            public GetAllArticlesHandler(
                ApplicationContext context)
            {
                _context = context;
            }
        
            public async Task<IGetArticleByIdResultContract> Handle(
                GetArticleByIdQuery message, 
                CancellationToken cancellationToken)
            {
                try
                {
                    var result = _context.Articles
                        .FirstOrDefault(c => c.Id == message.Id)
                        .MapTo<IArticleViewModel>();
                    
                    return new GetArticleByIdSuccessResultContract() { Data = result };
                }
                catch (Exception e)
                {
                    return new GetArticleByIdErrorResultContract() { Message = "!!!"};
                }
            }
        }
    }
}