using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests;

namespace Nano35.Storage.Processor.Consumers
{
    public class CreateCategoryConsumer : 
        IConsumer<ICreateCategoryRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        
        public CreateCategoryConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(
            ConsumeContext<ICreateCategoryRequestContract> context)
        {
            var message = context.Message;
            
            var request = new CreateCategoryCommand()
            {
                NewId = message.NewId,
                InstanceId = message.InstanceId,
                Name = message.Name,
                ParentCategoryId = message.ParentCategoryId,
            };
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case ICreateCategorySuccessResultContract:
                    await context.RespondAsync<ICreateCategorySuccessResultContract>(result);
                    break;
                case ICreateCategoryErrorResultContract:
                    await context.RespondAsync<ICreateCategoryErrorResultContract>(result);
                    break;
            }
        }
    }
}