﻿using Hangman.Data.Interfaces;
using Hangman.Models;
using Microsoft.EntityFrameworkCore;

namespace Hangman.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        #region Fields

        private readonly ApplicationDbContext context;
        private readonly DbSet<T> dbSet;

        #endregion

        #region Constructors

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet   = context.Set<T>();
        }

        #endregion

        #region Get methods

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual T Get(int id)
        {
            return dbSet.FirstOrDefault(c => c.Id == id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await dbSet.FirstOrDefaultAsync(c => c.Id == id);
        }

        #endregion

        #region CRUD methods

        public virtual bool Add(T entity)
        {
            context.Add(entity);
            return Save();
        }

        public virtual bool Update(T entity)
        {
            context.Update(entity);
            return Save();
        }

        public virtual bool Delete(T entity)
        {
            context.Remove(entity);
            return Save();
        }

        public virtual bool Save()
        {
            return context.SaveChanges() > 0;
        }

        #endregion
    }
}
