using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllPlacesOfStorageItemOnInstance
{
    public class GetAllPlacesOfStorageItemOnInstanceConsumer : 
        IConsumer<IGetAllPlacesOfStorageItemOnInstanceContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllPlacesOfStorageItemOnInstanceConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllPlacesOfStorageItemOnInstanceContract> context)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetAllPlacesOfStorageItemOnInstanceRequest>) _services
                .GetService(typeof(ILogger<LoggedGetAllPlacesOfStorageItemOnInstanceRequest>));
            var message = context.Message;
            var result =
                await new LoggedGetAllPlacesOfStorageItemOnInstanceRequest(logger,
                    new ValidatedGetAllPlacesOfStorageItemOnInstanceRequest(
                        new GetAllPlacesOfStorageItemOnInstanceRequest(dbContext, bus))
                ).Ask(message, context.CancellationToken);
            switch (result)
            {
                case IGetAllPlacesOfStorageItemOnInstanceSuccessResultContract:
                    await context.RespondAsync<IGetAllPlacesOfStorageItemOnInstanceSuccessResultContract>(result);
                    break;
                case IGetAllPlacesOfStorageItemOnInstanceErrorResultContract:
                    await context.RespondAsync<IGetAllPlacesOfStorageItemOnInstanceErrorResultContract>(result);
                    break;
            }
        }
    }
}