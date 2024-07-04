using BusinessLayer;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BusinessLayer.Repositories
{
    public class AppRepository
    {
        private readonly ApplicationDbContext _context;
        public AppRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Models.VocabularySetCard>> GetAllVocabularySetInfoAsync(string userId)
        {
            var premadeSets = await _context.VocabularySets
                .Where(vs => vs.IsPremade)
                .Join(_context.UserVocabularySets.Where(uvs => uvs.UserId == userId),
                      vs => vs.Id,
                      uvs => uvs.VocabularySetId,
                      (vs, uvs) => new Models.VocabularySetCard
                      {
                          Id = vs.Id,
                          Name = vs.Name,
                          GuessedWords = uvs.Progress,
                          TotalWords = vs.Words.Count
                      })
                .ToListAsync();

            var userSets = await _context.VocabularySets
                .Where(vs => vs.UserId == userId)
                .Join(
                    _context.UserVocabularySets,
                    vs => vs.Id,
                    uvs => uvs.VocabularySetId,
                    (vs, uvs) => new Models.VocabularySetCard
                    {
                        Id = vs.Id,
                        Name = vs.Name,
                        GuessedWords = uvs.Progress,
                        TotalWords = vs.Words.Count
                    }
                )
                .ToListAsync();

            return premadeSets.Concat(userSets).ToList();
        }

        public async Task<List<string[]>> GetVocabularyWordsAsync(int setId)
        {
            return await _context.Words
                .Where(w => w.VocabularySetId == setId)
                .Select(w => new[] { w.EnglishWord, w.JapaneseWord })
                .ToListAsync();
        }

        public async Task<string> GetVocabularySetNameAsync(int setId)
        {
            return await _context.VocabularySets
                .Where(vs => vs.Id == setId)
                .Select(vs => vs.Name)
                .FirstOrDefaultAsync();
        }
        public async Task<bool> IsPremadeSetAsync(int setId)
        {
            return await _context.VocabularySets
                .Where(vs => vs.Id == setId)
                .Select(vs => vs.IsPremade)
                .FirstOrDefaultAsync();
        }
        public async Task AddUserPremadeSetsAsync(string userId)
        {
            var premadeVocabularySets = await _context.VocabularySets
                .Where(set => set.IsPremade)
                .ToListAsync();

            foreach (var set in premadeVocabularySets)
            {
                _context.UserVocabularySets.Add(new UserVocabularySet
                {
                    UserId = userId,
                    VocabularySetId = set.Id,
                    Progress = 0
                });
            }

            var userWordCategories = PresetUserWordCategories(userId);
            _context.UserWordCategories.AddRange(userWordCategories);

            await _context.SaveChangesAsync();
        }
        
        private List<UserWordCategory> PresetUserWordCategories(string userId)
        {
            var userWordCategories = new List<UserWordCategory>
            {
                // Assign categories to words in Lesson 1
                new UserWordCategory { WordId = 3, CategoryId = 1, UserId = userId }, // to read - Verbs
                new UserWordCategory { WordId = 4, CategoryId = 1, UserId = userId }, // to listen - Verbs
                new UserWordCategory { WordId = 5, CategoryId = 1, UserId = userId }, // to watch - Verbs
                new UserWordCategory { WordId = 6, CategoryId = 2, UserId = userId }, // school - Places
                new UserWordCategory { WordId = 7, CategoryId = 2, UserId = userId }, // hospital - Places
        
                // Assign categories to words in Lesson 2
                new UserWordCategory { WordId = 10, CategoryId = 1, UserId = userId }, // to eat - Verbs
                new UserWordCategory { WordId = 11, CategoryId = 1, UserId = userId }, // to drink - Verbs
                new UserWordCategory { WordId = 12, CategoryId = 1, UserId = userId }, // to do - Verbs
                new UserWordCategory { WordId = 13, CategoryId = 1, UserId = userId }, // to come - Verbs
                new UserWordCategory { WordId = 14, CategoryId = 1, UserId = userId }, // to go - Verbs
                new UserWordCategory { WordId = 15, CategoryId = 1, UserId = userId }, // to return - Verbs
                new UserWordCategory { WordId = 20, CategoryId = 2, UserId = userId }  // library - Places
            };
            return userWordCategories;
        }
    }
}
