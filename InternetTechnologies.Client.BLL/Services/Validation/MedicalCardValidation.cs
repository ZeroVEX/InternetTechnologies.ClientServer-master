using InternetTechnologies.Client.BLL.Models.DTO;
using System;
using System.Threading.Tasks;

namespace InternetTechnologies.Client.BLL.Services.Validation
{
    internal static class MedicalCardValidation
    {
        public static async Task<bool> ValidateFieldsAsync(this MedicalCardDto medicalCard)
        {
            bool isValid = !(medicalCard == null
                            || string.IsNullOrEmpty(medicalCard.Name)
                            || medicalCard.Weigth <= 0.0
                            || medicalCard.Year < 0
                            || medicalCard.Growth <= 0
                            || !Enum.IsDefined(typeof(Blood), medicalCard.Blood));

            return await Task.FromResult(isValid);
        }
    }
}
