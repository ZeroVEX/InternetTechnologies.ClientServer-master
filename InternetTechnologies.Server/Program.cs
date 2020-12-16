using InternetTechnologies.DomainModels.Models.Entities;
using InternetTechnologies.Server.Models;
using System;
using System.Threading.Tasks;
using Autofac;
using InternetTechnologies.Server.DAL.Models;
using InternetTechnologies.DomainModels.Services.Interfaces;

namespace InternetTechnologies.Server
{
    internal class Program
    {

        private static readonly string _host = "127.0.0.1";

        private static readonly int _port = 25565;

        private static readonly string _pathToFile = "..\\..\\..\\Files\\MedicalCard.xml";

        private static readonly IContainer _container = DataAccessInjection.GetContainerBuilder(_pathToFile).Build();

        public static async Task Main(string[] args)
        {
            var repository = _container.Resolve<IRepository<MedicalCard>>();

            var facade = new ServerFacade<MedicalCard>(repository, _host, _port);

            await facade.StartServer();

            Console.ReadKey();
        }
    }
}
