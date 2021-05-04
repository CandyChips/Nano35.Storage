using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllCancellations
{
    public class GetAllCancellationsConsumer : 
        IConsumer<IGetAllCancellationsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllCancellationsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllCancellationsRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllCancellationsRequestContract, IGetAllCancellationsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllCancellationsRequestContract>)) as ILogger<IGetAllCancellationsRequestContract>,
                        new GetAllCancellationsRequest(                            
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}