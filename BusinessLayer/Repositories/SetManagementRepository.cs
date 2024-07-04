using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repositories
{
    public class SetManagementRepository
    {
        private readonly ApplicationDbContext _context;
        public SetManagementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateCustomVocabularySetAsync(string userId, string setName, List<string[]> words)
        {
            var vocabularySet = new VocabularySet
            {
                Name = setName,
                IsPremade = false,
                UserId = userId,
                Words = words.Select(w => new Word { EnglishWord = w[0], JapaneseWord = w[1] }).ToList()
            };

            _context.VocabularySets.Add(vocabularySet);
            await _context.SaveChangesAsync();

            var userVocabularySet = new UserVocabularySet
            {
                UserId = userId,
                VocabularySetId = vocabularySet.Id,
                Progress = 0
            };

            _context.UserVocabularySets.Add(userVocabularySet);
            await _context.SaveChangesAsync();
        }

        public async Task<Models.Set> GetSetAsync(string userId, int setId)
        {
            Models.Set set = await _context.VocabularySets
                .Where(s => s.UserId == userId && s.Id == setId)
                .Select(s => new Models.Set
                {
                    Id = setId,
                    Name = s.Name,
                    Words = s.Words.Select(w => new Models.Word
                    {
                        Id = w.Id,
                        EnglishWord = w.EnglishWord,
                        JapaneseWord = w.JapaneseWord,
                        Categories = w.UserWordCategories.Select(wc => new Models.Category
                        {
                            Id = wc.WordId,
                            Name = wc.Category.Name,
                            IsPremade = wc.Category.IsPremade
                        }).ToList()
                    }).ToList()
                })
                .FirstOrDefaultAsync();
            if (set == null)
            {
                throw new KeyNotFoundException("Set not found or you don't have access to it.");
            }
            return set;
        }

        public async Task AddWordAsync(string userId, int setId, Models.Word word)
        {
            var newWord = new Word
            {
                EnglishWord = word.EnglishWord,
                JapaneseWord = word.JapaneseWord,
                VocabularySetId = setId
            };
            _context.Words.Add(newWord);
            await _context.SaveChangesAsync();

            if (word.Categories != null)
            {
                foreach (var category in word.Categories)
                {
                    _context.UserWordCategories.Add(new UserWordCategory
                    {
                        WordId = newWord.Id,
                        CategoryId = category.Id,
                        UserId = userId
                    });
                }            
            }
            await _context.SaveChangesAsync();
        }
        public async Task DeleteWordAsync(int setId, int wordId)
        {
            var word = await _context.Words.FirstOrDefaultAsync(w => w.Id == wordId && w.VocabularySetId == setId);
            if (word != null)
            {
                _context.Words.Remove(word);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateWordAsync(string userId, int setId, Models.Word word)
        {
            var existingWord = await _context.Words.FirstOrDefaultAsync(w => w.Id == word.Id && w.VocabularySetId == setId);
            if (existingWord != null)
            {
                existingWord.EnglishWord = word.EnglishWord;
                existingWord.JapaneseWord = word.JapaneseWord;

                var existingCategories = await _context.UserWordCategories
                    .Where(uwc => uwc.WordId == word.Id)
                    .ToListAsync();

                _context.UserWordCategories.RemoveRange(existingCategories);

                if (word.Categories != null)
                {
                    foreach (var category in word.Categories)
                    {
                        _context.UserWordCategories.Add(new UserWordCategory
                        {
                            WordId = word.Id,
                            CategoryId = category.Id,
                            UserId = userId
                        });
                    }
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
