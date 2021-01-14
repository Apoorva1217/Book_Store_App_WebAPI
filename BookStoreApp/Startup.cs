using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.IServices;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer;
using RepositoryLayer.IServices;
using RepositoryLayer.Services;

namespace BookStoreApp
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
            var key = Encoding.UTF8.GetBytes(Configuration["Key"]);
            services.AddTransient<IAdminBL, AdminBL>();
            services.AddTransient<IAdminRL, AdminRL>();

            services.AddTransient<IUserBL, UserBL>();
            services.AddTransient<IUserRL, UserRL>();
            services.AddTransient<IProductRL, ProductRL>();
            services.AddTransient<IProductBL, ProductBL>();
            services.AddTransient<ICartRL, CartRL>();
            services.AddTransient<ICartBL, CartBL>();
            services.AddTransient<ICustomerRL, CustomerRL>();
            services.AddTransient<ICustomerBL, CustomerBL>();
            services.AddTransient<IOrderRL, OrderRL>();
            services.AddTransient<IOrderBL, OrderBL>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //ClockSkew = TimeSpan.Zero
                };
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo { Title = "BSA", Version = "v1.0", Description = "Book Store App" });
                //   c.OperationFilter<FileUploadedOperation>(); ////Register File Upload Operation Filter
                //c.DescribeAllEnumsAsStrings();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                //  c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                //  {
                //  { "Bearer", new string[] {} }
                //});
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new List<string>()
                      }
                    });
            });
            services.AddCors();

            services.AddSession(opts =>
            {
                opts.Cookie.Name = "LoggedInUser";
                //opts.IdleTimeout = TimeSpan.FromMinutes(180);
            });
            services.AddDbContext<BookStoreContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:BookStoreContext"]));
            services.AddMvc();
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "BookStoreApp (V 1.0)");
            });

            app.UseDeveloperExceptionPage();
            //app.UseMvc();
        }
    }
}