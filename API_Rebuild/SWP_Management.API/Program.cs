using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<IAssignmentStudentRepository, AssignmentStudentRepository>();
builder.Services.AddScoped<ISemesterRepository, SemesterRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<ILecturerRepository, LecturerRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentCourseRepository, StudentCourseRepository>();
builder.Services.AddScoped<IStudentTeamRepository, StudentTeamRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITopicRepository, TopicRepository>();
builder.Services.AddScoped<ITopicAssignRepository, TopicAssignRepository>();
builder.Services.AddScoped<ISubjectTopicRepository, SubjectTopicRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
builder.Services.AddCors(p => p.AddPolicy("MyCors", build => {
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));






// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SWP_DATAContext>(options => options.UseSqlServer("Server=database-1.cjxoyrrscajx.ap-southeast-2.rds.amazonaws.com,1433;Initial Catalog=SWP_DATA;Persist Security Info=True;User ID=admin;Password=12345678;TrustServerCertificate=True"));

var app = builder.Build();
app.UseCors("MyCors");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();


}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();