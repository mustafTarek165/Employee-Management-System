using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public  class GenericRepository<T> : IGenericRepositoryInterface<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<GeneralResponse> Insert(T item)
        {
            try
            {
                await _dbSet.AddAsync(item);
                await _context.SaveChangesAsync();
                return new(true, "Item added successfully");
            }
            catch (Exception ex)
            {
                return new(false, ex.Message);
            }
        }

        public async Task<GeneralResponse> Update(T item)
        {
            try
            {
                _dbSet.Update(item);    
                await _context.SaveChangesAsync();
                return new(true, "Item updated successfully");
            }
            catch (Exception ex)
            {
                return new(false, ex.Message);
            }
        }

        public async Task<GeneralResponse> DeleteById(int id)
        {
            try
            {
                var item = await GetById(id);
                if (item == null)
                {
                    return new(false, "Item Not Found");
                }

                _dbSet.Remove(item);
                await _context.SaveChangesAsync();
                return new(true,"Item deleted successfully");
            }
            catch (Exception ex)
            {
                return new (false, ex.Message); 
            }
        }
    }
}
