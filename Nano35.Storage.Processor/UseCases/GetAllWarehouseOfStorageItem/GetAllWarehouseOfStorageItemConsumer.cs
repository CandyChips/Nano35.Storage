using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllWarehouseOfStorageItem
{
    public class GetAllWarehouseOfStorageItemConsumer : 
        IConsumer<IGetAllWarehouseOfStorageItemRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllWarehouseOfStorageItemConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllWarehouseOfStorageItemRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllWarehouseOfStorageItemRequest>) _services.GetService(typeof(ILogger<LoggedGetAllWarehouseOfStorageItemRequest>));

            var message = context.Message;

            var result = await new LoggedGetAllWarehouseOfStorageItemRequest(logger,
                        new ValidatedGetAllWarehouseOfStorageItemRequest(
                            new GetAllWarehouseOfStorageItemRequest(dbContext, bus))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllWarehouseOfStorageItemSuccessResultContract:
                    await context.RespondAsync<IGetAllWarehouseOfStorageItemSuccessResultContract>(result);
                    break;
                case IGetAllWarehouseOfStorageItemErrorResultContract:
                    await context.RespondAsync<IGetAllWarehouseOfStorageItemErrorResultContract>(result);
                    break;
            }
        }
    }
}