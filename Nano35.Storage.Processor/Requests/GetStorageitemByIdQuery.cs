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
    public class GetStorageItemByIdQuery :
        IGetStorageItemByIdRequestContract, 
        ICommandRequest<IGetStorageItemByIdResultContract>
    {
        public Guid Id { get; set; }

        private class GetStorageItemByIdSuccessResultContract : 
            IGetStorageItemByIdSuccessResultContract
        {
            public IStorageItemViewModel Data { get; set; }
        }

        private class GetStorageItemByIdErrorResultContract :
            IGetStorageItemByIdErrorResultContract
        {
            public string Message { get; set; }
        }

        public class GetStorageItemByIdHandler : 
            IRequestHandler<GetStorageItemByIdQuery, IGetStorageItemByIdResultContract>
        {
            private readonly ApplicationContext _context;
            
            public GetStorageItemByIdHandler(
                ApplicationContext context)
            {
                _context = context;
            }
        
            public async Task<IGetStorageItemByIdResultContract> Handle(
                GetStorageItemByIdQuery message, 
                CancellationToken cancellationToken)
            {
                try
                {
                    var result = _context.StorageItems
                        .FirstOrDefault(c => c.Id == message.Id)
                        .MapTo<IStorageItemViewModel>();
                    
                    return new GetStorageItemByIdSuccessResultContract() { Data = result };
                }
                catch (Exception e)
                {
                    return new GetStorageItemByIdErrorResultContract() { Message = "!!!"};
                }
            }
        }
    }
}