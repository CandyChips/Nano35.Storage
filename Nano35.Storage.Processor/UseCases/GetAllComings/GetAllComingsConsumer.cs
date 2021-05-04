using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllComings
{
    public class GetAllComingsConsumer : 
        IConsumer<IGetAllComingsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllComingsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllComingsRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllComingsRequestContract, IGetAllComingsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllComingsRequestContract>)) as ILogger<IGetAllComingsRequestContract>,
                        new GetAllComingsRequest(                            
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}