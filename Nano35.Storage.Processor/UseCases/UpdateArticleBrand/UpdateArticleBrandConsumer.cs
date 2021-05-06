using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleBrand
{
    public class UpdateArticleBrandConsumer : 
        IConsumer<IUpdateArticleBrandRequestContract>
    {
        private readonly IServiceProvider _services;
        public UpdateArticleBrandConsumer(IServiceProvider services)
        {
            _services = services;
        }
        public async Task Consume(ConsumeContext<IUpdateArticleBrandRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IUpdateArticleBrandRequestContract, IUpdateArticleBrandResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateArticleBrandRequestContract>)) as
                            ILogger<IUpdateArticleBrandRequestContract>,
                        new TransactedUseCasePipeNode<IUpdateArticleBrandRequestContract,
                            IUpdateArticleBrandResultContract>(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            new UpdateArticleBrandRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}