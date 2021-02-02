using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests;

namespace Nano35.Storage.Processor.Consumers
{
    public class GetAllArticlesCategoryGroupsConsumer : 
        IConsumer<IGetAllArticlesCategoryGroupsRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        
        public GetAllArticlesCategoryGroupsConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(
            ConsumeContext<IGetAllArticlesCategoryGroupsRequestContract> context)
        {
            var message = context.Message;
            
            var request = new GetAllArticlesCategoryGroupsQuery()
            {
                InstanceId = message.InstanceId
            };
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGetAllArticlesCategoryGroupsSuccessResultContract:
                    await context.RespondAsync<IGetAllArticlesCategoryGroupsSuccessResultContract>(result);
                    break;
                case IGetAllArticlesCategoryGroupsErrorResultContract:
                    await context.RespondAsync<IGetAllArticlesCategoryGroupsErrorResultContract>(result);
                    break;
            }
        }
    }
}