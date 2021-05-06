using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleCategory
{
    public class UpdateArticleCategoryConsumer : 
        IConsumer<IUpdateArticleCategoryRequestContract>
    {
        private readonly IServiceProvider _services;
        public UpdateArticleCategoryConsumer(IServiceProvider services)
        {
            _services = services;
        }
        public async Task Consume(ConsumeContext<IUpdateArticleCategoryRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IUpdateArticleCategoryRequestContract, IUpdateArticleCategoryResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateArticleCategoryRequestContract>)) as
                            ILogger<IUpdateArticleCategoryRequestContract>,
                        new TransactedUseCasePipeNode<IUpdateArticleCategoryRequestContract,
                            IUpdateArticleCategoryResultContract>(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            new UpdateArticleCategoryRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}