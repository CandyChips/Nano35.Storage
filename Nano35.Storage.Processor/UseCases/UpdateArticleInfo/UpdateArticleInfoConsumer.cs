using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleInfo
{
    public class UpdateArticleInfoConsumer : 
        IConsumer<IUpdateArticleInfoRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public UpdateArticleInfoConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateArticleInfoRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IUpdateArticleInfoRequestContract>) _services.GetService(typeof(ILogger<IUpdateArticleInfoRequestContract>));
            var message = context.Message;
            var result =
                await new LoggedPipeNode<IUpdateArticleInfoRequestContract, IUpdateArticleInfoResultContract>(logger,
                    new ValidatedUpdateArticleInfoRequest(
                        new TransactedPipeNode<IUpdateArticleInfoRequestContract, IUpdateArticleInfoResultContract>(dbContext, 
                            new UpdateArticleInfoRequest(dbContext)))).Ask(message, context.CancellationToken);
            switch (result)
            {
                case IUpdateArticleInfoSuccessResultContract:
                    await context.RespondAsync<IUpdateArticleInfoSuccessResultContract>(result);
                    break;
                case IUpdateArticleInfoErrorResultContract:
                    await context.RespondAsync<IUpdateArticleInfoErrorResultContract>(result);
                    break;
            }
        }
    }
}