using Uniqlo.Models.Base;

namespace Uniqlo.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double CostPrice { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public int CategoryId { get;set; }
        public Category Category { get; set; }
        public List<Size> Sizes { get; set; }


    }
}
