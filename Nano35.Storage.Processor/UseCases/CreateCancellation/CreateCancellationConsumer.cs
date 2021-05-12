using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateCancellation
{
    public class CreateCancellationConsumer : IConsumer<ICreateCancellationRequestContract>
    {
        private readonly IServiceProvider _services;
        public CreateCancellationConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<ICreateCancellationRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<ICreateCancellationRequestContract, ICreateCancellationResultContract>(
                        _services.GetService(typeof(ILogger<ICreateCancellationRequestContract>)) as
                            ILogger<ICreateCancellationRequestContract>,
                        new TransactedUseCasePipeNode<ICreateCancellationRequestContract,
                            ICreateCancellationResultContract>(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            new CreateCancellationRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                                _services.GetService(typeof(IBus)) as IBus)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}