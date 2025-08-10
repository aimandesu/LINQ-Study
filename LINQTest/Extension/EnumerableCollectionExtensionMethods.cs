using LINQTest.LINQModel;

namespace LINQTest.Extension;

public static class EnumerableCollectionExtensionMethods
{
    public static IEnumerable<Employee> GetHighSalariedEmployees(this IEnumerable<Employee> list)
    {
        foreach (var item in list)
        {
            Console.WriteLine($"Accessing employee: {item.FirstName}  {item.AnnualSalary}");
            if(item.AnnualSalary >= 60000)
                yield return item; // yield return element one at a time
        }
    }
}