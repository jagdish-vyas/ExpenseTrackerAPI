using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Api.Data;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddControllers(); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 2. YE LINE ADD KI HAI
app.MapControllers(); 

app.Run();