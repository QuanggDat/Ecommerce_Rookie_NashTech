using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ProductModel
    {
        public Guid id { get; set; }
        public string name { get; set; } = null!;
        public string categoryName { get; set; } = null!;
        public double price { get; set; }
        public string? image { get; set; }
        public string? description { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
    }
    public class CreateProductModel
    {
        public string name { get; set; } = null!;
        public Guid categoryId { get; set; }
        public double price { get; set; }
        public string? image { get; set; }
        public string? description { get; set; }
    }
    public class UpdateProductModel
    {
        public string name { get; set; } = null!;
        public Guid categoryId { get; set; }
        public double price { get; set; }
        public string? image { get; set; }
        public string? description { get; set; }
    }
}
