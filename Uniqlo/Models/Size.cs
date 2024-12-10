using Uniqlo.Models.Base;

namespace Uniqlo.Models
{
    public class Size:BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set;} 
    }
}
