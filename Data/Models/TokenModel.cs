using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class TokenModel
    {
        public string accessToken { get; set; } = null!;
        public string tokenType { get; set; } = null!;
        public string userId { get; set; } = null!;
        public int expiresIn { get; set; }
        public string fullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
