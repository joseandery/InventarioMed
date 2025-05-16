using InventarioMed.Shared.Data.BD;
using InventarioMed_Console;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options=> options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<InventarioMedContext>();
builder.Services.AddTransient<DAL<Equipment>>();

var app = builder.Build();

app.MapGet("/Equipment", ([FromServices]DAL<Equipment> dal) =>
{
    return Results.Ok(dal.Read());
});

app.MapPost("/Equipment", ([FromServices]DAL<Equipment> dal,[FromBody] Equipment eqp) =>
{
    dal.Create(eqp);
    return Results.Created();
});

app.MapDelete("/Equipment/{id}", ([FromServices]DAL<Equipment> dal, int id) =>
{
    var eqp = dal.ReadBy(e=>e.Id==id);
    if (eqp is null) return Results.NotFound();
    dal.Delete(eqp);
    return Results.NoContent();
});

app.MapPut("/Equipment", ([FromServices] DAL<Equipment> dal, [FromBody]Equipment eqp) =>
{
    var eqpToEdit = dal.ReadBy(e => e.Id == eqp.Id);
    if (eqpToEdit is null) return Results.NotFound();
    eqpToEdit.Name = eqp.Name;
    eqpToEdit.Manufacturer = eqp.Manufacturer;
    dal.Update(eqpToEdit);
    return Results.Created();
});

app.Run();
