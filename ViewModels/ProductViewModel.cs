using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ProductViewModel
    {
        public Guid id { get; set; }
        public string name { get; set; } = null!;
        public Guid categoryId { get; set; }
        public string categoryName { get; set; } = null!;
        public double price { get; set; }
        public string? image { get; set; }
        public string? description { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
    }

    public class RootProduct
    {
        public List<ProductViewModel> data { get; set; }
        public int total { get; set; }
    }
}
