using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Interfaces;
using _2.UsersManagement.Application.DTOs.Addresses.In_Services;

namespace _2.UsersManagement.Application.Interfaces
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<GetOneFieldDto>> GetAllOneField(Expression<Func<TEntity, GetOneFieldDto>> filter);
        Task<GetOneFieldDto> GetSimpleOneFieldById(Expression<Func<TEntity, GetOneFieldDto>> select, Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> GetAllById(Expression<Func<TEntity, object>> order,
            params Expression<Func<TEntity, bool>>[] filters);
        Task<TEntity> GetSimpleById(Expression<Func<TEntity, bool>> filter);
        int GetEnumByName<T>(string name) where T : Enum;
        Task<TEntity> UpdateEntity<TDto>(TDto request) where TDto : class, IUpdateEntity;
        Task RegisterActivityLog();
        Task<int> GetCountAsync();
        Task<bool> EntityExists(string id, string reference);
        Task<bool> EntityNotExists(string id, string reference);
        Task<bool> EntityPropertyHas(string id, string reference, string otherId, string otherReference);
        Task<bool> EntityPropertyHasNot(string id, string reference, string otherId, string otherReference);
        Task<string> GetEntityProperty(string id, string reference, string proper);
        Task<string> GetEntityPropertyOrNull(string id, string reference, string proper);
    }
}
