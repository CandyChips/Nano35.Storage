using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.CreateArticle
{
    public class CanonicalizedCreateArticleRequest : 
        PipeInConvert
        <CreateArticleHttpBody, 
            IActionResult,
            ICreateArticleRequestContract, 
            ICreateArticleResultContract>
    {
        public CanonicalizedCreateArticleRequest(IPipeNode<ICreateArticleRequestContract, ICreateArticleResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(CreateArticleHttpBody input)
        {
            var converted = 
                new CreateArticleRequestContract()
                    {Brand = input.Brand,
                     CategoryId = input.CategoryId,
                     Info = input.Info ?? "",
                     InstanceId = input.InstanceId,
                     Model = input.Model,
                     NewId = input.NewId,
                     Specs = input
                         .Specs
                         .Select(a => 
                             new SpecViewModel()
                                 {Key = a.Key,
                                  Value = a.Value})
                         .ToList()};
            return await DoNext(converted) switch
            {
                ICreateArticleSuccessResultContract success => new OkObjectResult(success),
                ICreateArticleErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}