
using GYMFeeManagement_System_BE.Database;
using GYMFeeManagement_System_BE.IRepositories;
using GYMFeeManagement_System_BE.IServices;
using GYMFeeManagement_System_BE.Repositories;
using GYMFeeManagement_System_BE.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace GYMFeeManagement_System_BE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseWebRoot("wwwroot");

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddControllers()
             .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                 // options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                 options.JsonSerializerOptions.MaxDepth = 64;
             });

            builder.Services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = 5000; // Adjust this value as needed
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<GymDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("GymDbConnection")));

            /*branch*/
            builder.Services.AddScoped<IBranchRepository, BranchRepository>();
            builder.Services.AddScoped<IBranchService, BranchService>();

            /*programType*/
            builder.Services.AddScoped<IProgramTypeRepository, ProgramTypeRepository>();
            builder.Services.AddScoped<IProgramTypeService, ProgramTypeService>();

            /*member*/
            builder.Services.AddScoped<IMemberRepository, MemberRepository>();
            builder.Services.AddScoped<IMemberService, MemberService>();

            /*staff*/
            builder.Services.AddScoped<IStaffRepository, StaffRepository>();
            builder.Services.AddScoped<IStaffService, StaffService>();

            /*trainingProgram*/
            builder.Services.AddScoped<ITrainingProgramRepository, TrainingProgramRepository>();
            builder.Services.AddScoped<ITrainingProgramService, TrainingProgramService>();

            /*workoutPlan*/
            builder.Services.AddScoped<IWorkoutPlanRepository, WorkoutPlanRepository>();
            builder.Services.AddScoped<IWorkoutPlanService, WorkoutPlanService>();

            /*workoutEnroll*/
            builder.Services.AddScoped<IWorkoutEnrollRepository, WorkoutEnrollRepository>();
            builder.Services.AddScoped<IWorkoutEnrollService, WorkoutEnrollService>();

            /*enrollProgram*/
            builder.Services.AddScoped<IEnrollProgramRepository, EnrollProgramRepository>();
            builder.Services.AddScoped<IEnrollProgramService, EnrollProgramService>();

            /*alert*/
            builder.Services.AddScoped<IAlertRepository, AlertRepository>();
            builder.Services.AddScoped<IAlertService, AlertService>();

            /*payment*/
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();

            /*request*/
            builder.Services.AddScoped<IRequestRepository, RequestRepository>();
            builder.Services.AddScoped<IRequestService, RequestService>();

            /*review*/
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IReviewService, ReviewService>();

            /*request*/
            builder.Services.AddScoped<IContactUsMessageRepository, ContactUsMessageRepository>();
            builder.Services.AddScoped<IContactUsMessageService, ContactUsMessageService>();



            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("AllowSpecificOrigins",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowSpecificOrigins");

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}