using System.Threading.Tasks;

namespace InternetTechnologies.Client.BLL.Services.Interfaces
{
    public interface IValidationProvider<T>
    {
        Task<bool> IsValid(T item);
    }
}
