using InternetTechnologies.Client.BLL.Models.DTO;
using InternetTechnologies.Client.BLL.Services.Interfaces;
using InternetTechnologies.Client.BLL.Services.Validation;
using System.Threading.Tasks;

namespace InternetTechnologies.Client.BLL.Services.ValidationProviders
{
    internal class MedicalCardValidationProvider : IValidationProvider<MedicalCardDto>
    {
        public async Task<bool> IsValid(MedicalCardDto item)
        {
            return await item.ValidateFieldsAsync();
        }
    }
}
