using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllSells
{
    public class GetAllSellsConsumer : 
        IConsumer<IGetAllSellsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllSellsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllSellsRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<IGetAllSellsRequestContract>) _services.GetService(typeof(ILogger<IGetAllSellsRequestContract>));
            var message = context.Message;
            var result =
                await new LoggedPipeNode<IGetAllSellsRequestContract, IGetAllSellsResultContract>(logger,
                    new GetAllSellsRequest(dbContext, bus)).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllSellsSuccessResultContract:
                    await context.RespondAsync<IGetAllSellsSuccessResultContract>(result);
                    break;
                case IGetAllSellsErrorResultContract:
                    await context.RespondAsync<IGetAllSellsErrorResultContract>(result);
                    break;
            }
        }
    }
}