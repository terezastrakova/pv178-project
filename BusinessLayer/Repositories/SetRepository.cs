using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repositories
{
    public class SetRepository
    {
        private readonly ApplicationDbContext _context;

        public SetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<string[]>> GetWordsBySetsAndCategoriesAsync(string userId, List<int> setIds, List<int> categoryIds)
        {
            var words = await _context.Words
                .Where(w => setIds.Contains(w.VocabularySet.Id))
                .Join(_context.UserWordCategories,
                      w => w.Id,
                      uwc => uwc.WordId,
                      (w, uwc) => new { w, uwc })
                .Where(wuwc => wuwc.uwc.UserId == userId && categoryIds.Contains(wuwc.uwc.CategoryId))
                .Select(wuwc => new string[] { wuwc.w.EnglishWord, wuwc.w.JapaneseWord })
                .ToListAsync();
            return words;
        }

        public async Task<List<Models.Set>> GetUserVocabularySetsAsync(string userId)
        {
            return await _context.VocabularySets
                .Where(vs => vs.UserId == userId || vs.IsPremade)
                .Select(vs => new Models.Set
                {
                    Id = vs.Id,
                    Name = vs.Name
                })
                .ToListAsync();
        }
    }
}
