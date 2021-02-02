using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests;

namespace Nano35.Storage.Processor.Consumers
{
    public class GetArticleByIdConsumer : 
        IConsumer<IGetArticleByIdRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        
        public GetArticleByIdConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(
            ConsumeContext<IGetArticleByIdRequestContract> context)
        {
            var message = context.Message;
            
            var request = new GetArticleByIdQuery()
            {
                Id = message.Id
            };
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGetArticleByIdSuccessResultContract:
                    await context.RespondAsync<IGetArticleByIdSuccessResultContract>(result);
                    break;
                case IGetArticleByIdErrorResultContract:
                    await context.RespondAsync<IGetArticleByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}