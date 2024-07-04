using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class VocabularySet
    {
        public int Id { get; set; }
        public List<ApplicationUser> Users { get; } = [];
        public List<UserVocabularySet> UserVocabularySets { get; } = [];
        public string Name { get; set; }
        public List<Word> Words { get; set; } = [];
        public bool IsPremade { get; set; }
        public string? UserId { get; set; }
    }
}
