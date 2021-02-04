using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests;

namespace Nano35.Storage.Processor.Consumers
{
    public class CreateCancelationConsumer : 
        IConsumer<ICreateCancelationRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        
        public CreateCancelationConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(
            ConsumeContext<ICreateCancelationRequestContract> context)
        {
            var message = context.Message;
            
            var request = new CreateCancelationCommand()
            {
                NewId = message.NewId,
                IntsanceId = message.IntsanceId,
                UnitId = message.UnitId,
                Number = message.Number,
                Comment = message.Comment,
                Details = message.Details,
            };
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case ICreateCancelationSuccessResultContract:
                    await context.RespondAsync<ICreateCancelationSuccessResultContract>(result);
                    break;
                case ICreateCancelationErrorResultContract:
                    await context.RespondAsync<ICreateCancelationErrorResultContract>(result);
                    break;
            }
        }
    }
}