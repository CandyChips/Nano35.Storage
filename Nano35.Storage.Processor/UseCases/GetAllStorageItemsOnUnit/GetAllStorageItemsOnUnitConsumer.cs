using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItemsOnUnit
{
    public class GetAllStorageItemsOnUnitConsumer : 
        IConsumer<IGetAllStorageItemsOnUnitContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllStorageItemsOnUnitConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllStorageItemsOnUnitContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<IGetAllStorageItemsOnUnitContract>) _services
                .GetService(typeof(ILogger<IGetAllStorageItemsOnUnitContract>));
            var message = context.Message;
            var result =
                await new LoggedPipeNode<IGetAllStorageItemsOnUnitContract, IGetAllStorageItemsOnUnitResultContract>(logger,
                    new ValidatedGetAllStorageItemsOnUnitRequest(
                        new GetAllStorageItemsOnUnitRequest(dbContext, bus)))
                    .Ask(message, context.CancellationToken);
            switch (result)
            {
                case IGetAllStorageItemsOnUnitSuccessResultContract:
                    await context.RespondAsync<IGetAllStorageItemsOnUnitSuccessResultContract>(result);
                    break;
                case IGetAllStorageItemsOnUnitErrorResultContract:
                    await context.RespondAsync<IGetAllStorageItemsOnUnitErrorResultContract>(result);
                    break;
            }
        }
    }
}