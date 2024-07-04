using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public List<VocabularySet> VocabularySets { get; } = [];
        public List<UserVocabularySet> UserVocabularySets { get; } = [];
        public List<UserWordCategory> UserWordCategories { get; set; } = [];
    }
}
