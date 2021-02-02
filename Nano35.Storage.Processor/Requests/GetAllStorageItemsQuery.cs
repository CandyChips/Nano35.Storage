using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests
{
    public class GetAllStorageItemsQuery :
        IGetAllStorageItemsRequestContract, 
        ICommandRequest<IGetAllStorageItemsResultContract>
    {
        public Guid InstanceId { get; set; }

        private class GetAllStorageItemsSuccessResultContract : 
            IGetAllStorageItemsSuccessResultContract
        {
            public IEnumerable<IStorageItemViewModel> Data { get; set; }
        }

        private class GetAllStorageItemsErrorResultContract :
            IGetAllStorageItemsErrorResultContract
        {
            public string Message { get; set; }
        }

        public class GetAllStorageItemsHandler : 
            IRequestHandler<GetAllStorageItemsQuery, IGetAllStorageItemsResultContract>
        {
            private readonly ApplicationContext _context;
            
            public GetAllStorageItemsHandler(
                ApplicationContext context)
            {
                _context = context;
            }
        
            public async Task<IGetAllStorageItemsResultContract> Handle(
                GetAllStorageItemsQuery message, 
                CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _context
                        .StorageItems
                        .Where(c => c.InstanceId == message.InstanceId)
                        .MapAllToAsync<IStorageItemViewModel>();
                    
                    return new GetAllStorageItemsSuccessResultContract() { Data = result };
                }
                catch
                {
                    return new GetAllStorageItemsErrorResultContract() { Message = "!!!"};
                }
            }
        }
    }
}