using Data.DataAccess;
using Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.Core.CategoryService;
using Services.Core.ProductService;
using Services.Core.UserService;
using System.Text;
using System.Text.Json;

namespace Ecommerce_Rookie_NashTech.Extensions
{
    public static class StartupExtensions
    {
        //Add Config Identity
        public static void ConfigIdentityService(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            });

            builder.AddSignInManager<SignInManager<User>>();

            builder.AddEntityFrameworkStores<AppDbContext>()
                   .AddDefaultTokenProviders();            

            services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddUserManager<UserManager<User>>()
                    .AddRoleManager<RoleManager<Role>>()
                    .AddDefaultTokenProviders();

            services.AddAuthorization();
        }

        //Add Scoped
        public static void AddBussinessService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
        }

        public static void AddJWTAuthentication(this IServiceCollection services, string key, string issuer)
        {
            services.AddAuthorization()
                    .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(jwtconfig =>
                    {
                        jwtconfig.SaveToken = true;
                        jwtconfig.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                            ValidateAudience = false,
                            ValidIssuer = issuer,
                            ValidateIssuer = true,
                            ValidateLifetime = false,
                            RequireAudience = false,
                        };
                        jwtconfig.Events = new JwtBearerEvents
                        {
                            OnChallenge = context =>
                            {
                                context.HandleResponse();
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                context.Response.ContentType = "application/json";

                                // Ensure we always have an error and error description.
                                if (string.IsNullOrEmpty(context.Error))
                                    context.Error = "invalid_token";
                                if (string.IsNullOrEmpty(context.ErrorDescription))
                                    context.ErrorDescription = "This request requires a valid JWT access token to be provided";

                                // Add some extra context for expired tokens.
                                if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
                                {
                                    var authenticationException = context.AuthenticateFailure as SecurityTokenExpiredException;
                                    context.Response.Headers.Add("x-token-expired", authenticationException!.Expires.ToString("o"));
                                    context.ErrorDescription = $"The token expired on {authenticationException.Expires:o}";
                                }

                                return context.Response.WriteAsync(JsonSerializer.Serialize(new
                                {
                                    error = context.Error,
                                    error_description = context.ErrorDescription
                                }));
                            },
                            OnAuthenticationFailed = context =>
                            {
                                Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                                return Task.CompletedTask;
                            },
                            OnTokenValidated = context =>
                            {
                                Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                                return Task.CompletedTask;
                            },
                            OnMessageReceived = context =>
                            {
                                var accessToken = context.Request.Query["access_token"];

                                // If the request is for our hub...
                                var path = context.HttpContext.Request.Path;
                                if (path.StartsWithSegments("/notificationHub") || path.StartsWithSegments("/commentHub"))
                                {
                                // Read the token out of the query string
                                    context.Token = accessToken;
                                }
                                return Task.CompletedTask;
                            }
                        };
                    });
        }

        public static void AddSwaggerWithAuthentication(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "WorkshopManagementSystem_BWM_APP", Version = "1.0" }); opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement                   
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                          Array.Empty<string>()
                    }                    
                });
            });
        }
    }
}
