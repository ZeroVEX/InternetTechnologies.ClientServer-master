using InternetTechnologies.DomainModels.Models;
using InternetTechnologies.DomainModels.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace InternetTechnologies.Client.DAL.Services.Repositories
{
    internal class NetworkRepository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        private readonly string _host;

        private readonly int _port;

        private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();

        public NetworkRepository(string host, int port)
        {
            _host = host;
            _port = port;
        }

        private async Task<TOut> GetData<TOut>(DataContainer<T> dataContainer)
        {
            TOut outItem;

            using (TcpClient tcpClient = new TcpClient())
            {
                await tcpClient.ConnectAsync(_host, _port);

                using (var networkStream = tcpClient.GetStream())
                {
                    _binaryFormatter.Serialize(networkStream, dataContainer);

                    outItem = (TOut)_binaryFormatter.Deserialize(networkStream);
                }
            }

            return outItem;
        }

        private async Task SendData(T item, OperationType operation)
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                await tcpClient.ConnectAsync(_host, _port);

                using (var networkStream = tcpClient.GetStream())
                {
                    DataContainer<T> dataContainer = new DataContainer<T>
                    {
                        Data = item,
                        Operation = operation,
                    };

                    _binaryFormatter.Serialize(networkStream, dataContainer);
                }
            }
        }

        public async Task CreateAsync(T item)
        {
            DataContainer<T> dataContainer = new DataContainer<T>
            {
                Data = item,
                Operation = OperationType.Create,
            };

            item.Id = await GetData<int>(dataContainer);
        }

        public async Task<T> ReadAsync(int id)
        {
            DataContainer<T> dataContainer = new DataContainer<T>
            {
                Data = new T { Id = id },
                Operation = OperationType.Read,
            };

            var newItem = await GetData<T>(dataContainer);

            return (newItem.Id != -1) ? newItem : null;
        }

        public async Task UpdateAsync(T item)
        {
            await SendData(item, OperationType.Update);
        }

        public async Task DeleteAsync(int id)
        {
            T item = new T { Id = id };

            await SendData(item, OperationType.Delete);
        }

        public async Task<IEnumerable<T>> GetCollectionAsync()
        {
            DataContainer<T> dataContainer = new DataContainer<T>
            {
                Operation = OperationType.GetCollection
            };

            return await GetData<List<T>>(dataContainer);
        }
    }
}
