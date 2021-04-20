using System;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.storage;
using Nano35.Storage.Api.Requests;
using Nano35.Storage.Api.Requests.CreateArticle;
using Nano35.Storage.Api.Requests.GetAllArticleBrands;
using Nano35.Storage.Api.Requests.GetAllArticleModels;
using Nano35.Storage.Api.Requests.GetAllArticles;
using Nano35.Storage.Api.Requests.GetArticleById;
using Nano35.Storage.Api.Requests.UpdateArticleBrand;
using Nano35.Storage.Api.Requests.UpdateArticleCategory;
using Nano35.Storage.Api.Requests.UpdateArticleInfo;
using Nano35.Storage.Api.Requests.UpdateArticleModel;

namespace Nano35.Storage.Api.Controllers
{
    /// ToDo Hey Maslyonok
    /// <summary>
    /// http://localhost:6003/articles/[action]
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController :
        ControllerBase
    {
        
        /// ToDo Hey Maslyonok
        /// <summary>
        /// _services provide a DI services to request
        /// </summary>
        private readonly IServiceProvider _services;
        
        /// ToDo Hey Maslyonok
        /// <summary>
        /// Controller provide IServiceProvider from asp net core DI
        /// for registration services to pipe nodes
        /// </summary>
        public ArticlesController(
            IServiceProvider services)
        {
            _services = services;
        }
    
        /// ToDo Hey Maslyonok
        /// <summary>
        /// GET -> http://localhost:5104/articles/GetAllArticles
        /// success -> Articles[]
        /// error -> string
        /// ---$---$---
        /// Controllers accept a HttpContext type
        /// All controllers actions works by pipelines
        /// Implementation works with 3 steps
        /// 1. Setup DI services from IServiceProvider;
        /// 2. Building pipeline like a onion
        ///     '''(PipeNode1(PipeNode2(PipeNode3(...).Ask()).Ask()).Ask()).Ask()''';
        /// 3. Response pattern match of pipeline response;
        /// </summary>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllArticlesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllArticlesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllArticles(
            [FromQuery] GetAllArticlesHttpQuery query)
        {
            return await 
                new ValidatedPipeNode<GetAllArticlesHttpQuery, IActionResult>(
                    _services.GetService(typeof(IValidator<GetAllArticlesHttpQuery>)) as IValidator<GetAllArticlesHttpQuery>, 
                    new ConvertedGetAllArticlesOnHttpContext(
                        new LoggedPipeNode<IGetAllArticlesRequestContract, IGetAllArticlesResultContract>(
                            _services.GetService(typeof(ILogger<IGetAllArticlesRequestContract>)) as ILogger<IGetAllArticlesRequestContract>,
                            new GetAllArticlesUseCase(_services.GetService(typeof(IBus)) as IBus))))
                .Ask(query);
            
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetArticleByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetArticleByIdErrorHttpResponse))] 
        public async Task<IActionResult> GetArticleById(Guid id)
        {
            return await 
                new ValidatedPipeNode<Guid, IActionResult>(
                    _services.GetService(typeof(IValidator<Guid>)) as IValidator<Guid>, 
                    new ConvertedGetArticleByIdOnHttpContext(
                        new LoggedPipeNode<IGetArticleByIdRequestContract, IGetArticleByIdResultContract>(
                            _services.GetService(typeof(ILogger<IGetArticleByIdRequestContract>)) as ILogger<IGetArticleByIdRequestContract>,
                            new GetArticleByIdUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(id);
            
        }
        
        /// <summary>
        /// Creates a Article.
        /// </summary>
        /// <remarks>
        /// Note that the NewId, InstanceId and CategoryId is a GUID and not an integer.
        ///  
        ///     POST /CreateArticle
        ///     {
        ///        "NewId": "0e7ad584-7788-4ab1-95a6-ca0a5b444cbb",
        ///        "InstanceId": "0e7ad584-7788-4ab1-95a6-ca0a5b444cbb",
        ///        "Model": "Cool model",
        ///        "Brand": "Super brand",
        ///        "CategoryId": "0e7ad584-7788-4ab1-95a6-ca0a5b444cbb",
        ///        "Info": "Awesome thing",
        ///        "Specs":
        ///        {
        ///            "Key": "Color",
        ///            "Value": "Black",
        ///        }
        ///     }
        /// 
        /// </remarks>
        /// <param name="body"></param>
        /// <returns>New Created Article</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [Route("Article")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateArticleSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateArticleErrorHttpResponse))] 
        public async Task<IActionResult> CreateArticle(
            [FromBody] CreateArticleHttpBody body)
        {
            return await 
                new ValidatedPipeNode<CreateArticleHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<CreateArticleHttpBody>)) as IValidator<CreateArticleHttpBody>,
                    new ConvertedCreateArticleOnHttpContext(
                        new LoggedPipeNode<ICreateArticleRequestContract, ICreateArticleResultContract>(
                            _services.GetService(typeof(ILogger<ICreateArticleRequestContract>)) as ILogger<ICreateArticleRequestContract>,
                            new CreateArticleUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }

        [HttpPatch]
        [Route("Brand")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateArticleBrandSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateArticleBrandErrorHttpResponse))] 
        public async Task<IActionResult> UpdateArticleBrand(
            [FromBody] UpdateArticleBrandHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateArticleBrandHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateArticleBrandHttpBody>)) as IValidator<UpdateArticleBrandHttpBody>,
                    new ConvertedUpdateArticleBrandOnHttpContext(
                        new LoggedPipeNode<IUpdateArticleBrandRequestContract, IUpdateArticleBrandResultContract>(
                                _services.GetService(typeof(ILogger<IUpdateArticleBrandRequestContract>)) as ILogger<IUpdateArticleBrandRequestContract>,
                                new UpdateArticleBrandUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }
        
        [HttpPatch]
        [Route("Category")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateArticleCategorySuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateArticleCategoryErrorHttpResponse))] 
        public async Task<IActionResult> UpdateArticleCategory(
            [FromBody] UpdateArticleCategoryHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateArticleCategoryHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateArticleCategoryHttpBody>)) as IValidator<UpdateArticleCategoryHttpBody>,
                        new ConvertedUpdateArticleCategoryOnHttpContext(
                        new LoggedPipeNode<IUpdateArticleCategoryRequestContract, IUpdateArticleCategoryResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateArticleCategoryRequestContract>)) as ILogger<IUpdateArticleCategoryRequestContract>,
                            new UpdateArticleCategoryUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }
        
        [HttpPatch]
        [Route("Info")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateArticleInfoSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateArticleInfoErrorHttpResponse))] 
        public async Task<IActionResult> UpdateArticleInfo(
            [FromBody] UpdateArticleInfoHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateArticleInfoHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateArticleInfoHttpBody>)) as IValidator<UpdateArticleInfoHttpBody>,
                    new ConvertedUpdateArticleInfoOnHttpContext(
                        new LoggedPipeNode<IUpdateArticleInfoRequestContract, IUpdateArticleInfoResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateArticleInfoRequestContract>)) as ILogger<IUpdateArticleInfoRequestContract>,
                            new UpdateArticleInfoUseCase(_services.GetService(typeof(IBus)) as IBus))))
                .Ask(body);
        }
        
        [HttpPatch]
        [Route("Model")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateArticleModelSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateArticleModelErrorHttpResponse))] 
        public async Task<IActionResult> UpdateArticleModel(
            [FromBody] UpdateArticleModelHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateArticleModelHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateArticleModelHttpBody>)) as IValidator<UpdateArticleModelHttpBody>,
                    new ConvertedUpdateArticleModelOnHttpContext(
                        new LoggedPipeNode<IUpdateArticleModelRequestContract, IUpdateArticleModelResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateArticleModelRequestContract>)) as ILogger<IUpdateArticleModelRequestContract>,
                            new UpdateArticleModelUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteArticle()
        {
            return Ok();
        }
    }
}