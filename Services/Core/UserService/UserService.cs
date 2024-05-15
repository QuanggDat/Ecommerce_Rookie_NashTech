using Data.DataAccess;
using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        public UserService(AppDbContext dbContext, IConfiguration configuration, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            _dbContext = dbContext;

            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<ResultModel> Login(LoginModel model)
        {
            var result = new ResultModel();

            var checkUserByEmail = _dbContext.User.FirstOrDefault(s => s.Email == model.emailOrPhoneNumber || s.PhoneNumber == model.emailOrPhoneNumber);

            if (checkUserByEmail == null)           
            {
                result.errorMessage = "Không tìm thấy Email/Số điện thoại!";
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(checkUserByEmail.Email);   
                
                //if (!user.EmailConfirmed)
                //{
                //    result.succeed = false;
                //    result.errorMessage = "Email chưa được xác nhận. Vui lòng kiểm tra hộp thư điện tử để xác nhận!";
                //}
                //else
                //{                                    
                //}

                var check = await _signInManager.CheckPasswordSignInAsync(user, model.password, false);
                if (!check.Succeeded)
                {
                    result.errorMessage = "Mật khẩu không đúng!";
                }
                else
                {
                    if (user.banStatus)
                    {
                        result.errorMessage = "Tài khoản đã bị khoá!";
                    }
                    else
                    {
                        var userRoles = _dbContext.UserRoles.Where(ur => ur.UserId == user.Id).ToList();
                        var roles = new List<string>();

                        foreach (var userRole in userRoles)
                        {
                            var role = await _dbContext.Role.FindAsync(userRole.RoleId);
                            if (role != null) roles.Add(role.Name);
                        }

                        var token = await GetAccessToken(user, roles);

                        result.succeed = true;
                        result.Data = token;
                    }
                }
            }            
            return result;
        }

        public async Task<ResultModel> RegisterCustomers(CustomersRegisterModel model)
        {
            var result = new ResultModel { succeed = false };

            try
            {
                // Ensure the "Customers" role exists
                if (!await _roleManager.RoleExistsAsync("Customers"))
                {
                    await _roleManager.CreateAsync(new Role { Name = "Customers" });
                }

                var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "Customers");

                // Create a new user
                var user = new User
                {
                    Email = model.email,
                    PhoneNumber = model.phoneNumber,
                    fullName = model.fullName,
                    NormalizedEmail = model.email,
                    UserName = model.email,
                    roleId = role.Id
                };

                // Check if phone number is already registered
                var userByPhone = await _dbContext.User.FirstOrDefaultAsync(s => s.PhoneNumber == user.PhoneNumber);

                if (userByPhone != null)
                {
                    result.errorMessage = "Số Điện Thoại Đã Được Đăng Kí!";
                }
                else if (user.PhoneNumber.Length < 9 || user.PhoneNumber.Length > 10)
                {
                    result.errorMessage = "Số Điện Thoại Không Hợp Lệ!";
                }
                else
                {
                    // Check if email is already registered
                    var userByMail = await _dbContext.User.FirstOrDefaultAsync(s => s.Email == user.Email);

                    if (userByMail != null)
                    {
                        result.errorMessage = "Email Đã Tồn Tại!";
                    }
                    else
                    {
                        // Create the new user
                        var check = await _userManager.CreateAsync(user, model.password);

                        if (check.Succeeded)
                        {
                            // Assign the "Customers" role to the user
                            var userRole = new UserRole
                            {
                                RoleId = role.Id,
                                UserId = user.Id
                            };

                            _dbContext.UserRoles.Add(userRole);
                            await _dbContext.SaveChangesAsync();

                            result.succeed = true;
                            result.Data = user.Id;
                        }
                        else
                        {
                            result.errorMessage = "Xác Thực Sai!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorMessage = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }

        private async Task<TokenModel> GetAccessToken(User user, List<string> roles)
        {
            // Get claims for the user
            List<Claim> claims = GetClaims(user, roles);

            // Generate security key from the JWT key configuration
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(int.Parse(_configuration["Jwt:ExpireTimes"])),
                signingCredentials: creds
            );

            // Serialize the token
            var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

            // Create and return the token model
            return new TokenModel
            {
                accessToken = serializedToken,
                tokenType = "Bearer",
                expiresIn = int.Parse(_configuration["Jwt:ExpireTimes"]) * 3600,
                userId = user.Id.ToString(),
                PhoneNumber = user.PhoneNumber,
                Role = user.Role
            };
        }

        private List<Claim> GetClaims(User user, List<string> roles)
        {
            // Initialize identity options (this line seems redundant unless used elsewhere)
            IdentityOptions _options = new IdentityOptions();

            // Create a list of claims and add basic user information
            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim("UserName", user.UserName)
            };

            // Add role claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Add phone number claim if it exists
            if (!string.IsNullOrEmpty(user.PhoneNumber))
            {
                claims.Add(new Claim("PhoneNumber", user.PhoneNumber));
            }

            return claims;
        }
    }
}
