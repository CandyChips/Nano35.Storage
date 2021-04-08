using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllArticles
{
    public class GetAllArticlesConsumer : 
        IConsumer<IGetAllArticlesRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllArticlesConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllArticlesRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IGetAllArticlesRequestContract>) _services.GetService(typeof(ILogger<IGetAllArticlesRequestContract>));
            var message = context.Message;
            var result = await new LoggedPipeNode<IGetAllArticlesRequestContract, IGetAllArticlesResultContract>(logger,
                new GetAllArticlesRequest(dbContext)).Ask(message, context.CancellationToken);
            switch (result)
            {
                case IGetAllArticlesSuccessResultContract:
                    await context.RespondAsync<IGetAllArticlesSuccessResultContract>(result);
                    break;
                case IGetAllArticlesErrorResultContract:
                    await context.RespondAsync<IGetAllArticlesErrorResultContract>(result);
                    break;
            }
        }
    }
}