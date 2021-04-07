using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllComingDetails
{
    public class LoggedGetAllComingDetailsRequest :
        PipeNodeBase<
            IGetAllComingDetailsRequestContract, 
            IGetAllComingDetailsResultContract>
    {
        private readonly ILogger<LoggedGetAllComingDetailsRequest> _logger;

        public LoggedGetAllComingDetailsRequest(
            ILogger<LoggedGetAllComingDetailsRequest> logger,
            IPipeNode<IGetAllComingDetailsRequestContract,
                IGetAllComingDetailsResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllComingDetailsResultContract> Ask(
            IGetAllComingDetailsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllComingDetailsLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"GetAllComingDetailsLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}