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
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var result = await new LoggedPipeNode<ICreateComingRequestContract, ICreateComingResultContract>(
                _services.GetService(typeof(ILogger<ICreateComingRequestContract>)) as ILogger<ICreateComingRequestContract>,
                new TransactedPipeNode<ICreateComingRequestContract, ICreateComingResultContract>(dbContext,
                    new CreateComingRequest(
                        dbContext, 
                        _services.GetService(typeof(IBus)) as IBus)))
                .Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case ICreateComingSuccessResultContract: await context.RespondAsync<ICreateComingSuccessResultContract>(result); break;
                case ICreateComingErrorResultContract: await context.RespondAsync<ICreateComingErrorResultContract>(result); break;
            }
        }
    }
}