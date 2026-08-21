using System.Net.Http.Json;

namespace Students_Client
{
    internal class Program
    {
        private const string ApiBaseUrl = "https://localhost:7194";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Students Client");
            Console.WriteLine("----------------");
            Console.WriteLine("1. Get All Students");

            Console.Write("Enter your choice: ");
            string? choice = Console.ReadLine();

            await ProcessChoice(choice);
        }

        static async Task ProcessChoice(string? choice)
        {
            switch (choice)
            {
                case "1":
                    await GetAllStudents();
                    break;

                case "2":
                    await GetAllPassedStudents();
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        static async Task GetAllPassedStudents() 
        { 
            var students = new List<StudentDto>();

            try
            {
                using HttpClient client = new();

                client.BaseAddress = new Uri($"{ApiBaseUrl}/api/Students/passed");

                students = await client.GetFromJsonAsync<List<StudentDto>>(
                    client.BaseAddress);
            }
            catch (HttpRequestException ex) 
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        static async Task GetAllStudents()
        {
            var students = new List<StudentDto>();

            try
            {
                using HttpClient client = new();

                client.BaseAddress = new Uri($"{ApiBaseUrl}/api/Students");

                students = await client.GetFromJsonAsync<List<StudentDto>>(
                    client.BaseAddress);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            if (students == null)
            {
                Console.WriteLine("Failed to retrieve students.");
                return;
            }

            foreach (var student in students)
            {
                Console.WriteLine(
                    $"ID: {student.Id}, " +
                    $"Name: {student.Name}, " +
                    $"Age: {student.Age}, " +
                    $"Grade: {student.Grade}");
            }
        }

        }
    public class StudentDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public int Age { get; set; }
        public decimal Grade { get; set; }
    }
}
