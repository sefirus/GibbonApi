using Core.Interfaces;

namespace Core.Entities;

public class WorkspaceRole : ICreatableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}
