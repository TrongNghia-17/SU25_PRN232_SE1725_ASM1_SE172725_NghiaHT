namespace SMMS.Repositories.NghiaHT.ModelExtensions;

public class SearchRequest
{
    public int? urrentPage { get; set; } = 1;
    public int? pageSize { get; set; } = 10;
}

public class SearchRequestNghiaHt : SearchRequest
{
    public string? MedicationName { get; set; }
    public int? Quantity { get; set; }
    public string? CategoryName { get; set; }
}
