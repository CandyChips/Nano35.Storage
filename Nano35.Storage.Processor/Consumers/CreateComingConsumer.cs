using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests;

namespace Nano35.Storage.Processor.Consumers
{
    public class CreateComingConsumer : 
        IConsumer<ICreateComingRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        
        public CreateComingConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(
            ConsumeContext<ICreateComingRequestContract> context)
        {
            var message = context.Message;
            
            var request = new CreateComingCommand()
            {
                NewId = message.NewId,
                IntsanceId = message.IntsanceId,
                UnitId = message.UnitId,
                Number = message.Number,
                Comment = message.Comment,
                ClientId = message.ClientId,
                Details = message.Details,
            };
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case ICreateComingSuccessResultContract:
                    await context.RespondAsync<ICreateComingSuccessResultContract>(result);
                    break;
                case ICreateComingErrorResultContract:
                    await context.RespondAsync<ICreateComingErrorResultContract>(result);
                    break;
            }
        }
    }
}