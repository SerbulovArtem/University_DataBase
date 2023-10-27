using University.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace University.DAL.Concrete
{
    public class DepartmentRep
    {
        private string connectionString;
        List<Department> departments;

        public DepartmentRep(string connectionString)
        {
            this.connectionString = connectionString;
            departments = new List<Department>();
        }


        public void CreateDepartment(Department department)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO \"Departments\" (department_name) VALUES (@DepartmentName)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);

                cmd.ExecuteNonQuery();
            }
        }

        public List<Department> ReadAllDepartments()
        {
            departments.Clear();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM \"Departments\"";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Department department = new Department();
                    department.DepartmentId = reader.GetInt32(0);
                    department.DepartmentName = reader.GetString(1);
                    departments.Add(department);
                }
            }

            return departments;
        }


        public void UpdateDepartment(long departmentId, Department updatedDepartment)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE \"Departments\" SET department_name = @DepartmentName WHERE department_id = @DepartmentID";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@DepartmentName", updatedDepartment.DepartmentName);
                cmd.Parameters.AddWithValue("@DepartmentID", departmentId);

                cmd.ExecuteNonQuery();
            }
        }

        public bool DeleteDepartment(int departmentId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM \"Departments\" WHERE department_id = @DepartmentID";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@DepartmentID", departmentId);

                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public void ShowDepartmentList()
        {
            List<Models.Department> departmentList = new List<Models.Department>();
            departmentList = ReadAllDepartments();

            foreach (var department in departmentList)
            {
                Console.WriteLine(department.DepartmentId + ". " + department.DepartmentName);
            }
        }

        public Department GetDepartmentById(long departmentId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM \"Departments\" WHERE department_id = @DepartmentID";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@DepartmentID", departmentId);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Models.Department department = new Models.Department
                    {
                        DepartmentId = (long)reader["department_id"],
                        DepartmentName = reader["department_name"].ToString(),
                    };
                    return department;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
