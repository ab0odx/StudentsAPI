using Microsoft.Data.SqlClient;
using Models;
using System.Data;

namespace Students_DataAcess
{
    public static class StudentsRepository
    {
        internal static readonly string connectionString = "Server=.;Database=StudentsDB;Integrated Security=True;TrustServerCertificate=True;";
        public static List<StudentDto> GetAllStudents()
        {
            List<StudentDto> students = new List<StudentDto>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = new SqlCommand("SP_GetAllStudents", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        var student = new StudentDto
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Age = reader.GetInt32(reader.GetOrdinal("Age")),
                            Grade = reader.GetDecimal(reader.GetOrdinal("Grade"))
                        };

                        students.Add(student);
                    }
                }
            }
            return students;
        }

        public static List<StudentDto> GetAllPassedStudents()
        {
            List<StudentDto> students = new();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(
                    "SP_GetAllPassedStudents", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var student = new StudentDto
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Age = reader.GetInt32(reader.GetOrdinal("Age")),
                                Grade = reader.GetDecimal(reader.GetOrdinal("Grade"))
                            };

                            students.Add(student);
                        }
                    }
                }
            }

            return students;
        }

        public static double GetAverageGrade()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(
                    "SP_GetAverageGrade",
                    connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    object? result = command.ExecuteScalar();

                    return Convert.ToDouble(result == DBNull.Value ? 0 : result);
                }
            }
        }

        public static StudentDto? GetStudentById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(
                    "SP_GetStudentById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var student = new StudentDto
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Age = reader.GetInt32(reader.GetOrdinal("Age")),
                                Grade = reader.GetDecimal(reader.GetOrdinal("Grade"))
                            };

                            return student;
                        }
                    }
                }
            }

            return null;
        }
    }
}
