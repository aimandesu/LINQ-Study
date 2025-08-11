using System.Collections;
using System.Diagnostics.CodeAnalysis;
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

    [HttpGet("tutorial-4")]
    //Sorting Operators = OrderBy, OrderByDescending, ThenBy, ThenByDescending
    public IActionResult SortingOperator()
    {
        var employees = Data.GetEmployees();
        var departments = Data.GetDepartments();

        // Method Syntax
        // var results = employees.Join(
        //         departments,
        //         employee => employee.DepartmentId,
        //         department => department.Id,
        //         (employee, department) => new
        //         {
        //             Id = employee.Id,
        //             FirstName = employee.FirstName,
        //             LastName = employee.LastName,
        //             AnnualSalary = employee.AnnualSalary,
        //             DepartmentId = employee.DepartmentId,
        //             DepartmentName = department.LongName,
        //         })
        //     .OrderByDescending(employee => employee.DepartmentId) //OrderBy
        //     .ThenBy(employee => employee.AnnualSalary); //ThenByDescending

        var results = from emp in employees
            join dept in departments
                on emp.DepartmentId equals dept.Id
            orderby emp.DepartmentId, emp.AnnualSalary descending
            select new
            {
                Id = emp.Id,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                AnnualSalary = emp.AnnualSalary,
                DepartmentId = emp.DepartmentId,
                DepartmentName = dept.LongName,
            };
        
        return Ok(results);

    }
    
    [HttpGet("tutorial-5")] //Query employee list and group result by department ID property 
    //Grouping Operators = GroupBy, ToLookUp
    public IActionResult GroupingOperator()
    {
        var employees = Data.GetEmployees();
        var departments = Data.GetDepartments();
        
        //Method Syntax, deferred
        // var results = employees.GroupBy(e => e.DepartmentId)
        
        //Query Syntax, deferred
        // var results = from emp in employees
        //     orderby emp.DepartmentId
        //     group emp by emp.DepartmentId;

        var results = employees
            .OrderBy(o => o.DepartmentId)
            .ToLookup(e => e.DepartmentId); // immediately 
        
        return Ok(results);
        
    }
    
    [HttpGet("tutorial-6")]
    //Quantifier Operators = All, Any, Contains
    public IActionResult QuantifierOperator()
    {
        var employees = Data.GetEmployees();

        var annualSalaryCompare = 20000;
        
        bool isTrueAll = employees.All(emp => emp.AnnualSalary > annualSalaryCompare);
        
        bool isTrueAny = employees.Any(e => e.AnnualSalary > annualSalaryCompare);

        var searchEmployee = new Employee
        {
            Id = 2,
            FirstName = "Bob",
            LastName = "Smith",
            AnnualSalary = 50000m,
            IsManager = false,
            DepartmentId = 102
        };
        
        bool containsEmployee = employees.Contains(searchEmployee, new EmployeeComparer());
        
        return Ok(isTrueAll);
        

    }
    
    [HttpGet("tutorial-7")]
    //Filter Operators = OfType, Where
    public IActionResult FilterOperator()
    {
        ArrayList arrayList = new ArrayList();
        arrayList.Add(100);
        arrayList.Add("2020");
        
        var intResult = from i in arrayList.OfType<int>() // can also be type 
            select i;
        
        var stringResult = from i in arrayList.OfType<string>()
            select i;
        
        return Ok(stringResult);
        
    }
    
    [HttpGet("tutorial-8")]
    //Element Operators = ElementAt, ElementAtOrDefault, First, FirstOrDefault, Last, LastOrDefault, Single, SingleOrDefault
    public IActionResult ElementOperator()
    {
        var employees = Data.GetEmployees();
        var departments = Data.GetDepartments();

        var emp = employees.ElementAt(1); //ElementAtOrDefault

        return Ok(emp);
    }
 
    //to compare between class
    public class EmployeeComparer : IEqualityComparer<Employee>
    {
        public bool Equals([AllowNull] Employee x, [AllowNull] Employee y)
        {
            if (x.Id == y.Id && x.FirstName == y.FirstName && x.LastName == y.LastName)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode([DisallowNull] Employee obj)
        {
            return obj.Id.GetHashCode();
        }
        
    }
    
    //IMPORTANT
    //Equality Operator = SequenceEqual
    //Concatenation Operator = Concat
    //Set Operators = Distinct, Except, Intersect, Union
    //Generation Operators = DefaultIfEmpty, Empty, Range, Union
    //Aggregate Operators = Aggregate, Average, Count, Sum and Max
    //Partitioning Operators = Skip, SkipWhile, Take and TakeWhile
    //Conversion Operators = ToList, ToDictionary, and ToArray
    //Projection Operators = Select, SelectMany
    //Keywords = Let, Into
    
}