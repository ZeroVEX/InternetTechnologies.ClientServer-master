using InternetTechnologies.Client.BLL.Services;
using InternetTechnologies.Client.BLL.Services.Interfaces;
using System;
using System.IO;

namespace InternetTechnologies.Client.ConsoleUI.Models.Facades
{
    public abstract class ClientFacade<T>
        where T : class, IModel
    {
        private readonly IService<T> _service;

        protected readonly TextWriter _outStream;

        protected readonly TextReader _inStream;

        public ClientFacade(IService<T> service, TextWriter outStream, TextReader inStream)
        {
            _service = service;
            _outStream = outStream;
            _inStream = inStream;
        }

        public ClientFacade(IService<T> service) : this(service, Console.Out,
                                                                 Console.In)
        {
        }

        public async void Create(object sender, EventArgs e)
        {
            try
            {
                var item = InputItem();

                await _service.AddAsync(item);
            }
            catch (Exception ex) when (ex is ValidationException
                                       || ex is ArgumentException
                                       || ex is FormatException
                                       || ex is OverflowException)
            {
                _outStream.WriteLine(ex.Message);
            }
        }

        public async void Get(object sender, EventArgs e)
        {
            _outStream.WriteLine("Input id of item:\t");

            if (int.TryParse(_inStream.ReadLine(), out int id))
            {
                var item = await _service.GetAsync(id);

                if(item != null)
                {
                    PrintItem(item);
                }
                else
                {
                    _outStream.WriteLine($"There's no {typeof(T).Name}");
                }
            }
            else
            {
                _outStream.WriteLine("Invalid id");
            }
        }

        public async void Update(object sender, EventArgs e)
        {
            try
            {
                _outStream.WriteLine("Input id of item:\t");

                var id = int.Parse(_inStream.ReadLine());

                var item = InputItem();

                item.Id = id;

                await _service.UpdateAsync(item);
            }
            catch (Exception ex) when (ex is ValidationException
                                       || ex is ArgumentException
                                       || ex is FormatException
                                       || ex is OverflowException)
            {
                _outStream.WriteLine(ex.Message);
            }
        }

        public async void Delete(object sender, EventArgs e)
        {
            _outStream.WriteLine("Input id of item:\t");

            if (int.TryParse(_inStream.ReadLine(), out int id))
            {
                await _service.RemoveAsync(id);
            }
            else
            {
                _outStream.WriteLine("Invalid id");
            }
        }

        public async void GetAll(object sender, EventArgs e)
        {
            var collection = await _service.GetAllAsync();

            foreach (var item in collection)
            {
                PrintItem(item);
            }
        }

        protected abstract T InputItem();

        protected abstract void PrintItem(T item);
    }
}
