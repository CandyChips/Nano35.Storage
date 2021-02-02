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
            
            public GetAllArticlesCategoryGroupsHandler(
                ApplicationContext context)
            {
                _context = context;
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
                catch
                {
                    return new GetAllArticlesCategoryGroupsErrorResultContract() { Message = "!!!"};
                }
            }
        }
    }
}