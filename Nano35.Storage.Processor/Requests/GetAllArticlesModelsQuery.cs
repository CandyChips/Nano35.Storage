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
    public class GetAllArticlesModelsQuery :
        IGetAllArticlesModelsRequestContract, 
        ICommandRequest<IGetAllArticlesModelsResultContract>
    {
        public Guid InstanceId { get; set; }
        
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

        public class GetAllArticlesModelsHandler : 
            IRequestHandler<GetAllArticlesModelsQuery, IGetAllArticlesModelsResultContract>
        {
            private readonly ApplicationContext _context;
            private readonly IBus _bus;
            
            public GetAllArticlesModelsHandler(
                ApplicationContext context, 
                IBus bus)
            {
                _context = context;
                _bus = bus;
            }
        
            public async Task<IGetAllArticlesModelsResultContract> Handle(
                GetAllArticlesModelsQuery message, 
                CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _context
                        .Articles
                        .Where(c => c.InstanceId == message.InstanceId)
                        .Select(a => a.Model.Name)
                        .Distinct()
                        .ToListAsync();

                    return new GetAllArticlesModelsSuccessResultContract() { Data = result };
                }
                catch (Exception e)
                {
                    return new GetAllArticlesModelsErrorResultContract() { Message = "!!!"};
                }
            }
        }
    }
}