using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<IAssignmentStudentRepository, AssignmentStudentRepository>();
builder.Services.AddScoped<ISemesterRepository, SemesterRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SWP_DATAContext>(options => options.UseSqlServer("Server=database-1.cjxoyrrscajx.ap-southeast-2.rds.amazonaws.com,1433;Initial Catalog=SWP_DATA;Persist Security Info=True;User ID=admin;Password=12345678;TrustServerCertificate=True"));

var app = builder.Build();

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