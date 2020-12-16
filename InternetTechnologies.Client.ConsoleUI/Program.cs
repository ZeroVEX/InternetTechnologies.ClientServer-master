using InternetTechnologies.Client.BLL.Models.Factory;
using InternetTechnologies.Client.ConsoleUI.Models;
using InternetTechnologies.Client.ConsoleUI.Models.Facades;
using System;

namespace InternetTechnologies.Client.ConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            var dataAccess = DataAccessFactory.GetDataAccess("127.0.0.1", 25565);

            MedicalCardFacade cardFacade = new MedicalCardFacade(dataAccess.MedicalCard);

            new ConsoleMenu(cardFacade).Start();

            Console.ReadKey();
        }
    }
}
