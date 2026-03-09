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
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


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