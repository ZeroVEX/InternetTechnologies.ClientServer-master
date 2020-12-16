using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetTechnologies.Client.BLL.Services.Interfaces
{
    public interface IService<T>
        where T : class, IModel
    {
        Task AddAsync(T item);

        Task<T> GetAsync(int id);

        Task UpdateAsync(T item);

        Task RemoveAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();
    }
}
