using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region CROSS_ORIGIN
builder.Services.AddCors(options =>
{
    options.AddPolicy("devCorsPolicy", builder =>
    {
        builder.AllowAnyOrigin() // Replace with specific origins if needed
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
#endregion
#region Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

        };
    });
#endregion









// Add services to the container.
builder.Services.AddDbContext<EmployeeDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeCS")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //add pipeline of corss origin
    app.UseCors("devCorsPolicy");
}
app.UseHttpsRedirection();
//add authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
