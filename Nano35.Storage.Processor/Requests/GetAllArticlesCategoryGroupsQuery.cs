using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests
{
    public class GetAllArticlesCategoryGroupsQuery :
        IGetAllArticlesCategoryGroupsRequestContract, 
        ICommandRequest<IGetAllArticlesCategoryGroupsResultContract>
    {
        public Guid InstanceId { get; set; }
        
        private class GetAllArticlesCategoryGroupsSuccessResultContract : 
            IGetAllArticlesCategoryGroupsSuccessResultContract
        {
            public IEnumerable<string> Data { get; set; }
        }

        private class GetAllArticlesCategoryGroupsErrorResultContract :
            IGetAllArticlesCategoryGroupsErrorResultContract
        {
            public string Message { get; set; }
        }

        public class GetAllArticlesCategoryGroupsHandler : 
            IRequestHandler<GetAllArticlesCategoryGroupsQuery, IGetAllArticlesCategoryGroupsResultContract>
        {
            private readonly ApplicationContext _context;
            private readonly IBus _bus;
            
            public GetAllArticlesCategoryGroupsHandler(
                ApplicationContext context, 
                IBus bus)
            {
                _context = context;
                _bus = bus;
            }
        
            public async Task<IGetAllArticlesCategoryGroupsResultContract> Handle(
                GetAllArticlesCategoryGroupsQuery message, 
                CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _context
                        .Articles
                        .Where(c => c.InstanceId == message.InstanceId)
                        .Select(a => a.CategoryGroup.Name)
                        .Distinct()
                        .ToListAsync();
                    
                    return new GetAllArticlesCategoryGroupsSuccessResultContract() { Data = result };
                }
                catch (Exception e)
                {
                    return new GetAllArticlesCategoryGroupsErrorResultContract() { Message = "!!!"};
                }
            }
        }
    }
}