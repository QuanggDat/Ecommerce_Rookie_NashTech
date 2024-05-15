using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class UserModel
    {
    }

    //public class UserRegisterModel
    //{
    //    public string email { get; set; } = null!;
    //    public string phoneNumber { get; set; } = null!;
    //    public string password { get; set; } = null!;
    //    public string fullName { get; set; } = null!;
    //    public string? address { get; set; } = null!;
    //    public string? image { get; set; }
    //    public DateTime dob { get; set; }
    //    public EGender gender { get; set; }
    //}

    public class CustomersRegisterModel
    {
        public string email { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;
        public string password { get; set; } = null!;
        public string fullName { get; set; } = null!;
    }

    public class LoginModel
    {
        public string emailOrPhoneNumber { get; set; } = null!;
        public string password { get; set; } = null!;

    }
}
