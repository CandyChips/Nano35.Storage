using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllComingDetails
{
    public class GetAllComingDetailsConsumer : 
        IConsumer<IGetAllComingDetailsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllComingDetailsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllComingDetailsRequestContract> context)
        {
            var result = await new LoggedPipeNode<IGetAllComingDetailsRequestContract, IGetAllComingDetailsResultContract>(
                _services.GetService(typeof(ILogger<IGetAllComingDetailsRequestContract>)) as ILogger<IGetAllComingDetailsRequestContract>,
                new GetAllComingDetailsRequest(
                    _services.GetService(typeof(ApplicationContext)) as ApplicationContext, 
                    _services.GetService(typeof(IBus)) as IBus))
                .Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case IGetAllComingDetailsSuccessResultContract:
                    await context.RespondAsync<IGetAllComingDetailsSuccessResultContract>(result);
                    break;
                case IGetAllComingDetailsErrorResultContract:
                    await context.RespondAsync<IGetAllComingDetailsErrorResultContract>(result);
                    break;
            }
        }
    }
}