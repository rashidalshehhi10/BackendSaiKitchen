using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackendSaiKitchen.Repository
{
    public class Repository<T> : IRepositoryBase<T> where T : class
    {
        private readonly string[] excluded = { "CreatedDate", "CreatedBy" };

        public Repository(BackendSaiKitchen_dbContext repositoryContext)
        {
            if (repositoryContext == null)
            {
                throw new ArgumentNullException("repositoryContext");
            }

            RepositoryContext = repositoryContext;
            DbSet = repositoryContext.Set<T>();
        }

        protected BackendSaiKitchen_dbContext RepositoryContext { get; set; }
        protected DbSet<T> DbSet { get; set; }

        public IQueryable<T> FindAll()
        {
            return RepositoryContext.Set<T>();
        }

        public virtual void Create(T entity)
        {
            //var entry = RepositoryContext.Entry(entity);

            //entry.State = EntityState.Detached;
            RepositoryContext.Entry(entity).Property("CreatedDate").CurrentValue = Helper.Helper.GetDateTime();
            RepositoryContext.Entry(entity).Property("CreatedBy").CurrentValue = Constants.userId;
            RepositoryContext.Entry(entity).Property("UpdatedBy").CurrentValue = Constants.userId;
            RepositoryContext.Entry(entity).Property("UpdatedDate").CurrentValue = Helper.Helper.GetDateTime();
            RepositoryContext.Entry(entity).Property("IsActive").CurrentValue = true;
            RepositoryContext.Entry(entity).Property("IsDeleted").CurrentValue = false;

            RepositoryContext.Entry(entity).State = EntityState.Detached;
            RepositoryContext.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            //var entry = RepositoryContext.Entry(entity);
            //entry.State = EntityState.Detached;
            RepositoryContext.Entry(entity).Property("UpdatedBy").CurrentValue = Constants.userId;
            RepositoryContext.Entry(entity).Property("UpdatedDate").CurrentValue = Helper.Helper.GetDateTime();

            foreach (string val in excluded)
            {
                RepositoryContext.Entry(entity).Property(val).IsModified = false;
            }

            //var dbEntityEntry = RepositoryContext.Entry(entity);
            //foreach (var property in entry.OriginalValues.Properties)
            //{
            //    var original = entry.OriginalValues.GetValue<T>(property);
            //    var current = entry.CurrentValues.GetValue<T>(property);
            //    if (original != null && !original.Equals(current))
            //        entry.Property(property.ToString()).IsModified = true;
            //}
            RepositoryContext.Entry(entity).State = EntityState.Detached;
            RepositoryContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            RepositoryContext.Entry(entity).Property("IsDeleted").CurrentValue = true;
            RepositoryContext.Entry(entity).Property("UpdatedDate").CurrentValue = Helper.Helper.GetDateTime();
            RepositoryContext.Entry(entity).Property("UpdatedBy").CurrentValue = Constants.userId;
            RepositoryContext.Entry(entity).State = EntityState.Detached;
            RepositoryContext.Set<T>().Update(entity);
            //this.RepositoryContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return RepositoryContext.Set<T>().Where(expression);
        }


        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public void Escalate(T entity)
        {
            RepositoryContext.Entry(entity).Property("IsEscalationRequested").CurrentValue = true;
            RepositoryContext.Entry(entity).Property("UpdatedDate").CurrentValue = Helper.Helper.GetDateTime();
            RepositoryContext.Entry(entity).Property("UpdatedBy").CurrentValue = Constants.userId;
            RepositoryContext.Entry(entity).State = EntityState.Detached;
            RepositoryContext.Set<T>().Update(entity);
            //this.RepositoryContext.Set<T>().Remove(entity);
        }


        public async Task<IEnumerable<T>> GetPagedAsync(
            Func<IQueryable<T>,
                IOrderedQueryable<T>> orderBy,
            Expression<Func<T, bool>> filter = null,
            int? page = 0,
            int? pageSize = 10,
            //Expression<IGrouping<object, T>>[] groupBy,
            params Expression<Func<T, object>>[] includes
        )
        {
            IQueryable<T> query = DbSet;


            if (filter != null)
            {
                query = query.Where(filter);
            }

            //Includes
            if (includes != null && includes.Any())
            {
                query = includes.Aggregate(query,
                    (current, include) => current.Include(include));
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            else
            {
                throw new ArgumentNullException("The order by is necessary in Pagining");
            }

            if (page != null && page > 0)
            {
                //(0-1)
                if (pageSize == null)
                {
                    throw new ArgumentException(
                        "The take paremeter supplied is null, It should be included when skip is used");
                }

                query = query.Skip(((int)page - 1) * (int)pageSize);
            }

            if (pageSize != null)
            {
                query = query.Take((int)pageSize);
            }

            return await query.ToListAsync();
        }
    }
}