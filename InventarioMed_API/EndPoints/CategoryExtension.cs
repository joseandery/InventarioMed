﻿using InventarioMed.Shared.Data.BD;
using InventarioMed_API.Requests;
using InventarioMed_API.Responses;
using InventarioMed_Console;
using Microsoft.AspNetCore.Mvc;

namespace InventarioMed_API.EndPoints
{
    public static class CategoryExtension
    {
        public static void AddEndPointsCategory(this WebApplication app)
        {
            var groupBuilder = app.MapGroup("category")
                .RequireAuthorization()
                .WithTags("Categories");

            groupBuilder.MapGet("", ([FromServices] DAL<Category> dal) =>
            {
                var catList = dal.Read();
                var catResponseList = EntityListToResponseList(catList);
                return Results.Ok(catResponseList);
            });

            groupBuilder.MapGet("/{id}", (int id, [FromServices] DAL<Category> dal) =>
            {
                var cat = dal.ReadBy(c => c.Id == id);
                if (cat is null) return Results.NotFound();
                return Results.Ok(EntityToResponse(cat));
            });
            groupBuilder.MapPost("", ([FromServices] DAL<Category> dal, [FromBody] CategoryRequest catRequest) =>
            {
                dal.Create(new Category(catRequest.name));
                return Results.Ok();
            });


            groupBuilder.MapDelete("/{id}", ([FromServices] DAL<Category> dal, int id) =>
            {
                var cat = dal.ReadBy(c => c.Id == id);
                if (cat is null) return Results.NotFound();
                dal.Delete(cat);
                return Results.NoContent();
            });

            groupBuilder.MapPut("", ([FromServices] DAL<Category> dal, [FromBody] CategoryEditRequest catRequest) =>
            {
                var catToEdit = dal.ReadBy(a => a.Id == catRequest.id);
                if (catToEdit is null) return Results.NotFound();
                catToEdit.Name = catRequest.name;
                dal.Update(catToEdit);
                return Results.Created();
            });


        }

        private static ICollection<CategoryResponse> EntityListToResponseList(IEnumerable<Category> catList)
        {
            return catList.Select(a => EntityToResponse(a)).ToList();
        }
        private static CategoryResponse EntityToResponse(Category cat)
        {
            return new CategoryResponse(
                cat.Id, 
                cat.Name, 
                cat.Equipment?.Id?? 0,
                cat.Equipment?.Name?? "No equipment");
        }
    }
}
