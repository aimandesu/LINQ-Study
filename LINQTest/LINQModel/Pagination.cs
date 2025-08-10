namespace LINQTest.LINQModel;

public class Pagination<T>
{
    public List<T> Data { get; set; } = [];
    public int CurrentPage { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }
    public int LastPage { get; set; }
}