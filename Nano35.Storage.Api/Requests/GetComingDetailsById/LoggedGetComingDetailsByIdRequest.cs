using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetComingDetailsById
{
    public class LoggedGetComingDetailsByIdRequest :
        PipeNodeBase<IGetComingDetailsByIdRequestContract, IGetComingDetailsByIdResultContract>
    {
        private readonly ILogger<LoggedGetComingDetailsByIdRequest> _logger;

        public LoggedGetComingDetailsByIdRequest(
            ILogger<LoggedGetComingDetailsByIdRequest> logger,
            IPipeNode<IGetComingDetailsByIdRequestContract, IGetComingDetailsByIdResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetComingDetailsByIdResultContract> Ask(
            IGetComingDetailsByIdRequestContract input)
        {
            _logger.LogInformation($"GetComingDetailsByIdLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetComingDetailsByIdLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}