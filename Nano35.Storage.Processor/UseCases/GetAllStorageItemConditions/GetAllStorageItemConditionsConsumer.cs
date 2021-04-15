using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItemConditions
{
    public class GetAllStorageItemConditionsConsumer : 
        IConsumer<IGetAllStorageItemConditionsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllStorageItemConditionsConsumer(IServiceProvider services) { _services = services; }

        public async Task Consume(
            ConsumeContext<IGetAllStorageItemConditionsRequestContract> context)
        {
            var result = await new LoggedPipeNode<IGetAllStorageItemConditionsRequestContract, IGetAllStorageItemConditionsResultContract>(
                _services.GetService(typeof(ILogger<IGetAllStorageItemConditionsRequestContract>)) as ILogger<IGetAllStorageItemConditionsRequestContract>,
                new GetAllStorageItemConditionsRequest(
                    _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                .Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case IGetAllStorageItemConditionsSuccessResultContract:
                    await context.RespondAsync<IGetAllStorageItemConditionsSuccessResultContract>(result);
                    break;
                case IGetAllStorageItemConditionsErrorResultContract:
                    await context.RespondAsync<IGetAllStorageItemConditionsErrorResultContract>(result);
                    break;
            }
        }
    }
}