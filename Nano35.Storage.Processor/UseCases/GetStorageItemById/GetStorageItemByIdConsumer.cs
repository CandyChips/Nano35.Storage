using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetStorageItemById
{
    public class GetStorageItemByIdConsumer : 
        IConsumer<IGetStorageItemByIdRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetStorageItemByIdConsumer(IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetStorageItemByIdRequestContract> context)
        {
            var result = await new LoggedPipeNode<IGetStorageItemByIdRequestContract, IGetStorageItemByIdResultContract>(
                _services.GetService(typeof(ILogger<IGetStorageItemByIdRequestContract>)) as ILogger<IGetStorageItemByIdRequestContract>,
                new GetStorageItemByIdRequest(
                    _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                    _services.GetService(typeof(IBus)) as IBus))
                .Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case IGetStorageItemByIdSuccessResultContract:
                    await context.RespondAsync<IGetStorageItemByIdSuccessResultContract>(result);
                    break;
                case IGetStorageItemByIdErrorResultContract:
                    await context.RespondAsync<IGetStorageItemByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}