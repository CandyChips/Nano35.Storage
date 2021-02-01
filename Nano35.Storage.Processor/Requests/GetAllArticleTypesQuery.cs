using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Models;
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
            private readonly IBus _bus;
            
            public GetAllArticleTypesHandler(
                ApplicationContext context, 
                IBus bus)
            {
                _context = context;
                _bus = bus;
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
                catch (Exception e)
                {
                    return new GetAllArticleTypesErrorResultContract() { Message = "!!!"};
                }
            }
        }
    }
}