using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests;

namespace Nano35.Storage.Processor.Consumers
{
    public class CreateMoveConsumer : 
        IConsumer<ICreateMoveRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        
        public CreateMoveConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(
            ConsumeContext<ICreateMoveRequestContract> context)
        {
            var message = context.Message;
            
            var request = new CreateMoveCommand()
            {
                NewId = message.NewId,
                IntsanceId = message.IntsanceId,
                FromUnitId = message.FromUnitId,
                ToUnitId = message.ToUnitId,
                Number = message.Number,
                Details = message.Details,
            };
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case ICreateMoveSuccessResultContract:
                    await context.RespondAsync<ICreateMoveSuccessResultContract>(result);
                    break;
                case ICreateMoveErrorResultContract:
                    await context.RespondAsync<ICreateMoveErrorResultContract>(result);
                    break;
            }
        }
    }
}