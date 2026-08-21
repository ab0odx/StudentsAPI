using Models;
using Students_DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students_Business
{
    public static class StudentsService
    {
        public static List<StudentDto> GetAllStudents()
        {
            return StudentsRepository.GetAllStudents();
        }

        public static List<StudentDto> GetAllPassedStudents()
        {
            return StudentsRepository.GetAllPassedStudents();
        }

        public static double GetAverageGrade()
        {
            return StudentsRepository.GetAverageGrade();
        }

        public static StudentDto? GetStudentById(int id)
        {
            if (id < 1)
                return null;

            return StudentsRepository.GetStudentById(id);
        }
    }
}
