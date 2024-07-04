using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPremade { get; set; }
        public string? UserId { get; set; }

        public ApplicationUser User { get; set; }
        public List<UserWordCategory> UserWordCategories { get; set; } = [];
    }
}
