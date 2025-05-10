using InventarioMed_Console;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioMed.Shared.Data.BD
{
    public class EquipmentDAL
    {
        public void Create(Equipment eqp)
        {
            using var connection = new InventarioMedContext().Connect();
            connection.Open();

            string sql = "INSERT INTO Equipment (Name, Manufacturer) VALUES (@name, @manufacturer)";
            SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@name", eqp.Name);
            cmd.Parameters.AddWithValue("@manufacturer", eqp.Manufacturer);

            int retorno = cmd.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas: {retorno}");
        }
        public IEnumerable<Equipment> Read()
        {
            var list = new List<Equipment>();

            using var connection = new InventarioMedContext().Connect();
            connection.Open();

            string sql = "SELECT * FROM Equipment";
            SqlCommand cmd = new SqlCommand(sql, connection);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string eqpName = Convert.ToString(reader["Name"]);
                string eqpManufacturer = Convert.ToString(reader["Manufacturer"]);
                Equipment eqp = new(eqpName, eqpManufacturer);
                list.Add(eqp);
            }
            return list;
        }
        public void Update(Equipment eqp, int id)
        {
            using var connection = new InventarioMedContext().Connect();
            connection.Open();

            string sql = $"UPDATE Equipment SET Name = @name, Manufacturer = @manufacturer WHERE Id = @id";
            SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@name", eqp.Name);
            cmd.Parameters.AddWithValue("@manufacturer", eqp.Manufacturer);
            cmd.Parameters.AddWithValue("@id", id);

            int retorno = cmd.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas: {retorno}");
        }
        public void Delete(int id)
        {
            using var connection = new InventarioMedContext().Connect();
            connection.Open();

            string sql = $"DELETE FROM Equipment WHERE Id = @id";
            SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", id);
            int retorno = cmd.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas: {retorno}");
        }
    }
}
