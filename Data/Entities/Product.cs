using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }

        public string name { get; set; } = null!;

        public Guid categoryId { get; set; }

        public virtual Category Category { get; set; } = null!;

        public double price { get; set; }

        public string? image { get; set; }

        public string? description { get; set; }

        public DateTime createDate { get; set; }

        public DateTime updateDate { get; set; }
    }
}
