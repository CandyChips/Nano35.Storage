using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.PresentationGetAllArticles
{
    public class PresentationGetAllArticlesConsumer : 
        IConsumer<IPresentationGetAllArticlesRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public PresentationGetAllArticlesConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IPresentationGetAllArticlesRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IPresentationGetAllArticlesRequestContract>) _services.GetService(typeof(ILogger<IPresentationGetAllArticlesRequestContract>));
            var message = context.Message;
            var result = await new LoggedPipeNode<IPresentationGetAllArticlesRequestContract, IPresentationGetAllArticlesResultContract>(logger,
                new PresentationGetAllArticlesRequest(dbContext)).Ask(message, context.CancellationToken);
            switch (result)
            {
                case IPresentationGetAllArticlesSuccessResultContract:
                    await context.RespondAsync<IPresentationGetAllArticlesSuccessResultContract>(result);
                    break;
                case IPresentationGetAllArticlesErrorResultContract:
                    await context.RespondAsync<IPresentationGetAllArticlesErrorResultContract>(result);
                    break;
            }
        }
    }
}