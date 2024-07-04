using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string EnglishWord { get; set; }
        public string JapaneseWord { get; set; }
        public List<Category> Categories { get; set; }
        public ChangeType Change { get; set; } = ChangeType.None;
    }
}
