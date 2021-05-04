using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateSalle
{
    public class CreateSelleConsumer : 
        IConsumer<ICreateSelleRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public CreateSelleConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<ICreateSelleRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<ICreateSelleRequestContract, ICreateSelleResultContract>(
                        _services.GetService(typeof(ILogger<ICreateSelleRequestContract>)) as
                            ILogger<ICreateSelleRequestContract>,
                        new TransactedUseCasePipeNode<ICreateSelleRequestContract,
                            ICreateSelleResultContract>(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            new CreateSelleRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                                _services.GetService(typeof(IBus)) as IBus)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}