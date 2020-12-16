using AutoMapper;
using InternetTechnologies.Client.BLL.Services.Interfaces;
using InternetTechnologies.DomainModels.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetTechnologies.Client.BLL.Services
{
    internal class GenericService<T, TEntity> : IService<T>
        where T : class, IModel
        where TEntity : class, IEntity
    {
        private readonly IRepository<TEntity> _repository;

        private readonly IValidationProvider<T> _validator;

        private readonly IMapper _mapper;

        public GenericService(IRepository<TEntity> repository, IValidationProvider<T> validator, IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task AddAsync(T item)
        {
            if (!await _validator.IsValid(item))
            {
                throw new ValidationException($"Invalid {typeof(T).Name}");
            }

            TEntity entity = _mapper.Map<TEntity>(item);

            await _repository.CreateAsync(entity);
        }    

        public async Task<T> GetAsync(int id)
        {
            var entity = await _repository.ReadAsync(id);

            return _mapper.Map<T>(entity);
        }

        public async Task UpdateAsync(T item)
        {
            if (!await _validator.IsValid(item))
            {
                throw new ValidationException($"Invalid {typeof(T).Name}");
            }

            TEntity entity = _mapper.Map<TEntity>(item);

            await _repository.UpdateAsync(entity);
        }

        public async Task RemoveAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
     
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var collection = (await _repository.GetCollectionAsync());

            return collection
                            .Select(t => _mapper.Map<T>(t))
                            .ToList();
        }
    }
}
