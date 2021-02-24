using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests.GetAllWarehousesOfItem;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Consumers
{
    public class GetAllWarehousesOfItemConsumer : 
        IConsumer<IGetAllWarehousesOfItemRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllWarehousesOfItemConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllWarehousesOfItemRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetAllWarehousesOfItemRequest>) _services
                .GetService(typeof(ILogger<LoggedGetAllWarehousesOfItemRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedGetAllWarehousesOfItemRequest(logger,
                    new ValidatedGetAllWarehousesOfItemRequest(
                        new GetAllWarehousesOfItemRequest(dbContext))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IGetAllWarehousesOfItemSuccessResultContract:
                    await context.RespondAsync<IGetAllWarehousesOfItemSuccessResultContract>(result);
                    break;
                case IGetAllWarehousesOfItemErrorResultContract:
                    await context.RespondAsync<IGetAllWarehousesOfItemErrorResultContract>(result);
                    break;
            }
        }
    }
}