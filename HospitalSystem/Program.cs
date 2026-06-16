
using DomainLayer;
using DomainLayer.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using Persistence.Data;
using Persistence.Data.DataSeed;
using Persistence.Repository;
using Presentation;
using Service;
using Service.AutoMapper;
using ServiceAbstraction;
using System;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Add services to the container.

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddAutoMapper(x => x.AddProfile(new DoctorMapping()));
            builder.Services.AddAutoMapper(x => x.AddProfile(new PatientMappint()));
            builder.Services.AddDbContext<HospitalContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                //option.UseSqlServer(builder.Configuration.GetSection("ConnectionString")["DefaultConnection"]);
            });
            builder.Services.AddDbContext<IdentityDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });



            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IServiceMamager, ServiceManager>();
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddHttpClient();
            builder.Services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option=> {

                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["JWT:issuer"],
                    ValidAudience= builder.Configuration["JWT:audience"],
                    IssuerSigningKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]!)) 
                };

            });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
  .AddEntityFrameworkStores<IdentityDbContext>()
  .AddDefaultTokenProviders();
            // builder.Services.AddControllers()
            //.AddApplicationPart(typeof(DoctorController).Assembly);
            #endregion

            var app = builder.Build();
            var scope=app.Services.CreateScope();
            var dataSeed=scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await dataSeed.IdentityDataSeeding();

            #region Configure the HTTP request pipeline.

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
