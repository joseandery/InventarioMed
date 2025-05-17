using InventarioMed.Shared.Data.BD;
using InventarioMed.Shared.Models;
using InventarioMed_API.Requests;
using InventarioMed_API.Responses;
using InventarioMed_Console;
using Microsoft.AspNetCore.Mvc;

namespace InventarioMed_API.EndPoints
{
    public static class DepartmentExtension
    {
        public static void AddEndPointsDepartment(this WebApplication app)
        {
            app.MapGet("/Department", ([FromServices] DAL<Department> dal) =>
            {
                var deptList = dal.Read();
                if (deptList == null) return Results.NotFound();
                var deptResponseList = EntityListToResponseList(deptList);
                return Results.Ok(deptResponseList);
            });

            app.MapPost("/Department", ([FromServices] DAL<Department> dal, [FromBody] DepartmentRequest dept) =>
            {
                dal.Create(RequestToEntity(dept));
                return Results.Created();
            });

            app.MapDelete("/Department/{id}", ([FromServices] DAL<Department> dal, int id) =>
            {
                var dept = dal.ReadBy(e => e.Id == id);
                if (dept is null) return Results.NotFound();
                dal.Delete(dept);
                return Results.NoContent();
            });
        }

        private static Department RequestToEntity(DepartmentRequest dept)
        {
            return new Department() { Name = dept.Name };
        }

        private static List<DepartmentResponse> EntityListToResponseList(IEnumerable<Department> deptList)
        {
            return deptList.Select(d => EntityToResponse(d)).ToList();
        }

        private static DepartmentResponse EntityToResponse(Department d)
        {
            return new DepartmentResponse(d.Id, d.Name);
        }
    }
}
