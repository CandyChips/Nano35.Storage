using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateCategoryName
{
    public class UpdateCategoryNameConsumer : 
        IConsumer<IUpdateCategoryNameRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public UpdateCategoryNameConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateCategoryNameRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IUpdateCategoryNameRequestContract>) _services.GetService(typeof(ILogger<IUpdateCategoryNameRequestContract>));
            var message = context.Message;
            var result =
                await new LoggedPipeNode<IUpdateCategoryNameRequestContract, IUpdateCategoryNameResultContract>(logger,
                    new ValidatedUpdateCategoryNameRequest(
                        new TransactedPipeNode<IUpdateCategoryNameRequestContract, IUpdateCategoryNameResultContract>(dbContext, 
                            new UpdateCategoryNameRequest(dbContext)))).Ask(message, context.CancellationToken);
            switch (result)
            {
                case IUpdateCategoryNameSuccessResultContract:
                    await context.RespondAsync<IUpdateCategoryNameSuccessResultContract>(result);
                    break;
                case IUpdateCategoryNameErrorResultContract:
                    await context.RespondAsync<IUpdateCategoryNameErrorResultContract>(result);
                    break;
            }
        }
    }
}