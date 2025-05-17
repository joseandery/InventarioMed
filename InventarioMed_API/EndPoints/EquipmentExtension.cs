using InventarioMed.Shared.Data.BD;
using InventarioMed.Shared.Models;
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
            var groupBuilder = app.MapGroup("equipment")
                .RequireAuthorization()
                .WithTags("Equipamentos");

            groupBuilder.MapGet("", ([FromServices] DAL<Equipment> dal) =>
            {
                var eqpList = dal.Read();
                if (eqpList == null) return Results.NotFound();
                var eqpResponseList = EntityListToResponseList(eqpList);
                return Results.Ok(eqpResponseList);
            });

            groupBuilder.MapGet("/{id}", (int id, [FromServices] DAL<Equipment> dal) =>
            {
                var eqp = dal.ReadBy(e => e.Id == id);
                if (eqp is null) return Results.NotFound();
                return Results.Ok(EntityToResponse(eqp));
            });

            groupBuilder.MapPost("", ([FromServices] DAL<Equipment> dal, [FromServices]DAL<Department> deptdal, [FromBody] EquipmentRequest eqp) =>
            {
                dal.Create(
                    new Equipment(eqp.name, eqp.manufacturer) { Departments = eqp.Departments is not null?
                    DepartmentRequestConvert(eqp.Departments, deptdal) :
                    new List<Department>()}
                );
                return Results.Created();
            });

            groupBuilder.MapDelete("/{id}", ([FromServices] DAL<Equipment> dal, int id) =>
            {
                var eqp = dal.ReadBy(e => e.Id == id);
                if (eqp is null) return Results.NotFound();
                dal.Delete(eqp);
                return Results.NoContent();
            });

            groupBuilder.MapPut("", ([FromServices] DAL<Equipment> dal, [FromBody] EquipmentEditRequest eqp) =>
            {
                var eqpToEdit = dal.ReadBy(e => e.Id == eqp.id);
                if (eqpToEdit is null) return Results.NotFound();
                eqpToEdit.Name = eqp.name;
                eqpToEdit.Manufacturer = eqp.manufacturer;
                dal.Update(eqpToEdit);
                return Results.Created();
            });
        }

        private static List<Department> DepartmentRequestConvert(ICollection<DepartmentRequest> deptList, DAL<Department> deptdal)
        {
            var departmentList = new List<Department>();
            foreach (var item in deptList)
            {
                var dept = RequestToEntity(item);
                var deptBuscado = deptdal.ReadBy(d => d.Name.ToUpper().Equals(dept.Name.ToUpper()));
                if (deptBuscado is not null) departmentList.Add(deptBuscado);
                else departmentList.Add(dept);
            }
            return departmentList;
        }

        private static Department RequestToEntity(DepartmentRequest dept)
        {
            return new Department() {Name = dept.Name};
        }

        private static ICollection<EquipmentResponse> EntityListToResponseList (IEnumerable<Equipment> eqpList)
        {
            return eqpList.Select(a => EntityToResponse(a)).ToList();
        }
        private static EquipmentResponse EntityToResponse (Equipment eqp)
        {
            return new EquipmentResponse(eqp.Id, eqp.Name, eqp.Manufacturer);
        }
    }
}
