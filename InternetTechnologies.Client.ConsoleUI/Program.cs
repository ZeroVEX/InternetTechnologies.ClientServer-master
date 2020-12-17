using InternetTechnologies.Client.BLL.Models.Factory;
using InternetTechnologies.Client.ConsoleUI.Models;
using InternetTechnologies.Client.ConsoleUI.Models.Facades;
using System;

namespace InternetTechnologies.Client.ConsoleUI
{
    public class Program
    {
        private static readonly string _host = "127.0.0.1";

        private static readonly int _port = 25565;

        private static readonly string _apiAddress = "https://localhost:44336/";

        static void Main(string[] args)
        {
            var dataAccess = DataAccessFactory.GetDataAccess(apiAddress: _apiAddress);

            MedicalCardFacade cardFacade = new MedicalCardFacade(dataAccess.MedicalCard);

            new ConsoleMenu(cardFacade).Start();

            Console.ReadKey();
        }
    }
}
