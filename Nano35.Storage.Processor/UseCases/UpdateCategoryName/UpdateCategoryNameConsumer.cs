using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateCategoryName
{
    public class UpdateCategoryNameConsumer : 
        IConsumer<IUpdateCategoryNameRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public UpdateCategoryNameConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateCategoryNameRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IUpdateCategoryNameRequestContract, IUpdateCategoryNameResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateCategoryNameRequestContract>)) as
                            ILogger<IUpdateCategoryNameRequestContract>,
                        new TransactedUseCasePipeNode<IUpdateCategoryNameRequestContract,
                            IUpdateCategoryNameResultContract>(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            new UpdateCategoryNameRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}