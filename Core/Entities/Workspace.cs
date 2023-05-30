using Core.Interfaces;

namespace Core.Entities;

public class Workspace : ICreatableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
    public User Owner { get; set; }
    public bool IsAiEnabled { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset ModifiedDate { get; set; }
}