using InternetTechnologies.Client.BLL.Models.DTO;
using InternetTechnologies.Client.BLL.Services.Interfaces;
using System;
using System.IO;
using Blood = InternetTechnologies.Client.BLL.Models.DTO.Blood;

namespace InternetTechnologies.Client.ConsoleUI.Models.Facades
{
    public class MedicalCardFacade : ClientFacade<MedicalCardDto>
    {
        public MedicalCardFacade(IService<MedicalCardDto> service)
            : base(service)
        {
        }

        public MedicalCardFacade(IService<MedicalCardDto> service, TextWriter outStream, TextReader inStream) 
            : base(service, outStream, inStream)
        {
        }

        protected override MedicalCardDto InputItem()
        {
            MedicalCardDto medicalCard = new MedicalCardDto();

            _outStream.WriteLine($"Input {nameof(MedicalCardDto.Name)}:");
            medicalCard.Name = _inStream.ReadLine();
            _outStream.WriteLine($"Input {nameof(MedicalCardDto.Weigth)}:");
            medicalCard.Weigth = double.Parse(_inStream.ReadLine());
            _outStream.WriteLine($"Input {nameof(MedicalCardDto.Growth)}:");
            medicalCard.Growth = int.Parse(_inStream.ReadLine());
            _outStream.WriteLine($"Input {nameof(MedicalCardDto.Year)}:");
            medicalCard.Year = int.Parse(_inStream.ReadLine());
            _outStream.WriteLine($"Input {nameof(MedicalCardDto.Blood)}:");
            medicalCard.Blood = (Blood)Enum.Parse(typeof(Blood), _inStream.ReadLine());
            //medicalCard.Name = "123";
            //medicalCard.Weigth = 123;
            //medicalCard.Growth = 123;
            //medicalCard.Year = 123;
            //medicalCard.Blood = Blood.First;

            return medicalCard;
        }

        protected override void PrintItem(MedicalCardDto item)
        {
            _outStream.WriteLine("-----------------------------------");
            _outStream.WriteLine($"{nameof(MedicalCardDto.Id)}:{item.Id}");
            _outStream.WriteLine($"{nameof(MedicalCardDto.Name)}:{item.Name}");
            _outStream.WriteLine($"{nameof(MedicalCardDto.Weigth)}:{item.Weigth}");
            _outStream.WriteLine($"{nameof(MedicalCardDto.Growth)}:{item.Growth}");
            _outStream.WriteLine($"{nameof(MedicalCardDto.Year)}:{item.Year}"); 
            _outStream.WriteLine($"{nameof(MedicalCardDto.Blood)}:{item.Blood}");
            _outStream.WriteLine("-----------------------------------");
        }
    }
}
