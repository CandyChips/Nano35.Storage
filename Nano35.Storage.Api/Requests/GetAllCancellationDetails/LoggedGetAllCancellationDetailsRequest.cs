using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllCancellationDetails
{
    public class LoggedGetAllCancellationDetailsRequest :
        PipeNodeBase<IGetAllCancellationDetailsRequestContract, IGetAllCancellationDetailsResultContract>
    {
        private readonly ILogger<LoggedGetAllCancellationDetailsRequest> _logger;

        public LoggedGetAllCancellationDetailsRequest(
            ILogger<LoggedGetAllCancellationDetailsRequest> logger,
            IPipeNode<IGetAllCancellationDetailsRequestContract, IGetAllCancellationDetailsResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllCancellationDetailsResultContract> Ask(
            IGetAllCancellationDetailsRequestContract input)
        {
            _logger.LogInformation($"GetAllCancellationDetailsLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAllCancellationDetailsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}