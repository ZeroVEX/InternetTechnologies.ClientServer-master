using InternetTechnologies.DomainModels.Models;
using InternetTechnologies.DomainModels.Services.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace InternetTechnologies.Server.Models
{
    public class ServerFacade<T>
        where T : class, IEntity
    {
        private readonly IRepository<T> _repository;

        private readonly string _host;

        private readonly int _port;

        private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();

        private readonly TextWriter _logger = Console.Out;

        public ServerFacade(IRepository<T> repository, string host, int port)
        {
            _repository = repository;
            _host = host;
            _port = port;
        }

        public async Task StartServer()
        {
            try
            {
                TcpListener tcpListener = new TcpListener(IPAddress.Parse(_host), _port);

                tcpListener.Start();

                _logger.WriteLine("Server has started");

                while (true)
                {
                    var client = await tcpListener.AcceptTcpClientAsync();

                    using (var networkStream = client.GetStream())
                    {
                        var dataContainer = _binaryFormatter.Deserialize(networkStream) as DataContainer<T>;

                        await ExecuteActionAsync(dataContainer, networkStream);
                    }
                }               
            }
            catch (Exception ex)
            {
                _logger.WriteLine(ex.Message);

                throw;
            }
        }

        private async Task ExecuteActionAsync(DataContainer<T> dataContainer, NetworkStream networkStream)
        {
            var item = dataContainer.Data;

            switch (dataContainer.Operation)
            {
                case OperationType.Create:
                    await _repository.CreateAsync(item);

                    _binaryFormatter.Serialize(networkStream, item.Id);

                    _logger.WriteLine($"{typeof(T).Name} Created");
                    break;


                case OperationType.Read:

                    var newItem = await _repository.ReadAsync(item.Id);

                    if (newItem != null)
                    {
                        _binaryFormatter.Serialize(networkStream, newItem);
                    }
                    else
                    {
                        item.Id = -1;

                        _binaryFormatter.Serialize(networkStream, item);
                    }
                        
                    _logger.WriteLine($"{typeof(T).Name} was read");
                    break;

                case OperationType.Update:
                    await _repository.UpdateAsync(item);

                    _logger.WriteLine($"{typeof(T).Name} updated");
                    break;

                case OperationType.Delete:
                    await _repository.DeleteAsync(item.Id);

                    _logger.WriteLine($"{typeof(T).Name} deleted");
                    break;

                case OperationType.GetCollection:
                    var collection = (await _repository.GetCollectionAsync())
                                                                            .ToList();

                    _binaryFormatter.Serialize(networkStream, collection);

                    _logger.WriteLine($"{typeof(T).Name} was read");
                    break;

                default:
                    throw new ArgumentException("Invalid operation");
            }
        }
    }
}
