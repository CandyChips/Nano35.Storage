using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests;

namespace Nano35.Storage.Processor.Consumers
{
    public class GetAllStorageItemConditionsConsumer : 
        IConsumer<IGetAllStorageItemConditionsRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        
        public GetAllStorageItemConditionsConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(
            ConsumeContext<IGetAllStorageItemConditionsRequestContract> context)
        {
            var message = context.Message;

            var request = new GetAllStorageItemConditionsQuery();
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGetAllStorageItemConditionsSuccessResultContract:
                    await context.RespondAsync<IGetAllStorageItemConditionsSuccessResultContract>(result);
                    break;
                case IGetAllStorageItemConditionsErrorResultContract:
                    await context.RespondAsync<IGetAllStorageItemConditionsErrorResultContract>(result);
                    break;
            }
        }
    }
}