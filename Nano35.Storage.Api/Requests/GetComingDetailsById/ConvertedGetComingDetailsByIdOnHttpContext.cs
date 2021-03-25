using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetComingDetailsById
{
    public class ConvertedGetComingDetailsByIdOnHttpContext : 
        PipeInConvert
        <GetComingDetailsByIdHttpQuery, 
            IActionResult,
            IGetComingDetailsByIdRequestContract, 
            IGetComingDetailsByIdResultContract>
    {
        public ConvertedGetComingDetailsByIdOnHttpContext(
            IPipeNode<IGetComingDetailsByIdRequestContract, IGetComingDetailsByIdResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetComingDetailsByIdHttpQuery input)
        {
            var converted = new GetComingDetailsByIdRequestContract()
            {
                Id = input.Id
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetComingDetailsByIdSuccessResultContract success => new OkObjectResult(success),
                IGetComingDetailsByIdErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}