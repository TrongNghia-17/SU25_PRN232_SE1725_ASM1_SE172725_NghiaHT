namespace SMMS.Repositories.NghiaHT.ModelExtensions;

public class SearchRequest
{
    public int? CurrentPage { get; set; }
    public int? PageSize { get; set; }
}

public class SearchRequestNghiaHt : SearchRequest
{
    public string? MedicationName { get; set; }
    public int? Quantity { get; set; }
    public string? CategoryName { get; set; }
}
