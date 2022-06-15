using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using HepsiTools.AutoMapper;
using HepsiTools.Business;
using HepsiTools.Business.Abstract;
using HepsiTools.Business.Concrate;
using HepsiTools.DataAccess;
using HepsiTools.Entities;
using HepsiTools.GenericRepositories.Abstract;
using HepsiTools.GenericRepositories.Concrate;
using HepsiTools.Helper;
using HepsiTools.MiddleWare;
using HepsiTools.Models;
using HepsiTools.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiTools
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ToolDbContext>(options =>options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddHttpContextAccessor();
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ToolDbContext>().AddDefaultTokenProviders();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SigningKey"]))
                };
            });

            services.AddMemoryCache();
            services.AddHttpClient();
            services.AddControllers();
            services.AddMvc().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            }).AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(s =>
            {
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer eyJhbGci...')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
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

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IErrorRepository, ErrorRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompetitionAnalsesHistoryRepository, CompetitionAnalsesHistoryRepository>();
            services.AddScoped<ICompetitionAnalysesRepository, CompetitionAnalysesRepository>();
            services.AddScoped<ILisansRepository, LisansRepository>();
            services.AddScoped<IWooCommerceDataRepository, WooCommerceDataRepository>();


            services.AddControllers().AddFluentValidation();

            services.AddTransient<IValidator<LoginModel>, LoginValidator>();
            services.AddTransient<IValidator<ResetPasswordModel>, ResetPasswordValidator>();
            services.AddTransient<IValidator<ChangePasswordModel>, ChangePasswordValidator>();
            services.AddTransient<IValidator<ForgetPasswordModel>, ForgetPasswordValidator>();
            services.AddTransient<IValidator<UserModel>, UserValidator>();
            services.AddTransient<IValidator<WooCommerceInsertModel>, WooCommerceInsertModelValidator>();
           
            services.AddTransient<IValidator<CompanyInsertModel>, CompanyInsertModelValidator>();
            services.AddTransient<IValidator<CompetitionAnalysesInsertModel>, CompetitionAnalysesInsertModelValidator>();
            services.AddTransient<IValidator<CompetitionAnalysesUpdateModel>, CompetitionAnalysesUpdateModelValidator>();


            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new ToolsProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

           // services.AddHostedService<CompetitionBackgroundService>();
            //services.AddHostedService<TimedHostedService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            app.UseMiddleware<ErrorHandling>();

            DataSeeding.Seed(userManager,roleManager);

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
