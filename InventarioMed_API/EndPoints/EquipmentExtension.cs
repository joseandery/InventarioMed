using InventarioMed.Shared.Data.BD;
using InventarioMed_API.Requests;
using InventarioMed_API.Responses;
using InventarioMed_Console;
using Microsoft.AspNetCore.Mvc;

namespace InventarioMed_API.EndPoints
{
    public static class EquipmentExtension
    {
        public static void AddEndPointsEquipment(this WebApplication app)
        {
            app.MapGet("/Equipment", ([FromServices] DAL<Equipment> dal) =>
            {
                var eqpList = dal.Read();
                if (eqpList == null) return Results.NotFound();
                var eqpResponseList = EntityListToResponseList(eqpList);
                return Results.Ok(eqpResponseList);
            });

            app.MapPost("/Equipment", ([FromServices] DAL<Equipment> dal, [FromBody] EquipmentRequest eqp) =>
            {
                dal.Create(new Equipment(eqp.name, eqp.manufacturer));
                return Results.Created();
            });

            app.MapDelete("/Equipment/{id}", ([FromServices] DAL<Equipment> dal, int id) =>
            {
                var eqp = dal.ReadBy(e => e.Id == id);
                if (eqp is null) return Results.NotFound();
                dal.Delete(eqp);
                return Results.NoContent();
            });

            app.MapPut("/Equipment", ([FromServices] DAL<Equipment> dal, [FromBody] EquipmentEditRequest eqp) =>
            {
                var eqpToEdit = dal.ReadBy(e => e.Id == eqp.id);
                if (eqpToEdit is null) return Results.NotFound();
                eqpToEdit.Name = eqp.name;
                eqpToEdit.Manufacturer = eqp.manufacturer;
                dal.Update(eqpToEdit);
                return Results.Created();
            });
        }
        private static ICollection<EquipmentResponse> EntityListToResponseList (IEnumerable<Equipment> entities)
        {
            return entities.Select(a => EntityToResponse(a)).ToList();
        }
        private static EquipmentResponse EntityToResponse (Equipment entity)
        {
            return new EquipmentResponse(entity.Id, entity.Name, entity.Manufacturer);
        }
    }
}
