namespace APIKarakatsiya.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string NameEn { get; set; } = string.Empty;
        public string NameUk { get; set; } = string.Empty;

        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
