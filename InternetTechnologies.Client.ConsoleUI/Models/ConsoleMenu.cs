using InternetTechnologies.Client.ConsoleUI.Models.Facades;
using System;

namespace InternetTechnologies.Client.ConsoleUI.Models
{
    public class ConsoleMenu
    {
        public event EventHandler CreateItem;

        public event EventHandler GetItem;

        public event EventHandler UpdateItem;

        public event EventHandler RemoveItem;

        public event EventHandler GetAllItems;

        public ConsoleMenu(MedicalCardFacade cardFacade)
        {
            CreateItem = cardFacade.Create;
            GetItem = cardFacade.Get;
            UpdateItem = cardFacade.Update;
            RemoveItem = cardFacade.Delete;
            GetAllItems = cardFacade.GetAll;
        }

        public void Start()
        {
            bool isDone = false;

            while (!isDone)
            {
                Console.WriteLine("\t1.Create MedicalCard" +
                    "\n\t2.Get MedicalCard" +
                    "\n\t3.Update MedicalCard" +
                    "\n\t4.Remove MedicalCard" +
                    "\n\t5.GetAll Medical Cards" +
                    "\n\t6.Exit");

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                Console.WriteLine();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                        CreateItem?.Invoke(this, EventArgs.Empty);
                        break;

                    case ConsoleKey.D2:
                        GetItem?.Invoke(this, EventArgs.Empty);
                        break;

                    case ConsoleKey.D3:
                        UpdateItem?.Invoke(this, EventArgs.Empty);
                        break;

                    case ConsoleKey.D4:
                        RemoveItem?.Invoke(this, EventArgs.Empty);
                        break;

                    case ConsoleKey.D5:
                        GetAllItems?.Invoke(this, EventArgs.Empty);
                        break;

                    case ConsoleKey.D6:
                        isDone = true;
                        break;

                    case ConsoleKey.D7:
                        {
                            for (int i = 1; i < 100; i++)
                            {
                                CreateItem?.Invoke(this, EventArgs.Empty);
                            }
                        }
                        isDone = true;
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

    }
}
