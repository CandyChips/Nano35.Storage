using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateCategoryParentCategoryId
{
    public class UpdateParentCategoryConsumer : 
        IConsumer<IUpdateCategoryParentCategoryIdRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public UpdateParentCategoryConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateCategoryParentCategoryIdRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IUpdateCategoryParentCategoryIdRequestContract>) _services.GetService(typeof(ILogger<IUpdateCategoryParentCategoryIdRequestContract>));
            var message = context.Message;
            var result =
                await new LoggedPipeNode<IUpdateCategoryParentCategoryIdRequestContract, IUpdateCategoryParentCategoryIdResultContract>(logger,
                    new TransactedPipeNode<IUpdateCategoryParentCategoryIdRequestContract, IUpdateCategoryParentCategoryIdResultContract>(dbContext, 
                        new UpdateCategoryParentCategoryIdRequest(dbContext))).Ask(message, context.CancellationToken);
            switch (result)
            {
                case IUpdateCategoryParentCategoryIdSuccessResultContract:
                    await context.RespondAsync<IUpdateCategoryParentCategoryIdSuccessResultContract>(result);
                    break;
                case IUpdateCategoryParentCategoryIdErrorResultContract:
                    await context.RespondAsync<IUpdateCategoryParentCategoryIdErrorResultContract>(result);
                    break;
            }
        }
    }
}