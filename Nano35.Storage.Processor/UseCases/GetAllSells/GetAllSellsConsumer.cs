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
            var result =
                await new LoggedUseCasePipeNode<IGetAllSellsRequestContract, IGetAllSellsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllSellsRequestContract>)) as ILogger<IGetAllSellsRequestContract>,
                        new GetAllSellsRequest(                            
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}