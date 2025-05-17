using InventarioMed_API.EndPoints;
using InventarioMed.Shared.Data.BD;
using InventarioMed.Shared.Data.Models;
using InventarioMed.Shared.Models;
using InventarioMed_Console;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options=> options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<InventarioMedContext>();
builder.Services
    .AddIdentityApiEndpoints<AccessUser>()
    .AddEntityFrameworkStores<InventarioMedContext>();
builder.Services.AddAuthorization();
builder.Services.AddTransient<DAL<Equipment>>();
builder.Services.AddTransient<DAL<Category>>();
builder.Services.AddTransient<DAL<Department>>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseAuthorization();

app.AddEndPointsEquipment();
app.AddEndPointsCategory();
app.AddEndPointsDepartment();


app.MapGroup("auth").MapIdentityApi<AccessUser>().WithTags("Authorization");

//app.MapPost("auth/logout", async ([FromServices] SignInManager<AccessUser>signInManager) => {
//    await signInManager.SignOutAsync();
//    return Results.Ok;
//}).RequireAuthorization().WithTags("Authorization");

app.MapPost("auth/logout", async (HttpContext httpContext) => {
    var signInManager = httpContext.RequestServices.GetRequiredService<SignInManager<AccessUser>>();
    await signInManager.SignOutAsync();
    return Results.Ok();
})
.RequireAuthorization()
.WithTags("Authorization");


app.UseSwagger();
app.UseSwaggerUI();

app.Run();
