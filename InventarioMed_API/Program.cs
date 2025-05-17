using InventarioMed.Shared.Data.BD;
using InventarioMed.Shared.Models;
using InventarioMed_Console;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options=> options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<InventarioMedContext>();
builder.Services.AddTransient<DAL<Equipment>>();
builder.Services.AddTransient<DAL<Category>>();
builder.Services.AddTransient<DAL<Department>>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.AddEndPointsEquipment();
app.AddEndPointsCategory();
app.AddEndPointsDepartment();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
