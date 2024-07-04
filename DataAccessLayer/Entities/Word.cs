using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Word
    {
        public int Id { get; set; }
        public string EnglishWord { get; set; }
        public string JapaneseWord { get; set; }
        public VocabularySet VocabularySet { get; set; }
        public int? VocabularySetId { get; set; }
        public List<UserWordCategory> UserWordCategories { get; set; }
    }
}
