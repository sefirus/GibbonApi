using Core.Interfaces;

namespace Core.Entities;

public class Role : ICreatableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset ModifiedDate { get; set; }
}
