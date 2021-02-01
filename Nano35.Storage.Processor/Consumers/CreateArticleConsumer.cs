using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests;

namespace Nano35.Storage.Processor.Consumers
{
    public class CreateArticleConsumer : 
        IConsumer<ICreateArticleRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        
        public CreateArticleConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(
            ConsumeContext<ICreateArticleRequestContract> context)
        {
            var message = context.Message;
            
            var request = new CreateArticleCommand()
            {
                NewId = message.NewId,
                InstanceId = message.InstanceId,
                ArticleTypeId = message.ArticleTypeId,
                Model = message.Model,
                Brand = message.Brand,
                Category = message.Category,
                CategoryGroup = message.CategoryGroup
            };
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case ICreateArticleSuccessResultContract:
                    await context.RespondAsync<ICreateArticleSuccessResultContract>(result);
                    break;
                case ICreateArticleErrorResultContract:
                    await context.RespondAsync<ICreateArticleErrorResultContract>(result);
                    break;
            }
        }
    }
}