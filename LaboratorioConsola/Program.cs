using LaboratorioConsola;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static string connectionString = "Data Source=LAB1504-06\\SQLEXPRESS;Initial Catalog=Tecsup2023DB;User ID=userTecsup;Password=will230902"; // Reemplaza con tu cadena de conexión

    static void Main()
    {
        Console.WriteLine("Lista de Estudiantes usando DataTable:");
        List<Student> studentsUsingDataTable = GetStudentsUsingDataTable();
        foreach (var student in studentsUsingDataTable)
        {
            Console.WriteLine($"ID: {student.StudentId}, Nombre: {student.FirstName} {student.LastName}");
        }

        Console.WriteLine("\nLista de Estudiantes usando Lista de Objetos:");
        List<Student> studentsUsingList = GetStudentsUsingList();
        foreach (var student in studentsUsingList)
        {
            Console.WriteLine($"ID: {student.StudentId}, Nombre: {student.FirstName} {student.LastName}");
        }
    }

    // Esta es la función que obtiene la lista de estudiantes usando DataTable.
    static List<Student> GetStudentsUsingDataTable()
    {
        List<Student> students = new List<Student>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand("SELECT * FROM Students", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        students.Add(new Student
                        {
                            StudentId = Convert.ToInt32(row["StudentId"]),
                            FirstName = row["FirstName"].ToString(),
                            LastName = row["LastName"].ToString()
                        });
                    }
                }
            }
        }
        return students;
    }

    // Esta es la función que obtiene la lista de estudiantes usando una lista de objetos.
    static List<Student> GetStudentsUsingList()
    {
        List<Student> students = new List<Student>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand("SELECT * FROM Students", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            StudentId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2)
                        });
                    }
                }
            }
        }
        return students;
    }
}