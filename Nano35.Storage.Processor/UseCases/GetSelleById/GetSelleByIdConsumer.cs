using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetSelleById
{
    public class GetSelleByIdConsumer : 
        IConsumer<IGetSelleByIdRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetSelleByIdConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<IGetSelleByIdRequestContract> context)
        {
            var result = await new LoggedPipeNode<IGetSelleByIdRequestContract, IGetSelleByIdResultContract>(
                _services.GetService(typeof(ILogger<IGetSelleByIdRequestContract>)) as ILogger<IGetSelleByIdRequestContract>,
                new GetSelleByIdRequest(
                    _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                    _services.GetService(typeof(IBus)) as IBus))
                .Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case IGetSelleByIdSuccessResultContract:
                    await context.RespondAsync<IGetSelleByIdSuccessResultContract>(result);
                    break;
                case IGetSelleByIdErrorResultContract:
                    await context.RespondAsync<IGetSelleByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}