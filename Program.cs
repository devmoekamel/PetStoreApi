
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PetStore.Context;
using PetStore.Helper;
using PetStore.Model;
using PetStore.Services.AuthService;
using PetStore.Services.PetService;
using PetStore.Services.StoreService;
using System.Text;

namespace PetStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //---------------------------------------------------------------------------------
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtoptions =>
            {
                jwtoptions.RequireHttpsMetadata = false;
                jwtoptions.SaveToken = false;
                jwtoptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                };
            });
            //-------------------------------Register DBContext ------------------------------------
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DB"));
            });
     //-------------------------------Map JWT Data ------------------------------------
            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

     //------------------------------Roles&User---------------------------------
            builder.Services.AddIdentity<ApplicationUser,IdentityRole>().
                AddEntityFrameworkStores<ApplicationDBContext>();
    //------------------------------Addscope---------------------------------
           builder.Services.AddScoped<IAuthService,AuthService>();
           builder.Services.AddScoped<IStoreService, StoreService>();
           builder.Services.AddScoped<IPetService,PetService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
