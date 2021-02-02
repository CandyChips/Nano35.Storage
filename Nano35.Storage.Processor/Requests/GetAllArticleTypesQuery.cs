using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests
{
    public class GetAllArticleTypesQuery :
        IGetAllArticleTypesRequestContract, 
        ICommandRequest<IGetAllArticleTypesResultContract>
    {
        private class GetAllArticleTypesSuccessResultContract : 
            IGetAllArticleTypesSuccessResultContract
        {
            public IEnumerable<IArticleTypeViewModel> Data { get; set; }
        }

        private class GetAllArticleTypesErrorResultContract :
            IGetAllArticleTypesErrorResultContract
        {
            public string Message { get; set; }
        }

        public class GetAllArticleTypesHandler : 
            IRequestHandler<GetAllArticleTypesQuery, IGetAllArticleTypesResultContract>
        {
            private readonly ApplicationContext _context;
            
            public GetAllArticleTypesHandler(
                ApplicationContext context)
            {
                _context = context;
            }
        
            public async Task<IGetAllArticleTypesResultContract> Handle(
                GetAllArticleTypesQuery message, 
                CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _context
                        .ArticleTypes
                        .MapAllToAsync<IArticleTypeViewModel>();
                    
                    return new GetAllArticleTypesSuccessResultContract() { Data = result };
                }
                catch
                {
                    return new GetAllArticleTypesErrorResultContract() { Message = "!!!"};
                }
            }
        }
    }
}