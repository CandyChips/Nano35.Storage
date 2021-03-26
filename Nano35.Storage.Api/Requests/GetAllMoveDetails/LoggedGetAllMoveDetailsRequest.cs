using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllMoveDetails
{
    public class LoggedGetAllMoveDetailsRequest :
        PipeNodeBase<IGetAllMoveDetailsRequestContract, IGetAllMoveDetailsResultContract>
    {
        private readonly ILogger<LoggedGetAllMoveDetailsRequest> _logger;

        public LoggedGetAllMoveDetailsRequest(
            ILogger<LoggedGetAllMoveDetailsRequest> logger,
            IPipeNode<IGetAllMoveDetailsRequestContract, IGetAllMoveDetailsResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllMoveDetailsResultContract> Ask(
            IGetAllMoveDetailsRequestContract input)
        {
            _logger.LogInformation($"GetAllMoveDetailsLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAllMoveDetailsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}