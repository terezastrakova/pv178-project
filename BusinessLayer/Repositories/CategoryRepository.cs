using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Repositories
{
    public class CategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Models.Category>> GetUserCategoriesAsync(string userId)
        {
            var result = await _context.Categories
                .Where(u => u.UserId == userId || u.IsPremade == true)
                .Select(c => new Models.Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsPremade = c.IsPremade
                })
                .ToListAsync();
            return result;
        }

        public async Task<List<Models.Category>> GetPremadeCategoriesAsync()
        {
            return await _context.Categories
                .Where(c => c.IsPremade == true)
                .Select(c => new Models.Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsPremade = c.IsPremade
                })
                .ToListAsync();
        }

        public async Task<List<Models.Category>> GetUsermadeCategoriesAsync(string userId)
        {
            return await _context.Categories
                .Where(u => u.UserId == userId)
                .Select(c => new Models.Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsPremade = c.IsPremade
                })
                .ToListAsync();
        }

        public async Task AddCategoryAsync(string userId, string categoryName)
        {
            var category = new Category
            {
                Name = categoryName,
                IsPremade = false,
                UserId = userId
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(string userId, int categoryId)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCategoryAsync(string userId, Models.Category updatedCategory)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == updatedCategory.Id && c.UserId == userId);
            if (category != null)
            {
                category.Name = updatedCategory.Name;
                await _context.SaveChangesAsync();
            }
        }
    }
}
