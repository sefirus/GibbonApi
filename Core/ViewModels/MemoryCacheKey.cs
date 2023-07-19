namespace Core.ViewModels;

public struct MemoryCacheKey
{
    public Guid WorkspaceId { get; set; }
    public string ObjectName { get; set; }
    public MemoryCacheEntryType EntryType { get; set; }
}

public enum MemoryCacheEntryType
{
    SchemaObject, Lookup
}