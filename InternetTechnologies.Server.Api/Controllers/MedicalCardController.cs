using System.Linq;
using System.Threading.Tasks;
using InternetTechnologies.DomainModels.Models.Entities;
using InternetTechnologies.DomainModels.Services.Interfaces;
using InternetTechnologies.Server.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace InternetTechnologies.Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalCardController : ControllerBase
    {
        private readonly IRepository<MedicalCard> _repository;

        public MedicalCardController(IRepository<MedicalCard> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(MedicalCardDto medicalCardDto)
        {
            var model = medicalCardDto.ToModel();

            await _repository.CreateAsync(model);

            return CreatedAtAction(nameof(Post), model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _repository.ReadAsync(id);

            if(model != null)
            {
                var dtoModel = model.ToDto();

                return Ok(dtoModel);
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MedicalCardDto medicalCardDto)
        {
            if ((await _repository.GetCollectionAsync()).Any(t => t.Id == id))
            {
                medicalCardDto.Id = id;

                var model = medicalCardDto.ToModel();

                await _repository.UpdateAsync(model);

                return Ok(model);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if((await _repository.GetCollectionAsync()).Any(t => t.Id == id))
            {
                var model = await _repository.ReadAsync(id);

                await _repository.DeleteAsync(id);

                return Ok(model);
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var collection = (await _repository.GetCollectionAsync()).ToList();

            return Ok(collection);
        }
    }
}
