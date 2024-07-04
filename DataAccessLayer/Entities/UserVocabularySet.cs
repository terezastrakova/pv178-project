using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class UserVocabularySet
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int VocabularySetId { get; set; }
        public int Progress { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual VocabularySet VocabularySet { get; set; }
    }
}
