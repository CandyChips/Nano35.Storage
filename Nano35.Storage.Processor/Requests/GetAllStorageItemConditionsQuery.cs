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
    public class GetAllStorageItemConditionsQuery :
        IGetAllStorageItemConditionsRequestContract, 
        ICommandRequest<IGetAllStorageItemConditionsResultContract>
    {
        public Guid InstanceId { get; set; }

        private class GetAllStorageItemConditionsSuccessResultContract : 
            IGetAllStorageItemConditionsSuccessResultContract
        {
            public IEnumerable<IStorageItemConditionViewModel> Data { get; set; }
        }

        private class GetAllStorageItemConditionsErrorResultContract :
            IGetAllStorageItemConditionsErrorResultContract
        {
            public string Message { get; set; }
        }

        public class GetAllStorageItemConditionsHandler : 
            IRequestHandler<GetAllStorageItemConditionsQuery, IGetAllStorageItemConditionsResultContract>
        {
            private readonly ApplicationContext _context;
            
            public GetAllStorageItemConditionsHandler(
                ApplicationContext context)
            {
                _context = context;
            }
        
            public async Task<IGetAllStorageItemConditionsResultContract> Handle(
                GetAllStorageItemConditionsQuery message, 
                CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _context
                        .StorageItemConditions
                        .MapAllToAsync<IStorageItemConditionViewModel>();
                    
                    return new GetAllStorageItemConditionsSuccessResultContract() { Data = result };
                }
                catch
                {
                    return new GetAllStorageItemConditionsErrorResultContract() { Message = "!!!"};
                }
            }
        }
    }
}