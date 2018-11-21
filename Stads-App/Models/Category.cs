namespace Stads_App.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string ImgPath => $"../Assets/Categories/{CategoryId}.jpg";
    }
}