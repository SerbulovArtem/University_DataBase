using University.DAL.Concrete;
using University.DAL.Models;

namespace University.Admin.UI
{
    internal class AminMenu
    {
        private bool IsRunning { get; set; } = true;
        private readonly string _connectionString;
        private DepartmentRep _departmentRep;
        private SubjectRep _subjectRep;

        public AminMenu(string connectionString)
        {
            _connectionString = connectionString;
            _departmentRep = new DepartmentRep(connectionString);
            _subjectRep = new SubjectRep(connectionString);
        }



        public static void DisplayMenu(string title, string[] options, Action[] actions)
        {
            int optionCount = options.Length;
            int selectedOption = 0;

            while (true)
            {
                Console.WriteLine($"\t{title}");
                for (int i = 0; i < optionCount; i++)
                {
                    Console.WriteLine($"{i + 1}. {options[i]}");
                }

                Console.Write("Enter the option number -> ");
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input >= 1 && input <= optionCount)
                    {
                        selectedOption = input - 1;
                        Console.Clear();
                        actions[selectedOption]();
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection. Please select the correct option number.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter the option number as an integer.");
                }
            }
        }

        public void RunStartPage()
        {
            DisplayMenu("Start Page", new[] { "Departments", "Subjects" },
                new Action[] { DepartmentMenu, SubjectMenu });
        }

        private void DepartmentMenu()
        {
            DisplayMenu("Department Menu", new[] { "Create", "Read", "Update", "Delete", "Return to start menu" },
                new Action[] { CreateDepartment, ReadAllDepartments, UpdateDepartment, DeleteDepartmentByID, RunStartPage });
        }
        private void SubjectMenu()
        {
            DisplayMenu("Position Menu", new[] { "Create", "Read", "Update", "Delete", "Return to start menu" },
                new Action[] { CreateSubject, ReadAllSubjects, UpdateSubject, DeleteSubjectByID, RunStartPage });
        }

        private void CreateDepartment()
        {

            Console.Write("Enter the departments name -> ");
            string departmentName = Console.ReadLine();
            Department department = new Department

            {
                DepartmentName = departmentName
            };

            _departmentRep.CreateDepartment(department);

            Console.WriteLine("The departments has been successfully created :) ");
        }

        private void CreateSubject()
        {

            Console.Write("Enter the subject name -> ");
            string positionName = Console.ReadLine();
            Console.Write("Enter the departments id -> ");
            long departmentId = Convert.ToInt64(Console.ReadLine());

            _subjectRep.CreateSubject(positionName, departmentId);

            Console.WriteLine("The subject has been successfully created :) ");
        }

        private void ReadAllDepartments()
        {
            List<Department> departments = _departmentRep.ReadAllDepartments();

            Console.WriteLine("List of all departments -> ");
            foreach (var department in departments)
            {
                Console.WriteLine($"ID: {department.DepartmentId}, Name: {department.DepartmentName}");
            }
            Console.WriteLine();
        }

        private void ReadAllSubjects()
        {
            List<Subject> subjects = _subjectRep.ReadAllSubjects();

            Console.WriteLine("List of all subjects -> ");
            foreach (var subject in subjects)
            {
                Console.WriteLine($"ID: {subject.SubjectId}, Subject name: {subject.SubjectName}, Department ID: {subject.DepartmentId}");
            }
            Console.WriteLine();
        }
        private void UpdateDepartment()
        {
            Console.Write("Enter the ID of the department you want to update -> ");
            long departmentId = Convert.ToInt64(Console.ReadLine());
            Department existingDepartment = _departmentRep.GetDepartmentById(departmentId);

            if (existingDepartment != null)
            {
                Console.Write("Enter a new department name -> ");
                string newDepartmentName = Console.ReadLine();

                existingDepartment.DepartmentName = newDepartmentName;

                _departmentRep.UpdateDepartment(departmentId, existingDepartment);

                Console.WriteLine("Employee successfully updated.");
            }
            else
            {
                Console.WriteLine("No departments with this department ID was found.");
            }
        }

        private void UpdateSubject()
        {
            Console.Write("Enter the ID of the subject you want to update -> ");
            long subjectId = Convert.ToInt64(Console.ReadLine());
            Subject existingSubject = _subjectRep.GetSubjectById(subjectId);

            if (existingSubject != null)
            {
                Console.Write("Enter a new subject name -> ");
                string newSubjectName = Console.ReadLine();

                existingSubject.SubjectName = newSubjectName;

                _subjectRep.UpdateSubject(subjectId, existingSubject.SubjectName);

                Console.WriteLine("Employee successfully updated.");
            }
            else
            {
                Console.WriteLine("No subjects with this subject ID was found.");
            }
        }

        private void DeleteDepartmentByID()
        {

            Console.Write("Enter the departments ID to delete -> ");
            if (int.TryParse(Console.ReadLine(), out int departmentId))
            {

                bool isDeleted = _departmentRep.DeleteDepartment(departmentId);

                if (isDeleted)
                {
                    Console.WriteLine("Employee deleted successfully.");
                }
                else
                {
                    Console.WriteLine("No departments found with this ID.");
                }
            }
            else
            {
                Console.WriteLine("Incorrect departments ID format.");
            }
        }

        private void DeleteSubjectByID()
        {

            Console.Write("Enter the subject ID to delete -> ");
            if (int.TryParse(Console.ReadLine(), out int subjectId))
            {
                bool isDeleted = _subjectRep.DeleteSubject(subjectId);

                if (isDeleted)
                {
                    Console.WriteLine("The subject has been successfully deleted.");
                }
                else
                {
                    Console.WriteLine("No subjects with this ID were found.");
                }
            }
            else
            {
                Console.WriteLine("The subject ID format is incorrect.");
            }
        }
    }
}
