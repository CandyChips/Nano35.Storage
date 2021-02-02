using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests;

namespace Nano35.Storage.Processor.Consumers
{
    public class GetAllArticlesConsumer : 
        IConsumer<IGetAllArticlesRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        
        public GetAllArticlesConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(
            ConsumeContext<IGetAllArticlesRequestContract> context)
        {
            var message = context.Message;
            
            var request = new GetAllArticlesQuery()
            {
                InstanceId = message.InstanceId
            };
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGetAllArticlesSuccessResultContract:
                    await context.RespondAsync<IGetAllArticlesSuccessResultContract>(result);
                    break;
                case IGetAllArticlesErrorResultContract:
                    await context.RespondAsync<IGetAllArticlesErrorResultContract>(result);
                    break;
            }
        }
    }
}