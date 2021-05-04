using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllArticleCategories
{
    public class GetAllArticlesCategoriesConsumer : 
        IConsumer<IGetAllArticlesCategoriesRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllArticlesCategoriesConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllArticlesCategoriesRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllArticlesCategoriesRequestContract, IGetAllArticlesCategoriesResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllArticlesCategoriesRequestContract>)) as ILogger<IGetAllArticlesCategoriesRequestContract>,
                        new GetAllArticlesCategoriesRequest(                            
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}