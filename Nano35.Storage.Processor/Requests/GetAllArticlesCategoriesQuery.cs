using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests
{
    public class GetAllArticlesCategoriesQuery :
        IGetAllArticlesCategoriesRequestContract, 
        ICommandRequest<IGetAllArticlesCategoriesResultContract>
    {
        public Guid InstanceId { get; set; }
        public Guid ParentId { get; set; }

        private class GetAllArticlesCategoriesSuccessResultContract : 
            IGetAllArticlesCategoriesSuccessResultContract
        {
            public IEnumerable<ICategoryViewModel> Data { get; set; }
        }

        private class GetAllArticlesCategoriesErrorResultContract :
            IGetAllArticlesCategoriesErrorResultContract
        {
            public string Message { get; set; }
        }

        public class GetAllArticlesCategoriesHandler : 
            IRequestHandler<GetAllArticlesCategoriesQuery, IGetAllArticlesCategoriesResultContract>
        {
            private readonly ApplicationContext _context;
            private readonly IBus _bus;
            
            public GetAllArticlesCategoriesHandler(
                ApplicationContext context, 
                IBus bus)
            {
                _context = context;
                _bus = bus;
            }
        
            public async Task<IGetAllArticlesCategoriesResultContract> Handle(
                GetAllArticlesCategoriesQuery message, 
                CancellationToken cancellationToken)
            {
                try
                {
                    var result = message.ParentId == Guid.Empty
                        ? await _context.Categorys.Where(c => c.InstanceId == message.InstanceId)
                            .MapAllToAsync<ICategoryViewModel>()
                        : await _context.Categorys.Where(c => c.InstanceId == message.InstanceId && c.ParentCategoryId == message.ParentId)
                            .MapAllToAsync<ICategoryViewModel>();

                    return new GetAllArticlesCategoriesSuccessResultContract() { Data = result };
                }
                catch (Exception e)
                {
                    return new GetAllArticlesCategoriesErrorResultContract() { Message = "!!!"};
                }
            }
        }
    }
}