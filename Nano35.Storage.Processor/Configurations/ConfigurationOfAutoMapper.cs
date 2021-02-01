using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Configurations
{
    public class AutoMapperConfiguration : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            // ToDo
            //var mapperConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new InstancesAutoMapperProfile());
            //    mc.AddProfile(new ClientsAutoMapperProfile());
            //    mc.AddProfile(new ClientStatesAutoMapperProfile());
            //    mc.AddProfile(new ClientTypesAutoMapperProfile());
            //    mc.AddProfile(new InstanceTypesAutoMapperProfile());
            //    mc.AddProfile(new RegionAutoMapperProfile());
            //    mc.AddProfile(new UnitsAutoMapperProfile());
            //    mc.AddProfile(new UnitTypesAutoMapperProfile());
            //    mc.AddProfile(new WorkersAutoMapperProfile());
            //    mc.AddProfile(new WorkersRoleAutoMapperProfile());
            //});
            //IMapper mapper = mapperConfig.CreateMapper();
            //MappingPipe.Mapper = mapper;
            //services.AddSingleton(mapper);
        }
    }
}