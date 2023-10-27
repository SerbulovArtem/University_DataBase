using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.DAL.Models;
using Npgsql;

namespace University.DAL.Concrete
{
    public class SubjectRep
    {
        private string connectionString;
        private List<Subject> subjects;

        public SubjectRep(string connectionString)
        {
            this.connectionString = connectionString;
            subjects = new List<Subject>();
        }

        public void CreateSubject(string subjectName, long departmentId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO \"Subjects\" (subject_name, department_id) VALUES (@SubjectName, @DepartmentId)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@SubjectName", subjectName);
                cmd.Parameters.AddWithValue("@DepartmentId", departmentId);

                cmd.ExecuteNonQuery();
            }
        }

        public List<Subject> ReadAllSubjects()
        {
            subjects.Clear();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM \"Subjects\"";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Subject subject = new Subject();
                    subject.SubjectId = reader.GetInt64(0);
                    subject.SubjectName = reader.GetString(1);
                    subject.DepartmentId = reader.GetInt64(2);
                    subjects.Add(subject);
                }
            }

            return subjects;
        }


        public void UpdateSubject(long subjectID, string newSubjectName)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE \"Subjects\" SET subject_name = @NewSubjectName WHERE subject_id = @SubjectID";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@NewSubjectName", newSubjectName);
                cmd.Parameters.AddWithValue("@SubjectID", subjectID);

                cmd.ExecuteNonQuery();
            }
        }

        public bool DeleteSubject(int subjectID)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM \"Subjects\" WHERE subject_id = @SubjectID";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@SubjectID", subjectID);

                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0; 
            }
        }

        public void ShowSubjectList()
        {
            var subjectList = ReadAllSubjects();

            foreach (var subject in subjectList)
            {
                Console.WriteLine(subject.SubjectId + " " + subject.SubjectName + " " + subject.DepartmentId);
            }
        }

        public Subject GetSubjectById(long subjectID)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM \"Subjects\" WHERE subject_id = @SubjectID";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@SubjectID", subjectID);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Subject subject = new Subject
                    {
                        SubjectId = (long)reader["subject_id"],
                        SubjectName = reader["subject_name"].ToString(),
                        DepartmentId = (long)reader["department_id"]
                    };
                    return subject;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
