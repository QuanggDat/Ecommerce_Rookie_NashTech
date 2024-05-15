using Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class User : IdentityUser<Guid>
    {       
        public Guid? roleId { get; set; }

        public virtual Role? Role { get; set; } = null!;

        public string fullName { get; set; } = null!;

        public string? image { get; set; }

        public string? address { get; set; }

        public DateTime dob { get; set; }

        public bool banStatus { get; set; }

        public EGender gender { get; set; }       
    }
}
