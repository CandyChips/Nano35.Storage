using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.PresentationGetAllCategories
{
    public class PresentationGetAllCategoriesConsumer : 
        IConsumer<IPresentationGetAllCategoriesRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public PresentationGetAllCategoriesConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IPresentationGetAllCategoriesRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IPresentationGetAllCategoriesRequestContract, IPresentationGetAllCategoriesResultContract>(
                        _services.GetService(typeof(ILogger<IPresentationGetAllCategoriesRequestContract>)) as
                            ILogger<IPresentationGetAllCategoriesRequestContract>,
                        new PresentationGetAllCategoriesRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}