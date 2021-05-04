using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllCancellationDetails
{
    public class GetAllCancellationDetailsConsumer : 
        IConsumer<IGetAllCancellationDetailsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllCancellationDetailsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllCancellationDetailsRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllCancellationDetailsRequestContract, IGetAllCancellationDetailsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllCancellationDetailsRequestContract>)) as ILogger<IGetAllCancellationDetailsRequestContract>,
                        new GetAllCancellationDetailsRequest(                            
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}