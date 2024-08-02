using AutoMapper;
using RepositoryLayer;
using System.Linq.Expressions;

namespace BusinessServiceLayer
{
    public class BaseService<E, D> where E : class
    {
        private readonly BaseRepository<E> baseRepository;
        private readonly IMapper mapper;
        public BaseService(BaseRepository<E> baseRepository)
        {
            this.baseRepository = baseRepository;
        }
        public async Task<IEnumerable<D>> Get(Expression<Func<E, bool>> filter = null,
                              Func<IQueryable<E>, IOrderedQueryable<E>> orderBy = null, int pageNumber = 1, int pageSize = 10) {
            var resultList = new List<D>();
            var entities = await baseRepository.Get(filter, orderBy, pageNumber: pageNumber, pageSize: pageSize);
            foreach (var entity in entities)
            {
                var entitydto = mapper.Map<E, D>(entity);
                resultList.Add(entitydto);
            }
            return resultList;
        }
        public async Task<D> Get(int id)
        {
            var entity = await baseRepository.Get(id);
            var entitydto = mapper.Map<E, D>(entity);
            return entitydto;
        }
        public async Task<D> Post(E entity)
        {
            await baseRepository.Post(entity);
            var entitydto = mapper.Map<E, D>(entity);
            return entitydto;
        }
        public void Put(E entity)
        {
            baseRepository.Put(entity);
        }
        public void Delete(int id)
        {
            baseRepository.Delete(id);
        }
    }
}
