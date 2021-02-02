﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests
{
    public class GetAllArticlesCategoriesQuery :
        IGetAllArticlesCategoriesRequestContract, 
        ICommandRequest<IGetAllArticlesCategoriesResultContract>
    {
        public Guid InstanceId { get; set; }
        
        private class GetAllArticlesCategoriesSuccessResultContract : 
            IGetAllArticlesCategoriesSuccessResultContract
        {
            public IEnumerable<string> Data { get; set; }
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
                    var result = await _context
                        .Articles
                        .Where(c => c.InstanceId == message.InstanceId)
                        .Select(a => a.Category.Name)
                        .Distinct()
                        .ToListAsync();
                    
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