using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.CreateCategory
{
    public class ConvertedCreateCategoryOnHttpContext : 
        PipeInConvert
        <CreateCategoryHttpBody, 
            IActionResult,
            ICreateCategoryRequestContract, 
            ICreateCategoryResultContract>
    {
        public ConvertedCreateCategoryOnHttpContext(IPipeNode<ICreateCategoryRequestContract, ICreateCategoryResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(CreateCategoryHttpBody input)
        {
            var converted = new CreateCategoryRequestContract()
            {
                InstanceId = input.InstanceId,
                NewId = input.NewId,
                Name = input.Name,
                ParentCategoryId = input.ParentCategoryId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                ICreateCategorySuccessResultContract success => new OkObjectResult(success),
                ICreateCategoryErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}