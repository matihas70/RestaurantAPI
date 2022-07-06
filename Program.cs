using RestaurantAPI;
using AutoMapper;
using System.Reflection;
using RestaurantAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Register services

builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IDishService, DishService>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();
