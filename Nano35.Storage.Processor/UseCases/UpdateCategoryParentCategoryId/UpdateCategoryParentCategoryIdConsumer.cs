using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateCategoryParentCategoryId
{
    public class UpdateCategoryParentCategoryIdConsumer : 
        IConsumer<IUpdateCategoryParentCategoryIdRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public UpdateCategoryParentCategoryIdConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateCategoryParentCategoryIdRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IUpdateCategoryParentCategoryIdRequestContract, IUpdateCategoryParentCategoryIdResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateCategoryParentCategoryIdRequestContract>)) as
                            ILogger<IUpdateCategoryParentCategoryIdRequestContract>,
                        new TransactedUseCasePipeNode<IUpdateCategoryParentCategoryIdRequestContract,
                            IUpdateCategoryParentCategoryIdResultContract>(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            new UpdateCategoryParentCategoryIdRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}