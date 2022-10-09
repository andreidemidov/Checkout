using Checkout.Application.Articles.Commands.CreateArticle;
using Checkout.Application.Baskets.Commands.CreateBasket;
using Checkout.Domain.Interfaces;
using Checkout.Persistence.Configuration;
using Checkout.Persistence.Interfaces;
using Checkout.Persistence.Repositories;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBasketService, BasketRepository>();
builder.Services.AddMediatR(typeof(CreateBasketCommand).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(CreateArticleCommand).GetTypeInfo().Assembly);
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
