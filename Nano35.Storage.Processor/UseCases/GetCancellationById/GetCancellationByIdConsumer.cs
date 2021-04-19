using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetCancellationById
{
    public class GetCancellationByIdConsumer : 
        IConsumer<IGetCancellationByIdRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetCancellationByIdConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<IGetCancellationByIdRequestContract> context)
        {
            var result = await new LoggedPipeNode<IGetCancellationByIdRequestContract, IGetCancellationByIdResultContract>(
                _services.GetService(typeof(ILogger<IGetCancellationByIdRequestContract>)) as ILogger<IGetCancellationByIdRequestContract>,
                new GetCancellationByIdRequest(
                    _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                    _services.GetService(typeof(IBus)) as IBus))
                .Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case IGetCancellationByIdSuccessResultContract:
                    await context.RespondAsync<IGetCancellationByIdSuccessResultContract>(result);
                    break;
                case IGetCancellationByIdErrorResultContract:
                    await context.RespondAsync<IGetCancellationByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}