namespace DataAccessLayer.Entities
{
    public class UserWordCategory
    {
        public int WordId { get; set; }
        public Word Word { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}