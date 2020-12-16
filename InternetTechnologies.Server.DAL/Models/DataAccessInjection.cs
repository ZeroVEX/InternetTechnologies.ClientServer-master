using Autofac;
using InternetTechnologies.DomainModels.Models.Entities;
using InternetTechnologies.DomainModels.Services.Interfaces;
using InternetTechnologies.Server.DAL.Services.Repositories.XmlRepositories;

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
    }
}
