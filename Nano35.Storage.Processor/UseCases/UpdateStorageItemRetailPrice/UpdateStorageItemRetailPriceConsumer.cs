using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemRetailPrice
{
    public class UpdateStorageItemRetailPriceConsumer : IConsumer<IUpdateStorageItemRetailPriceRequestContract>
    {
        private readonly ApplicationContext _dbContext;
        private readonly ILogger<IUpdateStorageItemRetailPriceRequestContract> _logger;
        
        public UpdateStorageItemRetailPriceConsumer(
            ApplicationContext dbContext, 
            ILogger<IUpdateStorageItemRetailPriceRequestContract> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        
        public async Task Consume(ConsumeContext<IUpdateStorageItemRetailPriceRequestContract> context)
        {
            var result = await new LoggedPipeNode<IUpdateStorageItemRetailPriceRequestContract, IUpdateStorageItemRetailPriceResultContract>(_logger,
                new ValidatedUpdateStorageItemRetailPriceRequest(
                    new TransactedPipeNode<IUpdateStorageItemRetailPriceRequestContract, IUpdateStorageItemRetailPriceResultContract>(_dbContext, 
                        new UpdateStorageItemRetailPriceRequest(_dbContext)))).Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case IUpdateStorageItemRetailPriceSuccessResultContract:
                    await context.RespondAsync<IUpdateStorageItemRetailPriceSuccessResultContract>(result);
                    break;
                case IUpdateStorageItemRetailPriceErrorResultContract:
                    await context.RespondAsync<IUpdateStorageItemRetailPriceErrorResultContract>(result);
                    break;
            }
        }
    }
}