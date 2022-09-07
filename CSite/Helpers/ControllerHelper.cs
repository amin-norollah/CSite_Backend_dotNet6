using AutoMapper;
using CSite.Data.DdContexts;
using CSite.Shared.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Reflection;

namespace CSite.Helpers
{
    public class ControllerHelper
    {
        private readonly IUnitOfWork<CSiteDBContext> _unitOfWork;
        private readonly IMapper _mapper;

        public ControllerHelper(
            IUnitOfWork<CSiteDBContext> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<List<TEntityDTO>> GetAll<TEntity, TEntityDTO>(
            int pageIndex, 
            int pageSize,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            where TEntity : class
            where TEntityDTO : class
        {
            var outputs = await _unitOfWork.GetRepository<TEntity>().GetPagedListAsync(
                pageIndex: pageIndex,
                pageSize: pageSize, 
                predicate: predicate,
                include: include);
            return _mapper.Map<List<TEntityDTO>>(outputs.ToList());
        }


        public async Task<TEntityDTO> GetById<TEntity, TEntityDTO>(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            )
            where TEntity : class
            where TEntityDTO : class
        {
            var output = await _unitOfWork.GetRepository<TEntity>().GetFirstOrDefaultAsync(predicate: predicate, include: include);
            return _mapper.Map<TEntityDTO>(output);
        }


        public async Task<bool> Update<TEntity, TEntityDTO>(
            TEntityDTO data,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            where TEntity : class
            where TEntityDTO : class
        {
            var target = await _unitOfWork.GetRepository<TEntity>().GetFirstOrDefaultAsync(
                                      include: include,
                                      predicate: predicate
                                      );

            //guard: if item is not exist
            if (target == null)
                return false;

            //save id from target data
            object id = null;
            var property = target.GetType().GetProperty("Id");
            if (property != null)
                id = property.GetValue(target);

            //update
            _mapper.Map(data, target);
            if (property != null) //set id
                property.SetValue(target, id ,null);
            _unitOfWork.GetRepository<TEntity>().Update(target);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<TEntity> Create<TEntity, TEntityDTO>(TEntityDTO data)
            where TEntity : class
            where TEntityDTO : class
        {
            //remove id from requested data
            var property = data.GetType().GetProperty("Id");
            if (property != null)
                property.SetValue(data, null, null);

            var target = _mapper.Map<TEntity>(data);
            await _unitOfWork.GetRepository<TEntity>().InsertAsync(target);
            await _unitOfWork.SaveChangesAsync();

            return target;
        }


        public async Task Create<TEntity, TEntityDTO>(TEntityDTO[] data)
            where TEntity : class
            where TEntityDTO : class
        {
            PropertyInfo property;
            TEntity target;

            foreach (var item in data)
            {
                //remove id from requested data
                property = item.GetType().GetProperty("Id");
                if (property != null)
                    property.SetValue(item, null, null);

                target = _mapper.Map<TEntity>(item);
                await _unitOfWork.GetRepository<TEntity>().InsertAsync(target);
                await _unitOfWork.SaveChangesAsync();
            }
        }


        public async Task<bool> Remove<TEntity>(
            int id,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            where TEntity : class
        {
            var target = await _unitOfWork.GetRepository<TEntity>().GetFirstOrDefaultAsync(
                                      include: include,
                                      predicate: predicate
                                      );

            //guard: if car is not exist
            if (target == null)
                return false;

            //delete
            await _unitOfWork.GetRepository<TEntity>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
