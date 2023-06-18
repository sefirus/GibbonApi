namespace Core.ViewModels.Schema;

public class SchemaFieldViewModel
{
    public string Type { get; set; }
    public int Length { get; set; }
    public bool IsPrimaryKey { get; set; }
    public string? Summary { get; set; }
    public string? Pattern { get; set; }
    
}