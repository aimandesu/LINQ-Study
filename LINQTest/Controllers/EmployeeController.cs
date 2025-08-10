using LINQTest.Extension;
using LINQTest.LINQModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LINQTest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    public EmployeeController()
    {
        
    }
    
    [HttpGet("tutorial-0")]
    public IActionResult GetTutorialZero([FromQuery] int page = 1, [FromQuery] int perPage = 2)
    {
        // var employees = Data.GetEmployees();
        // var paginated = employees.PaginatedAnother(page, perPage);
        // return Ok(paginated);

        // List<Employee> employees = Data.GetEmployees();
        // var filteredEmployees = employees.Where(emp => emp.IsManager == true);
        // return Ok(filteredEmployees);
        
        //SQL Like Query Syntax
        var employees = Data.GetEmployees();
        var departments = Data.GetDepartments();

        var resultLIst = from employee in employees
            join department in departments 
                on employee.DepartmentId equals department.Id 
            select new
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                AnnualSalary = employee.AnnualSalary,
                IsManager = employee.IsManager,
                Department = department.LongName
            };
        
        return Ok(resultLIst.ToList());
        
    }

    [HttpGet("tutorial-1")]
    public IActionResult GetTutorialOne()
    {
        var employees = Data.GetEmployees();
        var departments = Data.GetDepartments();

        //Select and Where Operators, method syntax
        // var results = employees.Select(e =>
        // { // we dont need this {}, just here have for Console.WriteLine je 
        //     // Console.WriteLine(e.IsManager);
        //     return new
        //     {
        //         FullName =  e.FirstName + " " + e.LastName,
        //         AnnualSalary = e.AnnualSalary,
        //     };
        // }).Where(e => e.AnnualSalary >= 75000);

        //Select and Where Operators, query syntax
        var results = from emp in employees
            where emp.AnnualSalary >= 75000
            select new
            {
                FullName = emp.FirstName + " " + emp.LastName,
                AnnualSalary = emp.AnnualSalary,
            };
        
        employees.Add(new Employee
        {
            Id = 5,
            FirstName = "John",
            LastName = "Doe",
            AnnualSalary = 75000,
            IsManager = true,
            DepartmentId = 103
        });
        
        return Ok(results.ToList()); //this is deferred execution bcs John Doe appears in the final list

    }

    //Deferred Execution and Immediate Execution
    [HttpGet("tutorial-2")]
    public IActionResult DeferredEmployer()
    {
        var employees = Data.GetEmployees();
        
        //Deferred Execution
        // var results = from emp in employees.GetHighSalariedEmployees()
        //     select new
        //     {
        //         FullName = emp.FirstName + " " + emp.LastName,
        //         AnnualSalary = emp.AnnualSalary,
        //     };
        //
        // employees.Add(new Employee
        // {
        //     Id = 5,
        //     FirstName = "John",
        //     LastName = "Doe",
        //     AnnualSalary = 75000,
        //     IsManager = true,
        //     DepartmentId = 103
        // });
        //
        // foreach (var emp in results)
        // {
        //     Console.WriteLine($"{emp.FullName}  {emp.AnnualSalary}");
        // }
        
        //Immediate Execution
        var results = (from emp in employees.GetHighSalariedEmployees()
            select new
            {
                FullName = emp.FirstName + " " + emp.LastName,
                AnnualSalary = emp.AnnualSalary,
            }).ToList();
        
        employees.Add(new Employee
        {
            Id = 5,
            FirstName = "John",
            LastName = "Doe",
            AnnualSalary = 75000,
            IsManager = true,
            DepartmentId = 103
        });

        foreach (var emp in results)
        {
            Console.WriteLine($"{emp.FullName}  {emp.AnnualSalary}");
        }
        
        return Ok(results);
        
    }

    [HttpGet("tutorial-3")]
    public IActionResult JoinedEmployee()
    {
        var employees = Data.GetEmployees();
        var departments = Data.GetDepartments();

        // Inner Join Operation - Method Syntax
        // var results = departments.Join(
        //     employees,
        //     department => department.Id,
        //     employee => employee.DepartmentId,
        //     (department, employee) => new
        //     {
        //         FullName = employee.FirstName + " " + employee.LastName,
        //         AnnualSalary = employee.AnnualSalary,
        //         DepartmentName = department.LongName, 
        //     });
        
        // Inner Join Operation - Query Syntax

        // var results = from dept in departments
        //     join emp in employees
        //         on dept.Id equals emp.DepartmentId
        //     select new
        //     {
        //         FullName = emp.FirstName + " " + emp.LastName,
        //         AnnualSalary = emp.AnnualSalary,
        //         DepartmentName = dept.LongName,
        //     };

        var results = departments.GroupJoin(
            employees,
            department => department.Id,
            employee => employee.DepartmentId,
            (department, employee) => new
            {
                Employee = employee,
                DepartmentName = department.LongName,
            });
        
        return Ok(results);

    }
    
}