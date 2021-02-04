using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests;

namespace Nano35.Storage.Processor.Consumers
{
    public class CreateStorageItemConsumer : 
        IConsumer<ICreateStorageItemRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        
        public CreateStorageItemConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(
            ConsumeContext<ICreateStorageItemRequestContract> context)
        {
            var message = context.Message;
            
            var request = new CreateStorageItemCommand()
            {
                NewId = message.NewId,
                ArticleId = message.ArticleId,
                ConditionId = message.ConditionId,
                InstanceId = message.InstanceId,
                Comment = message.Comment,
                HiddenComment = message.HiddenComment,
                RetailPrice = message.RetailPrice,
                PurchasePrice = message.PurchasePrice
            };
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case ICreateStorageItemSuccessResultContract:
                    await context.RespondAsync<ICreateStorageItemSuccessResultContract>(result);
                    break;
                case ICreateStorageItemErrorResultContract:
                    await context.RespondAsync<ICreateStorageItemErrorResultContract>(result);
                    break;
            }
        }
    }
}