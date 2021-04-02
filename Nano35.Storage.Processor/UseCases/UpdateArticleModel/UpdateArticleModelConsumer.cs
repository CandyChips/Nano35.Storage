using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleModel
{
    public class UpdateArticleModelConsumer : 
        IConsumer<IUpdateArticleModelRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public UpdateArticleModelConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateArticleModelRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IUpdateArticleModelRequestContract>) _services
                .GetService(typeof(ILogger<IUpdateArticleModelRequestContract>));
            var message = context.Message;
            var result =
                await new LoggedPipeNode<IUpdateArticleModelRequestContract, IUpdateArticleModelResultContract>(logger,
                    new ValidatedUpdateArticleModelRequest(
                        new TransactedPipeNode<IUpdateArticleModelRequestContract, IUpdateArticleModelResultContract>(dbContext, 
                            new UpdateArticleModelRequest(dbContext)))).Ask(message, context.CancellationToken);
            switch (result)
            {
                case IUpdateArticleModelSuccessResultContract:
                    await context.RespondAsync<IUpdateArticleModelSuccessResultContract>(result);
                    break;
                case IUpdateArticleModelErrorResultContract:
                    await context.RespondAsync<IUpdateArticleModelErrorResultContract>(result);
                    break;
            }
        }
    }
}