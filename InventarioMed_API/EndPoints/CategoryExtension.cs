using InventarioMed.Shared.Data.BD;
using InventarioMed_API.Responses;
using InventarioMed_Console;
using Microsoft.AspNetCore.Mvc;

namespace InventarioMed_API.EndPoints
{
    public static class CategoryExtension
    {
        public static void AddEndPointsCategory(this WebApplication app)
        {
            app.MapGet("/Category", ([FromServices] DAL<Category> dal) =>
            {
                var catList = dal.Read();
                var catResponseList = EntityListToResponseList(catList);
                return Results.Ok(catResponseList);
            });

            app.MapPost("/Category", ([FromServices] DAL<Category> dal, [FromBody] Category cat) =>
            {
                dal.Create(cat);
                return Results.Created();
            });

            app.MapDelete("/Category/{id}", ([FromServices] DAL<Category> dal, int id) =>
            {
                var cat = dal.ReadBy(c => c.Id == id);
                if (cat is null) return Results.NotFound();
                dal.Delete(cat);
                return Results.NoContent();
            });

            app.MapPut("/Category", ([FromServices] DAL<Category> dal, [FromBody] Category cat) =>
            {
                var catToEdit = dal.ReadBy(c => c.Id == cat.Id);
                if (catToEdit is null) return Results.NotFound();
                catToEdit.Name = cat.Name;
                dal.Update(catToEdit);
                return Results.Created();
            });

        }

        private static ICollection<CategoryResponse> EntityListToResponseList(IEnumerable<Category> entities)
        {
            return entities.Select(a => EntityToResponse(a)).ToList();
        }
        private static CategoryResponse EntityToResponse(Category entity)
        {
            return new CategoryResponse(
                entity.Id, 
                entity.Name, 
                entity.Equipment?.Id?? 0,
                entity.Equipment?.Name?? "No equipment");
        }
    }
}
