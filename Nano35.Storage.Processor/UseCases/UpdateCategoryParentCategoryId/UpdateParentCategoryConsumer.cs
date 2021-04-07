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
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IUpdateCategoryParentCategoryIdRequestContract>) _services.GetService(typeof(ILogger<IUpdateCategoryParentCategoryIdRequestContract>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedPipeNode<IUpdateCategoryParentCategoryIdRequestContract, IUpdateCategoryParentCategoryIdResultContract>(logger,
                    new ValidatedUpdateCategoryParentCategoryIdRequest(
                        new UpdateCategoryParentCategoryIdRequest(dbContext))).Ask(message, context.CancellationToken);
            
            // Check response of create article request
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