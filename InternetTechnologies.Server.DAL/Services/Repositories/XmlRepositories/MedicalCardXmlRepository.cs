using InternetTechnologies.DomainModels.Models.Entities;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InternetTechnologies.Server.DAL.Services.Repositories.XmlRepositories
{
    internal class MedicalCardXmlRepository : XmlRepositoryTemplate<MedicalCard>
    {
        public MedicalCardXmlRepository(string pathToFile)
            : base(pathToFile)
        {
        }

        protected override async Task<XElement> CreateElement(MedicalCard item)
        {
            var xElement = new XElement(nameof(MedicalCard),
                                        new XAttribute(nameof(MedicalCard.Id), item.Id),
                                        new XElement(nameof(MedicalCard.Name), item.Name),
                                        new XElement(nameof(MedicalCard.Year), item.Year),
                                        new XElement(nameof(MedicalCard.Growth), item.Growth),
                                        new XElement(nameof(MedicalCard.Weigth), item.Weigth),
                                        new XElement(nameof(MedicalCard.Blood), item.Blood.ToString()));

            return await Task.FromResult(xElement);
        }

        protected override async Task<MedicalCard> ReadItem(XElement xElement)
        {
            var numFormat = new NumberFormatInfo();

            var element = new MedicalCard
            {
                Id = int.Parse(xElement.Attribute(nameof(MedicalCard.Id)).Value),
                Name = xElement.Element(nameof(MedicalCard.Name)).Value,
                Year = int.Parse(xElement.Element(nameof(MedicalCard.Year)).Value),
                Growth = int.Parse(xElement.Element(nameof(MedicalCard.Growth)).Value),
                Weigth = double.Parse(xElement.Element(nameof(MedicalCard.Weigth)).Value, numFormat),
                Blood = (Blood)Enum.Parse(typeof(Blood), xElement.Element(nameof(MedicalCard.Blood)).Value),
            };

            return await Task.FromResult(element);
        }
    }
}
