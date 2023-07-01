namespace Core.ViewModels.Schema;

public class SchemaFieldViewModel
{
    public string? FieldName { get; set; }
    public string Type { get; set; }
    public double Min { set; get; }
    public double Max { get; set; }
    public int Length { get; set; }
    public SchemaFieldViewModel? ArrayElement { get; set; }
    public List<SchemaFieldViewModel>? Fields { get; set; }
    public bool IsPrimaryKey { get; set; }
    public string? Summary { get; set; }
    public string? Pattern { get; set; }
}