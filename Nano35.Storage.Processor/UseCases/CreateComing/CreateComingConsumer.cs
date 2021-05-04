using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateComing
{
    public class CreateComingConsumer : IConsumer<ICreateComingRequestContract>
    {
        private readonly IServiceProvider _services;
        public CreateComingConsumer(IServiceProvider services) { _services = services; }
        public async Task Consume(ConsumeContext<ICreateComingRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<ICreateComingRequestContract, ICreateComingResultContract>(
                        _services.GetService(typeof(ILogger<ICreateComingRequestContract>)) as
                            ILogger<ICreateComingRequestContract>,
                        new TransactedUseCasePipeNode<ICreateComingRequestContract,
                            ICreateComingResultContract>(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            new CreateComingRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            _services.GetService(typeof(IBus)) as IBus)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}