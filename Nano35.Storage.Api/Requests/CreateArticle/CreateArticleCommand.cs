using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests.Behaviours;

namespace Nano35.Storage.Api.Requests.CreateArticle
{
    public class CreateArticleCommand :
        ICreateArticleRequestContract, 
        ICommandRequest<ICreateArticleResultContract>
    {
        public Guid NewId { get; set; }
        public Guid InstanceId { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public Guid CategoryId { get; set; }
    }
}