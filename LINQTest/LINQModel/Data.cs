namespace LINQTest.LINQModel;

public static class Data
{
    public static List<Employee> GetEmployees()
    {
        return new List<Employee>
        {
            new Employee
            {
                Id = 1,
                FirstName = "Alice",
                LastName = "Johnson",
                AnnualSalary = 75000m,
                IsManager = true,
                DepartmentId = 101
            },
            new Employee
            {
                Id = 2,
                FirstName = "Bob",
                LastName = "Smith",
                AnnualSalary = 50000m,
                IsManager = false,
                DepartmentId = 102
            },
            new Employee
            {
                Id = 3,
                FirstName = "Charlie",
                LastName = "Brown",
                AnnualSalary = 60000m,
                IsManager = false,
                DepartmentId = 101
            }
        };
    }

    public static List<Department> GetDepartments()
    {
        return new List<Department>
        {
            new Department
            {
                Id = 101,
                ShortName = "HR",
                LongName = "Human Resources"
            },
            new Department
            {
                Id = 102,
                ShortName = "IT",
                LongName = "Information Technology"
            },
            new Department
            {
                Id = 103,
                ShortName = "FIN",
                LongName = "Finance"
            }
        };
    }
    
}