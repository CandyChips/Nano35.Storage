using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllWarehousesOfUnit
{
    public class GetAllWarehousesOfUnitConsumer : 
        IConsumer<IGetAllWarehousesOfUnitRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllWarehousesOfUnitConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllWarehousesOfUnitRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetAllWarehousesOfUnitRequest>) _services
                .GetService(typeof(ILogger<LoggedGetAllWarehousesOfUnitRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedGetAllWarehousesOfUnitRequest(logger,
                    new ValidatedGetAllWarehousesOfUnitRequest(
                        new GetAllWarehousesOfUnitRequest(dbContext))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IGetAllWarehousesOfUnitSuccessResultContract:
                    await context.RespondAsync<IGetAllWarehousesOfUnitSuccessResultContract>(result);
                    break;
                case IGetAllWarehousesOfUnitErrorResultContract:
                    await context.RespondAsync<IGetAllWarehousesOfUnitErrorResultContract>(result);
                    break;
            }
        }
    }
}