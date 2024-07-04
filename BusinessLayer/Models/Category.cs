namespace BusinessLayer.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPremade { get; set; }
        public ChangeType Change { get; set; } = ChangeType.None;
    }
}