using InternetTechnologies.DomainModels.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InternetTechnologies.Server.DAL.Services.Repositories.XmlRepositories
{
    internal abstract class XmlRepositoryTemplate<T> : IRepository<T>
       where T : class, IEntity
    {
        private readonly string _pathToFile;

        public XmlRepositoryTemplate(string pathToFile)
        {
            _pathToFile = pathToFile;

            _pathToFile.ValidateXmlPath();

            _pathToFile.ValidateFileExistance();
        }

        public async Task CreateAsync(T item)
        {
            item.IsNotNull();

            XDocument xDoc = XDocument.Load(_pathToFile);

            var collection = xDoc.Root.Elements();

            item.Id = collection.Any() ? (GetId(collection.Last()) + 1) : 1;

            xDoc.Root.Add(await CreateElement(item));

            xDoc.Save(_pathToFile);
        }

        public async Task<T> ReadAsync(int id)
        {
            T item;

            XDocument xDoc = XDocument.Load(_pathToFile);

            var xElement = xDoc.Root.Elements()
                                    .FirstOrDefault(t => GetId(t) == id);

            item = xElement != null ? await ReadItem(xElement) : default;

            return item;
        }

        public async Task UpdateAsync(T item)
        {
            item.IsNotNull();

            XDocument xDoc = XDocument.Load(_pathToFile);


            var collection = xDoc.Root.Elements().ToList();

            if(collection.Any(t => GetId(t) == item.Id))
            {
                collection = collection.Where(t => GetId(t) != item.Id)
                                   .ToList();

                var xElement = await CreateElement(item);

                collection.Add(xElement);

                xDoc.Root.RemoveAll();

                xDoc.Root.Add(collection.OrderBy(t => GetId(t)));

                xDoc.Save(_pathToFile);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await Task.Run(() =>
            {
                XDocument xDoc = XDocument.Load(_pathToFile);

                var collection = xDoc.Root.Elements()
                            .Where(t => GetId(t) != id)
                            .ToList();

                xDoc.Root.RemoveAll();

                xDoc.Root.Add(collection);

                xDoc.Save(_pathToFile);
            });
        }

        public async Task<IEnumerable<T>> GetCollectionAsync()
        {
            var collection = new List<T>();

            XDocument xDoc = XDocument.Load(_pathToFile);

            foreach (var item in xDoc.Root.Elements())
            {
                collection.Add(await ReadItem(item));
            }

            return await Task.FromResult(collection);
        }

        protected abstract Task<XElement> CreateElement(T item);
        protected abstract Task<T> ReadItem(XElement xElement);

        private int GetId(XElement xElement)
            => int.Parse(xElement.Attribute(nameof(IEntity.Id)).Value);
    }
}
