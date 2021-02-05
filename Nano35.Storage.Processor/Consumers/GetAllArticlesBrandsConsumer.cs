using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests;

namespace Nano35.Storage.Processor.Consumers
{
    public class GetAllArticlesBrandsConsumer : 
        IConsumer<IGetAllArticlesBrandsRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        
        public GetAllArticlesBrandsConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(
            ConsumeContext<IGetAllArticlesBrandsRequestContract> context)
        {
            var message = context.Message;
            
            var request = new GetAllArticlesBrandsQuery()
            {
                CategoryId = message.CategoryId,
                InstanceId = message.InstanceId
            };
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGetAllArticlesBrandsSuccessResultContract:
                    await context.RespondAsync<IGetAllArticlesBrandsSuccessResultContract>(result);
                    break;
                case IGetAllArticlesBrandsErrorResultContract:
                    await context.RespondAsync<IGetAllArticlesBrandsErrorResultContract>(result);
                    break;
            }
        }
    }
}