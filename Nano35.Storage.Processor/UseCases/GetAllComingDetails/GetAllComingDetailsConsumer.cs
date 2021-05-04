using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllComingDetails
{
    public class GetAllComingDetailsConsumer : 
        IConsumer<IGetAllComingDetailsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllComingDetailsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllComingDetailsRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllComingDetailsRequestContract, IGetAllComingDetailsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllComingDetailsRequestContract>)) as ILogger<IGetAllComingDetailsRequestContract>,
                        new GetAllComingDetailsRequest(                            
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}