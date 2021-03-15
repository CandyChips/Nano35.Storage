using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllPlacesOnStorage
{
    public class GetAllPlacesOnStorageConsumer : 
        IConsumer<IGetAllPlacesOnStorageContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllPlacesOnStorageConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllPlacesOnStorageContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetAllPlacesOnStorageRequest>) _services
                .GetService(typeof(ILogger<LoggedGetAllPlacesOnStorageRequest>));
            var message = context.Message;
            var result =
                await new LoggedGetAllPlacesOnStorageRequest(logger,
                    new ValidatedGetAllPlacesOnStorageRequest(
                        new GetAllPlacesOnStorageRequest(dbContext))
                ).Ask(message, context.CancellationToken);
            switch (result)
            {
                case IGetAllPlacesOnStorageSuccessResultContract:
                    await context.RespondAsync<IGetAllPlacesOnStorageSuccessResultContract>(result);
                    break;
                case IGetAllPlacesOnStorageErrorResultContract:
                    await context.RespondAsync<IGetAllPlacesOnStorageErrorResultContract>(result);
                    break;
            }
        }
    }
}