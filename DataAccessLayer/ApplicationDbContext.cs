using DataAccessLayer.Configurations;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<VocabularySet> VocabularySets { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<UserVocabularySet> UserVocabularySets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserWordCategory> UserWordCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new VocabularySetConfiguration());
            modelBuilder.ApplyConfiguration(new UserVocabularySetConfiguration());
            modelBuilder.ApplyConfiguration(new WordConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new UserWordCategoryConfiguration());

            var sets = new List<VocabularySet>
            {
                new VocabularySet { Id = 1, Name = "Lesson 1", IsPremade = true },
                new VocabularySet { Id = 2, Name = "Lesson 2", IsPremade = true }
            };

            var words = new List<Word>()
            {
                // Lesson 1 words
                new Word { Id = 1, VocabularySetId = 1, EnglishWord = "apple", JapaneseWord = "りんご" },
                new Word { Id = 2, VocabularySetId = 1, EnglishWord = "banana", JapaneseWord = "バナナ" },
                new Word { Id = 3, VocabularySetId = 1, EnglishWord = "to read", JapaneseWord = "よむ" },
                new Word { Id = 4, VocabularySetId = 1, EnglishWord = "to listen", JapaneseWord = "きく" },
                new Word { Id = 5, VocabularySetId = 1, EnglishWord = "to watch", JapaneseWord = "みる" },
                new Word { Id = 6, VocabularySetId = 1, EnglishWord = "school", JapaneseWord = "がっこう" },
                new Word { Id = 7, VocabularySetId = 1, EnglishWord = "hospital", JapaneseWord = "びょういん" },
        
                // Lesson 2 words
                new Word { Id = 8, VocabularySetId = 2, EnglishWord = "car", JapaneseWord = "くるま" },
                new Word { Id = 9, VocabularySetId = 2, EnglishWord = "train", JapaneseWord = "でんしゃ" },
                new Word { Id = 10, VocabularySetId = 2, EnglishWord = "to eat", JapaneseWord = "たべる" },
                new Word { Id = 11, VocabularySetId = 2, EnglishWord = "to drink", JapaneseWord = "のむ" },
                new Word { Id = 12, VocabularySetId = 2, EnglishWord = "to do", JapaneseWord = "する" },
                new Word { Id = 13, VocabularySetId = 2, EnglishWord = "to come", JapaneseWord = "くる" },
                new Word { Id = 14, VocabularySetId = 2, EnglishWord = "to go", JapaneseWord = "いく" },
                new Word { Id = 15, VocabularySetId = 2, EnglishWord = "to return", JapaneseWord = "かえる" },
                new Word { Id = 16, VocabularySetId = 2, EnglishWord = "desk", JapaneseWord = "つくえ" },
                new Word { Id = 17, VocabularySetId = 2, EnglishWord = "pen", JapaneseWord = "ペン" },
                new Word { Id = 18, VocabularySetId = 2, EnglishWord = "book", JapaneseWord = "ほん" },
                new Word { Id = 19, VocabularySetId = 2, EnglishWord = "dictionary", JapaneseWord = "じしょ" },
                new Word { Id = 20, VocabularySetId = 2, EnglishWord = "library", JapaneseWord = "としょかん" }
            };

            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Verbs", IsPremade = true },
                new Category { Id = 2, Name = "Places", IsPremade = true }
            };

            modelBuilder.Entity<VocabularySet>().HasData(sets);
            modelBuilder.Entity<Word>().HasData(words);
            modelBuilder.Entity<Category>().HasData(categories);
        }
    }
}
