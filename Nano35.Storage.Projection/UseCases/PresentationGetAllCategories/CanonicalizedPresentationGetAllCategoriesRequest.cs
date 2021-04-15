using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.storage;

namespace Nano35.Storage.Projection.UseCases.PresentationGetAllCategories
{
    public class CanonicalizedPresentationGetAllCategoriesRequestRequest : 
        PipeInConvert
        <PresentationGetAllCategoriesHttpQuery, 
            IActionResult,
            IPresentationGetAllCategoriesRequestContract, 
            IPresentationGetAllCategoriesResultContract>
    {
        public CanonicalizedPresentationGetAllCategoriesRequestRequest(
            IPipeNode<IPresentationGetAllCategoriesRequestContract, IPresentationGetAllCategoriesResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(PresentationGetAllCategoriesHttpQuery input)
        {
            var converted = new PresentationGetAllCategoriesRequestContract()
            {
                InstanceId = input.InstanceId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IPresentationGetAllCategoriesSuccessResultContract success => new OkObjectResult(success),
                IPresentationGetAllCategoriesErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}