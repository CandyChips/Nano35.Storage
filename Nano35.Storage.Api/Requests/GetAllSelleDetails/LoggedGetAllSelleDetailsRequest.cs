using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllSelleDetails
{
    public class LoggedGetAllSelleDetailsRequest :
        PipeNodeBase<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract>
    {
        private readonly ILogger<LoggedGetAllSelleDetailsRequest> _logger;

        public LoggedGetAllSelleDetailsRequest(
            ILogger<LoggedGetAllSelleDetailsRequest> logger,
            IPipeNode<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllSelleDetailsResultContract> Ask(
            IGetAllSelleDetailsRequestContract input)
        {
            _logger.LogInformation($"GetAllSelleDetailsLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAllSelleDetailsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}