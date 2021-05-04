using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllSelleDetails
{
    public class GetAllSelleDetailsConsumer : 
        IConsumer<IGetAllSelleDetailsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllSelleDetailsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllSelleDetailsRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllSelleDetailsRequestContract>)) as ILogger<IGetAllSelleDetailsRequestContract>,
                        new GetAllSelleDetailsRequest(                            
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}