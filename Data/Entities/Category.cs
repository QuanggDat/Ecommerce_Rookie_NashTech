using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }
        public string name { get; set; } = null!;
        public string? description { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
        public virtual List<Product> Products { get; set; } = new();
    }
}
