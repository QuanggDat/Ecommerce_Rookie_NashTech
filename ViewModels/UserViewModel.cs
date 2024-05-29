using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Enums;

namespace ViewModels
{
    public class UserViewModel
    {
        public Guid id { get; set; }
        public string fullName { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;
        public string email { get; set; } = null!;
        public string? address { get; set; }
        public string? image { get; set; }
        public DateTime dob { get; set; }
        public EGender gender { get; set; }
        public bool banStatus { get; set; }
    }

    public class CustomersRegisterViewModel
    {
        [EmailAddress(ErrorMessage = "Incorrect email format")]
        public string email { get; set; } = null!;

        [RegularExpression(@"0[9875]\d{8}", ErrorMessage = "The Vietnamese mobile phone number format is not correct!")]
        public string phoneNumber { get; set; } = null!;

        [MinLength(6, ErrorMessage = "Minimum 6 characters!")]
        [Required(ErrorMessage = "Please enter password !")]
        public string password { get; set; } = null!;


        [Required(ErrorMessage = "Please enter full name !")]
        [MaxLength(60, ErrorMessage = "Maximum 60 characters!")]
        public string fullName { get; set; } = null!;


        [MaxLength(60, ErrorMessage = "Maximum 60 characters!")]
        public string? address { get; set; }

        public string? image { get; set; }

        [DataType(DataType.Date)]
        public DateTime dob { get; set; }

        public EGender gender { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter email or phone number !")]
        public string emailOrPhoneNumber { get; set; } = null!;


        [MaxLength(20, ErrorMessage = "Maximum 20 characters!")]

        [MinLength(6, ErrorMessage = "Minimum 6 characters!")]
        public string password { get; set; } = null!;

    }

    public class UserUpdateViewModel
    {
        public Guid id { get; set; }
        public string fullName { get; set; } = null!;
        public string address { get; set; } = null!;
        public string? image { get; set; }
        public DateTime dob { get; set; }
        public EGender gender { get; set; }
    }
}
