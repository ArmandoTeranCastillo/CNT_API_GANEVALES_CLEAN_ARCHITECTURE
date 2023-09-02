using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Interfaces;
using _2.UsersManagement.Application.DTOs.Addresses.In_Services;
using _2.UsersManagement.Application.Interfaces;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using Microsoft.EntityFrameworkCore;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        private readonly CNTContext _cnt;
        private readonly DbSet<TEntity> _entities;

        public GenericService(CNTContext cnt)
        {
            _cnt = cnt;
            _entities = _cnt.Set<TEntity>();
        }
     
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var result = await _entities.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<GetOneFieldDto>> GetAllOneField(Expression<Func<TEntity, GetOneFieldDto>> filter)
        {
            var result = await _entities.Select(filter)
                .OrderBy(c => c.Field != "Mexico")
                .ThenBy(c => c.Field)
                .ToListAsync();
            
            return result;
        }

        public async Task<GetOneFieldDto> GetSimpleOneFieldById(Expression<Func<TEntity, GetOneFieldDto>> select, Expression<Func<TEntity, bool>> filter)
        {
            var result = await _entities
                .Where(filter)
                .Select(select)
                .FirstOrDefaultAsync();
            if (result is null) throw new NotFoundException(Codes.EmptyField);

            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAllById(Expression<Func<TEntity, object>> order, params Expression<Func<TEntity, bool>>[] filters)
        {
            var query = _entities.AsQueryable();
            foreach (var filter in filters)
            {
                query = query.Where(filter);
            }
            var result = await query
                .OrderBy(order)
                .ToListAsync();
            return result;
        }
        
        public async Task<TEntity> GetSimpleById(Expression<Func<TEntity, bool>> filter)
        {
            var result = await _entities.FirstOrDefaultAsync(filter);
            return result;
        }

        public int GetEnumByName<T>(string name) where T : Enum
        {
            try
            {
                var enumValue = (T)Enum.Parse(typeof(T), name, true);
                return Convert.ToInt32(enumValue);
            }
            catch (ArgumentException)
            {
                throw new InternalServerError(Codes.FailedEnumParse);
            }
        }

        public async Task<TEntity> UpdateEntity<TDto>(TDto request)
            where TDto : class, IUpdateEntity
        {
            var entity = await ValidateAndGetEntity(request);

            UpdateEntityProperties(entity, request);

            await _cnt.SaveChangesAsync();

            return entity;
        }

        private async Task<TEntity> ValidateAndGetEntity<TDto>(TDto request)
            where TDto : class, IUpdateEntity
        {
            var entity = await _entities.FindAsync(request.id);

            if (entity is null || request.modiffiedBy is null)
            {
                throw new NotFoundException(Codes.EmptyField);
            }

            return entity;
        }

        private static void UpdateEntityProperties<TUEntity, TDto>(TUEntity entity, TDto request)
            where TDto : class, IUpdateEntity
        {
            var dtoProperties = typeof(TDto).GetProperties();
            var entityProperties = typeof(TEntity).GetProperties();

            foreach (var dtoProperty in dtoProperties)
            {
                if (dtoProperty.Name == "id") continue;

                var entityProperty = entityProperties.FirstOrDefault(p => p.Name == dtoProperty.Name && p.PropertyType == dtoProperty.PropertyType);

                if (entityProperty is null) continue;
                var dtoValue = dtoProperty.GetValue(request);
                if (dtoValue is not null) 
                {
                    entityProperty.SetValue(entity, dtoValue);
                }
            }
        }

        public async Task RegisterActivityLog()
        {
            var logLogin = await _cnt.LogsLogin.FirstOrDefaultAsync(i => i.FirtsLogin.Date == DateTime.Now.Date);
            if (logLogin is null) throw new Exception("Log not found");

            logLogin.LastActivity = DateTime.Now;
            await _cnt.SaveChangesAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _entities.CountAsync();
        }

        public async Task<bool> EntityExists(string id, string reference)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, reference);
            var idExpression = Expression.Constant(id);
            var condition = Expression.Equal(property, idExpression);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(condition, parameter);

            if (!await _entities.Where(lambda).AnyAsync())
            {
                throw new NotFoundException(Codes.EmptyField);
            }
            return true;
        }

        public async Task<bool> EntityNotExists(string id, string reference)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, reference);
            var idExpression = Expression.Constant(id);
            var condition = Expression.Equal(property, idExpression);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(condition, parameter);

            if (await _entities.Where(lambda).AnyAsync())
            {
                throw new BadRequestException(Codes.EntityAlreadyCreated);
            }
            return true;
        }

        public async Task<bool> EntityPropertyHas(string id, string reference, string otherId, string otherReference)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, reference);
            var idExpression = Expression.Constant(id);
            var firstCondition = Expression.Equal(property, idExpression);
            
            var otherProperty = Expression.Property(parameter, otherReference);
            var otherIdExpression = Expression.Constant(otherId);
            var secondCondition = Expression.Equal(otherProperty, otherIdExpression);
            
            var combinedCondition = Expression.AndAlso(firstCondition, secondCondition);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(combinedCondition, parameter);
            
            if (!await _entities.Where(lambda).AnyAsync())
            {
                throw new BadRequestException("Entity not found with the given properties");
            }
            return true;
        }
        
        public async Task<bool> EntityPropertyHasNot(string id, string reference, string otherId, string otherReference)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, reference);
            var idExpression = Expression.Constant(id);
            var firstCondition = Expression.Equal(property, idExpression);
            
            var otherProperty = Expression.Property(parameter, otherReference);
            var otherIdExpression = Expression.Constant(otherId);
            var secondCondition = Expression.Equal(otherProperty, otherIdExpression);
            
            var combinedCondition = Expression.AndAlso(firstCondition, secondCondition);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(combinedCondition, parameter);
            
            if (await _entities.Where(lambda).AnyAsync())
            {
                throw new BadRequestException(Codes.EntityAlreadyCreated);
            }
            return true;
        }

        public async Task<string> GetEntityProperty(string id, string reference, string proper)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, reference);
            var idExpression = Expression.Constant(id);
            var condition = Expression.Equal(property, idExpression);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(condition, parameter);
            var entity = await _entities.FirstOrDefaultAsync(lambda);
            if (entity is null)
            {
                throw new NotFoundException(Codes.EmptyField);
            }
            var entityIdProperty = typeof(TEntity).GetProperty(proper);
            if (entityIdProperty == null) throw new NotFoundException(Codes.FieldNotValid);
            var entityId = entityIdProperty.GetValue(entity)?.ToString();
            if (entityId is null)
            {
                throw new InternalServerError(Codes.FieldNotValid);
            }
            return entityId;
        }
        
        public async Task<string> GetEntityPropertyOrNull(string id, string reference, string proper)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, reference);
            var idExpression = Expression.Constant(id);
            var condition = Expression.Equal(property, idExpression);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(condition, parameter);
            var entity = await _entities.FirstOrDefaultAsync(lambda);
            if (entity is null)
            {
                return null;
            }
            var entityIdProperty = typeof(TEntity).GetProperty(proper);
            if (entityIdProperty == null) return null;
            var entityId = entityIdProperty.GetValue(entity)?.ToString();
            return entityId;
        }
    }
}

