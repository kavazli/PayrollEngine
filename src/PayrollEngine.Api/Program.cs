using Microsoft.EntityFrameworkCore;
using PayrollEngine.Application;
using PayrollEngine.Infrastructure;
using Scalar.AspNetCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<PayrollEngineDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    
    Console.WriteLine("\n🚀 API is running!");
    Console.WriteLine($"📚 Scalar API Documentation: {app.Urls.FirstOrDefault()?.Replace("http://", "https://")}/scalar/v1");
    Console.WriteLine($"📄 OpenAPI spec: {app.Urls.FirstOrDefault()?.Replace("http://", "https://")}/openapi/v1.json\n");
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


//   { "month": 1,  "workDay": 31, "baseSalary": 33030, "salaryIncreaseRate": 0, "overtime50": 0, "overtime100": 0, "bonusAmount": 0, "privateHealthInsurance": 0, "shoppingVoucher": 0 },
//   { "month": 2,  "workDay": 28, "baseSalary": 33030, "salaryIncreaseRate": 0, "overtime50": 0, "overtime100": 0, "bonusAmount": 0, "privateHealthInsurance": 0, "shoppingVoucher": 0 },
//   { "month": 3,  "workDay": 31, "baseSalary": 33030, "salaryIncreaseRate": 0, "overtime50": 0, "overtime100": 0, "bonusAmount": 0, "privateHealthInsurance": 0, "shoppingVoucher": 0 },
//   { "month": 4,  "workDay": 30, "baseSalary": 33030, "salaryIncreaseRate": 0, "overtime50": 0, "overtime100": 0, "bonusAmount": 0, "privateHealthInsurance": 0, "shoppingVoucher": 0 },
//   { "month": 5,  "workDay": 31, "baseSalary": 33030, "salaryIncreaseRate": 0, "overtime50": 0, "overtime100": 0, "bonusAmount": 0, "privateHealthInsurance": 0, "shoppingVoucher": 0 },
//   { "month": 6,  "workDay": 30, "baseSalary": 33030, "salaryIncreaseRate": 0, "overtime50": 0, "overtime100": 0, "bonusAmount": 0, "privateHealthInsurance": 0, "shoppingVoucher": 0 },
//   { "month": 7,  "workDay": 31, "baseSalary": 33030, "salaryIncreaseRate": 0, "overtime50": 0, "overtime100": 0, "bonusAmount": 0, "privateHealthInsurance": 0, "shoppingVoucher": 0 },
//   { "month": 8,  "workDay": 31, "baseSalary": 33030, "salaryIncreaseRate": 0, "overtime50": 0, "overtime100": 0, "bonusAmount": 0, "privateHealthInsurance": 0, "shoppingVoucher": 0 },
//   { "month": 9,  "workDay": 30, "baseSalary": 33030, "salaryIncreaseRate": 0, "overtime50": 0, "overtime100": 0, "bonusAmount": 0, "privateHealthInsurance": 0, "shoppingVoucher": 0 },
//   { "month": 10, "workDay": 31, "baseSalary": 33030, "salaryIncreaseRate": 0, "overtime50": 0, "overtime100": 0, "bonusAmount": 0, "privateHealthInsurance": 0, "shoppingVoucher": 0 },
//   { "month": 11, "workDay": 30, "baseSalary": 33030, "salaryIncreaseRate": 0, "overtime50": 0, "overtime100": 0, "bonusAmount": 0, "privateHealthInsurance": 0, "shoppingVoucher": 0 },
//   { "month": 12, "workDay": 31, "baseSalary": 33030, "salaryIncreaseRate": 0, "overtime50": 0, "overtime100": 0, "bonusAmount": 0, "privateHealthInsurance": 0, "shoppingVoucher": 0 }