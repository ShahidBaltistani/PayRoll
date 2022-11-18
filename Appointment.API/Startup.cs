using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using VUPayRoll.Database;
using VUPayRoll.Entities;
using VUPayRoll.Repository;
using VUPayRoll.Repository.Interfaces;
using VUPayRoll.Service;

namespace VUPayRoll.API
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
            services.AddCors(option1 =>
            {
                option1.AddPolicy("AllowOrigin", option => option.AllowAnyOrigin());
            });

            services.AddDbContextPool<VUPayRollDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("VUPayRollDBConnection")));
            services.AddControllers();
            services.AddMvc().AddXmlSerializerFormatters();
            var config = new AutoMapper.MapperConfiguration(c =>
            {
                c.AddProfile(new AutoMapperProfile());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IVUPayRoll<Employee>, EmployeeRepository>();
            services.AddScoped<IVUPayRoll<Dependent>, DependentRepository>();
            services.AddScoped<IVUPayRoll<Document>, DocumentRepository>();
            services.AddScoped<IVUPayRoll<EmployeeAllowance>, EmployeeAllowanceRepository>();
            services.AddScoped<IVUPayRoll<Leave>, LeaveRepository>();
            services.AddScoped<IVUPayRoll<Salary>, SalaryRepository>();
            services.AddScoped<ISalaryRepository, SalaryRepository>();
            services.AddScoped<IVUPayRoll<Attendance>, AttendanceRepository>();
            services.AddScoped<IVUPayRoll<Announcement>, AnnouncementRepository>();
            services.AddScoped<IVUPayRoll<ProjectName>, ProjectManagementRepository>();

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IEmployeeAllowanceRepository, EmployeeAllowanceRepository>();
            services.AddScoped<IDependentRepository, DependentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ILeaveRepository, LeaveRepository>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddScoped<IProjectManagementRepository, ProjectManagementRepository>();
            services.AddScoped<LeaveService>();
            services.AddScoped<EmployeeServices>();
            services.AddScoped<DependentService >();
            services.AddScoped<DocumentService>();
            services.AddScoped<EmployeeAllowanceService>();
            services.AddScoped<SalaryService>();
            services.AddScoped<AccountService>();
            services.AddScoped<AttendanceService>();
            services.AddScoped<AnnouncementService>();
            services.AddScoped<ProjectManagementService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options => {
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuerSigningKey = true,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                     .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                 ValidateIssuer = false,
                 ValidateAudience = false
             };
         });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }


}
