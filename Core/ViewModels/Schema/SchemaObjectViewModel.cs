namespace Core.ViewModels.Schema;

public class SchemaObjectViewModel
{
    public Guid Id { get; set; }
    public Guid WorkspaceId { get; set; }
    public string Name { get; set; }
    public Dictionary<string, SchemaFieldViewModel> Fields { get; set; }
    public int? NumberOfDocuments { get; set; }
    public int? NumberOfFields { get; set; }
}