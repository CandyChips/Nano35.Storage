using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateMove
{
    public class CreateMoveConsumer : 
        IConsumer<ICreateMoveRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public CreateMoveConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<ICreateMoveRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<ICreateMoveRequestContract, ICreateMoveResultContract>(
                        _services.GetService(typeof(ILogger<ICreateMoveRequestContract>)) as
                            ILogger<ICreateMoveRequestContract>,
                        new TransactedUseCasePipeNode<ICreateMoveRequestContract,
                            ICreateMoveResultContract>(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            new CreateMoveRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                                _services.GetService(typeof(IBus)) as IBus)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}