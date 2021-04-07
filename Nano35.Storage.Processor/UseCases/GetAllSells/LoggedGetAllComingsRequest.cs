using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllSells
{
    public class LoggedGetAllSellsRequest :
        PipeNodeBase<
            IGetAllSellsRequestContract, 
            IGetAllSellsResultContract>
    {
        private readonly ILogger<LoggedGetAllSellsRequest> _logger;

        public LoggedGetAllSellsRequest(
            ILogger<LoggedGetAllSellsRequest> logger,
            IPipeNode<IGetAllSellsRequestContract,
                IGetAllSellsResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllSellsResultContract> Ask(
            IGetAllSellsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllSellsLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"GetAllSellsLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}