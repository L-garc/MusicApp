using Microsoft.EntityFrameworkCore;
using MusicApi.Data;
using System.Data.Common;
using System.Linq.Expressions;

namespace MusicApi.Repository {
    public class Repo<T> : IRepo<T> where T : class {
        private readonly MusicContext _db;
        internal DbSet<T> dbSet;
        public Repo(MusicContext db) {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public async Task CreateAsync(T entity) {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(T entity) {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null) {
            IQueryable<T> query = dbSet;

            if (filter != null) {
                query = query.Where(filter);
            }
            if (includeProperties != null) {
                foreach(var includeProp in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                    query = query.Include(includeProp);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includeProperties = null) {
            IQueryable<T> query = dbSet;

            if (!tracked) {
                query = query.AsNoTracking();
            }
            if (filter != null) {
                query = query.Where(filter);
            }
            if (includeProperties != null) {
                foreach (var includeProp in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                    query = query.Include(includeProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task SaveAsync() {
            await _db.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity) {
            _db.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
