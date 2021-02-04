using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests;

namespace Nano35.Storage.Processor.Consumers
{
    public class CreateSalleConsumer : 
        IConsumer<ICreateSalleRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        
        public CreateSalleConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(
            ConsumeContext<ICreateSalleRequestContract> context)
        {
            var message = context.Message;
            
            var request = new CreateSalleCommand()
            {
                NewId = message.NewId,
                IntsanceId = message.IntsanceId,
                UnitId = message.UnitId,
                Number = message.Number,
                ClientId = message.ClientId,
                Details = message.Details,
            };
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case ICreateSalleSuccessResultContract:
                    await context.RespondAsync<ICreateSalleSuccessResultContract>(result);
                    break;
                case ICreateSalleErrorResultContract:
                    await context.RespondAsync<ICreateSalleErrorResultContract>(result);
                    break;
            }
        }
    }
}