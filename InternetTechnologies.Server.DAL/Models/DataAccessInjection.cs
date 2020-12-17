using Autofac;
using InternetTechnologies.DomainModels.Models.Entities;
using InternetTechnologies.DomainModels.Services.Interfaces;
using InternetTechnologies.Server.DAL.Services.Repositories.XmlRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace InternetTechnologies.Server.DAL.Models
{
    public static class DataAccessInjection
    {
        public static ContainerBuilder GetContainerBuilder(string pathToFile)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder
                            .RegisterType(typeof(MedicalCard))
                            .AsSelf();

            containerBuilder.RegisterType(typeof(MedicalCardXmlRepository))
                            .As(typeof(IRepository<MedicalCard>))
                            .WithParameter(nameof(pathToFile), pathToFile);


            return containerBuilder;
        }

        public static IServiceCollection GetServiceCollection(string pathToFile)
        {
            var services = new ServiceCollection();

            services.ConfigureServices(pathToFile);

            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services, string pathToFile)
        {
            services.AddTransient<MedicalCard>();

            services.AddScoped(typeof(IRepository<MedicalCard>), provider => 
            {
                return new MedicalCardXmlRepository(pathToFile);
            });

            return services;
        }
    }
}
