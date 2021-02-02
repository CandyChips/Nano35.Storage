using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests;

namespace Nano35.Storage.Processor.Consumers
{
    public class GetAllStorageItemsConsumer : 
        IConsumer<IGetAllStorageItemsRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        
        public GetAllStorageItemsConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(
            ConsumeContext<IGetAllStorageItemsRequestContract> context)
        {
            var message = context.Message;
            
            var request = new GetAllStorageItemsQuery()
            {
                InstanceId = message.InstanceId
            };
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGetAllStorageItemsSuccessResultContract:
                    await context.RespondAsync<IGetAllStorageItemsSuccessResultContract>(result);
                    break;
                case IGetAllStorageItemsErrorResultContract:
                    await context.RespondAsync<IGetAllStorageItemsErrorResultContract>(result);
                    break;
            }
        }
    }
}