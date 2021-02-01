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
    public class GetAllArticlesQuery :
        IGetAllArticlesRequestContract, 
        ICommandRequest<IGetAllArticlesResultContract>
    {
        public Guid InstanceId { get; set; }

        private class GetAllArticlesSuccessResultContract : 
            IGetAllArticlesSuccessResultContract
        {
            public IEnumerable<IArticleViewModel> Data { get; set; }
        }

        private class GetAllArticlesErrorResultContract :
            IGetAllArticlesErrorResultContract
        {
            public string Message { get; set; }
        }

        public class GetAllArticlesHandler : 
            IRequestHandler<GetAllArticlesQuery, IGetAllArticlesResultContract>
        {
            private readonly ApplicationContext _context;
            private readonly IBus _bus;
            
            public GetAllArticlesHandler(
                ApplicationContext context, 
                IBus bus)
            {
                _context = context;
                _bus = bus;
            }
        
            public async Task<IGetAllArticlesResultContract> Handle(
                GetAllArticlesQuery message, 
                CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _context.Articles
                        .Where(c => c.InstanceId == message.InstanceId)
                        .MapAllToAsync<IArticleViewModel>();
                    
                    return new GetAllArticlesSuccessResultContract() { Data = result };
                }
                catch (Exception e)
                {
                    return new GetAllArticlesErrorResultContract() { Message = "!!!"};
                }
            }
        }
    }
}