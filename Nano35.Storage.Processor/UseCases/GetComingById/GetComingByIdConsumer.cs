using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetComingById
{
    public class GetComingByIdConsumer : 
        IConsumer<IGetComingByIdRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetComingByIdConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<IGetComingByIdRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IGetComingByIdRequestContract, IGetComingByIdResultContract>(
                        _services.GetService(typeof(ILogger<IGetComingByIdRequestContract>)) as ILogger<IGetComingByIdRequestContract>,
                        new GetComingByIdRequest(                            
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}