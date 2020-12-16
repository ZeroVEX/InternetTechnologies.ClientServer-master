using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetTechnologies.DomainModels.Services.Interfaces
{
    public interface IRepository<T>
        where T : class, IEntity
    {
        Task CreateAsync(T item);

        Task<T> ReadAsync(int id);

        Task UpdateAsync(T item);

        Task DeleteAsync(int id);

        Task<IEnumerable<T>> GetCollectionAsync();
    }
}
