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
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IPresentationGetAllCategoriesRequestContract>) _services.GetService(typeof(ILogger<IPresentationGetAllCategoriesRequestContract>));
            var message = context.Message;
            var result = await new LoggedPipeNode<IPresentationGetAllCategoriesRequestContract, IPresentationGetAllCategoriesResultContract>(logger,
                new PresentationGetAllCategoriesRequest(dbContext)).Ask(message, context.CancellationToken);
            switch (result)
            {
                case IPresentationGetAllCategoriesSuccessResultContract:
                    await context.RespondAsync<IPresentationGetAllCategoriesSuccessResultContract>(result);
                    break;
                case IPresentationGetAllCategoriesErrorResultContract:
                    await context.RespondAsync<IPresentationGetAllCategoriesErrorResultContract>(result);
                    break;
            }
        }
    }
}