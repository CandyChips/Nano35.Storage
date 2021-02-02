
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests;

namespace Nano35.Storage.Processor.Consumers
{
    public class GetAllArticleTypesConsumer : 
        IConsumer<IGetAllArticleTypesRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        
        public GetAllArticleTypesConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(
            ConsumeContext<IGetAllArticleTypesRequestContract> context)
        {
            var message = context.Message;
            
            var request = new GetAllArticleTypesQuery();
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGetAllArticleTypesSuccessResultContract:
                    await context.RespondAsync<IGetAllArticleTypesSuccessResultContract>(result);
                    break;
                case IGetAllArticleTypesErrorResultContract:
                    await context.RespondAsync<IGetAllArticleTypesErrorResultContract>(result);
                    break;
            }
        }
    }
}