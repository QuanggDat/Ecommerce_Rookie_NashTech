using Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string fullName { get; set; } = null!;

        public string? image { get; set; }

        public string? address { get; set; }

        public DateTime dob { get; set; }

        public EGender gender { get; set; }
    }
}
