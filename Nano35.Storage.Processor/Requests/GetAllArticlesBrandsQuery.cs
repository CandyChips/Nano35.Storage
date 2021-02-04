using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests
{
    public class GetAllArticlesBrandsQuery :
        IGetAllArticlesBrandsRequestContract, 
        ICommandRequest<IGetAllArticlesBrandsResultContract>
    {
        public Guid InstanceId { get; set; }
        
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

        public class GetAllArticlesBrandsHandler : 
            IRequestHandler<GetAllArticlesBrandsQuery, IGetAllArticlesBrandsResultContract>
        {
            private readonly ApplicationContext _context;
            private readonly IBus _bus;
            
            public GetAllArticlesBrandsHandler(
                ApplicationContext context, 
                IBus bus)
            {
                _context = context;
                _bus = bus;
            }
        
            public async Task<IGetAllArticlesBrandsResultContract> Handle(
                GetAllArticlesBrandsQuery message, 
                CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _context
                        .Articles
                        .Where(c => c.InstanceId == message.InstanceId)
                        .Select(a => a.Brand)
                        .Distinct()
                        .ToListAsync(cancellationToken: cancellationToken);

                    return new GetAllArticlesBrandsSuccessResultContract() { Data = result };
                }
                catch (Exception e)
                {
                    return new GetAllArticlesBrandsErrorResultContract() { Message = "!!!"};
                }
            }
        }
    }
}