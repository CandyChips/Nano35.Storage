using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetArticleById
{
    public class GetArticleByIdConsumer : 
        IConsumer<IGetArticleByIdRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetArticleByIdConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<IGetArticleByIdRequestContract> context)
        {
            var result = await new LoggedPipeNode<IGetArticleByIdRequestContract, IGetArticleByIdResultContract>(
                _services.GetService(typeof(ILogger<IGetArticleByIdRequestContract>)) as ILogger<IGetArticleByIdRequestContract>,
                new GetArticleByIdRequest(
                    _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                .Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case IGetArticleByIdSuccessResultContract:
                    await context.RespondAsync<IGetArticleByIdSuccessResultContract>(result);
                    break;
                case IGetArticleByIdErrorResultContract:
                    await context.RespondAsync<IGetArticleByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}