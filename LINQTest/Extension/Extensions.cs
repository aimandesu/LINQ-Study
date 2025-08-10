using LINQTest.LINQModel;

namespace LINQTest.Extension;
using System.Collections.Generic;

public static class Extensions
{
    public static List<T> Filter<T>(this List<T> list, Func<T, bool> predicate)
    {
        List<T> filteredList = new List<T>();

        foreach (T item in list)
        {
            if (predicate(item))
            {
                filteredList.Add(item);
            }
        }
        
        return filteredList;
    }

    // Pagination Testing
    public static Pagination<T> Paginated<T> (
        this List<T> list, 
        int currentPage, 
        int perPage)
    {
        var total = list.Count;
        var skip = (currentPage - 1) * perPage;

        var pagedData = list
            .Skip(skip)
            .Take(perPage)
            .ToList();

        return new Pagination<T>
        {
            Data = pagedData,
            CurrentPage = currentPage,
            PerPage = perPage,
            LastPage = (int)Math.Ceiling(total / (double)perPage)
        };
    }
    
    public static Pagination<T> PaginatedAnother<T>(
        this List<T> source,
        int currentPage,
        int perPage)
    {
        var total = source.Count;
        var skip = (currentPage - 1) * perPage;

        var pagedData = source
            .Skip(skip)
            .Take(perPage)
            .ToList();

        return new Pagination<T>
        {
            Data = pagedData,
            CurrentPage = currentPage,
            PerPage = perPage,
            Total = total,
            LastPage = (int)Math.Ceiling(total / (double)perPage)
        };
    }
    
}