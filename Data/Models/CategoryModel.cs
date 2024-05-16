using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class CategoryModel
    {
        public Guid id { get; set; }
        public string name { get; set; } = null!;
        public string? description { get; set; }
        public int quantityProduct { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
    }

    public class CreateCategoryModel
    {
        public string name { get; set; } = null!;
        public string? description { get; set; }   
    }

    public class UpdateCategoryModel
    {
        public Guid id { get; set; }
        public string name { get; set; } = null!;
        public string? description { get; set; }
    }
}
