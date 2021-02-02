using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests;

namespace Nano35.Storage.Processor.Consumers
{
    public class GetStorageItemByIdConsumer : 
        IConsumer<IGetStorageItemByIdRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        
        public GetStorageItemByIdConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(
            ConsumeContext<IGetStorageItemByIdRequestContract> context)
        {
            var message = context.Message;
            
            var request = new GetStorageItemByIdQuery()
            {
                Id = message.Id
            };
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGetStorageItemByIdSuccessResultContract:
                    await context.RespondAsync<IGetStorageItemByIdSuccessResultContract>(result);
                    break;
                case IGetStorageItemByIdErrorResultContract:
                    await context.RespondAsync<IGetStorageItemByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}