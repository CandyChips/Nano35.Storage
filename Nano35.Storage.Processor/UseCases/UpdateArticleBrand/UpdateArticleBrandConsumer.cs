using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleBrand
{
    public class UpdateArticleBrandConsumer : 
        IConsumer<IUpdateArticleBrandRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public UpdateArticleBrandConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateArticleBrandRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IUpdateArticleBrandRequestContract>) _services.GetService(typeof(ILogger<IUpdateArticleBrandRequestContract>));
            var message = context.Message;
            var result =
                await new LoggedPipeNode<IUpdateArticleBrandRequestContract, IUpdateArticleBrandResultContract>(logger,
                    new TransactedPipeNode<IUpdateArticleBrandRequestContract, IUpdateArticleBrandResultContract>(dbContext,
                        new UpdateArticleBrandRequest(dbContext))).Ask(message, context.CancellationToken);
            switch (result)
            {
                case IUpdateArticleBrandSuccessResultContract:
                    await context.RespondAsync<IUpdateArticleBrandSuccessResultContract>(result);
                    break;
                case IUpdateArticleBrandErrorResultContract:
                    await context.RespondAsync<IUpdateArticleBrandErrorResultContract>(result);
                    break;
            }
        }
    }
}