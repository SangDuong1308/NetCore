using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Data;

namespace src.Repository.BaseRepository
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDBContext _context;
        protected BaseRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            try
            {
                var list = await _context.Set<TEntity>().ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting entity: {ex.Message}", ex);
            }
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            try
            {
                var entity = await _context.Set<TEntity>().FindAsync(id) ?? throw new ArgumentNullException("Error getting entity");
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting entity: {ex.Message}", ex);
            }
        }

        public async Task AddAsync(TEntity entity)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding entity: {ex.Message}", ex);
            }
        }
        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity == null)
                    throw new ArgumentNullException("Error deleting entity: Entity not found");

                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting entity: {ex.Message}", ex);
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating entity: {ex.Message}", ex);
            }
        }
    }
}