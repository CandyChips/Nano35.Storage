using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests;

namespace Nano35.Storage.Processor.Consumers
{
    public class GetAllArticlesCategoriesConsumer : 
        IConsumer<IGetAllArticlesCategoriesRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        
        public GetAllArticlesCategoriesConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(
            ConsumeContext<IGetAllArticlesCategoriesRequestContract> context)
        {
            var message = context.Message;
            
            var request = new GetAllArticlesCategoriesQuery()
            {
                InstanceId = message.InstanceId
            };
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGetAllArticlesCategoriesSuccessResultContract:
                    await context.RespondAsync<IGetAllArticlesCategoriesSuccessResultContract>(result);
                    break;
                case IGetAllArticlesCategoriesErrorResultContract:
                    await context.RespondAsync<IGetAllArticlesCategoriesErrorResultContract>(result);
                    break;
            }
        }
    }
}