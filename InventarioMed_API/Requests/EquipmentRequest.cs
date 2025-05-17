using InventarioMed.Shared.Models;

namespace InventarioMed_API.Requests
{
    public record EquipmentRequest (string name, string manufacturer, ICollection<DepartmentRequest> Departments = null);
}
