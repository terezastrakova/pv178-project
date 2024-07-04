using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class VocabularySetCard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GuessedWords { get; set; }
        public int TotalWords { get; set; }
    }
}
