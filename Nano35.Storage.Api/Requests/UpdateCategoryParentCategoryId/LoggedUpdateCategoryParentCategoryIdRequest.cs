using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateCategoryParentCategoryId
{
    public class LoggedUpdateCategoryParentCategoryIdRequest :
        PipeNodeBase<IUpdateCategoryParentCategoryIdRequestContract, IUpdateCategoryParentCategoryIdResultContract>
    {
        private readonly ILogger<LoggedUpdateCategoryParentCategoryIdRequest> _logger;

        public LoggedUpdateCategoryParentCategoryIdRequest(
            ILogger<LoggedUpdateCategoryParentCategoryIdRequest> logger,
            IPipeNode<IUpdateCategoryParentCategoryIdRequestContract, IUpdateCategoryParentCategoryIdResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateCategoryParentCategoryIdResultContract> Ask(
            IUpdateCategoryParentCategoryIdRequestContract input)
        {
            _logger.LogInformation($"UpdateCategoryParentCategoryIdLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"UpdateCategoryParentCategoryIdLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}