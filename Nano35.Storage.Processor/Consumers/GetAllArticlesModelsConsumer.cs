using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests;

namespace Nano35.Storage.Processor.Consumers
{
    public class GetAllArticlesModelsConsumer : 
        IConsumer<IGetAllArticlesModelsRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        
        public GetAllArticlesModelsConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(
            ConsumeContext<IGetAllArticlesModelsRequestContract> context)
        {
            var message = context.Message;
            
            var request = new GetAllArticlesModelsQuery()
            {
                InstanceId = message.InstanceId
            };
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGetAllArticlesModelsSuccessResultContract:
                    await context.RespondAsync<IGetAllArticlesModelsSuccessResultContract>(result);
                    break;
                case IGetAllArticlesModelsErrorResultContract:
                    await context.RespondAsync<IGetAllArticlesModelsErrorResultContract>(result);
                    break;
            }
        }
    }
}