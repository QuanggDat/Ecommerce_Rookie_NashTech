using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class CategoryViewModel
    {
        public Guid id { get; set; }
        public string name { get; set; } = null!;
        public string? description { get; set; }
        public int quantityProduct { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
    }
    public class RootObject
    {
        public List<CategoryViewModel> data { get; set; }
        public int total { get; set; }
    }
}
