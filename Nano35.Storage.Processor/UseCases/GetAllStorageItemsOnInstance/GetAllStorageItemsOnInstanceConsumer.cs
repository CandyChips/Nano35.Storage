using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItemsOnInstance
{
    public class GetAllStorageItemsOnInstanceConsumer : 
        IConsumer<IGetAllStorageItemsOnInstanceContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllStorageItemsOnInstanceConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllStorageItemsOnInstanceContract> context)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IGetAllStorageItemsOnInstanceContract>) _services
                .GetService(typeof(ILogger<IGetAllStorageItemsOnInstanceContract>));

            var message = context.Message;

            var result =
                await new LoggedPipeNode<IGetAllStorageItemsOnInstanceContract, IGetAllStorageItemsOnInstanceResultContract>(logger,
                    new ValidatedGetAllStorageItemsOnInstanceRequest(
                        new GetAllStorageItemsOnInstanceRequest(dbContext, bus))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllStorageItemsOnInstanceSuccessResultContract:
                    await context.RespondAsync<IGetAllStorageItemsOnInstanceSuccessResultContract>(result);
                    break;
                case IGetAllStorageItemsOnInstanceErrorResultContract:
                    await context.RespondAsync<IGetAllStorageItemsOnInstanceErrorResultContract>(result);
                    break;
            }
        }
    }
}